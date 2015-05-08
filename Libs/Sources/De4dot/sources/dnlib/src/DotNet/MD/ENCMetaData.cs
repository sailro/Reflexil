/*
    Copyright (C) 2012-2014 de4dot@gmail.com

    Permission is hereby granted, free of charge, to any person obtaining
    a copy of this software and associated documentation files (the
    "Software"), to deal in the Software without restriction, including
    without limitation the rights to use, copy, modify, merge, publish,
    distribute, sublicense, and/or sell copies of the Software, and to
    permit persons to whom the Software is furnished to do so, subject to
    the following conditions:

    The above copyright notice and this permission notice shall be
    included in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
    CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
    TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
    SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using dnlib.IO;
using dnlib.PE;
using dnlib.Threading;

namespace dnlib.DotNet.MD {
	/// <summary>
	/// Used when a #- stream is present in the metadata
	/// </summary>
	sealed class ENCMetaData : MetaData {
		static readonly UTF8String DeletedName = "_Deleted";
		bool hasMethodPtr, hasFieldPtr, hasParamPtr, hasEventPtr, hasPropertyPtr;
		bool hasDeletedRows;
		readonly Dictionary<Table, SortedTable> sortedTables = new Dictionary<Table, SortedTable>();
#if THREAD_SAFE
		readonly Lock theLock = Lock.Create();
#endif

		/// <summary>
		/// Sorts a table by key column
		/// </summary>
		sealed class SortedTable {
			RowInfo[] rows;

			/// <summary>
			/// Remembers <c>rid</c> and key
			/// </summary>
			[DebuggerDisplay("{rid} {key}")]
			struct RowInfo : IComparable<RowInfo> {
				public readonly uint rid;
				public readonly uint key;

				/// <summary>
				/// Constructor
				/// </summary>
				/// <param name="rid">Row ID</param>
				/// <param name="key">Key</param>
				public RowInfo(uint rid, uint key) {
					this.rid = rid;
					this.key = key;
				}

				/// <inheritdoc/>
				public int CompareTo(RowInfo other) {
					if (key < other.key)
						return -1;
					if (key > other.key)
						return 1;
					return rid.CompareTo(other.rid);
				}
			}

			/// <summary>
			/// Constructor
			/// </summary>
			/// <param name="mdTable">The MD table</param>
			/// <param name="keyColIndex">Index of key column</param>
			public SortedTable(MDTable mdTable, int keyColIndex) {
				InitializeKeys(mdTable, keyColIndex);
				Array.Sort(rows);
			}

			void InitializeKeys(MDTable mdTable, int keyColIndex) {
				var keyColumn = mdTable.TableInfo.Columns[keyColIndex];
				rows = new RowInfo[mdTable.Rows + 1];
				if (mdTable.Rows == 0)
					return;
				using (var reader = mdTable.CloneImageStream()) {
					reader.Position = keyColumn.Offset;
					int increment = mdTable.TableInfo.RowSize - keyColumn.Size;
					for (uint i = 1; i <= mdTable.Rows; i++) {
						rows[i] = new RowInfo(i, keyColumn.Read(reader));
						if (i < mdTable.Rows)
							reader.Position += increment;
					}
				}
			}

			/// <summary>
			/// Binary searches for a row with a certain key
			/// </summary>
			/// <param name="key">The key</param>
			/// <returns>The row or 0 if not found</returns>
			int BinarySearch(uint key) {
				int lo = 1, hi = rows.Length - 1;
				while (lo <= hi) {
					int curr = (lo + hi) / 2;
					uint key2 = rows[curr].key;
					if (key == key2)
						return curr;
					if (key2 > key)
						hi = curr - 1;
					else
						lo = curr + 1;
				}

				return 0;
			}

			/// <summary>
			/// Find all rids that contain <paramref name="key"/>
			/// </summary>
			/// <param name="key">The key</param>
			/// <returns>A new <see cref="RidList"/> instance</returns>
			public RidList FindAllRows(uint key) {
				int startIndex = BinarySearch(key);
				if (startIndex == 0)
					return RidList.Empty;
				int endIndex = startIndex + 1;
				for (; startIndex > 1; startIndex--) {
					if (key != rows[startIndex - 1].key)
						break;
				}
				for (; endIndex < rows.Length; endIndex++) {
					if (key != rows[endIndex].key)
						break;
				}
				var list = new RandomRidList(endIndex - startIndex);
				for (int i = startIndex; i < endIndex; i++)
					list.Add(rows[i].rid);
				return list;
			}
		}

		/// <inheritdoc/>
		public ENCMetaData(IPEImage peImage, ImageCor20Header cor20Header, MetaDataHeader mdHeader)
			: base(peImage, cor20Header, mdHeader) {
		}

		/// <inheritdoc/>
		protected override void InitializeInternal() {
			IImageStream imageStream = null;
			DotNetStream dns = null;
			try {
				var mdRva = cor20Header.MetaData.VirtualAddress;
				foreach (var sh in mdHeader.StreamHeaders) {
					var rva = mdRva + sh.Offset;
					imageStream = peImage.CreateStream(rva, sh.StreamSize);
					switch (sh.Name.ToUpperInvariant()) {
					case "#STRINGS":
						if (stringsStream == null) {
							stringsStream = new StringsStream(imageStream, sh);
							imageStream = null;
							allStreams.Add(stringsStream);
							continue;
						}
						break;

					case "#US":
						if (usStream == null) {
							usStream = new USStream(imageStream, sh);
							imageStream = null;
							allStreams.Add(usStream);
							continue;
						}
						break;

					case "#BLOB":
						if (blobStream == null) {
							blobStream = new BlobStream(imageStream, sh);
							imageStream = null;
							allStreams.Add(blobStream);
							continue;
						}
						break;

					case "#GUID":
						if (guidStream == null) {
							guidStream = new GuidStream(imageStream, sh);
							imageStream = null;
							allStreams.Add(guidStream);
							continue;
						}
						break;

					case "#~":	// Only if #Schema is used
					case "#-":
						if (tablesStream == null) {
							tablesStream = new TablesStream(imageStream, sh);
							imageStream = null;
							allStreams.Add(tablesStream);
							continue;
						}
						break;
					}
					dns = new DotNetStream(imageStream, sh);
					imageStream = null;
					allStreams.Add(dns);
					dns = null;
				}
			}
			finally {
				if (imageStream != null)
					imageStream.Dispose();
				if (dns != null)
					dns.Dispose();
			}

			if (tablesStream == null)
				throw new BadImageFormatException("Missing MD stream");
			tablesStream.Initialize(peImage);

			// The pointer tables are used iff row count != 0
			hasFieldPtr = !tablesStream.FieldPtrTable.IsEmpty;
			hasMethodPtr = !tablesStream.MethodPtrTable.IsEmpty;
			hasParamPtr = !tablesStream.ParamPtrTable.IsEmpty;
			hasEventPtr = !tablesStream.EventPtrTable.IsEmpty;
			hasPropertyPtr = !tablesStream.PropertyPtrTable.IsEmpty;
			hasDeletedRows = tablesStream.HasDelete;
		}

		/// <inheritdoc/>
		public override RidList GetTypeDefRidList() {
			if (!hasDeletedRows)
				return base.GetTypeDefRidList();
			uint rows = tablesStream.TypeDefTable.Rows;
			var list = new RandomRidList((int)rows);
			for (uint rid = 1; rid <= rows; rid++) {
				var row = tablesStream.ReadTypeDefRow(rid);
				if (row == null)
					continue;	// Should never happen since rid is valid

				// RTSpecialName is ignored by the CLR. It's only the name that indicates
				// whether it's been deleted.
				if (stringsStream.ReadNoNull(row.Name) == DeletedName)
					continue;	// ignore this deleted row
				list.Add(rid);
			}
			return list;
		}

		/// <inheritdoc/>
		public override RidList GetExportedTypeRidList() {
			if (!hasDeletedRows)
				return base.GetExportedTypeRidList();
			uint rows = tablesStream.ExportedTypeTable.Rows;
			var list = new RandomRidList((int)rows);
			for (uint rid = 1; rid <= rows; rid++) {
				var row = tablesStream.ReadExportedTypeRow(rid);
				if (row == null)
					continue;	// Should never happen since rid is valid

				// RTSpecialName is ignored by the CLR. It's only the name that indicates
				// whether it's been deleted.
				if (stringsStream.ReadNoNull(row.TypeName) == DeletedName)
					continue;	// ignore this deleted row
				list.Add(rid);
			}
			return list;
		}

		/// <summary>
		/// Converts a logical <c>Field</c> rid to a physical <c>Field</c> rid
		/// </summary>
		/// <param name="listRid">A valid rid</param>
		/// <returns>Converted rid or any invalid rid value if <paramref name="listRid"/> is invalid</returns>
		uint ToFieldRid(uint listRid) {
			if (!hasFieldPtr)
				return listRid;
			uint listValue;
			return tablesStream.ReadColumn(tablesStream.FieldPtrTable, listRid, 0, out listValue) ? listValue : 0;
		}

		/// <summary>
		/// Converts a logical <c>Method</c> rid to a physical <c>Method</c> rid
		/// </summary>
		/// <param name="listRid">A valid rid</param>
		/// <returns>Converted rid or any invalid rid value if <paramref name="listRid"/> is invalid</returns>
		uint ToMethodRid(uint listRid) {
			if (!hasMethodPtr)
				return listRid;
			uint listValue;
			return tablesStream.ReadColumn(tablesStream.MethodPtrTable, listRid, 0, out listValue) ? listValue : 0;
		}

		/// <summary>
		/// Converts a logical <c>Param</c> rid to a physical <c>Param</c> rid
		/// </summary>
		/// <param name="listRid">A valid rid</param>
		/// <returns>Converted rid or any invalid rid value if <paramref name="listRid"/> is invalid</returns>
		uint ToParamRid(uint listRid) {
			if (!hasParamPtr)
				return listRid;
			uint listValue;
			return tablesStream.ReadColumn(tablesStream.ParamPtrTable, listRid, 0, out listValue) ? listValue : 0;
		}

		/// <summary>
		/// Converts a logical <c>Event</c> rid to a physical <c>Event</c> rid
		/// </summary>
		/// <param name="listRid">A valid rid</param>
		/// <returns>Converted rid or any invalid rid value if <paramref name="listRid"/> is invalid</returns>
		uint ToEventRid(uint listRid) {
			if (!hasEventPtr)
				return listRid;
			uint listValue;
			return tablesStream.ReadColumn(tablesStream.EventPtrTable, listRid, 0, out listValue) ? listValue : 0;
		}

		/// <summary>
		/// Converts a logical <c>Property</c> rid to a physical <c>Property</c> rid
		/// </summary>
		/// <param name="listRid">A valid rid</param>
		/// <returns>Converted rid or any invalid rid value if <paramref name="listRid"/> is invalid</returns>
		uint ToPropertyRid(uint listRid) {
			if (!hasPropertyPtr)
				return listRid;
			uint listValue;
			return tablesStream.ReadColumn(tablesStream.PropertyPtrTable, listRid, 0, out listValue) ? listValue : 0;
		}

		/// <inheritdoc/>
		public override RidList GetFieldRidList(uint typeDefRid) {
			var list = GetRidList(tablesStream.TypeDefTable, typeDefRid, 4, tablesStream.FieldTable);
			if (list.Length == 0 || (!hasFieldPtr && !hasDeletedRows))
				return list;

			var destTable = tablesStream.FieldTable;
			var newList = new RandomRidList((int)list.Length);
			for (uint i = 0; i < list.Length; i++) {
				var rid = ToFieldRid(list[i]);
				if (destTable.IsInvalidRID(rid))
					continue;
				if (hasDeletedRows) {
					// It's a deleted row if RTSpecialName is set and name is "_Deleted"
					var row = tablesStream.ReadFieldRow(rid);
					if (row == null)
						continue;	// Should never happen since rid is valid
					if ((row.Flags & (uint)FieldAttributes.RTSpecialName) != 0) {
						if (stringsStream.ReadNoNull(row.Name) == DeletedName)
							continue;	// ignore this deleted row
					}
				}
				// It's a valid non-deleted rid so add it
				newList.Add(rid);
			}
			return newList;
		}

		/// <inheritdoc/>
		public override RidList GetMethodRidList(uint typeDefRid) {
			var list = GetRidList(tablesStream.TypeDefTable, typeDefRid, 5, tablesStream.MethodTable);
			if (list.Length == 0 || (!hasMethodPtr && !hasDeletedRows))
				return list;

			var destTable = tablesStream.MethodTable;
			var newList = new RandomRidList((int)list.Length);
			for (uint i = 0; i < list.Length; i++) {
				var rid = ToMethodRid(list[i]);
				if (destTable.IsInvalidRID(rid))
					continue;
				if (hasDeletedRows) {
					// It's a deleted row if RTSpecialName is set and name is "_Deleted"
					var row = tablesStream.ReadMethodRow(rid);
					if (row == null)
						continue;	// Should never happen since rid is valid
					if ((row.Flags & (uint)MethodAttributes.RTSpecialName) != 0) {
						if (stringsStream.ReadNoNull(row.Name) == DeletedName)
							continue;	// ignore this deleted row
					}
				}
				// It's a valid non-deleted rid so add it
				newList.Add(rid);
			}
			return newList;
		}

		/// <inheritdoc/>
		public override RidList GetParamRidList(uint methodRid) {
			var list = GetRidList(tablesStream.MethodTable, methodRid, 5, tablesStream.ParamTable);
			if (list.Length == 0 || !hasParamPtr)
				return list;

			var destTable = tablesStream.ParamTable;
			var newList = new RandomRidList((int)list.Length);
			for (uint i = 0; i < list.Length; i++) {
				var rid = ToParamRid(list[i]);
				if (destTable.IsInvalidRID(rid))
					continue;
				newList.Add(rid);
			}
			return newList;
		}

		/// <inheritdoc/>
		public override RidList GetEventRidList(uint eventMapRid) {
			var list = GetRidList(tablesStream.EventMapTable, eventMapRid, 1, tablesStream.EventTable);
			if (list.Length == 0 || (!hasEventPtr && !hasDeletedRows))
				return list;

			var destTable = tablesStream.EventTable;
			var newList = new RandomRidList((int)list.Length);
			for (uint i = 0; i < list.Length; i++) {
				var rid = ToEventRid(list[i]);
				if (destTable.IsInvalidRID(rid))
					continue;
				if (hasDeletedRows) {
					// It's a deleted row if RTSpecialName is set and name is "_Deleted"
					var row = tablesStream.ReadEventRow(rid);
					if (row == null)
						continue;	// Should never happen since rid is valid
					if ((row.EventFlags & (uint)EventAttributes.RTSpecialName) != 0) {
						if (stringsStream.ReadNoNull(row.Name) == DeletedName)
							continue;	// ignore this deleted row
					}
				}
				// It's a valid non-deleted rid so add it
				newList.Add(rid);
			}
			return newList;
		}

		/// <inheritdoc/>
		public override RidList GetPropertyRidList(uint propertyMapRid) {
			var list = GetRidList(tablesStream.PropertyMapTable, propertyMapRid, 1, tablesStream.PropertyTable);
			if (list.Length == 0 || (!hasPropertyPtr && !hasDeletedRows))
				return list;

			var destTable = tablesStream.PropertyTable;
			var newList = new RandomRidList((int)list.Length);
			for (uint i = 0; i < list.Length; i++) {
				var rid = ToPropertyRid(list[i]);
				if (destTable.IsInvalidRID(rid))
					continue;
				if (hasDeletedRows) {
					// It's a deleted row if RTSpecialName is set and name is "_Deleted"
					var row = tablesStream.ReadPropertyRow(rid);
					if (row == null)
						continue;	// Should never happen since rid is valid
					if ((row.PropFlags & (uint)PropertyAttributes.RTSpecialName) != 0) {
						if (stringsStream.ReadNoNull(row.Name) == DeletedName)
							continue;	// ignore this deleted row
					}
				}
				// It's a valid non-deleted rid so add it
				newList.Add(rid);
			}
			return newList;
		}

		/// <summary>
		/// Gets a rid list (eg. field list)
		/// </summary>
		/// <param name="tableSource">Source table, eg. <c>TypeDef</c></param>
		/// <param name="tableSourceRid">Row ID in <paramref name="tableSource"/></param>
		/// <param name="colIndex">Column index in <paramref name="tableSource"/>, eg. 4 for <c>TypeDef.FieldList</c></param>
		/// <param name="tableDest">Destination table, eg. <c>Field</c></param>
		/// <returns>A new <see cref="RidList"/> instance</returns>
		RidList GetRidList(MDTable tableSource, uint tableSourceRid, int colIndex, MDTable tableDest) {
			var column = tableSource.TableInfo.Columns[colIndex];
			uint startRid, nextListRid;
			bool hasNext;
#if THREAD_SAFE
			tablesStream.theLock.EnterWriteLock(); try {
#endif
			if (!tablesStream.ReadColumn_NoLock(tableSource, tableSourceRid, column, out startRid))
				return RidList.Empty;
			hasNext = tablesStream.ReadColumn_NoLock(tableSource, tableSourceRid + 1, column, out nextListRid);
#if THREAD_SAFE
			} finally { tablesStream.theLock.ExitWriteLock(); }
#endif

			uint lastRid = tableDest.Rows + 1;
			if (startRid == 0 || startRid >= lastRid)
				return RidList.Empty;
			uint endRid = hasNext && nextListRid != 0 ? nextListRid : lastRid;
			if (endRid < startRid)
				endRid = startRid;
			if (endRid > lastRid)
				endRid = lastRid;
			return new ContiguousRidList(startRid, endRid - startRid);
		}

		/// <inheritdoc/>
		protected override uint BinarySearch_NoLock(MDTable tableSource, int keyColIndex, uint key) {
			var keyColumn = tableSource.TableInfo.Columns[keyColIndex];
			uint ridLo = 1, ridHi = tableSource.Rows;
			while (ridLo <= ridHi) {
				uint rid = (ridLo + ridHi) / 2;
				uint key2;
				if (!tablesStream.ReadColumn_NoLock(tableSource, rid, keyColumn, out key2))
					break;	// Never happens since rid is valid
				if (key == key2)
					return rid;
				if (key2 > key)
					ridHi = rid - 1;
				else
					ridLo = rid + 1;
			}

			if (tableSource.Table == Table.GenericParam && !tablesStream.IsSorted(tableSource))
				return LinearSearch_NoLock(tableSource, keyColIndex, key);

			return 0;
		}

		/// <summary>
		/// Linear searches the table (O(n)) for a <c>rid</c> whose key column at index
		/// <paramref name="keyColIndex"/> is equal to <paramref name="key"/>.
		/// </summary>
		/// <param name="tableSource">Table to search</param>
		/// <param name="keyColIndex">Key column index</param>
		/// <param name="key">Key</param>
		/// <returns>The <c>rid</c> of the found row, or 0 if none found</returns>
		uint LinearSearch_NoLock(MDTable tableSource, int keyColIndex, uint key) {
			if (tableSource == null)
				return 0;
			var keyColumn = tableSource.TableInfo.Columns[keyColIndex];
			for (uint rid = 1; rid <= tableSource.Rows; rid++) {
				uint key2;
				if (!tablesStream.ReadColumn_NoLock(tableSource, rid, keyColumn, out key2))
					break;	// Never happens since rid is valid
				if (key == key2)
					return rid;
			}
			return 0;
		}

		/// <inheritdoc/>
		protected override RidList FindAllRowsUnsorted(MDTable tableSource, int keyColIndex, uint key) {
			if (tablesStream.IsSorted(tableSource))
				return FindAllRows(tableSource, keyColIndex, key);
			SortedTable sortedTable;
#if THREAD_SAFE
			theLock.EnterWriteLock(); try {
#endif
			if (!sortedTables.TryGetValue(tableSource.Table, out sortedTable))
				sortedTables[tableSource.Table] = sortedTable = new SortedTable(tableSource, keyColIndex);
#if THREAD_SAFE
			} finally { theLock.ExitWriteLock(); }
#endif
			return sortedTable.FindAllRows(key);
		}
	}
}
