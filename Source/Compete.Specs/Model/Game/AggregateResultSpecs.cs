using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compete.Model.Game;
using Machine.Specifications;

namespace Compete.Specs.Model.Game
{
  public class when_aggregating_results_from_a_single_result_with_a_winner
    : AggregateResultSpecs
  {
    Establish context = () =>
    {
      result = new AggregateResult(new [] { GameResult.WinnerLoser(player1, player2) });
    };

    It should_have_the_same_winner_as_the_single_result = () =>
      result.Winner.ShouldEqual(player1);

    It should_be_not_be_a_tie = () =>
      result.IsTie.ShouldBeFalse();
  }

  public class when_aggregating_results_from_a_single_result_with_a_tie
  {
    It should_be_a_tie;
  }

  public class when_aggregating_results_from_two_results_with_opposing_winners
  {
    It should_be_a_tie;
  }

  public class when_aggregating_results_from_two_results_with_a_clear_winner
  {
    It should_indicate_the_winner;
  }

  public class AggregateResultSpecs
  {
    protected static IPlayer player1;
    protected static IPlayer player2;
    protected static AggregateResult result;

    Establish context = () =>
    {
      player1 = new Player();
      player2 = new Player();
    };
  }

  public class Player : IPlayer
  {
    public GameDecision MakeDecision(GameState state)
    {
      return null;
    }
  }
}
