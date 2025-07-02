using Application.RepositoriesInterface;
using System;
using System.Threading.Tasks;

namespace Application.UseCase.GuestUseCase.DeleteGuest
{
    public class DeleteGuestUseCase : IDeleteGuestUseCase
    {
        private readonly IDeleteGuestRepository _deleteGuestRepository;
        public DeleteGuestUseCase(IDeleteGuestRepository deleteGuestRepository)
        {
            _deleteGuestRepository = deleteGuestRepository;
        }
        public async Task<DeleteGuestResult> DeleteGuestAsync(Guid guestId)
        {
            try
            {
                await _deleteGuestRepository.DeleteAsync(guestId);
                return DeleteGuestResult.Success;
            }
            catch (KeyNotFoundException)
            {
                return DeleteGuestResult.NotFound;
            }
            catch (Exception)
            {
                return DeleteGuestResult.UnexpectedError;
            }
        }
    }
}
