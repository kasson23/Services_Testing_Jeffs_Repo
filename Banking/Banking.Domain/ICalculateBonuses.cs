namespace Banking.Domain
{
    public interface ICalculateBonuses
    {
        decimal GetBonusForDeposit(BankAccount bankAccount, decimal amountToDeposit);
    }
}