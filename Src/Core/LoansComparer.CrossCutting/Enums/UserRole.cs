using System.ComponentModel;

namespace LoansComparer.CrossCutting.Enums
{
    public enum UserRole
    {
        [Description("Debtor")]
        Debtor = 0,

        [Description("Bank employee")]
        BankEmployee = 1
    }
}
