//
// TypeDefinition.cs
//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// Copyright (c) 2008 - 2011 Jb Evain
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;

namespace Mono.Cecil {

	public sealed partial class TypeDefinition {

        private Mono.Collections.Generic.Collection<MethodDefinition> FilterMethods(Func<MethodDefinition, bool> filter)
        {
            Mono.Collections.Generic.Collection<MethodDefinition> result = new Collections.Generic.Collection<MethodDefinition>();
            foreach (MethodDefinition mdef in Methods)
            {
                if (filter(mdef))
                {
                    result.Add(mdef);
                }
            }
            return result;
        }

        private Mono.Collections.Generic.Collection<MethodDefinition> Constructors
        {
            get
            {
                return FilterMethods(m => m.IsConstructor);
            }
        }

        private Mono.Collections.Generic.Collection<MethodDefinition> StrictMethods
        {
            get
            {
                return FilterMethods(m => !m.IsConstructor);
            }
        }

		public override void Accept (IReflectionVisitor visitor)
		{
			visitor.VisitTypeDefinition (this);

            visitor.VisitGenericParameterCollection(this.GenericParameters);
            visitor.VisitInterfaceCollection(this.Interfaces);
            visitor.VisitConstructorCollection(this.Constructors);
            visitor.VisitMethodDefinitionCollection(this.StrictMethods);
            visitor.VisitFieldDefinitionCollection(this.Fields);
            visitor.VisitPropertyDefinitionCollection(this.Properties);
            visitor.VisitEventDefinitionCollection(this.Events);
            visitor.VisitNestedTypeCollection(this.NestedTypes);
            visitor.VisitCustomAttributeCollection(this.CustomAttributes);
            visitor.VisitSecurityDeclarationCollection(this.SecurityDeclarations);
		}
	}
}
