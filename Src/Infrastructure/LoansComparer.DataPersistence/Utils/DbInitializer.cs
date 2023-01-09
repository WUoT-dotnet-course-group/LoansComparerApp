namespace LoansComparer.DataPersistence.Utils
{
    public static class DbInitializer
    {
        public static void Initialize(RepositoryDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            dbContext.SaveChanges();
        }
    }
}