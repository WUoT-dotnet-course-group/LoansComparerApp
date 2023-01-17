﻿using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.DTO.LoaningBank;

namespace LoansComparer.Services.Abstract
{
    public interface ILoaningService
    {
        Task<BaseResponse<CreateInquiryResponse>> Inquire(CreateInquiryDTO inquiryData);
        Task<BaseResponse<GetInquiryResponse>> GetInquiry(Guid inquiryId);
        Task<BaseResponse<GetOfferResponse>> GetOfferById(Guid offerId);
        Task<BaseResponse<OfferDTO>> GetOfferByInquiryId(Guid inquiryId);
        Task<BaseResponse> UploadFile(Guid offerId, Stream fileStream, string filename);
        Task<BaseResponse<PaginatedResponse<OfferDTO>>> GetBankOffers(PagingParameter pagingParams);
    }
}
