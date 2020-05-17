using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SabberStoneCore.Model;
using SabberStoneCoreConsole.src.Evolution;

namespace SabberStoneCoreConsole.src
{
	class DynamicVectorCalculator
	{
		double wAOE = 1.0;
		double wRemove = 1.0;
		double wHQ = 1.0;
		double wCost = 1.0;
		static bool inited = false;

		public double CalculateByEvolution(LargeCombo large1, LargeCombo large2, DNA dna)
		{
			wAOE = dna.weights[DNA.AOEW];
			wRemove = dna.weights[DNA.RMW];
			wHQ = dna.weights[DNA.HQW];
			wCost = dna.weights[DNA.CW];


			int count = large1.ComboCards.Count + large2.ComboCards.Count;
			double vecCost = (large1.Cost + large2.Cost) / count;
			double vecAOE = (large1.AOEScore + large2.AOEScore) / count;
			double vecRemove = (large1.RMScore + large2.RMScore) / count;
			double vecHQ = (large1.HQScore + large2.HQScore) / count;

			double costPenalty = Math.Max(0, vecCost - 4.5) * wCost;
			double vecP = ((vecAOE * wAOE + vecRemove * wRemove + vecHQ * wHQ) / (100 * (wRemove + wHQ + wAOE))) - costPenalty;

			return Math.Max(0.1, vecP);
		}

	}
}
