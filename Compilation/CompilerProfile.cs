using System;

namespace Compilation
{
	[Serializable]
	public class CompilerProfile
	{
		public string Caption { get; set; }
		public string CompilerVersion { get; set; }
		public bool NoStdLib { get; set; }

		public override string ToString()
		{
			return Caption;
		}
	}
}
