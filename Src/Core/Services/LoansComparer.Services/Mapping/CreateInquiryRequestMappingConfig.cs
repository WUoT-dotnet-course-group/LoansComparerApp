using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;
using Mapster;
namespace LoansComparer.Services.Mapping
{
    public class CreateInquiryRequestMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AddInquiryDTO, CreateInquiryRequest>()
                .Map(dest => dest.PersonalData, src => new PersonalData()
                {
                    FirstName = src.PersonalData.FirstName,
                    LastName = src.PersonalData.LastName,
                    BirthDate = src.PersonalData.BirthDate,
                })
                .Map(dest => dest.GovernmentDocument, src => new GovernmentDocument()
                {
                    Id = src.PersonalData.GovernmentDocument.GovernmentIdType.Id,
                    Name = src.PersonalData.GovernmentDocument.GovernmentIdType.Name,
                    Number = src.PersonalData.GovernmentDocument.GovernmentId,
                })
                .Map(dest => dest.JobDetails, src => new JobDetails()
                {
                    Id = src.PersonalData.JobDetails.JobType.Id,
                    Name = src.PersonalData.JobDetails.JobType.Name,
                    JobStartDate = src.PersonalData.JobDetails.JobStartDate,
                    JobEndDate = src.PersonalData.JobDetails.JobEndDate,
                });
        }
    }
}
