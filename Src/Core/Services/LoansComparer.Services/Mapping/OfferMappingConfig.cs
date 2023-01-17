using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;
using Mapster;

namespace LoansComparer.Services.Mapping
{
    public class OfferMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<GetOfferDetailsResponse, OfferDTO>()
                .Map(dest => dest.Id, src => src.OfferID)
                .Map(dest => dest.LoanPeriod, src => src.NumberOfInstallments)
                .Map(dest => dest.InquiryId, src => src.InquiryID)
                .Map(dest => dest.CreateDate, src => src.OfferCreateDate)
                .Map(dest => dest.UpdateDate, src => src.OfferUpdateDate)
                .Map(dest => dest.CreateDate, src => src.OfferCreateDate);
        }
    }
}
