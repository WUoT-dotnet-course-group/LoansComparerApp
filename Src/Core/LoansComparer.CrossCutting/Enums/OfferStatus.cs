using System.ComponentModel;

namespace LoansComparer.CrossCutting.Enums
{
    public enum OfferStatus
    {
        [Description("Unknown")]
        Unknown = 0,
        [Description("Uncompleted")]
        Uncompleted = 1,
        [Description("Pending")]
        Pending = 2,
        [Description("Accepted")]
        Accepted = 3,
        [Description("Declined")]
        Declined = 4,
    }
}
