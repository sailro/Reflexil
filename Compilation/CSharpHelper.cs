
#region " Imports "
using System;
using System.Collections.Generic;
using System.Text;
using Mono.Cecil;
#endregion

namespace Reflexil.Compilation
{
    internal class CSharpHelper : BaseLanguageHelper
    {

        #region " Constants "
        protected const string REFERENCE_PARAMETER = "ref ";
        protected const string GENERIC_CONSTRAINT = " where ";
        protected const string GENERIC_CONSTRAINT_LIST_START = " : ";
        protected const string GENERIC_LIST_START = "<";
        protected const string GENERIC_LIST_END = ">";
        protected const string METHOD_BODY_START = "{";
        protected const string METHOD_BODY_END = "}";
        protected const string RETURN = "return ";
        protected const string DEFAULT_START = "default(";
        protected const string DEFAULT_END = ");";
        protected const string NAMESPACE_START = "{";
        protected const string NAMESPACE_END = "}";
        protected const string CLASS_START = "{";
        protected const string CLASS_END = "}";
        protected const string USING = "using ";
        protected const string NAMESPACE = "namespace ";
        protected const string CLASS = "class ";
        protected const string REGION_START = "#region ";
        protected const string REGION_END = "#endregion ";
        protected const string SEPARATOR = ";";
        protected const string COMMENT_START = "/* ";
        protected const string COMMENT_END = "*/";
        protected const string STATIC = "static ";
        #endregion

        #region " Methods "
        public CSharpHelper()
        {
            m_aliases.Add("System.Object", "object");
            m_aliases.Add("System.Int16", "short");
            m_aliases.Add("System.Int32", "int");
            m_aliases.Add("System.Int64", "long");
            m_aliases.Add("System.UInt16", "ushort");
            m_aliases.Add("System.UInt32", "uint");
            m_aliases.Add("System.UInt64", "ulong");
            m_aliases.Add("System.Boolean", "bool");
            m_aliases.Add("System.Char", "char");
            m_aliases.Add("System.Decimal", "decimal");
            m_aliases.Add("System.Double", "double");
            m_aliases.Add("System.Single", "float");
            m_aliases.Add("System.String", "string");
            m_aliases.Add("System.Void", "void");
        }

        protected override void WriteMethodSignature(MethodDefinition mdef)
        {
            mdef.Accept(this);

            if (mdef.GenericParameters.Count > 0)
            {
                foreach (GenericParameter genparam in mdef.GenericParameters)
                {
                    if (genparam.Constraints.Count > 0)
                    {
                        Write(GENERIC_CONSTRAINT);
                        genparam.Accept(this);
                        VisitVisitableCollection(GENERIC_CONSTRAINT_LIST_START, String.Empty, BASIC_SEPARATOR, false, genparam.Constraints);
                    }
                }
            }
        }

        protected override void WriteMethodBody(MethodDefinition mdef)
        {
            WriteLine();
            IdentLevel++;
            WriteLine(METHOD_BODY_START);
            if (mdef.ReturnType.ReturnType.FullName != typeof(void).FullName)
            {
                Write(RETURN);
                Write(DEFAULT_START);
                VisitTypeReference(mdef.ReturnType.ReturnType);
                WriteLine(DEFAULT_END);
            }
            UnIdent();
            IdentLevel--;
            Ident();
            WriteLine(METHOD_BODY_END);
        }

        protected override void WriteField(FieldDefinition fdef)
        {
            fdef.Accept(this);
        }

        protected override void WriteTypeSignature(TypeDefinition tdef)
        {
            tdef.Accept(this);

            if (tdef.GenericParameters.Count > 0)
            {
                foreach (GenericParameter genparam in tdef.GenericParameters)
                {
                    if (genparam.Constraints.Count > 0)
                    {
                        Write(GENERIC_CONSTRAINT);
                        genparam.Accept(this);
                        VisitVisitableCollection(GENERIC_CONSTRAINT_LIST_START, String.Empty, BASIC_SEPARATOR, false, genparam.Constraints);
                    }
                }
                Replace(GENERIC_TYPE_TAG + tdef.GenericParameters.Count, String.Empty);
            }
        }

        public override void VisitFieldDefinition(FieldDefinition field)
        {
            VisitTypeReference(field.FieldType);
            Write(SPACE);
            Write(field.Name);
            Write(SEPARATOR);
        }

        public override void VisitMethodDefinition(MethodDefinition method)
        {
            if (method.IsStatic)
            {
                Write(STATIC);
            }
            if (method.IsConstructor)
            {
                string name = method.DeclaringType.Name;
                if (method.DeclaringType.GenericParameters.Count > 0)
                {
                    name = name.Replace(GENERIC_TYPE_TAG + method.DeclaringType.GenericParameters.Count, String.Empty);
                }
                Write(name);
            }
            else
            {
                VisitTypeReference(method.ReturnType.ReturnType);
                Write(SPACE);
                Write(method.Name);
            }
        }

        public override void VisitTypeDefinition(TypeDefinition type)
        {
            HandleName(type, type.Name);
        }

        public override void VisitTypeReference(TypeReference type)
        {
            string name = type.Name;

            if (type.Name.EndsWith(REFERENCE_TYPE_TAG))
            {
                Write(REFERENCE_PARAMETER);
                name = name.Replace(REFERENCE_TYPE_TAG, String.Empty);
            }
            if (type.Namespace != String.Empty)
            {
                name = type.Namespace + NAMESPACE_SEPARATOR + name;
            }
            if (type is GenericInstanceType)
            {
                GenericInstanceType git = type as GenericInstanceType;
                name = name.Replace(GENERIC_TYPE_TAG + git.GenericArguments.Count, String.Empty);
                HandleName(type, name);
                VisitVisitableCollection(GENERIC_LIST_START, GENERIC_LIST_END, BASIC_SEPARATOR, false, git.GenericArguments);
            }
            else
            {
                HandleName(type, name);
            }
        }

        public override void VisitGenericParameterCollection(GenericParameterCollection genparams)
        {
            VisitVisitableCollection(GENERIC_LIST_START, GENERIC_LIST_END, BASIC_SEPARATOR, false, genparams);
        }

        public override void VisitParameterDefinitionCollection(ParameterDefinitionCollection parameters)
        {
            VisitVisitableCollection(PARAMETER_LIST_START, PARAMETER_LIST_END, BASIC_SEPARATOR, true, parameters);
        }

        public override void VisitParameterDefinition(ParameterDefinition parameter)
        {
            VisitTypeReference(parameter.ParameterType);
            Write(SPACE);
            Write(parameter.Name);
        }

        private void WriteMethodsStubs(MethodDefinition mdef, MethodDefinitionCollection methods)
        {
            Write(REGION_START);
            WriteLine("\" Methods stubs \"");
            WriteComment("Do not add or update any method. If compilation fails because of a method declaration, comment it");
            foreach (MethodDefinition smdef in methods)
            {
                if (mdef != smdef)
                {
                    WriteMethodSignature(smdef);
                    WriteMethodBody(smdef);
                    WriteLine();
                }
            }
            WriteLine(REGION_END);
        }

        private void WriteFieldsStubs(FieldDefinitionCollection fields)
        {
            Write(REGION_START);
            WriteLine("\" Fields stubs \"");
            WriteComment("Do not add or update any field. If compilation fails because of a field declaration, comment it");
            foreach (FieldDefinition fdef in fields)
            {
                WriteField(fdef);
                WriteLine();
            }
            WriteLine(REGION_END);
        }

        private void WriteDefaultNamespaces()
        {
            foreach (string item in DEFAULT_NAMESPACES)
            {
                Write(USING);
                Write(item);
                WriteLine(SEPARATOR);
            }
        }

        private void WriteReferencedAssemblies(List<AssemblyNameReference> references)
        {
            IdentLevel++;
            WriteLine(COMMENT_START);
            WriteLine("[Referenced assemblies]");
            foreach (AssemblyNameReference asmref in references)
            {
                WriteLine(String.Format("- {0} v{1}", asmref.Name, asmref.Version));
            }
            ReIdent(IdentLevel - 1);
            WriteLine(COMMENT_END);
        }

        private void WriteComment(string comment)
        {
            Write(COMMENT_START);
            Write(comment);
            WriteLine(COMMENT_END);
        }

        private void WriteClass(MethodDefinition mdef)
        {
            Write(CLASS);
            WriteTypeSignature(mdef.DeclaringType as TypeDefinition);
            WriteLine();
            IdentLevel++;
            WriteLine(CLASS_START);

            IdentLevel++;
            WriteLine(COMMENT_START);
            WriteLine("Limited support!");
            WriteLine("You can only reference methods or fields defined in the class (not in ancestors classes)");
            WriteLine("Fields and methods stubs are needed for compilation purposes only.");
            IdentLevel--;
            WriteLine("Reflexil will automaticaly map current type, fields or methods to original references.");
            WriteLine(COMMENT_END);
            WriteMethodSignature(mdef);
            WriteMethodBody(mdef);

            WriteLine();
            WriteMethodsStubs(mdef, (mdef.DeclaringType as TypeDefinition).Methods);

            WriteLine();
            WriteFieldsStubs((mdef.DeclaringType as TypeDefinition).Fields);

            ReIdent(IdentLevel - 1);
            WriteLine(CLASS_END);
        }

        public override string GenerateSourceCode(MethodDefinition mdef, List<AssemblyNameReference> references)
        {
            Reset();

            WriteDefaultNamespaces(); WriteLine();
            WriteReferencedAssemblies(references); WriteLine();

            if (mdef.DeclaringType.Namespace != string.Empty)
            {
                Write(NAMESPACE);
                WriteLine(mdef.DeclaringType.Namespace);
                IdentLevel++;
                WriteLine(NAMESPACE_START);
            }

            WriteClass(mdef);

            if (mdef.DeclaringType.Namespace != string.Empty)
            {
                ReIdent(IdentLevel - 1);
                WriteLine(NAMESPACE_END);
            }

            return GetResult();
        }
        #endregion

    }
}
