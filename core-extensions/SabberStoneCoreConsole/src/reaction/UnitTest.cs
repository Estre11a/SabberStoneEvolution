using System;
using System.Collections.Generic;
using SabberStoneCore.Actions;
using SabberStoneCore.Config;
using SabberStoneCore.Enums;
using SabberStoneCore.Model;
using SabberStoneCore.Model.Entities;
using SabberStoneCore.Tasks.PlayerTasks;

namespace SabberStoneCoreConsole.src
{
	public class UnitTest
	{
		public UnitTest()
		{
		}
		public static Dictionary<string, int> CARDCLASS = new Dictionary<string, int>()
		{
			{"DEATHKNIGHT",1 },
			{"DRUID",  2 },
			{"HUNTER",  3 },
			{"MAGE",  4 },
			{"PALADIN",  5 },
			{"PRIEST",  6 },
			{"ROGUE",  7 },
			{"SHAMAN", 8 },
			{"WARLOCK",  9 },
			{ "WARRIOR",10},
			{"DREAM" , 11 },
			{"NEUTRA" , 12 },
			{"WHIZBANG" , 13 }

		};

		public static int OneRoundAOE(List<string> AICards, List<Card> combo, string comboClass)
		{
			if (!CARDCLASS.ContainsKey(comboClass))
				return -1;
			var game = new Game(new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.MAGE,
				
				Player2HeroClass = (CardClass)CARDCLASS[comboClass],
			
				Shuffle = false,
				FillDecks = true,
				FillDecksPredictably = false
			});

			game.Player1.BaseMana = 10;
			game.Player2.BaseMana = 10;
			game.StartGame();

			foreach(string cardname in AICards)//draw all cards due to mana cost limit to 10/round
			{
				//Console.WriteLine(cardname);
				game.Process(PlayCardTask.Any(game.CurrentPlayer,Entity.FromCard(game.CurrentPlayer, Cards.FromName(cardname), zone: game.CurrentPlayer.HandZone)));

				game.Process(EndTurnTask.Any(game.CurrentPlayer));//end round switch to our side
				game.Process(EndTurnTask.Any(game.CurrentPlayer));//end round skip our side, back to opponent
			}
			game.Process(EndTurnTask.Any(game.CurrentPlayer));//end round switch to our side

			if (combo[0].Type.ToString() != "MINION")//minion card use first
				combo.Reverse();
			
			foreach(Card card in combo)//use card one by one
			{
				if(card.Type.ToString() != "MINION")
				{
					IPlayable SpellTest = Generic.DrawCard(game.CurrentPlayer, card);
					game.Process(PlayCardTask.SpellTarget(game.CurrentPlayer, SpellTest, game.CurrentOpponent.BoardZone[0]));
				}
				else //spell or weapon
				{
					game.Process(PlayCardTask.Any(game.CurrentPlayer, Entity.FromCard(game.CurrentPlayer, card, zone: game.CurrentPlayer.HandZone)));
				
				}
			}
	

			/*Program.ShowLog(game, LogLevel.VERBOSE);
			Console.WriteLine(game.CurrentOpponent.BoardZone.FullPrint());
			Console.WriteLine(game.CurrentPlayer.BoardZone.FullPrint());*/

			int health = game.CurrentOpponent.BoardZone.Health();
			return health;
		}

		public static int OneRoundRemove(string AICard, List<Card> combo, string comboClass)
		{
			if (!CARDCLASS.ContainsKey(comboClass))
				return -1;
			var game = new Game(new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.MAGE,
				Player1Deck = new List<Card>()
				{
					Cards.FromName(AICard)
				},
				Player2HeroClass = (CardClass)CARDCLASS[comboClass],

				Player2Deck = new List<Card>(),
				Shuffle = false,
				FillDecks = false,
				FillDecksPredictably = false
			});
			
			foreach (Card testCard in combo)
				game.Player2.DeckCards.Add(testCard);

			game.Player1.BaseMana = 10;
			game.Player2.BaseMana = 10;
			game.StartGame();


			//AI player
			var MinionAI = (ICharacter)Generic.DrawCard(game.CurrentPlayer, Cards.FromName(AICard));//generate opponent minion
			game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionAI));//use
			game.Process(EndTurnTask.Any(game.CurrentPlayer));//end round switch to our side

			//Test case
			foreach (Card testCard in combo) //use combo card one by one
			{
				if (testCard.Type.ToString() == "MINION")
				{
					var MinionTest = (ICharacter)Generic.DrawCard(game.CurrentPlayer, testCard);//draw
					game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionTest));//use
				}
				else//spell or weapon
				{
					IPlayable SpellTest = Generic.DrawCard(game.CurrentPlayer, testCard);//draw
					game.Process(PlayCardTask.SpellTarget(game.CurrentPlayer, SpellTest, MinionAI));//use taget to opponet minion
				}
			}
			/*Program.ShowLog(game, LogLevel.VERBOSE);
			Console.WriteLine(game.CurrentOpponent.BoardZone.FullPrint());
			Console.WriteLine(game.CurrentPlayer.BoardZone.FullPrint());*/

			int health = game.CurrentOpponent.BoardZone.Health();
			return health;
		}
		
		public static int TestRemove(List<Card> combo, string comboClass)
		{
			var MinionsToAttack = new List<string>() {
				"Murloc Raider",	//1
				"Bloodfen Raptor",	//2
				"River Crocolisk",	//3
				"Ogre Magi",		//4
				"Chillwind Yeti",	//5
				"Spiteful Smith",	//6
				"Boulderfist Ogre",	//7
				"Sea Giant",		//8
				"Grommash Hellscream"		//9
			};
			int TotalScore = 0;
			foreach (string AICard in MinionsToAttack)
			{
				TotalScore += OneRoundRemove(AICard, combo, comboClass); //health
			}
			return TotalScore;
		}


		public static int TestAOE(List<Card> combo, string comboClass)
		{
			var MinionsToAttack = new List<List<string>>() { 
			new List<string>(){ "Murloc Raider", "Murloc Raider", "Bloodfen Raptor"},        //112
			new List<string>(){ "Bloodfen Raptor", "Bloodfen Raptor", "River Crocolisk" },         //223
			new List<string>(){ "River Crocolisk", "River Crocolisk", "Ogre Magi" },            //334
			new List<string>(){ "Ogre Magi", "Ogre Magi", "Chillwind Yeti" },            //445
			new List<string>(){ "Chillwind Yeti", "Chillwind Yeti", "Spiteful Smith"  },			//556
			new List<string>(){ "Spiteful Smith", "Spiteful Smith", "Boulderfist Ogre"},           //667
			new List<string>(){ "Boulderfist Ogre", "Boulderfist Ogre", "Sea Giant" },        //778
			new List<string>(){ "Sea Giant", "Sea Giant", "Grommash Hellscream" },     //889
			new List<string>(){ "Grommash Hellscream","Grommash Hellscream","Grommash Hellscream" },	//999
			};

			int TotalScore = 0;
			foreach (List<string> AICards in MinionsToAttack)
			{
				TotalScore += OneRoundAOE(AICards, combo, comboClass); //health
			}
			return TotalScore;

		}
		/*public static int TestHQ(List<Card> combo, string comboClass)
		{
			//TODO:
			return -1;
		}*/
	}
}
