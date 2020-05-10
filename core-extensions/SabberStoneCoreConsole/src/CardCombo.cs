using System;
using System.Collections.Generic;
using SabberStoneCore.Model;
namespace SabberStoneCoreConsole.src
{
	public class CardCombo
	{
		public HashSet<string> Tags { get; }
		public List<Card> ComboCards { get; }
		public string ComboClass { get; }
		public int Score { get; }

		public CardCombo(CardInfo card1, CardInfo card2) //from writeCombo
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
			if (card1 != null && card2 != null)
				Score = UnitTest.Test(new Card[] {card1.card, card2.card});
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
