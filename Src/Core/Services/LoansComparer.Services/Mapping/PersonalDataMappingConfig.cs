using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.Enums;
using LoansComparer.Domain.Entities;
using Mapster;

namespace LoansComparer.Services.Mapping
{
    public class PersonalDataMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PersonalDataDTO, PersonalData>()
                .Map(dest => dest.JobType, src => (JobType)src.JobDetails.JobType.Id)
                .Map(dest => dest.GovernmentIdType, src => (GovernmentIdType)src.GovernmentDocument.GovernmentIdType.Id)
                .Map(dest => dest.JobStartDate, src => src.JobDetails.JobStartDate)
                .Map(dest => dest.JobEndDate, src => src.JobDetails.JobEndDate)
                .Map(dest => dest.GovernmentId, src => src.GovernmentDocument.GovernmentId);

            config.NewConfig<PersonalData, PersonalDataDTO>()
                .Map(dest => dest.JobDetails, src => GetJobDetails(src))
                .Map(dest => dest.GovernmentDocument, src => GetGovernmentDocument(src));
        }

        private static JobDetailsDTO GetJobDetails(PersonalData src)
            => new()
            {
                JobType = src.JobType.ToDictionary(),
                JobStartDate = src.JobStartDate,
                JobEndDate = src.JobEndDate,
            };

        private static GovernmentDocumentDTO GetGovernmentDocument(PersonalData src)
            => new()
            {
                GovernmentIdType = src.GovernmentIdType.ToDictionary(),
                GovernmentId = src.GovernmentId,
            };
    }
}
