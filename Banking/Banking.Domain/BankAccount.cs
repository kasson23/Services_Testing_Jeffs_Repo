namespace Banking.Domain;

public class BankAccount
{
    private decimal _currentBalance = 5000M;
    private readonly ICalculateBonuses _bonusCalculator;
    private readonly INotifyTheFed _fedNotifier;

    public BankAccount(ICalculateBonuses bonusCalculator, INotifyTheFed fedNotifier)
    {
        _bonusCalculator = bonusCalculator;
        _fedNotifier = fedNotifier;
    }

    public decimal GetBalance()
    {
        return _currentBalance;
    }

    public void Withdraw(decimal amountToWithdraw)
    {
        if (amountToWithdraw <= _currentBalance)
        {
            _currentBalance = _currentBalance - amountToWithdraw;
            if (amountToWithdraw >= 100M)
            {
                _fedNotifier.AccountWithdraw(this, amountToWithdraw);
            }
           
        } else
        {
            throw new OverdraftException();
        }
    }

    public void Deposit(decimal amountToDeposit)
    {
        // WTCYWYH
        decimal bonus = _bonusCalculator.GetBonusForDeposit(this, amountToDeposit);
        _currentBalance += amountToDeposit + bonus;
    }
}