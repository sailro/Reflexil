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
using dnlib.DotNet.MD;
using dnlib.IO;
using dnlib.PE;
using dnlib.Threading;

namespace dnlib.DotNet.MD {
	/// <summary>
	/// Used when a #~ stream is present in the metadata
	/// </summary>
	sealed class CompressedMetaData : MetaData {
		/// <inheritdoc/>
		public CompressedMetaData(IPEImage peImage, ImageCor20Header cor20Header, MetaDataHeader mdHeader)
			: base(peImage, cor20Header, mdHeader) {
		}

		static HotHeapVersion GetHotHeapVersion(string version) {
			if (version.StartsWith(MDHeaderRuntimeVersion.MS_CLR_20_PREFIX))
				return HotHeapVersion.CLR20;
			if (version.StartsWith(MDHeaderRuntimeVersion.MS_CLR_40_PREFIX))
				return HotHeapVersion.CLR40;

			return HotHeapVersion.CLR20;
		}

		/// <inheritdoc/>
		protected override void InitializeInternal() {
			var hotHeapVersion = GetHotHeapVersion(mdHeader.VersionString);

			IImageStream imageStream = null, fullStream = null;
			DotNetStream dns = null;
			List<HotStream> hotStreams = null;
			HotStream hotStream = null;
			var newAllStreams = new List<DotNetStream>(allStreams);
			try {
				var mdRva = cor20Header.MetaData.VirtualAddress;
				for (int i = mdHeader.StreamHeaders.Count - 1; i >= 0; i--) {
					var sh = mdHeader.StreamHeaders[i];
					var rva = mdRva + sh.Offset;
					var fileOffset = peImage.ToFileOffset(rva);
					imageStream = peImage.CreateStream(fileOffset, sh.StreamSize);
					switch (sh.Name) {
					case "#Strings":
						if (stringsStream == null) {
							stringsStream = new StringsStream(imageStream, sh);
							imageStream = null;
							newAllStreams.Add(stringsStream);
							continue;
						}
						break;

					case "#US":
						if (usStream == null) {
							usStream = new USStream(imageStream, sh);
							imageStream = null;
							newAllStreams.Add(usStream);
							continue;
						}
						break;

					case "#Blob":
						if (blobStream == null) {
							blobStream = new BlobStream(imageStream, sh);
							imageStream = null;
							newAllStreams.Add(blobStream);
							continue;
						}
						break;

					case "#GUID":
						if (guidStream == null) {
							guidStream = new GuidStream(imageStream, sh);
							imageStream = null;
							newAllStreams.Add(guidStream);
							continue;
						}
						break;

					case "#~":
						if (tablesStream == null) {
							tablesStream = new TablesStream(imageStream, sh);
							imageStream = null;
							newAllStreams.Add(tablesStream);
							continue;
						}
						break;

					case "#!":
						if (hotStreams == null)
							hotStreams = new List<HotStream>();
						fullStream = peImage.CreateFullStream();
						hotStream = HotStream.Create(hotHeapVersion, imageStream, sh, fullStream, fileOffset);
						fullStream = null;
						hotStreams.Add(hotStream);
						newAllStreams.Add(hotStream);
						hotStream = null;
						imageStream = null;
						continue;
					}
					dns = new DotNetStream(imageStream, sh);
					imageStream = null;
					newAllStreams.Add(dns);
					dns = null;
				}
			}
			finally {
				if (imageStream != null)
					imageStream.Dispose();
				if (fullStream != null)
					fullStream.Dispose();
				if (dns != null)
					dns.Dispose();
				if (hotStream != null)
					hotStream.Dispose();
				newAllStreams.Reverse();
				allStreams = ThreadSafeListCreator.MakeThreadSafe(newAllStreams);
			}

			if (tablesStream == null)
				throw new BadImageFormatException("Missing MD stream");

			if (hotStreams != null) {
				hotStreams.Reverse();
				InitializeHotStreams(hotStreams);
			}

			tablesStream.Initialize(peImage);
		}

		int GetPointerSize() {
			return peImage.ImageNTHeaders.OptionalHeader.Magic == 0x10B ? 4 : 8;
		}

		void InitializeHotStreams(IList<HotStream> hotStreams) {
			if (hotStreams == null || hotStreams.Count == 0)
				return;

			// If this is a 32-bit image, make sure that we emulate this by masking
			// all base offsets to 32 bits.
			long offsetMask = GetPointerSize() == 8 ? -1L : uint.MaxValue;

			// It's always the last one found that is used
			var hotTable = hotStreams[hotStreams.Count - 1].HotTableStream;
			if (hotTable != null) {
				hotTable.Initialize(offsetMask);
				tablesStream.HotTableStream = hotTable;
			}

			HotHeapStream hotStrings = null, hotBlob = null, hotGuid = null, hotUs = null;
			for (int i = hotStreams.Count - 1; i >= 0; i--) {
				var hotStream = hotStreams[i];
				var hotHeapStreams = hotStream.HotHeapStreams;
				if (hotHeapStreams == null)
					continue;

				// It's always the last one found that is used
				for (int j = hotHeapStreams.Count - 1; j >= 0; j--) {
					var hotHeap = hotHeapStreams[j];
					switch (hotHeap.HeapType) {
					case HeapType.Strings:
						if (hotStrings == null) {
							hotHeap.Initialize(offsetMask);
							hotStrings = hotHeap;
						}
						break;

					case HeapType.Guid:
						if (hotGuid == null) {
							hotHeap.Initialize(offsetMask);
							hotGuid = hotHeap;
						}
						break;

					case HeapType.Blob:
						if (hotBlob == null) {
							hotHeap.Initialize(offsetMask);
							hotBlob = hotHeap;
						}
						break;

					case HeapType.US:
						if (hotUs == null) {
							hotHeap.Initialize(offsetMask);
							hotUs = hotHeap;
						}
						break;
					}
				}
			}
			InitializeNonExistentHeaps();
			stringsStream.HotHeapStream = hotStrings;
			guidStream.HotHeapStream = hotGuid;
			blobStream.HotHeapStream = hotBlob;
			usStream.HotHeapStream = hotUs;
		}

		/// <inheritdoc/>
		public override RidList GetFieldRidList(uint typeDefRid) {
			return GetRidList(tablesStream.TypeDefTable, typeDefRid, 4, tablesStream.FieldTable);
		}

		/// <inheritdoc/>
		public override RidList GetMethodRidList(uint typeDefRid) {
			return GetRidList(tablesStream.TypeDefTable, typeDefRid, 5, tablesStream.MethodTable);
		}

		/// <inheritdoc/>
		public override RidList GetParamRidList(uint methodRid) {
			return GetRidList(tablesStream.MethodTable, methodRid, 5, tablesStream.ParamTable);
		}

		/// <inheritdoc/>
		public override RidList GetEventRidList(uint eventMapRid) {
			return GetRidList(tablesStream.EventMapTable, eventMapRid, 1, tablesStream.EventTable);
		}

		/// <inheritdoc/>
		public override RidList GetPropertyRidList(uint propertyMapRid) {
			return GetRidList(tablesStream.PropertyMapTable, propertyMapRid, 1, tablesStream.PropertyTable);
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
			uint endRid = hasNext ? nextListRid : lastRid;
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

			return 0;
		}
	}
}
