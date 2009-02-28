using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Compete.Core.Infrastructure;
using Compete.Model;
using Compete.Model.Game;
using Compete.Model.Repositories;
using Compete.Site.Controllers;
using Compete.TeamManagement;
using Machine.Specifications;
using Rhino.Mocks;

namespace Compete.Specs.Controllers
{
  public class MyTeamControllerSpecs
  {
    protected static ITeamRepository teamRepository;
    protected static ILeaderboardRepository leaderboardRepository;
    protected static IFormsAuthentication formsAuthentication;
    protected static IConfigurationRepository configurationRepository;
    protected static ITeamManagementQueries teamManagementQueries;

    protected static void SetUserAsBeingSignedInWithUsername(string name)
    {
      formsAuthentication = MockRepository.GenerateMock<IFormsAuthentication>();
      formsAuthentication.Stub(x => formsAuthentication.IsCurrentlySignedIn).Return(true);
      formsAuthentication.Stub(x => formsAuthentication.SignedInUserName).Return(name);
    }

    protected static void SetupTeamInRepositoryNamed(string name)
    {
      var team = new Team(name, name, new TeamMember[0], name);
      teamRepository = MockRepository.GenerateMock<ITeamRepository>();
      teamRepository.Stub(x => x.FindByTeamName(name)).Return(team);
    }

    protected static void SetupMatchResultsForTeam(string name)
    {
      var leaderboard = new Leaderboard();
      var matchResults = new[] { MatchResult.WinnerAndLoser(name, Guid.NewGuid().ToString(), "Win") };
      leaderboard.Include(matchResults);

      leaderboardRepository = MockRepository.GenerateMock<ILeaderboardRepository>();
      leaderboardRepository.Stub(x => x.GetLeaderboard()).Return(leaderboard);
    }

    protected static void SetupConfigurationToShowAStartingRoundOf(int round)
    {
      configurationRepository = MockRepository.GenerateMock<IConfigurationRepository>();
      var config = new Configuration();
      round = round == 0 ? 1 : round;
      while (config.RoundNumber != round)
      {
        config.AdvanceToNextRound();
      }
      configurationRepository.Stub(x => x.GetConfiguration()).Return(config);
    }

    protected static void SetupControllerDependencies()
    {
      teamManagementQueries = new TeamManagementQueries(teamRepository, leaderboardRepository, formsAuthentication);
    }
  }

  public class when_viewing_the_my_team_page : MyTeamControllerSpecs
  {
    Establish context = () =>
    {
      SetUserAsBeingSignedInWithUsername(teamName);
      SetupTeamInRepositoryNamed(teamName);
      SetupMatchResultsForTeam(teamName);
      SetupMatchResultsForTeam(teamName);
      SetupConfigurationToShowAStartingRoundOf(round);
      SetupControllerDependencies();

      controller = new MyTeamController(configurationRepository, teamManagementQueries);
    };

    Because of = () =>
      controller.Index();

    It should_should_show_my_team_information = () =>
      controller.ViewData["currentTeam"].ShouldEqual(teamName);

    It should_provide_the_current_round = () =>
      ((int)controller.ViewData["currentRound"]).ShouldEqual(1);

    It should_provide_match_results_for_my_team = () =>
      ((IEnumerable<RecentMatch>)controller.ViewData["results"]).First().Result.ShouldEqual("Win");

    static string teamName = "foo";
    static MyTeamController controller;
    static int round = 1;
  }
}
