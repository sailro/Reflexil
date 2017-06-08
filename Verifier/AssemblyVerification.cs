/* Taken and adapted from AssemblyVerifier by Jason Bock
 * https://github.com/JasonBock/AssemblyVerifier/blob/master/LICENSE.md
 */
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;
using Reflexil.Utils;

namespace Reflexil.Verifier
{
	[SecurityPermission(SecurityAction.LinkDemand)]
	public static class AssemblyVerification
	{
		public static void Verify(FileSystemInfo assemblyLocation)
		{
			if (!assemblyLocation.Exists)
				throw new FileNotFoundException("The assembly could not be found.", assemblyLocation.FullName);

			InternalVerify(assemblyLocation.FullName);
		}

		public static void Verify(string assemblyLocation)
		{
			Verify(new FileInfo(assemblyLocation));
		}

		public static void Verify(Assembly assembly)
		{
			InternalVerify(assembly.Location);
		}

		public static void Verify(AssemblyBuilder assemblyBuilder)
		{
			var assemblyName = assemblyBuilder.GetName().Name;
			assemblyName += assemblyBuilder.EntryPoint != null ? ".exe" : ".dll";

			var localPath = new Uri(assemblyBuilder.GetName().CodeBase).LocalPath;
			var directoryName = Path.GetDirectoryName(localPath);

			if (directoryName != null)
				InternalVerify(Path.Combine(directoryName, assemblyName));
		}

		private static void InternalVerify(string assemblyFileLocation)
		{
			var arguments = "\"" + assemblyFileLocation + "\" /MD /IL";
			ReadOnlyCollection<VerificationError> errors = null;

			PEVerifyUtility.CallPEVerifyUtility(arguments, false, reader => errors = VerificationErrorCollectionCreator.Create(reader));

			if (errors.Count > 0)
				throw new VerificationException(errors);
		}
	}
}