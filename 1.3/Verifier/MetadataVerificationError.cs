using System;

namespace Reflexil.Verifier
{
	internal sealed class MetadataVerificationError : VerificationError
	{
		// See if I can gen a dynamic assembly (LCG in 2.0) that
		// will take a int32 and return a RuntimeHandle (all three types, Method, Type, and Field)
		// It's up to me (the caller) to call the correct GetXXXHandle() method.
		// Note: Method is 0x06, Type is either 0x01 (for a ref) or 0x02 (for a def), and
		// Field is 0x04.
		// FYI http://www.interact-sw.co.uk/iangblog/2005/08/31/methodinfofromtoken
		private const string ErrorHeader = "[MD]: Error: ";

		internal MetadataVerificationError(string error)
			: base()
		{
			var errorStartLocation = error.LastIndexOf("[", StringComparison.CurrentCultureIgnoreCase) + 1;
			var errorEndLocation = error.LastIndexOf("]", StringComparison.CurrentCultureIgnoreCase) - 1;
			this.Location = error.Substring(errorStartLocation, 
				errorEndLocation - errorStartLocation + 1).Trim();

			errorEndLocation += 2;

			this.Description = error.Substring(ErrorHeader.Length).Trim();
		}
	}
}
