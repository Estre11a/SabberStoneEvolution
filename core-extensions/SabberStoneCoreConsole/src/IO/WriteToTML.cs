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
			StreamWriter sw = new StreamWriter("/Users/hc/Desktop/Evostone-master/TestBed/StrategySearch/resources/decks/pools/metaDecks.tml");
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

		public static void WriteDecks(List<LargeCombo> Decks)
		{
			//!!TODO:
			Console.WriteLine("write WriteDecks in IO/WriteTOTML.cs");
			int nameCounter = 1;
			Console.WriteLine("Writing............");
			foreach (LargeCombo cm in Decks)
			{
				StreamWriter sw = new StreamWriter(@"C:\Users\weizsw\iCloudDrive\Documents\Ai for Games\EvoStone\TestBed\StrategySearch\resources\decks\pools\tml\test" + nameCounter + ".tml");
				sw.WriteLine("PoolName = \"Meta Decks\"");
				sw.WriteLine("[[Decks]]");
				sw.WriteLine("DeckName = \"test\"");
				string className = cm.ComboClass.ToLower();
				sw.WriteLine("ClassName = \"" + Char.ToUpper(className[0]) + className.Substring(1) + "\"");

				sw.Write("CardList = [");
				int i = 0;
				while (i < 29)
				{
					sw.Write("\"" + cm.ComboCards[i].Name + "\"" + ", ");
					i++;
				}
				sw.Write("\"" + cm.ComboCards[29].Name + "\"");
				sw.Write("]");
				nameCounter++;
				sw.Flush();
				sw.Close();

				//using (StreamWriter wr = File.AppendText(@"C:\Users\weizsw\iCloudDrive\Documents\Ai for Games\EvoStone\TestBed\StrategySearch\weight.txt"))
				//{

				//	//sw.WriteLine("PoolName = \"Meta Decks\"");
				//	//wr.WriteLine("[[Decks]]");
				//	//wr.WriteLine("DeckName = \"test" + "\"");
				//	string newclassName = cm.ComboClass.ToLower();
				//	wr.WriteLine("ClassName =" + Char.ToUpper(newclassName[0]) + newclassName.Substring(1));

				//	wr.Write("CardList =");
				//	int j = 0;
				//	while (j < 29)
				//	{
				//		wr.Write(cm.ComboCards[j].Name + ",");
				//		j++;
				//	}
				//	wr.Write(cm.ComboCards[29].Name);
				//	//wr.Write("]");
				//	wr.WriteLine("\n");
				//	wr.Write("win = ");
				//	wr.Flush();
				//	wr.Close();
				//}
			}

		}
	}
}
