using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SabberStoneCore.Model;
using static SabberStoneCoreConsole.src.DynamicVectorCalculator;

namespace SabberStoneCoreConsole.src
{
	static class WriteToTML
	{
		public static void Write(LargeCombo cardList)
		{
			StreamWriter sw = new StreamWriter("/Users/hc/Desktop/out.tml");
			Console.WriteLine("Writing............");
			sw.WriteLine("PoolName = \"Meta Decks\"");
			sw.WriteLine("[[Decks]]");
			sw.WriteLine("DeckName = \"Tempo Rogue\"");
			string className = cardList.ComboClass.ToLower();
			sw.WriteLine("ClassName = \"" + Char.ToUpper(className[0])+ className.Substring(1) + "\"");

			sw.Write("CardList = [");
			int i = 0;
			while (i < 29)
			{
				sw.Write("\"" + cardList.ComboCards[i].Name + "\"" + ", ");
				i++;
			}
			sw.Write("\"" + cardList.ComboCards[29].Name + "\"");
			sw.Write("]");

			sw.Flush();
			sw.Close();

		}
	}
}
