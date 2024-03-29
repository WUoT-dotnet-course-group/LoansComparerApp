﻿using LoansComparer.CrossCutting.Enums;

namespace LoansComparer.CrossCutting.DTO
{
    public class OfferDTO
    {
        public string Id { get; set; } = default!;
        public float Percentage { get; set; }
        public float MonthlyInstallment { get; set; }
        public float LoanValue { get; set; }
        public short LoanPeriod { get; set; }
        public OfferStatus Status { get; set; }
        public string StatusDescription { get; set; } = default!;
        public string InquiryId { get; set; } = default!;
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? ApprovedBy { get; set; }
        public string? DocumentLink { get; set; }
        public DateTime? DocumentLinkValidDate { get; set; }
        public string BankId { get; set; } = default!;
        public string BankName { get; set; } = default!;
    }
}
