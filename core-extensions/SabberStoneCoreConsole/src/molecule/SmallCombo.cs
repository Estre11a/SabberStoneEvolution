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
		public int RMScore { get; }
		public int AOEScore { get; }
		public int HQScore { get; }
		public int Cost { get; set; }

		public SmallCombo(SingleCard card1, SingleCard card2) //from writeCombo
		{
			Tags = new HashSet<string>(card1.Tags);
			Tags.UnionWith(card2.Tags);

			ComboCards = new List<Card>();
			ComboCards.Add(card1.card);
			ComboCards.Add(card2.card);

			ComboClass = "NEUTRAL";
			if (card1.card.Class.ToString() != "NEUTRAL")
				ComboClass = card1.card.Class.ToString();
			if (card2.card.Class.ToString() != "NEUTRAL")
				ComboClass = card2.card.Class.ToString();

			Cost = -1;
			RMScore = -1;
			HQScore = -1;
			AOEScore = -1;
			if (card1 != null && card2 != null)
			{
				Cost = card1.card.Cost + card2.card.Cost;
				if (ComboClass != "NEUTRAL")
				{
					RMScore = UnitTest.TestRemove(ComboCards, ComboClass);
					if (Tags.Contains("AOE"))
						AOEScore = UnitTest.TestAOE(ComboCards, ComboClass);
				}		
				if(Tags.Contains("High Quality"))
					HQScore = card1.card.Health + card2.card.Health
							+ card1.card.ATK + card2.card.ATK
							- Cost;

				
				
			}
		}
		public void output()
		{
			
				Console.WriteLine("Class:" + ComboClass);
				Console.WriteLine("AOEScore:" + AOEScore);
			Console.WriteLine("RMScore:" + RMScore);
			Console.WriteLine("HQScore:" + HQScore);
			foreach (Card card in ComboCards)
					Console.WriteLine("Card:" + card.Name);
				foreach (string tag in Tags)
					Console.WriteLine("Tag:" + tag);
				Console.WriteLine("====================");
		}
	}
	
}
