public class DeleteGuestErrorHandler
{
    public static string GetMessage(DeleteGuestResult result)
    {
        return result switch
        {
            DeleteGuestResult.Success => "Usuario exclu�do com sucesso.",
            DeleteGuestResult.NotFound => "Usuario n�o encontrado.",
            DeleteGuestResult.UnexpectedError => "Erro inesperado ao excluir o usuario.",
            _ => "Erro desconhecido."
        };
    }
}