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
			
		}
	}

	class Foo : Foo<int>.Bar<string>
	{
		void Test()
		{
			var x = new Foo<int>.Bar<string>();
		}
	}
}
