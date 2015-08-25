//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2015 Jb Evain
// Copyright (c) 2008 - 2011 Novell, Inc.
//
// Licensed under the MIT/X11 license.
//

using System;
using System.IO;

namespace Mono.Cecil {

	public sealed class EmbeddedResource : Resource {

		readonly MetadataReader reader;

		uint? offset;
		byte [] data;
		// HACK - Reflexil - Alternate resource access
	    private bool deferredloading = true;

	    public byte[] Data
	    {
	        get
	        {
                if (deferredloading)
                {
                    if (offset.HasValue)
                        data = reader.GetManagedResourceStream(offset.Value).ToArray();
                    else
                        throw new InvalidOperationException();

                    deferredloading = false;
                }

	            return data;
	        }
            set {
                deferredloading = false;
                data = value;
            }

	    }
		// HACK - Reflexil - Ends

		public override ResourceType ResourceType {
			get { return ResourceType.Embedded; }
		}

		public EmbeddedResource (string name, ManifestResourceAttributes attributes, byte [] data) :
			base (name, attributes)
		{
			// HACK - Reflexil - Alternate resource access
		    Data = data;
		}

		internal EmbeddedResource (string name, ManifestResourceAttributes attributes, uint offset, MetadataReader reader)
			: base (name, attributes)
		{
			this.offset = offset;
			this.reader = reader;
		}

		public Stream GetResourceStream ()
		{
			// HACK - Reflexil - Alternate resource access
			return new MemoryStream (Data);
		}

		// HACK - Reflexil - Alternate resource access
        [Obsolete("GetResourceData method is now deprecated, please use Data property instead")]
        public byte[] GetResourceData()
        {
            return Data;
        }

	}
}
