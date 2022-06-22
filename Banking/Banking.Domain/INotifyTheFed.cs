namespace Banking.Domain
{
    public interface INotifyTheFed
    {
        void AccountWithdraw(BankAccount bankAccount, decimal amountToWithdraw);
    }
}