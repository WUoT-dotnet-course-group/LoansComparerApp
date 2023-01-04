using System.ComponentModel;

namespace LoansComparer.CrossCutting.Enums
{
    public enum GovernmentIdType
    {
        [Description("Driver License")]
        DriverLicense = 1,
        [Description("Passport")]
        Passport = 2,
        [Description("Government Id")]
        ID = 3,
    }
}
