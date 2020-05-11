using System;
using System.Collections.Generic;
using SabberStoneCore.Model;

namespace SabberStoneCoreConsole.src
{
	public static class LargeComboGenerator
	{
		public static List<LargeCombo> Container = new List<LargeCombo>();
		static LargeCombo Deck = null;
		static Random rand = new Random();


		public static List<LargeCombo> InitToLarge(List<CardInfo> CardWithTag, HashSet<CardCombo> ComboSet)
		{
			
			foreach(CardInfo singleCard in CardWithTag)
			{
				Container.Add(new LargeCombo(singleCard));
			}
			foreach(CardCombo smallCombo in ComboSet)
			{
				Container.Add(new LargeCombo(smallCombo));
			}
			return Container;
		}

		public static bool CheckMerge(LargeCombo large1, LargeCombo large2)
		{
			if (large1.ComboCards.Count + large2.ComboCards.Count > 30)
				return false;
			if (large1.ComboClass != "NEUTRAL" && large2.ComboClass != "NEUTRAL"
				&& large1.ComboClass != large2.ComboClass)
				return false;
			if (HasIllegalDuplicate(large1, large2))
				return false;
			double p = new DynamicVectorCalculator().RandomWeightCalcualte(large1, large2);
			////????
			if(rand.NextDouble() > p)
			{
				Console.WriteLine("ref, p = " + p.ToString());
				return false;
			}
			return true;
		}

		public static bool HasIllegalDuplicate(LargeCombo large1, LargeCombo large2)
		{
			Dictionary<string, int> countOfCard = new Dictionary<string, int>();
			foreach(Card card in large1.ComboCards)
			{
				string key = card.Name;
				if (!countOfCard.ContainsKey(key))
					countOfCard.Add(key, 0);
				countOfCard[key]++;
				if (countOfCard[key] > 2)
					return true;
			}
			foreach (Card card in large2.ComboCards)
			{
				string key = card.Name;
				if (!countOfCard.ContainsKey(key))
					countOfCard.Add(key, 0);
				countOfCard[key]++;
				if (countOfCard[key] > 2)
					return true;
			}
			return false;
		}

		public static void randomCombine()
		{
			int index1 = rand.Next(Container.Count), index2 = rand.Next(Container.Count);
			LargeCombo large1 = Container[index1], large2 = Container[index2];
			if (CheckMerge(large1, large2))
			{
				LargeCombo merged = new LargeCombo(large1, large2);
				if (merged.ComboCards.Count == 30)
				{
					Deck = merged;
					return;
				}
				Container.Add(merged);
			}	
		}

		public static LargeCombo DeckBuilding(List<CardInfo> CardWithTag, HashSet<CardCombo> ComboSet)
		{
			InitToLarge(CardWithTag, ComboSet);
			while (Deck == null)
				randomCombine();
			return Deck;
		}
	}
}
