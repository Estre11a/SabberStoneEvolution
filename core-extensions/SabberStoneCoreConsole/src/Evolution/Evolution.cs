using System;
using System.Collections.Generic;

namespace SabberStoneCoreConsole.src.Evolution
{
	public class Evolution
	{
		public List<DNA> population;
		List<DNA> matingPool;

		double mutationRate = 0.25;
		Random rand = new Random();
		int totalPopulation = 20;
		int parentPopulation = 10;

		//initialize
		
		public Evolution(List<DNA> pop)
		{

			this.population = new List<DNA>();
			if (pop != null)
			{
				this.population.AddRange(pop);
			}

			//if population < totalPopulation -> randomly generate rest
			//first round population is 0, so all population are randomly generated
			for (int i = population.Count; i < totalPopulation; i++)
			{
				this.population.Add(new DNA());
			}

			matingPool = new List<DNA>();
		}

		public void Draw() //evolution process
		{
			//build mating pool
			//sort population by win rate
			//select top parent population to be parent
			population.Sort();
			for(int i = 0; i < parentPopulation; i++)
			{
				matingPool.Add(population[i]);
			}
			//reproduction
			List<DNA> children = new List<DNA>();

			for (int i = 0; i < population.Count; i++)
			{
				//select parent randomly
				DNA partnerA = matingPool[rand.Next(matingPool.Count)];
				DNA partnerB = matingPool[rand.Next(matingPool.Count)];
				//Crossover
				DNA child = partnerA.crossover(partnerB);
				//Mutation
				child.mutate(mutationRate);
				//overwriting the population with the new children
				children.Add(child);
			}

			population = children;
		
		}
	}
}
