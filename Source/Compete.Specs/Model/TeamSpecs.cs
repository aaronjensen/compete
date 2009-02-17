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
    protected static IEnumerable<TeamMember> teamMembers = new TeamMember[0];
  }

  public class when_trying_to_create_a_team_with_a_name_that_is_longer_than_sixteen_characters : TeamSpecs
  {
    Establish context = () =>
      teamName = "more than sixteen characters";

    Because of = () =>
      result = Catch.Exception(()=> new Team(teamName, longTeamName, teamMembers));

    It should_not_allow_the_team_to_be_created = () =>
      result.GetType().ShouldEqual(typeof(ArgumentException));
  }

  public class when_trying_to_create_a_team_which_contains_anything_besides_numbers_letters_or_underscores : TeamSpecs
  {
    Establish context = () =>
      teamName = "_%abc 123";

    Because of = () =>
      result = Catch.Exception(() => new Team(teamName, longTeamName, teamMembers));

    It should_not_allow_the_team_to_be_created = () =>
      result.GetType().ShouldEqual(typeof(ArgumentException));
  }

  public class when_trying_to_create_a_team_with_an_empty_name : TeamSpecs
  {
    Establish context = () =>
      teamName = "";

    Because of = () =>
      result = Catch.Exception(() => new Team(teamName, longTeamName, teamMembers));

    It should_not_allow_the_team_to_be_created = () =>
      result.GetType().ShouldEqual(typeof(ArgumentException));
  }
}
