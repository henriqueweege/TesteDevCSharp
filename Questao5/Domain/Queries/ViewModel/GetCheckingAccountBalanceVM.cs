using Domain.Models.Contracts;

namespace Domain.Queries.ViewModel;

public class GetCheckingAccountBalanceVM : IModel
{
    public double Balance { get; private set; }
    public int AccountNumber { get; private set; }
    public string HolderName { get; private set; }
	public DateTime Date { get; private set; }
	public GetCheckingAccountBalanceVM(double balance, int number, string holderName)
	{
		AccountNumber=number;
		HolderName=holderName;
		Balance= balance;
		Date = DateTime.Now.Date;
	}
}
