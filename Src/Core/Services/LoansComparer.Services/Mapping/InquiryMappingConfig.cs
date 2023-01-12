using LoansComparer.CrossCutting.DTO;
using LoansComparer.Domain.Entities;
using Mapster;

namespace LoansComparer.Services.Mapping
{
    public class InquiryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<InquirySearch, GetInquiryDTO>()
                .Map(dest => dest.ChosenBank, src => GetBankName(src));
        }

        private string GetBankName(InquirySearch inquiry) => inquiry.BankName ??  "-";
    }
}
