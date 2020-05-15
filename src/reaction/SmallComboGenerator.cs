using System;
using System.Collections.Generic;
using System.Linq;
using SabberStoneCore.Model;

namespace SabberStoneCoreConsole.src
{
	public static class SmallComboGenerator
	{
		static public List<SmallCombo> comboList = new List<SmallCombo>();
		static int CountLimit = 200;
		static Random rand = new Random();

		public static HashSet<SmallCombo> RandomComboGenerator(List<SingleCard> CardWithtag)
		{
			HashSet<SmallCombo> result = new HashSet<SmallCombo>();
			int count = 0;
			while(count < CountLimit)
			{
				int index1 = rand.Next(CardWithtag.Count);
				int index2 = rand.Next(CardWithtag.Count);
				HashSet<int[]> randomSet = new HashSet<int[]>();
				if(index1 > index2) //always index1 <= index2
				{
					int tmp = index1;
					index1 = index2;
					index2 = tmp;
				}
				int[] indexSet = new int[] { index1, index2 };
				if (index1 != index2 && !randomSet.Contains(indexSet) && CanReact(CardWithtag[index1], CardWithtag[index2]))
				{
					result.Add(new SmallCombo(CardWithtag[index1], CardWithtag[index2]));
					randomSet.Add(indexSet);
					count++;
				}
			}
			return result;
		}
		///
		/*public List<T> GetRandomElements<T>(this IEnumerable<T> list, int elementsCount)
		{
			return list.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToList();
		}*/


		public static bool CanReact(SingleCard card1, SingleCard card2)
		{
			// Rule 0 : not belongs to different classes and total cost less than 10
			if (card1.card.Cost + card2.card.Cost > 10)
				return false;
			if (card1.card.Class.ToString() != "NEUTRAL" && card2.card.Class.ToString() != "NEUTRAL"
				&& card1.card.Class != card2.card.Class)
				return false;
			// Rule 1 : has same tag
			HashSet<string> intersect = new HashSet<string>(card1.Tags);
			intersect.IntersectWith(card2.Tags);
			if (intersect.Count > 0)
				return true;
			// Rule 2 : charge + buff (= Remove)
			if ((card1.Tags.Contains("Charge") && card2.Tags.Contains("Buff"))
				|| (card1.Tags.Contains("Buff") && card2.Tags.Contains("Charge")))
				return true;
			// Rule 3 : AOE + Taunt
			if ((card1.Tags.Contains("AOE") && card2.Tags.Contains("Taunt"))
				|| (card1.Tags.Contains("Taunt") && card2.Tags.Contains("AOE")))
				return true;

			return false;
		}

		public static HashSet<SmallCombo> Evo(List<SingleCard> CardWithtag)
		{
			HashSet<SmallCombo> result = new HashSet<SmallCombo>();
			while(result.Count < CountLimit)
			{
				HashSet<SmallCombo> comboTemp = RandomComboGenerator(CardWithtag);
				foreach (SmallCombo combo in comboTemp)
				{
					//Console.WriteLine(combo.Score);
					if (combo.RMScore >= 0 && combo.RMScore < 15)
						result.Add(combo);
					else if (combo.Tags.Contains("AOE") && combo.AOEScore < 80)
						result.Add(combo);
					else if (combo.Tags.Contains("High Quality") && combo.HQScore > 8)
						result.Add(combo);

				}
			}
			return result;
		}
	}
}