using System;
using System.Collections.Generic;
using SabberStoneCore.Model;

namespace SabberStoneCoreConsole.src
{
	public class CardInfo
	{

		public HashSet<string> tags { get; set; }

		public Card card { get; }

		public CardInfo(HashSet<string> tags, Card card)
		{
			this.tags = tags;
			this.card = card;


			//////
			//List<CardInfo> cardlist = new List<CardInfo>();
			//cardlist.Add(new CardInfo(new HashSet<string>(), Cards.FromName("xxxx")));
		}
		
	}
}
