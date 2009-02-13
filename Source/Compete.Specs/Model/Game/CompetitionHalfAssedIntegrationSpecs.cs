using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compete.Model.Game;
using Compete.SampleGame;
using Machine.Specifications;

namespace Compete.Specs.Model.Game
{
  [Subject("Competition")]
  public class when_running_a_competition_wioth_one_horribly_bad_player : CompetitionSpec
  {
    Establish context = () =>
    {
      competition = new Competition(new GuessANumberBelowTenOrSomethingBitch());
      unstupidPlayer = new GuessingPlayer(5);
      competition.AddPlayer(unstupidPlayer);
      competition.AddPlayer(new GuessingPlayer(20));

    };

    Because of = () => competition.PlayRound();

    It should_declare_less_bad_player_the_winner = () => competition.Leader.ShouldEqual(unstupidPlayer);

    static Competition competition;
    static GuessingPlayer unstupidPlayer;
  }

  class GuessingPlayer : IPlayer
  {
    int _guessAlways;

    public GuessingPlayer(int guessAlways)
    {
      _guessAlways = guessAlways;
    }

    public GameDecision MakeDecision(GameState state)
    {
      return new MyBestGuess(_guessAlways);
    }
  }

  public class CompetitionSpec
  {
  }
}
