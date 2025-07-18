namespace Application.UseCase.SalesPersonUseCase.DeleteSalesPerson
{
    public class DeleteSalesPersonErrorHandler
    {
        public static string GetMessage(DeleteSalesPersonResult result)
        {
            return result switch
            {
                DeleteSalesPersonResult.Success => "Usuario exclu�do com sucesso.",
                DeleteSalesPersonResult.NotFound => "Usuario n�o encontrado.",
                DeleteSalesPersonResult.UnexpectedError => "Erro inesperado ao excluir o usuario.",
                _ => "Erro desconhecido."
            };
        }
    }
}