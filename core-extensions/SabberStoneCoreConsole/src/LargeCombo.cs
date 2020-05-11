using System;
using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Model;

namespace SabberStoneCoreConsole.src
{
	public class LargeCombo
	{
		public string ComboClass { get; }
		public Dictionary<string, int> Tags { get; set; }
		public int Cost { get; set; }
		public List<Card> ComboCards { get; }

		/*public LargeCombo()
		{
			ComboClass = "NEUTRAL";
			Tags.Add("AOE", 0);
			Tags.Add("Remove", 0);
			Tags.Add("High Quality", 0);
			Cost = 0;
			ComboCards = new List<Card>();
		}*/
		public LargeCombo(CardInfo cardInfo)
		{
			
			ComboClass = cardInfo.card.Class.ToString();
			Tags = new Dictionary<string, int>();
			Tags.Add("AOE", 0);
			Tags.Add("Remove", 0);
			Tags.Add("High Quality", 0);
			foreach (string tag in cardInfo.tags)
			{
				if (Tags.ContainsKey(tag))
				{
					Tags[tag]++;
				}
			}

			Cost = cardInfo.card.Cost;

			ComboCards = new List<Card>();
			ComboCards.Add(cardInfo.card);
		}

		public LargeCombo(CardCombo cardCombo)
		{
			ComboClass = cardCombo.ComboClass;
			Tags = new Dictionary<string, int>();
			Tags.Add("AOE", 0);
			Tags.Add("Remove", 0);
			Tags.Add("High Quality", 0);
			foreach (string tag in cardCombo.Tags)
			{
				if (Tags.ContainsKey(tag))
				{
					Tags[tag]++;
				}
			}

			Cost = cardCombo.Cost;

			ComboCards = new List<Card>(cardCombo.ComboCards);
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
		}
	}
}
