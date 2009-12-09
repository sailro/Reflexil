using System;

namespace Reflexil.Verifier
{
	internal sealed class IntermediateLanguageVerificationError : VerificationError
	{
		internal IntermediateLanguageVerificationError(string error)
			: base()
		{
			var errorStartLocation = error.IndexOf(" : ",
				error.IndexOf("[", StringComparison.CurrentCultureIgnoreCase) + 1, StringComparison.CurrentCultureIgnoreCase)
				 + 3;
			var errorEndLocation = error.IndexOf("]", errorStartLocation, StringComparison.CurrentCultureIgnoreCase) - 1;
			this.Location = error.Substring(errorStartLocation, errorEndLocation - errorStartLocation + 1).Trim();

			errorEndLocation += 2;

			this.Description = error.Substring(errorEndLocation).Trim();
		}
	}
}
