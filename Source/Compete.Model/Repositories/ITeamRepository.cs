using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compete.Model.Repositories
{
  public interface ITeamRepository
  {
    Team FindById(Guid id);
    Team FindByTeamName(string teamName);
    void Add(Team team);
    int Count();
    IEnumerable<Team> GetAllTeams();
  }
}
