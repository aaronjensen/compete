using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Compete.TeamManagement;
using Machine.Specifications;
using Rhino.Mocks;

namespace Compete.Specs.Services
{
  public class NewTeamParamsValidatorSpecs
  {
    protected static INewTeamParamsValidator validator;
    protected static ITeamManagementQueries queries;

    Establish context = () =>
    {
      queries = MockRepository.GenerateMock<ITeamManagementQueries>();
      validator = new NewTeamParamsValidator(queries);
    };
    
    protected static bool result;
  }

  public class when_validating_two_passwords_that_do_not_match : NewTeamParamsValidatorSpecs
  {
    Because of = () =>
      result = validator.PasswordsMatch(password, passwordAgain);

    It should_fail = () =>
      result.ShouldBeFalse();

    protected static string password = "fail";
    protected static string passwordAgain = "fial";
  }

  public class when_validating_two_passwords_that_match : NewTeamParamsValidatorSpecs
  {
    Because of = () =>
      result = validator.PasswordsMatch(password, passwordAgain);

    It should_succeed = () =>
      result.ShouldBeTrue();

    protected static string password = "success";
    protected static string passwordAgain = "success";
  }

  public class when_validating_a_team_name_that_is_empty : NewTeamParamsValidatorSpecs
  {
    Because of = () =>
      result = validator.TeamNameIsValid(teamName);

    It should_fail = () =>
      result.ShouldBeFalse();

    protected static string teamName = string.Empty;
  }

  public class when_validating_a_team_name_that_contains_only_whitespace : NewTeamParamsValidatorSpecs
  {
    Because of = () =>
      result = validator.TeamNameIsValid(teamName);

    It should_fail = () =>
      result.ShouldBeFalse();

    protected static string teamName = "  ";
  }

  public class when_validating_a_team_name_that_is_more_than_16_characters : NewTeamParamsValidatorSpecs
  {
    Because of = () =>
      result = validator.TeamNameIsValid(teamName);

    It should_fail = () =>
      result.ShouldBeFalse();

    protected static string teamName = "obviouslymorethansixteencharacters";
  }

  public class when_validating_a_team_name_that_contains_characters_besides_letters_numbers_or_underscores : NewTeamParamsValidatorSpecs
  {
    Because of = () =>
      result = validator.TeamNameIsValid(teamName);

    It should_fail = () =>
      result.ShouldBeFalse();

    protected static string teamName = "ho ~!@#!";
  }

  public class when_validating_a_team_name_that_is_already_taken : NewTeamParamsValidatorSpecs
  {
    Establish context = () =>
      SetTeamNameToBeTaken(teamName);

    Because of = () =>
      result = validator.TeamNameIsAvailable(teamName);

    It should_fail = () =>
      result.ShouldBeFalse();

    protected static string teamName = "taken";

    private static void SetTeamNameToBeTaken(string name)
    {
      queries.Stub(x => x.TeamNameIsAvailable(name)).Return(false);
    }
  }

  public class when_validating_a_team_name_that_is_not_taken : NewTeamParamsValidatorSpecs
  {
    Establish context = () =>
      SetTeamNameToBeAvailable(teamName);

    Because of = () =>
      result = validator.TeamNameIsAvailable(teamName);

    It should_succeed = () =>
      result.ShouldBeTrue();

    protected static string teamName = "nottaken";

    private static void SetTeamNameToBeAvailable(string name)
    {
      queries.Stub(x => x.TeamNameIsAvailable(name)).Return(true);
    }
  }
}
