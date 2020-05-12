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

		// for range
		static double wAOEL = 0.0 ,wAOEH = 1.0;
		static double wRemoveL = 0.0, wRemoveH = 1.0;
		static double wHQL = 0.0, wHQH = 1.0;
		static double wCostL =0.0, wCostH = 1.0;
		static double High = 1.0;
		static double Low = 0.0;
		static double Mid = 0.5;

		public double CalculateNew(LargeCombo large1, LargeCombo large2)
		{
			double vecP = 1.0;
			double vecCost = 1.0;
			double vecAOE = 1.0;
			double vecRemove = 1.0;
			double vecHQ = 1.0;

			vecP = (vecCost * wCost + vecAOE * wAOE + vecRemove * wRemove + vecHQ * wHQ) / (wRemove + wHQ + wAOE + wCost);
			return vecP;
		}
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

		public double Calculate(LargeCombo large1, LargeCombo large2, int RangeFlag)
		{
			// 0: low, 1: high
			double vecP = 1.0;
			double vecCost = 1.0;
			double vecAOE = 1.0;
			double vecRemove = 1.0;
			double vecHQ = 1.0;

			double wAOE, wRemove, wHQ, wCost;
			

			if (RangeFlag == 0)
			{
				wAOE = wAOEL;
				wRemove = wRemoveL;
				wHQ = wHQL;
				wCost = wCostL;
			} else
			{
				wAOE = wAOEH;
				wRemove = wRemoveH;
				wHQ = wHQH;
				wCost = wCostH;
			}	
			

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
			}
			else if (((large1.Tags["AOE"] + large2.Tags["AOE"]) == 1) || ((large1.Tags["AOE"] + large2.Tags["AOE"]) == 2))
			{
				vecAOE = 0.8;
			}

			if ((large1.Tags["Remove"] + large2.Tags["Remove"]) > 4)
			{
				vecRemove = 0.1;
			}
			else if (((large1.Tags["Remove"] + large2.Tags["Remove"]) == 1) || ((large1.Tags["Remove"] + large2.Tags["Remove"]) == 2))
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
			}
			else
			{
				if (avgSmall <= 2.7)
				{
					vecCost = 1;
				}
				else if (avgSmall >= 5 && avgBig <= 3.5)
				{
					vecCost = 1;
				}
				else
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

				wCostL = random.NextDouble()/2;
				wAOEL = random.NextDouble()/2;
				wHQL = random.NextDouble()/2;
				wRemoveL = random.NextDouble()/2;

				wCostH = random.NextDouble()/2 + 0.5;
				wAOEH = random.NextDouble()/2 + 0.5;
				wHQH = random.NextDouble()/2 + 0.5;
				wRemoveH = random.NextDouble()/2 + 0.5;

				inited = true;
			}

			return Calculate(large1, large2);
		}

		public double RandomWeightCalcualte(LargeCombo large1, LargeCombo large2, int flag)
		{
			Random random = new Random();
			if (!inited)
			{
				wCost = random.NextDouble();
				wAOE = random.NextDouble();
				wHQ = random.NextDouble();
				wRemove = random.NextDouble();

				wCostL = random.NextDouble() / 2;
				wAOEL = random.NextDouble() / 2;
				wHQL = random.NextDouble() / 2;
				wRemoveL = random.NextDouble() / 2;

				wCostH = random.NextDouble() / 2 + 0.5;
				wAOEH = random.NextDouble() / 2 + 0.5;
				wHQH = random.NextDouble() / 2 + 0.5;
				wRemoveH = random.NextDouble() / 2 + 0.5;

				inited = true;
			}

			return Calculate(large1, large2, flag);
		}


		public static void UpdateWeight(int winCountLow, int winCountHigh)
		{
			Random random = new Random();
			double MIN = 0.03;
			// input: winCount, out of 100
			if (winCountLow == winCountHigh || High - Low < MIN)
				return;

			if (winCountHigh > winCountLow)
			{
				Low = Mid;
			}
			else
			{
				High = Mid;
			}
			Mid = (Low + High) / 2;

			double dis = 1 - Mid < Mid - 0 ? 1 - Mid : Mid;

			wCostL = Mid - random.NextDouble() * dis ;
			wAOEL = Mid - random.NextDouble() * dis;
			wRemoveL = Mid - random.NextDouble() * dis;
			wHQL = Mid - random.NextDouble() * dis;

			wCostH = Mid + random.NextDouble() * dis;
			wAOEH = Mid + random.NextDouble() * dis;
			wRemoveH = Mid + random.NextDouble() * dis;
			wHQH = Mid + random.NextDouble() * dis;

			return;
		}

	}
}
