

using Banking.Domain;

namespace Banking.UnitTests;

public class MakingWithdrawals
{
    private readonly BankAccount _account;
       
    private readonly decimal _openingBalance;

    public MakingWithdrawals()
    {
        _account = new BankAccount(new Mock<ICalculateBonuses>().Object, new Mock<INotifyTheFed>().Object);
        _openingBalance = _account.GetBalance();
    }

    [Fact]
    public void MakingAWithdrawalReducesBalance()
    {
        // Given
       
        decimal amountToWithdraw = 100M;
        // When
        _account.Withdraw(amountToWithdraw);
        // Then
        Assert.Equal(_openingBalance - 100M, _account.GetBalance());
    }

    [Fact]
    public void OverdraftDoesNotDecreaseBalance()
    {
       
        var amountToWithdraw = _openingBalance + 1;

        try
        {
            _account.Withdraw(amountToWithdraw);
        }
        catch (Exception)
        {

            // swallow the exception
        }

        Assert.Equal(_openingBalance, _account.GetBalance());
    }

    [Fact]
    public void OverdraftExceptionThrownOnOverdraft()
    {
       

        Assert.Throws<OverdraftException>(() =>_account.Withdraw(_account.GetBalance() + .01M));

    }

    [Fact]
    public void CustomerCanTakeEntireBalance()
    {
       
        var amountToWithdraw = _openingBalance;

        _account.Withdraw(amountToWithdraw);

        Assert.Equal(0, _account.GetBalance());
    }
}
