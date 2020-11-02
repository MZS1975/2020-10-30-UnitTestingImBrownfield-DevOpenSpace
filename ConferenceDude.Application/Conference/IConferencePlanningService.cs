namespace ConferenceDude.Application.Conference
{
    using Session;

    public interface IConferencePlanningService
    {
        ISessionPlanningService SessionPlanner { get; }
    }
}
