using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Permissions;
using System.Runtime.Serialization;

namespace Reflexil.Verifier
{
	/// <summary>
	/// The exception that is thrown when verification errors are detected from peverify.
	/// </summary>
	[Serializable]
	public sealed class VerificationException : Exception
	{
		private ReadOnlyCollection<VerificationError> errors;
		
		/// <summary>
		/// Creates a new <see cref="VerificationException" /> instance.
		/// </summary>
		public VerificationException()
			: base()
		{
			this.errors = new ReadOnlyCollection<VerificationError>(new List<VerificationError>());
		}

		/// <summary>
		/// Creates a new <see cref="VerificationException" /> instance
		/// with a specified error message.
		/// </summary>
		/// <param name="message">
		/// The message that describes the error.
		/// </param>
		public VerificationException(string message)
			: base(message)
		{
			this.errors = new ReadOnlyCollection<VerificationError>(new List<VerificationError>());
		}

		/// <summary>
		/// Creates a new <see cref="VerificationException" /> instance
		/// with a specified error message and a reference to the inner exception that is the cause of this exception. 
		/// </summary>
		/// <param name="message">
		/// The message that describes the error.
		/// </param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, 
		/// or a null reference if no inner exception is specified. 
		/// </param>
		public VerificationException(string message, Exception innerException)
			: base(message, innerException)
		{
			this.errors = new ReadOnlyCollection<VerificationError>(new List<VerificationError>());
		}

		private VerificationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>
		/// Creates a new <see cref="VerificationException" /> instance
		/// based a list of errors.
		/// </summary>
		/// <exception cref="ArgumentNullException">Thrown if <param name="errors" /> is <c>null</c>.</exception>
		public VerificationException(ReadOnlyCollection<VerificationError> errors)
			: this()
		{
			//errors.CheckParameterForNull("errors");
			this.errors = errors;
		}

		/// <summary>
		/// Defaults to the base implementation.
		/// </summary>
		/// <param name="info">The object that holds the serialized object data.</param>
		/// <param name="context">The contextual information about the source or destination.</param>
		[SecurityPermission(SecurityAction.LinkDemand,
		   Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
		
		/// <summary>
		/// Gets the list of verification errors.
		/// </summary>
		public ReadOnlyCollection<VerificationError> Errors
		{
			get
			{
				return this.errors;
			}
		}
	}
}
