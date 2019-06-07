/* Taken and adapted from AssemblyVerifier by Jason Bock
 * https://github.com/JasonBock/AssemblyVerifier/blob/master/LICENSE.md
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Permissions;
using System.Runtime.Serialization;

namespace Reflexil.Verifier
{
	[Serializable]
	public sealed class VerificationException : Exception
	{
		private readonly ReadOnlyCollection<VerificationError> _errors;

		public VerificationException()
		{
			_errors = new ReadOnlyCollection<VerificationError>(new List<VerificationError>());
		}

		public VerificationException(string message) : base(message)
		{
			_errors = new ReadOnlyCollection<VerificationError>(new List<VerificationError>());
		}

		public VerificationException(string message, Exception innerException) : base(message, innerException)
		{
			_errors = new ReadOnlyCollection<VerificationError>(new List<VerificationError>());
		}

		private VerificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		public VerificationException(ReadOnlyCollection<VerificationError> errors) : this()
		{
			_errors = errors;
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		public ReadOnlyCollection<VerificationError> Errors
		{
			get { return _errors; }
		}
	}
}
