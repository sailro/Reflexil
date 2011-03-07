using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Reflexil.Verifier
{
	/// <summary>
	/// Contains a method to create a collection of error information.
	/// </summary>
	public static class VerificationErrorCollectionCreator
	{
		/// <summary>
		/// Creates a <see cref="ReadOnlyCollection&lt;VerificationError&gt;" /> based
		/// on the output of peverify.
		/// </summary>
		/// <param name="peVerifyOutput">The output from peverify.</param>
		/// <returns>A collection of errors.</returns>
		public static ReadOnlyCollection<VerificationError> Create(TextReader peVerifyOutput)
		{
			var errors = new List<VerificationError>();

			if(peVerifyOutput != null)
			{
				var peOutputLine = peVerifyOutput.ReadLine();
				while(peOutputLine != null)
				{
					peOutputLine = peOutputLine.Replace("\0", string.Empty);

					if(peOutputLine.Length > 0)
					{
						var error = VerificationError.Create(peOutputLine);

						if(error != null)
						{
							errors.Add(error);
						}
					}

					peOutputLine = peVerifyOutput.ReadLine();
				}
			}
			
			return errors.AsReadOnly();
		}
	}
}

