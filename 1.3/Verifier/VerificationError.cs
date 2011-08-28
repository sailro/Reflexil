using System;

namespace Reflexil.Verifier
{
	/// <summary>
	/// Defines a base class for peverify error types.
	/// </summary>
	public abstract class VerificationError : IEquatable<VerificationError>
	{
		private const string ILError = "IL";
		private const string MDError = "MD";

		/// <summary>
		/// Creates a new <see cref="VerificationError" /> instance.
		/// </summary>
		protected VerificationError()
			: base()
		{
		}

		/// <summary>
		/// Creates the correct kind of <see cref="VerificationError" />.
		/// </summary>
		/// <param name="error">The error string from peverify.</param>
		/// <returns>A <see cref="VerificationError" />-based instance.</returns>
		/// <exception cref="ArgumentNullException">Thrown if <paramref name="error"/> is <c>null</c>.</exception>
		/// <exception cref="ArgumentException">Thrown if <c>error</c> is empty.</exception>
		public static VerificationError Create(string error)
		{
			//error.CheckParameterForNull("error");

			if(error.Length == 0)
			{
				throw new ArgumentException("No verification error was given.", "error");
			}

			VerificationError verificationError = null;

			var errorType = error.Substring(
				error.IndexOf("[", 0, StringComparison.CurrentCultureIgnoreCase) + 1, 2);

			if(string.Equals(errorType, ILError) == true)
			{
				verificationError = new IntermediateLanguageVerificationError(error);
			}
			else if(string.Equals(errorType, MDError) == true)
			{
				verificationError = new MetadataVerificationError(error);
			}

			return verificationError;
		}

		/// <summary>
		/// Returns the hash code for this <see cref="VerificationError" /> instance.
		/// </summary>
		/// <returns>The hash code for this <see cref="VerificationError" /> instance.</returns>
		public override int GetHashCode()
		{
			return this.Description.GetHashCode() ^ this.Location.GetHashCode();
		}

		/// <summary>
		/// Provides a type-safe equality check.
		/// </summary>
		/// <param name="other">The object to check for equality.</param>
		/// <returns>Returns <c>true</c> if the objects are equals; otherwise, <c>false</c>.</returns>
		public bool Equals(VerificationError other)
		{
			var areEqual = false;

			if(other != null)
			{
				areEqual = (this.Description == other.Description) &&
				  (this.Location == other.Location);
			}

			return areEqual;
		}

		/// <summary>
		/// Checks to see if the given object is equal to the current <see cref="VerificationError" /> instance.
		/// </summary>
		/// <param name="obj">The object to check for equality.</param>
		/// <returns>Returns <c>true</c> if the objects are equals; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj)
		{
			return this.Equals(obj as VerificationError);
		}

		/// <summary>
		/// Gets the description.
		/// </summary>
		public string Description
		{
			get;
			protected set;
		}

		/// <summary>
		/// Gets the location.
		/// </summary>
		public string Location
		{
			get;
			protected set;
		}
	}
}
