using System.Linq;

using NUnit.Framework;

using DeMono.Cecil.Rocks;

namespace DeMono.Cecil.Tests {

	[TestFixture]
	public class ModuleDefinitionRocksTests : BaseTestFixture {

		[TestCSharp ("Types.cs")]
		public void GetAllTypesTest (ModuleDefinition module)
		{
			var sequence = new [] {
				module.GetType ("<Module>"),
				module.GetType ("Foo"),
				module.GetType ("Foo/Bar"),
				module.GetType ("Foo/Gazonk"),
				module.GetType ("Foo/Gazonk/Baz"),
				module.GetType ("Pan"),
			};

			Assert.IsTrue (sequence.SequenceEqual (module.GetAllTypes ()));
		}
	}
}
