//
// CustomAttribute.cs
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

using System.Collections;

namespace Mono.Cecil
{

    public partial struct CustomAttributeNamedArgument
    {
        //Used by gridview datasource for property binding!
        public TypeReference Type
        {
            get { return argument.Type; }
        }

        public object Value
        {
            get { return argument.Value; }
        }
    }

    public sealed partial class CustomAttribute : IReflectionVisitable
    {

        public void Accept(IReflectionVisitor visitor)
        {
            visitor.VisitCustomAttribute(this);
        }

        internal static void CloneDictionary(IDictionary original, IDictionary target)
        {
            target.Clear();
            foreach (DictionaryEntry entry in original)
                target.Add(entry.Key, entry.Value);
        }

        internal static CustomAttribute Clone(CustomAttribute custattr, ModuleDefinition context)
        {
            var ca = new CustomAttribute(context.Import(custattr.Constructor));
            custattr.CopyTo(ca, context);
            return ca;
        }

        void CopyTo(CustomAttribute target, ModuleDefinition context)
        {
            foreach (var arg in ConstructorArguments)
                target.ConstructorArguments.Add(new CustomAttributeArgument(context.Import(arg.Type), arg.Value));

            foreach (var field in Fields)
                target.Fields.Add(new CustomAttributeNamedArgument(field.Name, new CustomAttributeArgument(context.Import(field.Argument.Type), field.Argument.Value)));

            foreach (var prop in Properties)
                target.Properties.Add(new CustomAttributeNamedArgument(prop.Name, new CustomAttributeArgument(context.Import(prop.Argument.Type), prop.Argument.Value)));
        }

    }
}
