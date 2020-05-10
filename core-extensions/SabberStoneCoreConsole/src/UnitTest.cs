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
		public static int OneRoundMinionSpell(string MinionName1, Card minion, Card spell)
		{
			var game = new Game(new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.MAGE,
				Player1Deck = new List<Card>()
				{
					Cards.FromName(MinionName1)

				},
				Player2HeroClass = CardClass.MAGE,

				Player2Deck = new List<Card>()
				{
					minion,
					spell,
				},
				Shuffle = false,
				FillDecks = false,
				FillDecksPredictably = false
			});

			game.Player1.BaseMana = 10;
			game.Player2.BaseMana = 10;
			game.StartGame();


			//AI player
			var MinionAI = (ICharacter)Generic.DrawCard(game.CurrentPlayer, Cards.FromName(MinionName1));
			game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionAI));
			game.Process(EndTurnTask.Any(game.CurrentPlayer));

			//Test case
			var MinionTest = (ICharacter)Generic.DrawCard(game.CurrentPlayer, minion);
			game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionTest));
			IPlayable SpellTest = Generic.DrawCard(game.CurrentPlayer, spell);
			game.Process(PlayCardTask.SpellTarget(game.CurrentPlayer, SpellTest, MinionAI));

			//ShowLog(game, LogLevel.VERBOSE);

			int health = game.CurrentOpponent.BoardZone.Health();

			//Console.WriteLine(cost1 + ", " + cost2);
			//Console.WriteLine(game.CurrentOpponent.BoardZone.FullPrint());
			//Console.WriteLine(game.CurrentPlayer.BoardZone.FullPrint());

			return health;
		}

		public static int OneRoundMinion(string MinionName1, Card minion2, Card minion3)
		{
			var game = new Game(new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.MAGE,
				Player1Deck = new List<Card>()
				{
					Cards.FromName(MinionName1)
				},
				Player2HeroClass = CardClass.MAGE,

				Player2Deck = new List<Card>()
				{
					minion2,
					minion3,
				},
				Shuffle = false,
				FillDecks = false,
				FillDecksPredictably = false
			});

			game.Player1.BaseMana = 10;
			game.Player2.BaseMana = 10;
			game.StartGame();


			//AI player
			var MinionAI = (ICharacter)Generic.DrawCard(game.CurrentPlayer, Cards.FromName(MinionName1));
			game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionAI));
			game.Process(EndTurnTask.Any(game.CurrentPlayer));

			//Test case
			var MinionTest2 = (ICharacter)Generic.DrawCard(game.CurrentPlayer, minion2);
			game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionTest2));
			var MinionTest3 = (ICharacter)Generic.DrawCard(game.CurrentPlayer, minion3);
			game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionTest3));


			//ShowLog(game, LogLevel.VERBOSE);

			int health = game.CurrentOpponent.BoardZone.Health();

			//Console.WriteLine(cost1 + ", " + cost2);
			//Console.WriteLine(game.CurrentOpponent.BoardZone.FullPrint());
			//Console.WriteLine(game.CurrentPlayer.BoardZone.FullPrint());

			return health;
		}


		public static int OneRoundSpell(string MinionName1, Card spell1, Card spell2)
		{
			var game = new Game(new GameConfig
			{
				StartPlayer = 1,
				Player1HeroClass = CardClass.MAGE,
				Player1Deck = new List<Card>()
				{
					Cards.FromName(MinionName1)

				},
				Player2HeroClass = CardClass.MAGE,

				Player2Deck = new List<Card>()
				{
					spell1,
					spell2,
				},
				Shuffle = false,
				FillDecks = false,
				FillDecksPredictably = false
			});

			game.Player1.BaseMana = 10;
			game.Player2.BaseMana = 10;
			game.StartGame();


			//AI player
			var MinionAI = (ICharacter)Generic.DrawCard(game.CurrentPlayer, Cards.FromName(MinionName1));
			game.Process(PlayCardTask.Minion(game.CurrentPlayer, MinionAI));
			game.Process(EndTurnTask.Any(game.CurrentPlayer));

			//Test case

			IPlayable SpellTest1 = Generic.DrawCard(game.CurrentPlayer, spell1);
			game.Process(PlayCardTask.SpellTarget(game.CurrentPlayer, SpellTest1, MinionAI));
			IPlayable SpellTest2 = Generic.DrawCard(game.CurrentPlayer, spell2);
			game.Process(PlayCardTask.SpellTarget(game.CurrentPlayer, SpellTest2, MinionAI));

			//ShowLog(game, LogLevel.VERBOSE);

			int health = game.CurrentOpponent.BoardZone.Health();

			//Console.WriteLine(cost1 + ", " + cost2);
			//Console.WriteLine(game.CurrentOpponent.BoardZone.FullPrint());
			//Console.WriteLine(game.CurrentPlayer.BoardZone.FullPrint());

			return health;
		}


		public static int Test(Card[] Combo)
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
			foreach (Card card in Combo) //cards mana cost
			{
				if (card == null) //null card return -1
					return -1;
				TotalScore += card.Cost;
			}
			for (int j = 0; j < MinionsToAttack.Count; j++)
			{
				//Console.WriteLine(card1.Type + " " + card2.Type);
				Card card1 = Combo[0], card2 = Combo[1];
				string type1 = card1.Type.ToString(), type2 = card2.Type.ToString();
				if (type1 == "MINION" && type2 != "MINION")
					TotalScore += OneRoundMinionSpell(MinionsToAttack[j], card1, card2); //health
				else if (type2 == "MINION" && type1 != "MINION")
					TotalScore += OneRoundMinionSpell(MinionsToAttack[j], card2, card1); //health
				else if (type1 == "MINION" && type2 == "MINION")
					TotalScore = OneRoundMinion(MinionsToAttack[j], card1, card2); //health
				else if (type1 == "SPELL" && type2 == "SPELL")
					TotalScore = OneRoundSpell(MinionsToAttack[j], card1, card2); //health
			}
			return TotalScore;

		}
	}
}
