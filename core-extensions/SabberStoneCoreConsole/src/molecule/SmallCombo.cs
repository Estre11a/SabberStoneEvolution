using System;
using System.Collections.Generic;
using SabberStoneCore.Model;
namespace SabberStoneCoreConsole.src
{
	public class SmallCombo
	{
		public HashSet<string> Tags { get; }
		public List<Card> ComboCards { get; }
		public string ComboClass { get; }
		public int Score { get; }
		public int Cost { get; set; }

		public SmallCombo(SingleCard card1, SingleCard card2) //from writeCombo
		{
			Tags = new HashSet<string>(card1.tags);
			Tags.UnionWith(card2.tags);

			ComboCards = new List<Card>();
			ComboCards.Add(card1.card);
			ComboCards.Add(card2.card);

			ComboClass = "NEUTRAL";
			if (card1.card.Class.ToString() != "NEUTRAL")
				ComboClass = card1.card.Class.ToString();
			if (card2.card.Class.ToString() != "NEUTRAL")
				ComboClass = card2.card.Class.ToString();

			Score = -1;
			Cost = -1;
			if (card1 != null && card2 != null)
			{
				Cost = card1.card.Cost + card2.card.Cost;

				Score = Cost + UnitTest.TestRemove(ComboCards, ComboClass);
				if(Tags.Contains("AOE"))
					Score = Math.Min(Score, UnitTest.TestAOE(ComboCards, ComboClass));
				if(Tags.Contains("High Quality"))
					Score = Math.Min(Score, UnitTest.TestHQ(ComboCards, ComboClass));
				
			}
		}
		public void output()
		{
			
				Console.WriteLine("Class:" + ComboClass);
				Console.WriteLine("Score:" + Score);
				foreach (Card card in ComboCards)
					Console.WriteLine("Card:" + card.Name);
				foreach (string tag in Tags)
					Console.WriteLine("Tag:" + tag);
				Console.WriteLine("====================");
		}
	}
	
}
