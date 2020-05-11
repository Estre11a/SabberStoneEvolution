using System;
using System.Collections.Generic;
using System.Text;
using SabberStoneCore.Model;

namespace SabberStoneCoreConsole.src
{
	class DynamicVectorCalculator	
	{
		static double wAOE = 1.0;
		static double wRemove = 1.0;
		static double wHQ = 1.0;
		static double wCost = 1.0;
		static bool inited = false;
		public double Calculate(LargeCombo large1, LargeCombo large2)
		{
			double vecP = 1.0;
			double vecCost = 1.0;
			double vecAOE = 1.0;
			double vecRemove = 1.0;
			double vecHQ = 1.0;

			int len1 = large1.ComboCards.Count;
			int len2 = large2.ComboCards.Count;
			int lenBig = len2;
			int lenSmall = len1;

			double avg1 = large1.Cost / len1;
			double avg2 = large2.Cost / len2;

			double avgBig = avg1;
			double avgSmall = avg2;

			if (len1 > len2)
			{
				lenBig = len1;
				lenSmall = len2;
				avgBig = avg1;
				avgSmall = avg2;
			}

			if ((large1.Tags["AOE"] + large2.Tags["AOE"]) > 4)
			{
				vecAOE = 0.1;
			}else if (((large1.Tags["AOE"] + large2.Tags["AOE"]) == 1) || ((large1.Tags["AOE"] + large2.Tags["AOE"]) == 2))
			{
				vecAOE = 0.8;
			}

			if ((large1.Tags["Remove"] + large2.Tags["Remove"]) > 4)
			{
				vecRemove = 0.1;
			}else if (((large1.Tags["Remove"] + large2.Tags["Remove"]) == 1) || ((large1.Tags["Remove"] + large2.Tags["Remove"]) == 2))
			{
				vecRemove = 0.8;
			}

			if ((large1.Tags["High Quality"] + large2.Tags["High Quality"]) > 4)
			{
				vecHQ = 0.1;
			}
			else if (((large1.Tags["High Quality"] + large2.Tags["High Quality"]) == 1) || ((large1.Tags["High Quality"] + large2.Tags["High Quality"]) == 2))
			{
				vecHQ = 0.8;
			}

			if ((len1 + len2) < 20)
			{
				vecCost = 1;
			}else
			{
				if (avgSmall <= 2.7)
				{
					vecCost = 1;
				}else if (avgSmall >= 5 && avgBig <= 3.5)
				{
					vecCost = 1;
				}else
				{
					vecCost = 0.5;
				}
			}

			vecP = (vecCost * wCost + vecAOE * wAOE + vecRemove * wRemove + vecHQ * wHQ) / (wRemove + wHQ + wAOE + wCost);
			return vecP;
		}

		public double RandomWeightCalcualte(LargeCombo large1, LargeCombo large2)
		{
			Random random = new Random();
			if (!inited)
			{
				wCost = random.NextDouble();
				wAOE = random.NextDouble();
				wHQ = random.NextDouble();
				wRemove = random.NextDouble();
				inited = true;
			}

			return Calculate(large1, large2);
		}

	}
}
