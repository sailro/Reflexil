//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2015 Jb Evain
// Copyright (c) 2008 - 2011 Novell, Inc.
//
// Licensed under the MIT/X11 license.
//

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle (Consts.AssemblyName)]

#if !NET_CORE
[assembly: Guid ("fd225bb4-fa53-44b2-a6db-85f5e48dcb54")]
#endif

// HACK - Reflexil - Access to internals
[assembly: InternalsVisibleTo ("Mono.Cecil.Pdb.Reflexil, PublicKey=" + Consts.PublicKey)]
[assembly: InternalsVisibleTo ("Mono.Cecil.Mdb.Reflexil, PublicKey=" + Consts.PublicKey)]
[assembly: InternalsVisibleTo ("Mono.Cecil.Rocks, PublicKey=" + Consts.PublicKey)]
[assembly: InternalsVisibleTo ("Mono.Cecil.Tests, PublicKey=" + Consts.PublicKey)]
[assembly: InternalsVisibleTo("Reflexil, PublicKey=" + Consts.PublicKey)]
[assembly: InternalsVisibleTo("Reflexil.Reflector, PublicKey=" + Consts.PublicKey)]
[assembly: InternalsVisibleTo("Reflexil.CecilStudio, PublicKey= " + Consts.PublicKey)]
[assembly: InternalsVisibleTo("Reflexil.ILSpy.Plugin, PublicKey= " + Consts.PublicKey)]
[assembly: InternalsVisibleTo("Reflexil.JustDecompile.Module, PublicKey= " + Consts.PublicKey)]
