using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;
using Reflexil.Utils;

namespace Reflexil.Verifier
{
	/// <summary>
	/// Provides methods to check the validity of an assembly.
	/// </summary>
	[SecurityPermission(SecurityAction.LinkDemand)]
	public static class AssemblyVerification
	{
		/// <summary>
		/// Verifies an assembly based on its location on disk.
		/// </summary>
		/// <param name="assemblyLocation">The assembly's location on disk.</param>
		/// <exception cref="ArgumentNullException">Thrown if <c>assemblyLocation</c> is <c>null</c>.</exception>
		/// <exception cref="FileNotFoundException">Thrown if the given file does not exist.</exception>
		/// <exception cref="VerificationException">Thrown if the assembly has verification errors.</exception>
		public static void Verify(FileSystemInfo assemblyLocation)
		{
			//assemblyLocation.CheckParameterForNull("assemblyLocation");

			if(!assemblyLocation.Exists)
			{
				throw new FileNotFoundException(
					"The assembly could not be found.", assemblyLocation.FullName);
			}
			
			AssemblyVerification.InternalVerify(assemblyLocation.FullName);
		}

        public static void Verify(String assemblyLocation)
        {
            AssemblyVerification.Verify(new FileInfo(assemblyLocation));
        }

		/// <summary>
		/// Verifies an assembly based on an <see cref="Assembly" /> instance.
		/// </summary>
		/// <param name="assembly">The <see cref="Assembly" /> instance.</param>
		/// <exception cref="ArgumentNullException">Thrown if <c>assembly</c> is <c>null</c>.</exception>
		/// <exception cref="FileNotFoundException">Thrown if the given file does not exist.</exception>
		/// <exception cref="VerificationException">Thrown if the assembly has verification errors.</exception>
		public static void Verify(Assembly assembly)
		{
			//assembly.CheckParameterForNull("assembly");
			AssemblyVerification.InternalVerify(assembly.Location);
		}

		/// <summary>
		/// Verifies an assembly based on an <see cref="AssemblyBuilder" /> instance.
		/// </summary>
		/// <param name="assemblyBuilder">The <see cref="AssemblyBuilder" /> instance.</param>
		/// <exception cref="ArgumentNullException">Thrown if <c>assemblyBuilder</c> is <c>null</c>.</exception>
		/// <exception cref="FileNotFoundException">Thrown if the given file does not exist.</exception>
		/// <exception cref="VerificationException">Thrown if the assembly has verification errors.</exception>
		public static void Verify(AssemblyBuilder assemblyBuilder)
		{
			//assemblyBuilder.CheckParameterForNull("assemblyBuilder");

			string assemblyName = assemblyBuilder.GetName().Name;

			if(assemblyBuilder.EntryPoint != null)
			{
				assemblyName += ".exe";
			}
			else
			{
				assemblyName += ".dll";
			}

			AssemblyVerification.InternalVerify(Path.Combine(
				Path.GetDirectoryName(new Uri(assemblyBuilder.GetName().CodeBase).LocalPath),
				assemblyName));
		}

		private static void InternalVerify(string assemblyFileLocation)
		{
            string arguments = "\"" + assemblyFileLocation + "\" /MD /IL";
			ReadOnlyCollection<VerificationError> errors = null;
            
            PEVerifyUtility.CallPEVerifyUtility(arguments, false, (reader) => errors = VerificationErrorCollectionCreator.Create(reader));

			if(errors.Count > 0)
			{
				throw new VerificationException(errors);
			}
		}
	}
}
