using System;
using System.Collections.Generic;
using SabberStoneCore.Model;

namespace SabberStoneCoreConsole.src
{
	public class SingleCard
	{

		public HashSet<string> tags { get; set; }

		public Card card { get; }

		public SingleCard(HashSet<string> tags, Card card)
		{
			this.tags = tags;
			this.card = card;
		}
		
	}
}
