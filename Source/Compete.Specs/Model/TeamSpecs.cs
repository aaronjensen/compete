using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compete.Model;
using Machine.Specifications;

namespace Compete.Specs.Model
{
  public class TeamSpecs
  {
    protected static Exception result;
    protected static Team team;
    protected static string teamName;
    protected static string longTeamName = "The Who Cares";
    protected static string password = "password1";
    protected static IEnumerable<TeamMember> teamMembers = new TeamMember[0];
  }

  public class when_trying_to_create_a_team_with_a_name_that_is_longer_than_sixteen_characters : TeamSpecs
  {
    Establish context = () =>
      teamName = "more than sixteen characters";

    Because of = () =>
      result = Catch.Exception(()=> new Team(teamName, longTeamName, teamMembers, password));

    It should_not_allow_the_team_to_be_created = () =>
      result.GetType().ShouldEqual(typeof(ArgumentException));
  }

  public class when_trying_to_create_a_team_which_contains_anything_besides_numbers_letters_or_underscores : TeamSpecs
  {
    Establish context = () =>
      teamName = "_%abc 123";

    Because of = () =>
      result = Catch.Exception(() => new Team(teamName, longTeamName, teamMembers, password));

    It should_not_allow_the_team_to_be_created = () =>
      result.GetType().ShouldEqual(typeof(ArgumentException));
  }

  public class when_trying_to_create_a_team_with_an_empty_name : TeamSpecs
  {
    Establish context = () =>
      teamName = "";

    Because of = () =>
      result = Catch.Exception(() => new Team(teamName, longTeamName, teamMembers, password));

    It should_not_allow_the_team_to_be_created = () =>
      result.GetType().ShouldEqual(typeof(ArgumentException));
  }

  public class when_adding_a_standing_to_a_team_with_a_previous_standing : TeamSpecs
  {
    Establish context = () =>
      CreateTeamWithAPreviousStanding();

    Because of = () =>
      team.AddStanding(1,1,0,0);

    It should_add_that_standing_to_the_list_of_standings_for_a_total_of_two_standings = () =>
      team.Standings.Count().ShouldEqual(2);

    It should_make_the_new_standings_last_change_equal_its_current_rank_minus_the_previous_standings_rank = () =>
      team.Standings.Last().LastChange.ShouldEqual(-2);

    static void CreateTeamWithAPreviousStanding()
    {
      team = new Team("foobar", "", new List<TeamMember>(), "");
      team.AddStanding(3,1,1,0);
    }
  }

  public class when_adding_a_standing_to_a_team_without_a_previous_standing : TeamSpecs
  {
    Establish context = () =>
      CreateTeamWithoutAPreviousStanding();

    Because of = () =>
      team.AddStanding(1,1,0,0);

    It should_add_that_standing_to_the_list_of_standings = () =>
      team.Standings.Count().ShouldEqual(1);

    It should_make_the_new_standings_last_change_equal_to_zero = () =>
      team.Standings.Last().LastChange.ShouldEqual(0);

    static void CreateTeamWithoutAPreviousStanding()
    {
      team = new Team("foobar", "", new List<TeamMember>(), "");
    }
  }
}
