using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using SabberStoneCore.Model;
using SabberStoneCoreConsole.src.Evolution;

namespace SabberStoneCoreConsole.src
{
	public class Read
	{
		public Read()
		{
		}
		public class CardPool
		{
			public string id { get; set; }
			public string set { get; set; }
			public string type { get; set; }
			public string name { get; set; }
			public int cost { get; set; }
			public int attack { get; set; }
			public int health { get; set; }
			public string text { get; set; }
			public List<string> tag { get; set; }
			public string class_ { get; set; }
		}

		public static List<DNA> ReadWeightFile()
		{
			//!!TODO:
			Console.WriteLine("write read weight file in IO/Read.cs");
			return null;
		}

		public static List<SingleCard> ReadCardsFromJson()
		{
			using (StreamReader r = new StreamReader(@"C:\Users\weizsw\iCloudDrive\Documents\Ai for Games\SabberStone\cards.collectible.json"))
			{
				string json = r.ReadToEnd();
				List<CardPool> items = JsonConvert.DeserializeObject<List<CardPool>>(json);
				List<SingleCard> cardContainer = new List<SingleCard>();
				int count = 0;
				foreach (var item in items)
				{
					if ((item.set != "EXPERT1" && item.set != "CORE"))
					{
						continue;
					}
					if (item.type == "HERO")
					{
						continue;
					}
					HashSet<string> tagSet = new HashSet<string>();
					List<string> tagList = new List<string>();


					Card cardFromRead = Cards.FromName(item.name);
					if ((String.Compare(item.type, "MINION") == 0) && (item.attack + item.health) > 10)
					{
						tagSet.Add("High Quality");
					}
					if ((cardFromRead == null) || (cardFromRead.Text == null))
					{
						continue;
					}
		
					tagList = getTags(cardFromRead.Text);

					foreach (string tag in tagList)
					{
						tagSet.Add(tag);
					}
					

					cardContainer.Add(new SingleCard(tagSet, cardFromRead));
					

				}
				return cardContainer;
				/*foreach (CardInfo item in cardContainer)
				{
					foreach (string tag in item.tags)
					{
						Console.Write(tag + ",");
					}
					Console.WriteLine();
				}*/
			}
		}

		public static List<string> getTags(string text)
		{
			List<string> tags = new List<string>();
			if ((text.Contains("all minions") == true) || (text.Contains("all enemies") == true) || (text.Contains("damage to all enemy") == true))
			{
				tags.Add("AOE");
			}

			if (text.Contains("Deal $") == true)
			{
				tags.Add("Remove");
			}

			if (text.Contains("Transform a minion") == true)
			{
				tags.Add("Remove");
			}

			if (text.Contains("Battlecry") == true)
			{
				tags.Add("Battlecry");
			}

			if (text.Contains("Deathrattle") == true)
			{
				tags.Add("Deathrattle");
			}

			if (text.Contains("Taunt") == true)
			{
				tags.Add("Taunt");
			}

			if (text.Contains("Charge"))
			{
				tags.Add("Charge");
			}

			if (text.Contains("Give a minion +"))
			{
				tags.Add("Buff");
			}

			return tags;
		}
	}
}
