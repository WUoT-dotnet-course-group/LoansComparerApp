﻿using LoansComparer.CrossCutting.DTO;
using LoansComparer.CrossCutting.Enums;
using LoansComparer.DataPersistence.Utils;
using LoansComparer.Domain.Entities;
using LoansComparer.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LoansComparer.DataPersistence.Repositories
{
    internal sealed class InquiryRepository : IInquiryRepository
    {
        private readonly RepositoryDbContext _dbContext;

        public InquiryRepository(RepositoryDbContext dbContext) => _dbContext = dbContext;

        public async Task<Guid> Add(Inquiry inquiry)
        {
            await _dbContext.Inquiries.AddAsync(inquiry);
            await _dbContext.SaveChangesAsync();
            return inquiry.ID;
        }

        public async Task<PaginatedResponse<Inquiry>> GetByUser<TResult>(Guid userId, int pageIndex, int pageSize, SortOrder sortOrder, string sortHeader)
        {
            var query = _dbContext.Inquiries.Where(x => x.UserID == userId);

            if (sortOrder is not SortOrder.Undefined)
            {
                query = query.Sort<TResult, Inquiry>(sortOrder, sortHeader);
            }

            return await query.Paginate(pageIndex, pageSize);
        }

        public async Task<Inquiry> GetById(Guid id)
        {
            // TODO: do validation in advance
            return await _dbContext.Inquiries.SingleAsync(x => x.ID == id);
        }

        public async Task<int> Count() => await _dbContext.Inquiries.CountAsync();

        public async Task<User> GetDebtorByOffer(string bankId, string offerId)
        {
            var userData = await _dbContext.Inquiries.Select(x => new { x.ChosenOfferId, x.ChosenBankId, x.User })
                .SingleAsync(x => x.ChosenOfferId == offerId && x.ChosenBankId == bankId);

            return userData.User;
        }
    }
}
