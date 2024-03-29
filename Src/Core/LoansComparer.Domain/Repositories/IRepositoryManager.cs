﻿namespace LoansComparer.Domain.Repositories
{
    public interface IRepositoryManager
    {
        IUserRepository UserRepository { get; }

        IInquiryRepository InquiryRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
