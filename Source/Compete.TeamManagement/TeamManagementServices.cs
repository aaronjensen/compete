using Compete.TeamManagement;
using Machine.Container;
using Machine.Container.Plugins;

namespace Compete.TeamManagement
{
  public class TeamManagementServices : IServiceCollection
  {
    public void RegisterServices(ContainerRegisterer register)
    {
      register.Type<ITeamManagementCommands>().ImplementedBy<TeamManagementCommands>();
      register.Type<ITeamManagementQueries>().ImplementedBy<TeamManagementQueries>();
      register.Type<INewTeamParamsValidator>().ImplementedBy<NewTeamParamsValidator>();
    }
  }
}