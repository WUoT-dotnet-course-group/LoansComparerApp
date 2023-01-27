using System.ComponentModel;

namespace LoansComparer.CrossCutting.Enums
{
    public enum JobType
    {
        [Description("Director")]
        Director = 30,
        [Description("Agent")]
        Agent = 37,
        [Description("Administrator")]
        Administrator = 44,
        [Description("Coordinator")]
        Coordinator = 47,
        [Description("Specialist")]
        Specialist = 60,
        [Description("Orchestrator")]
        Orchestrator = 62,
        [Description("Assistant")]
        Assistant = 64,
        [Description("Designer")]
        Designer = 69,
        [Description("Facilitator")]
        Facilitator = 72,
        [Description("Analyst")]
        Analyst = 79,
        [Description("Producer")]
        Producer = 80,
        [Description("Technician")]
        Technician = 81,
        [Description("Manager")]
        Manager = 84,
        [Description("Liaison")]
        Liaison = 87,
        [Description("Associate")]
        Associate = 88,
        [Description("Consultant")]
        Consultant = 89,
        [Description("Engineer")]
        Engineer = 92,
        [Description("Strategist")]
        Strategist = 93,
        [Description("Supervisor")]
        Supervisor = 94,
        [Description("Executive")]
        Executive = 95,
        [Description("Planner")]
        Planner = 96,
        [Description("Developer")]
        Developer = 97,
        [Description("Officer")]
        Officer = 98,
        [Description("Architect")]
        Architect = 99,
        [Description("Representative")]
        Representative = 100,
    }
}
