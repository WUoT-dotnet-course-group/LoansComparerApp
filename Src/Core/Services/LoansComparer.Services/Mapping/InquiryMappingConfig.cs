using LoansComparer.CrossCutting.DTO;
using LoansComparer.Domain.Entities;
using Mapster;

namespace LoansComparer.Services.Mapping
{
    public class InquiryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Inquiry, GetInquiryDTO>()
                .Map(dest => dest.ChosenBank, src => GetBankName(src));
        }

        private string GetBankName(Inquiry inquiry) => inquiry.Bank switch
        {
            null => "-",
            _ => inquiry.Bank.Name,
        };
    }
}
