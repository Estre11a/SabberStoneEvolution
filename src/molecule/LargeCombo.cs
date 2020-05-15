using System;
using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Model;

namespace SabberStoneCoreConsole.src
{
	public class LargeCombo
	{
		public List<Card> ComboCards { get; }
		public string ComboClass { get; }
		public Dictionary<string, int> Tags { get; set; }

		public int Cost { get; set; }

		public int RMScore { get; }
		public int AOEScore { get; }
		public int HQScore { get; }

		/*public LargeCombo()
		{
			ComboClass = "NEUTRAL";
			Tags.Add("AOE", 0);
			Tags.Add("Remove", 0);
			Tags.Add("High Quality", 0);
			Cost = 0;
			ComboCards = new List<Card>();
		}*/
		public LargeCombo(SingleCard singleCard)
		{
			ComboClass = singleCard.card.Class.ToString();
			Tags = new Dictionary<string, int>();
			Tags.Add("AOE", 0);
			Tags.Add("Remove", 0);
			Tags.Add("High Quality", 0);
			foreach (string tag in singleCard.Tags)
			{
				if (Tags.ContainsKey(tag))
				{
					Tags[tag]++;
				}
			}

			Cost = singleCard.card.Cost;

			ComboCards = new List<Card>();
			ComboCards.Add(singleCard.card);
			RMScore = singleCard.RMScore;
			AOEScore = singleCard.AOEScore;
			HQScore = singleCard.HQScore;
		}

		public LargeCombo(SmallCombo smallCombo)
		{
			ComboClass = smallCombo.ComboClass;
			Tags = new Dictionary<string, int>();
			Tags.Add("AOE", 0);
			Tags.Add("Remove", 0);
			Tags.Add("High Quality", 0);
			foreach (string tag in smallCombo.Tags)
			{
				if (Tags.ContainsKey(tag))
				{
					Tags[tag]++;
				}
			}

			Cost = smallCombo.Cost;

			ComboCards = new List<Card>(smallCombo.ComboCards);
			RMScore = smallCombo.RMScore;
			AOEScore = smallCombo.AOEScore;
			HQScore = smallCombo.HQScore;
		}

		public LargeCombo(LargeCombo large1, LargeCombo large2)
		{
			ComboClass = "NEUTRAL";
			if (large1.ComboClass != "NEUTRAL")
				ComboClass = large1.ComboClass;
			if (large2.ComboClass != "NEUTRAL")
				ComboClass = large2.ComboClass;
			Tags = new Dictionary<string, int>();
			Tags.Add("AOE", 0);
			Tags.Add("Remove", 0);
			Tags.Add("High Quality", 0);
			foreach (var pair in large1.Tags)
			{
				string key = pair.Key;
				int value = pair.Value;
				Tags[key] += value;
			}
			foreach (var pair in large2.Tags)
			{
				string key = pair.Key;
				int value = pair.Value;
				Tags[key] += value;
			}

			Cost = large1.Cost + large2.Cost;
			ComboCards = large1.ComboCards.Concat(large2.ComboCards).ToList();

			RMScore = (large1.RMScore + large2.RMScore);
			AOEScore = (large1.AOEScore + large2.AOEScore);
			HQScore = (large1.HQScore + large2.HQScore);
		}
	}
}
