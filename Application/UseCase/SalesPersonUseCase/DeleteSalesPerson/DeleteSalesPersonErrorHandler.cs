namespace Application.UseCase.SalesPersonUseCase.DeleteSalesPerson
{
    public class DeleteSalesPersonErrorHandler
    {
        public static string GetMessage(DeleteSalesPersonResult result)
        {
            return result switch
            {
                DeleteSalesPersonResult.Success => "Usuario excluído com sucesso.",
                DeleteSalesPersonResult.NotFound => "Usuario não encontrado.",
                DeleteSalesPersonResult.UnexpectedError => "Erro inesperado ao excluir o usuario.",
                _ => "Erro desconhecido."
            };
        }
    }
}