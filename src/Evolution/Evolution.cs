using System;
using System.Collections.Generic;

namespace SabberStoneCoreConsole.src.Evolution
{
	public class Evolution
	{
		DNA[] population;
		List<DNA> matingPool;

		double mutationRate = 0.25;
		Random rand = new Random();
		int totalPopulation = 100;
		int parentPopulation = 50;

		public Evolution() //initialize in first round
		{
			population = new DNA[totalPopulation];
			for(int i = 0; i < totalPopulation; i++)
				population[i] = new DNA();

			matingPool = new List<DNA>();
		}
		public Evolution(DNA[] population)
		{
			this.population = population;
			matingPool = new List<DNA>();
		}

		public void Draw() //evolution process
		{
			//build mating pool
			//sort population by win rate
			//select top parent population to be parent
			Array.Sort(population);
			for(int i = 0; i < parentPopulation; i++)
			{
				matingPool.Add(population[i]);
			}
			//reproduction
			for (int i = 0; i < population.Length; i++)
			{
				//select parent randomly
				DNA partnerA = matingPool[rand.Next(matingPool.Count)];
				DNA partnerB = matingPool[rand.Next(matingPool.Count)];
				//Crossover
				DNA child = partnerA.crossover(partnerB);
				//Mutation
				child.mutate(mutationRate);
				//overwriting the population with the new children
				population[i] = child;
			}
		}
	}
}
