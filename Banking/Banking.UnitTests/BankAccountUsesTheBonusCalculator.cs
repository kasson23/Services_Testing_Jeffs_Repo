

namespace Banking.UnitTests;
// interaction test.
public class BankAccountUsesTheBonusCalculator
{

    [Fact]
    public void BonusIsAppliedToTheBalance()
    {
        // Given
        var stubbedBonusCalculator = new Mock<ICalculateBonuses>();
        var account = new BankAccount(stubbedBonusCalculator.Object, new Mock<INotifyTheFed>().Object); // Need a stub here
        var openingBalance = account.GetBalance();
        var amountOfDeposit = 100M;
        var amountOfBonus = 18M;
        stubbedBonusCalculator.Setup(b => b.GetBonusForDeposit(account, amountOfDeposit)).Returns(amountOfBonus);

        // When
        account.Deposit(amountOfDeposit);


        // Then "State Based or 'Beckian' Testing"
        Assert.Equal(openingBalance + amountOfDeposit + amountOfBonus, account.GetBalance()); 

    }
}
