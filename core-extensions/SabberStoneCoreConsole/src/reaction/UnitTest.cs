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

		public static int OneRoundAOE(List<string> AICards, List<Card> combo)
		{
			var game = new Game(new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.MAGE,
				Player1Deck = new List<Card>(),
				Player2HeroClass = CardClass.MAGE,
				Player2Deck = new List<Card>(),
				Shuffle = false,
				FillDecks = false,
				FillDecksPredictably = false
			});
			foreach (string cardName in AICards)
				game.Player1.DeckCards.Add(Cards.FromName(cardName));
			foreach (Card testCard in combo)
				game.Player2.DeckCards.Add(testCard);

			game.Player1.BaseMana = 10;
			game.Player2.BaseMana = 10;
			game.StartGame();


			//AI player
			var MinionAI = (ICharacter)Generic.DrawCard(game.CurrentPlayer, Cards.FromName(AICards[0]));
			game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionAI));
			game.Process(EndTurnTask.Any(game.CurrentPlayer));

			//Test case
			foreach(Card testCard in combo)
			{
				if(testCard.Type.ToString() == "MINION")
				{
					var MinionTest = (ICharacter)Generic.DrawCard(game.CurrentPlayer, testCard);
					game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionTest));
				} else
				{
					IPlayable SpellTest = Generic.DrawCard(game.CurrentPlayer, testCard);
					game.Process(PlayCardTask.SpellTarget(game.CurrentPlayer, SpellTest, MinionAI));
				}
			}
			Program.ShowLog(game, LogLevel.VERBOSE);
			Console.WriteLine(game.CurrentOpponent.BoardZone.FullPrint());

			int health = game.CurrentOpponent.BoardZone.Health();
			return health;
		}

		public static int OneRoundRemove(string AICard, List<Card> combo)
		{
			var game = new Game(new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.MAGE,
				Player1Deck = new List<Card>()
				{
					Cards.FromName(AICard)
				},
				Player2HeroClass = CardClass.MAGE,

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
			var MinionAI = (ICharacter)Generic.DrawCard(game.CurrentPlayer, Cards.FromName(AICard));
			game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionAI));
			game.Process(EndTurnTask.Any(game.CurrentPlayer));

			//Test case
			foreach (Card testCard in combo)
			{
				if (testCard.Type.ToString() == "MINION")
				{
					var MinionTest = (ICharacter)Generic.DrawCard(game.CurrentPlayer, testCard);
					game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionTest));
				}
				else
				{
					IPlayable SpellTest = Generic.DrawCard(game.CurrentPlayer, testCard);
					game.Process(PlayCardTask.SpellTarget(game.CurrentPlayer, SpellTest, MinionAI));
				}
			}
			//ShowLog(game, LogLevel.VERBOSE);
			//Console.WriteLine(game.CurrentOpponent.BoardZone.FullPrint());
			//Console.WriteLine(game.CurrentPlayer.BoardZone.FullPrint());

			int health = game.CurrentOpponent.BoardZone.Health();
			return health;
		}
		
		public static int TestRemove(List<Card> combo)
		{
			var MinionsToAttack = new List<string>() {
				"Abusive Sergeant",			//1
				"Kobold Geomancer",			//2
				"Ironfur Grizzly",			//3
				"Dark Iron Dwarf",			//4
				"Chillwind Yeti",			//5
				"Temple Enforcer",			//6
				"Boulderfist Ogre",			//7
				"Ironbark Protector",		//8
				"Grommash Hellscream",		//9
			};
			int TotalScore = 0;
			foreach (string AICard in MinionsToAttack)
			{
				TotalScore += OneRoundRemove(AICard, combo); //health
			}
			return TotalScore;
		}


		public static int TestAOE(List<Card> combo)
		{
			var MinionsToAttack = new List<List<string>>() { 
			new List<string>(){ "Abusive Sergeant","Abusive Sergeant", "Kobold Geomancer"},        //112
			new List<string>(){ "Kobold Geomancer","Kobold Geomancer","Ironfur Grizzly" },         //223
			new List<string>(){ "Ironfur Grizzly","Ironfur Grizzly","Dark Iron Dwarf" },            //334
			new List<string>(){ "Dark Iron Dwarf","Dark Iron Dwarf", "Chillwind Yeti" },            //445
			new List<string>(){ "Chillwind Yeti", "Chillwind Yeti","Temple Enforcer"  },			//556
			new List<string>(){ "Temple Enforcer","Temple Enforcer" , "Boulderfist Ogre"},           //667
			new List<string>(){ "Boulderfist Ogre","Boulderfist Ogre","Ironbark Protector" },        //778
			new List<string>(){ "Ironbark Protector","Ironbark Protector","Grommash Hellscream" },     //889
			new List<string>(){ "Grommash Hellscream","Grommash Hellscream","Grommash Hellscream" },	//999
			};

			int TotalScore = 0;
			foreach (List<string> AICards in MinionsToAttack)
			{
				TotalScore += OneRoundAOE(AICards, combo); //health
			}
			return TotalScore;

		}
		public static int TestHQ(List<Card> combo)
		{
			//TODO:
			return -1;
		}
	}
}
