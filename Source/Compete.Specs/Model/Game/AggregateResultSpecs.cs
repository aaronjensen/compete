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
      result = new AggregateResult(new [] { GameResult.WinnerAndLoser(player1, player2) });
    };

    It should_have_the_same_winner_as_the_single_result = () =>
      result.Winner.ShouldEqual(player1);

    It should_be_not_be_a_tie = () =>
      result.IsTie.ShouldBeFalse();
  }

  public class when_aggregating_results_from_a_single_result_with_a_tie
    : AggregateResultSpecs
  {
    Establish context = () =>
    {
      result = new AggregateResult(new [] { GameResult.Tie(new [] { player1, player2 }) } );
    };

    It should_be_a_tie = ()=>
      result.IsTie.ShouldBeTrue();
  }

  public class when_aggregating_results_from_two_results_with_opposing_winners
    : AggregateResultSpecs
  {
    Establish context = () =>
    {
      result = new AggregateResult(new []
      {
        GameResult.WinnerAndLoser(player1, player2),
        GameResult.WinnerAndLoser(player2, player1)
      } );
    };

    It should_be_a_tie = ()=>
      result.IsTie.ShouldBeTrue();
  }

  public class when_aggregating_results_from_two_results_with_a_clear_winner
    : AggregateResultSpecs
  {
    Establish context = () =>
    {
      result = new AggregateResult(new []
      {
        GameResult.WinnerAndLoser(player1, player2),
        GameResult.WinnerAndLoser(player1, player2)
      } );
    };

    It should_indicate_the_winner =()=>
      result.Winner.ShouldEqual(player1);

    It should_be_not_be_a_tie = () =>
      result.IsTie.ShouldBeFalse();
  }

  public class AggregateResultSpecs
  {
    protected static BotPlayer player1;
    protected static BotPlayer player2;
    protected static AggregateResult result;

    Establish context = () =>
    {
      player1 = new BotPlayer("A", null);
      player2 = new BotPlayer("B", null);
    };
  }
}
