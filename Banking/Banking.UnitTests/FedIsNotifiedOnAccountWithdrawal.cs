
namespace Banking.UnitTests;

public class FedIsNotifiedOnAccountWithdrawal
{
    [Fact]
    public void FedNotified()
    {
        // Given
        var mockedFed = new Mock<INotifyTheFed>();
        var account = new BankAccount(new Mock<ICalculateBonuses>().Object, mockedFed.Object);
        var amountToWithdraw = 1000M;
        // When
        account.Withdraw(amountToWithdraw);
        // Then ??
        mockedFed.Verify(f => f.AccountWithdraw(account, amountToWithdraw), Times.Once);
        
    }

    [Fact]
    public void FedIsNotNotifiedForWithdrawalsUnder100()
    {
        // Given
        var mockedFed = new Mock<INotifyTheFed>();
        var account = new BankAccount(new Mock<ICalculateBonuses>().Object, mockedFed.Object);
        var amountToWithdraw = 99.99M;
        // When
        account.Withdraw(amountToWithdraw);
        // Then ??
        mockedFed.Verify(f => f.AccountWithdraw(account, amountToWithdraw), Times.Never);

    }
}
