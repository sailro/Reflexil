/* Taken and adapted from AssemblyVerifier by Jason Bock
 * https://github.com/JasonBock/AssemblyVerifier/blob/master/LICENSE.md
 */

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Reflexil.Verifier
{
	public static class VerificationErrorCollectionCreator
	{
		public static ReadOnlyCollection<VerificationError> Create(TextReader peVerifyOutput)
		{
			var errors = new List<VerificationError>();

			if (peVerifyOutput == null)
				return errors.AsReadOnly();

			var peOutputLine = peVerifyOutput.ReadLine();
			while (peOutputLine != null)
			{
				peOutputLine = peOutputLine.Replace("\0", string.Empty);

				if (peOutputLine.Length > 0)
				{
					var error = VerificationError.Create(peOutputLine);

					if (error != null)
						errors.Add(error);
				}

				peOutputLine = peVerifyOutput.ReadLine();
			}

			return errors.AsReadOnly();
		}
	}
}
