//
// PdbWriter.cs
//
// Author:
//   Jb Evain (jbevain@gmail.com)
//
// (C) 2006 Jb Evain
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

using Mono.Cecil.Binary;
using Mono.Cecil.Cil;

namespace Mono.Cecil.Pdb {

	using System;
	using System.Collections;
	using System.Diagnostics.SymbolStore;
	using System.IO;

	public class PdbWriter : Cil.ISymbolWriter {

		ModuleDefinition m_module;
		SymWriter m_writer;
		Hashtable m_documents;
		string m_assembly;

		internal PdbWriter (SymWriter writer, ModuleDefinition module, string assembly)
		{
			m_writer = writer;
			m_module = module;
			m_documents = new Hashtable ();
			m_assembly = assembly;
		}

		public void Write (MethodBody body)
		{
			CreateDocuments (body);
			m_writer.OpenMethod (new SymbolToken ((int) body.Method.MetadataToken.ToUInt ()));
			CreateScopes (body, body.Scopes, new SymbolToken (body.LocalVarToken));
			m_writer.CloseMethod ();
		}

		void CreateScopes (MethodBody body, ScopeCollection scopes, SymbolToken localVarToken)
		{
			foreach (Scope s in scopes) {
				int startOffset = s.Start.Offset;
				int endOffset = s.End == body.Instructions.Outside ?
					body.Instructions[body.Instructions.Count - 1].Offset + 1 :
					s.End.Offset;

				m_writer.OpenScope (startOffset);
				m_writer.UsingNamespace (body.Method.DeclaringType.Namespace);
				m_writer.OpenNamespace (body.Method.DeclaringType.Namespace);

				int start = body.Instructions.IndexOf (s.Start);
				int end = s.End == body.Instructions.Outside ?
					body.Instructions.Count - 1 :
					body.Instructions.IndexOf (s.End);

				ArrayList instructions = CollectSequencePoints (body, start, end);
				DefineSequencePoints (instructions);

				CreateLocalVariable (s, startOffset, endOffset, localVarToken);

				CreateScopes (body, s.Scopes, localVarToken);
				m_writer.CloseNamespace ();

				m_writer.CloseScope (endOffset);
			}
		}

		private ArrayList CollectSequencePoints (MethodBody body, int start, int end)
		{
			ArrayList instructions = new ArrayList();
			for (int i = start; i <= end; i++)
				if (body.Instructions [i].SequencePoint != null)
					instructions.Add (body.Instructions [i]);
			return instructions;
		}

		private void DefineSequencePoints (ArrayList instructions)
		{
			if (instructions.Count == 0)
				return;

			Document doc = null;

			int [] offsets = new int [instructions.Count];
			int [] startRows = new int [instructions.Count];
			int [] startCols = new int [instructions.Count];
			int [] endRows = new int [instructions.Count];
			int [] endCols = new int [instructions.Count];

			for (int i = 0; i < instructions.Count; i++) {
				Instruction instr = (Instruction) instructions [i];
				offsets [i] = instr.Offset;

				if (doc == null)
					doc = instr.SequencePoint.Document;

				startRows [i] = instr.SequencePoint.StartLine;
				startCols [i] = instr.SequencePoint.StartColumn;
				endRows [i] = instr.SequencePoint.EndLine;
				endCols [i] = instr.SequencePoint.EndColumn;
			}

			m_writer.DefineSequencePoints (GetDocument (doc),
			                               offsets, startRows, startCols, endRows, endCols);
		}

		void CreateLocalVariable (IVariableDefinitionProvider provider, int startOffset, int endOffset, SymbolToken localVarToken)
		{
			for (int i = 0; i < provider.Variables.Count; i++) {
				VariableDefinition var = provider.Variables [i];
 				m_writer.DefineLocalVariable2(
  					var.Name,
  					0,
 					localVarToken,
  					SymAddressKind.ILOffset,
 					var.Index,
  					0,
  					0,
  					startOffset,
					endOffset);
			}
		}

		void CreateDocuments (MethodBody body)
		{
			foreach (Instruction instr in body.Instructions) {
				if (instr.SequencePoint == null)
					continue;

				GetDocument (instr.SequencePoint.Document);
			}
		}

		SymDocumentWriter GetDocument (Document document)
		{
			if (document == null)
				return null;

			SymDocumentWriter docWriter = m_documents[document.Url] as SymDocumentWriter;
			if (docWriter != null)
				return docWriter;

			docWriter = m_writer.DefineDocument (
				document.Url,
				document.Language,
				document.LanguageVendor,
				document.Type);

			m_documents [document.Url] = docWriter;
			return docWriter;
		}

		public void Dispose ()
		{
			Patch ();
		}

		void Patch ()
		{
			// patch debug info in PE file to match PDB

			byte[] DebugInfo = m_writer.GetDebugInfo ();
			m_writer.Close();

			RVA debugHeaderRVA = m_module.Image.PEOptionalHeader.DataDirectories.Debug.VirtualAddress;
			long debugHeaderPos = m_module.Image.ResolveVirtualAddress (debugHeaderRVA);
			uint sizeUntilData = 0x1c; // copied from ImageWriter
			long debugDataPos = debugHeaderPos + sizeUntilData;

			using (FileStream fs = new FileStream(m_assembly, FileMode.Open, FileAccess.Write))
			{
				BinaryWriter writer = new BinaryWriter(fs);
				writer.BaseStream.Position = debugDataPos;
				writer.Write(DebugInfo);
			}
		}
	}
}
