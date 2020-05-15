using System;
using System.Collections.Generic;

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
		double WinRate;

		Random random = new Random();

		public DNA()
		{
			weights = new double[count];
			for(int i = 0; i < count; i++)
			{
				weights[i] = random.NextDouble();
			}
			WinRate = 0;
		}

		public DNA(double[] weights, double WinRate)
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
				WinRate = 0;
			}
				
		}

		public DNA(double AOEWeight, double HQWegiht, double RMWeight, double CostWeight, double WinRate)
		{
			weights[AOEW] = AOEWeight;
			weights[HQW] = HQWegiht;
			weights[RMW] = RMWeight;
			weights[CW] = CostWeight;
			this.WinRate = WinRate;
		}

		public int CompareTo(DNA other)
		{
			return other.WinRate.CompareTo(this.WinRate);//降序
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
