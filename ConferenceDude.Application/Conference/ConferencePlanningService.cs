namespace ConferenceDude.Application.Conference
{
    using Session;

    public class ConferencePlanningService : IConferencePlanningService
    {
        public ConferencePlanningService(ISessionPlanningService sessionPlanner)
        {
            SessionPlanner = sessionPlanner;
        }

        public ISessionPlanningService SessionPlanner { get; }
    }
}
