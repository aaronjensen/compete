using System;
using System.Collections.Generic;

namespace Compete.Model.Repositories
{
  public interface ITeamRepository
  {
    Team FindById(Guid id);
    Team FindByTeamName(string teamName);
    void Add(Team team);
    int Count();
    IEnumerable<Team> GetAllTeams();
    IDictionary<string, string> GetTeamNamesMap();
  }
}
