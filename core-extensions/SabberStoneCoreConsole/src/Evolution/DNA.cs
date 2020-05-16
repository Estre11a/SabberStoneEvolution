using System;
using System.Collections.Generic;
using System.Linq;

namespace SabberStoneCoreConsole.src.Evolution
{
	public class DNA: IComparable<DNA>
	{
		public static int AOEW = 0; //index
		public static int HQW = 1;
		public static int RMW = 2;
		public static int CW = 3;

		public static int count = 4; // size

		public double[] weights; //dna weights
		List<double> WinRate;
		double AVGWin;
		double MAXWin;

		Random random = new Random();

		public DNA()
		{
			weights = new double[count];
			for(int i = 0; i < count; i++)
			{
				weights[i] = random.NextDouble();
			}
			WinRate = new List<double>();
			AVGWin = 0;
			MAXWin = 0;
		}

		public DNA(double[] weights, List<double> WinRate)
		{
			if(weights.Length == count)
			{
				this.weights = weights;
				this.WinRate = WinRate;
			} else
			{
				weights = new double[count];
				for (int i = 0; i < count; i++)
				{
					weights[i] = random.NextDouble();
				}
				WinRate = new List<double>();
				AVGWin = WinRate.Average();
				MAXWin = WinRate.Max();
			}
				
		}

		public DNA(double AOEWeight, double HQWegiht, double RMWeight, double CostWeight, List<double> WinRate)
		{
			weights[AOEW] = AOEWeight;
			weights[HQW] = HQWegiht;
			weights[RMW] = RMWeight;
			weights[CW] = CostWeight;
			this.WinRate = WinRate;
			AVGWin = WinRate.Average();
			MAXWin = WinRate.Max();
		}

		public DNA(string[] weightSet, string[] winCount)
		{
			weights[AOEW] = Convert.ToDouble(weightSet[AOEW]);
			weights[HQW] = Convert.ToDouble(weightSet[HQW]);
			weights[RMW] = Convert.ToDouble(weightSet[RMW]);
			weights[CW] = Convert.ToDouble(weightSet[CW]);
			foreach(string s in winCount)
			{
				WinRate.Add(Convert.ToDouble(s));
			}
			AVGWin = WinRate.Average();
			MAXWin = WinRate.Max();
		}

		public int CompareTo(DNA other)
		{
			int maxCMP = other.MAXWin.CompareTo(this.MAXWin);
			if (maxCMP != 0)
				return maxCMP;
			return other.AVGWin.CompareTo(this.AVGWin);//降序
		}

		public DNA crossover(DNA partner)
		{
			DNA child = new DNA();
			int midpoint = random.Next(weights.Length);
			for (int i = 0; i < weights.Length; i++)
			{
				if(i > midpoint)
					child.weights[i] = weights[i];
				else
					child.weights[i] = partner.weights[i];
			}
			return child;
		}
		public void mutate(double mutationRate)
		{
			for (int i = 0; i < weights.Length; i++)
			{
				if (random.NextDouble() < mutationRate)
				{
					weights[i] = random.NextDouble();
				}
			}
		}
	}
}
