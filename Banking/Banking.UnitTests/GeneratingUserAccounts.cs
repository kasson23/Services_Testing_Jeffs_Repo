
using Moq.Protected;

namespace Banking.UnitTests;

public class GeneratingUserAccounts
{

    [Theory]
    [InlineData("Bob", "Smith", 38, "bob-smith-18")]
    [InlineData("Jill", "Jones", 18, "jill-jones-18")]
    public void CanGenerateUserAccounts(string first, string last, int age, string expected)
    {
        var rng = new Mock<IGenerateUserAccountSeeds>();
        rng.Setup(r => r.GetSeedFor(It.IsAny<int>())).Returns(18);
        var generator = new UserAccountGenerator(rng.Object);

        var result = generator.CreateUserAccount(first, last, age);

        Assert.Equal(expected, result);

        // var a2 = new UserAccountGenerator();
    }



}

//public class ScaffoldedUserAccountGenerator : UserAccountGenerator
//{
//    protected override int GetRandomKey(int age)
//    {
//        return 18;
//    }
//}