

using Banking.Domain;

namespace Banking.UnitTests;

public class MakingDeposits
{
    [Fact]
    public void DepositsIncreaseBalance()
    {
        var account = new BankAccount(new Mock<ICalculateBonuses>().Object, new Mock<INotifyTheFed>().Object);
        var openingBalance = account.GetBalance();
        var amountToDeposit = 100M;

        account.Deposit(amountToDeposit);

        Assert.Equal(openingBalance + amountToDeposit, account.GetBalance());
    }
}
