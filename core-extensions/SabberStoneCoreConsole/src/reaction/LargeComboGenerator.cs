﻿using System;
using System.Collections.Generic;
using SabberStoneCore.Model;
using SabberStoneCoreConsole.src.Evolution;

namespace SabberStoneCoreConsole.src
{
	public static class LargeComboGenerator
	{
		public static List<LargeCombo> Container = new List<LargeCombo>();
		static LargeCombo Deck = null;
		static Random rand = new Random();
		static LargeCombo[] DeckArray = new LargeCombo[2];


		public static List<LargeCombo> InitToLarge(List<SingleCard> CardWithTag, HashSet<SmallCombo> ComboSet)
		{
			
			foreach(SingleCard singleCard in CardWithTag)
			{
				Container.Add(new LargeCombo(singleCard));
			}
			foreach(SmallCombo smallCombo in ComboSet)
			{
				Container.Add(new LargeCombo(smallCombo));
			}
			return Container;
		}

		public static bool CheckMerge(LargeCombo large1, LargeCombo large2, DNA dna)
		{
			if (large1.ComboCards.Count + large2.ComboCards.Count > 30)
				return false;
			if (large1.ComboClass != "NEUTRAL" && large2.ComboClass != "NEUTRAL"
				&& large1.ComboClass != large2.ComboClass)
				return false;
			if (HasIllegalDuplicate(large1, large2))
				return false;
			double p = new DynamicVectorCalculator().CalculateByEvolution(large1, large2, dna);
			if (rand.NextDouble() > p)
			{
				//Console.WriteLine("ref, p = " + p.ToString());
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

		public static void randomCombine(DNA dna)
		{
			int index1 = rand.Next(Container.Count), index2 = rand.Next(Container.Count);
			LargeCombo large1 = Container[index1], large2 = Container[index2];
			if (CheckMerge(large1, large2, dna))
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


		public static LargeCombo DeckBuilding(List<SingleCard> CardWithTag, HashSet<SmallCombo> ComboSet, DNA dna)
		{
			InitToLarge(CardWithTag, ComboSet);
			while (Deck == null)
				randomCombine(dna);
			return Deck;
		}


		
	}
}
