using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoansComparer.Domain.Entities
{
    public class DebtorInquiry : Inquiry
    {
        public Guid ChosenBankId { get; set; }
        public Guid ChosenBankInquiryId { get; set; }
    }
}
