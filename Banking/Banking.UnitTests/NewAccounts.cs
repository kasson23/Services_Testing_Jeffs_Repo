


using Banking.Domain;

namespace Banking.UnitTests;

public class NewAccounts
{
    [Fact]
    public void NewAccountsHaveCorrectOpeningBalance()
    {
        // Given
        var account = new BankAccount(new Mock<ICalculateBonuses>().Object, new Mock<INotifyTheFed>().Object);
        // When
        decimal balance = account.GetBalance();
        // Then
        Assert.Equal(5000M, balance);
    }
}

//public class DummyBonusCalculator : ICalculateBonuses
//{
//    public decimal GetBonusForDeposit(BankAccount bankAccount, decimal amountToDeposit)
//    {
//        return default;
//    }
//}