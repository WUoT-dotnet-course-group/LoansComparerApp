using LoansComparer.CrossCutting.DTO;
using LoansComparer.Domain.Entities;
using Mapster;
using System.Linq.Expressions;

namespace LoansComparer.Services.Mapping
{
    public class InquiryMappingConfig : IRegister
    {
        private string GetBankName(Inquiry inquiry) => inquiry.Bank switch
        {
            null => string.Empty,
            _ => inquiry.Bank.Name,
        };

        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Inquiry, GetInquiryDTO>()
                .Map(dest => dest.ChosenBank, src => GetBankName(src));
        }
    }
}
