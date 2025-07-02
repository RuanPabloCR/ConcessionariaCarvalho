public class DeleteGuestErrorHandler
{
    public static string GetMessage(DeleteGuestResult result)
    {
        return result switch
        {
            DeleteGuestResult.Success => "Usuario excluído com sucesso.",
            DeleteGuestResult.NotFound => "Usuario não encontrado.",
            DeleteGuestResult.UnexpectedError => "Erro inesperado ao excluir o usuario.",
            _ => "Erro desconhecido."
        };
    }
}