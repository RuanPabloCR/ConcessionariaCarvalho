namespace Application.RepositoriesInterface
{
    public interface IDeleteSalesPersonRepository
    {
        Task DeleteAsync(Guid guestId);
    }
}
