using System;

namespace Reflexil.Verifier
{
	internal sealed class IntermediateLanguageVerificationError : VerificationError
	{
		internal IntermediateLanguageVerificationError(string error)
		{
			var errorStartLocation = error.IndexOf(" : ",
				error.IndexOf("[", StringComparison.CurrentCultureIgnoreCase) + 1, StringComparison.CurrentCultureIgnoreCase)
			                         + 3;
			var errorEndLocation = error.IndexOf("]", errorStartLocation, StringComparison.CurrentCultureIgnoreCase) - 1;
			Location = error.Substring(errorStartLocation, errorEndLocation - errorStartLocation + 1).Trim();

			errorEndLocation += 2;
			Description = error.Substring(errorEndLocation).Trim();
		}
	}
}