using System;
using System.Dynamic;
using System.IO;
using System.Text;


namespace SabberStoneCoreConsole.src
{
	public class Dynamic: DynamicObject
	{
		Program program;
		public Dynamic()
		{
			program = new Program();
		}
		public override bool TryGetMember(GetMemberBinder binder, out object result)
		{
			result = null;
			switch (binder.Name)
			{
				case "ReadAndWrite":
					result = (Func<string, string, string>)((string a, string b)
						  => Program.UnitTest(a, b));
					return true;
			}
			return false;
		}
	}
}
