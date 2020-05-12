using System;
using System.Collections.Generic;
using SabberStoneCore.Model;

namespace SabberStoneCoreConsole.src
{
	public class SingleCard
	{

		public HashSet<string> Tags { get; set; }
		public int RMScore { get; }
		public int AOEScore { get; }
		public int HQScore { get; }
		public Card card { get; }

		public SingleCard(HashSet<string> Tags, Card card)
		{
			this.Tags = Tags;
			this.card = card;
			
			RMScore = card.ATK;
			AOEScore = card.ATK;
			HQScore = card.Health + card.ATK - card.Cost;
			
			
			RMScore = UnitTest.TestRemove(new List<Card> { card }, card.Class.ToString());
			if (Tags.Contains("AOE"))
				AOEScore = UnitTest.TestAOE(new List<Card> { card }, card.Class.ToString());
		}
		public void output()
		{

			Console.WriteLine("Class:" + card.Class.ToString());
			Console.WriteLine("AOEScore:" + AOEScore);
			Console.WriteLine("RMScore:" + RMScore);
			Console.WriteLine("HQScore:" + HQScore);
			Console.WriteLine("Card:" + card.Name);
			foreach (string tag in Tags)
				Console.WriteLine("Tag:" + tag);
			Console.WriteLine("====================");
		}

	}
}
