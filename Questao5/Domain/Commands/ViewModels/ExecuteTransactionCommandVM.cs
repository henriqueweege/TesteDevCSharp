using Domain.Models.Contracts;

namespace Domain.Commands.ViewModels;

public class ExecuteTransactionCommandVM : IModel
{
    public Guid IdTransaction { get; private set; }
	public ExecuteTransactionCommandVM(Guid idTransaction)
	{
		IdTransaction = idTransaction;
	}
}
