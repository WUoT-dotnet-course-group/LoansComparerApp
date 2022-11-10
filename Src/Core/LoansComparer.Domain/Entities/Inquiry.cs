using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoansComparer.Domain.Entities
{
    public abstract class Inquiry
    {
        public Guid Id { get; set; }
        public int LoanValue { get; set; }
        public short NumberOfInstallments { get; set; }
        public DateTime InquireDate { get; set; }
        public PersonalData PersonalData { get; set; }
    }
}
