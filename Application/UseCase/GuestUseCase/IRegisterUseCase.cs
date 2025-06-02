namespace Application.UseCase.GuestUseCase
{
    public interface IRegisterUseCase
    {
        Task<bool> RegisterUserAsync(string email, string password);
    }
}