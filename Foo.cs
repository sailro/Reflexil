using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testing
{
	class Foo<T>
	{
		public class Bar<Y>
		{
			public static void Test<Z>()
			{
			}
		}
	}

	class Foo : Foo<int>.Bar<bool>
	{
		void Test()
		{
			new Foo<int>.Bar<string>();
			Foo<int>.Bar<string>.Test<bool>();
			FooTest<Array>();
		}

		void FooTest<Z>()
		{

		}
	}
}
