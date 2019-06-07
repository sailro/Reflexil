/* Taken and adapted from AssemblyVerifier by Jason Bock
 * https://github.com/JasonBock/AssemblyVerifier/blob/master/LICENSE.md
 */

using System;

namespace Reflexil.Verifier
{
	internal sealed class IntermediateLanguageVerificationError : VerificationError
	{
		internal IntermediateLanguageVerificationError(string error)
		{
			var start = error.IndexOf(" : ", error.IndexOf("[", StringComparison.CurrentCultureIgnoreCase) + 1, StringComparison.CurrentCultureIgnoreCase) + 3;
			var end = error.IndexOf("]", start, StringComparison.CurrentCultureIgnoreCase) - 1;
			Location = error.Substring(start, end - start + 1).Trim();

			end += 2;
			Description = error.Substring(end).Trim();
		}
	}
}
