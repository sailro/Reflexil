/* Taken and adapted from AssemblyVerifier by Jason Bock
 * https://github.com/JasonBock/AssemblyVerifier/blob/master/LICENSE.md
 */

using System;

namespace Reflexil.Verifier
{
	public abstract class VerificationError : IEquatable<VerificationError>
	{
		private const string ILError = "IL";
		private const string MDError = "MD";

		public static VerificationError Create(string error)
		{
			if (error.Length == 0)
				throw new ArgumentException(@"No verification error was given.", "error");

			VerificationError verificationError = null;

			var errorType = error.Substring(
				error.IndexOf("[", 0, StringComparison.CurrentCultureIgnoreCase) + 1, 2);

			if (string.Equals(errorType, ILError))
			{
				verificationError = new IntermediateLanguageVerificationError(error);
			}
			else if (string.Equals(errorType, MDError))
			{
				verificationError = new MetadataVerificationError(error);
			}

			return verificationError;
		}

		public override int GetHashCode()
		{
			return Description.GetHashCode() ^ Location.GetHashCode();
		}

		public bool Equals(VerificationError other)
		{
			var areEqual = false;

			if (other != null)
			{
				areEqual = (Description == other.Description) &&
				           (Location == other.Location);
			}

			return areEqual;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as VerificationError);
		}

		public string Description { get; protected set; }
		public string Location { get; protected set; }
	}
}
