
namespace Banking.Domain;

public class RandomUserAccountSeeder : IGenerateUserAccountSeeds
{
    public int GetSeedFor(int age)
    {
        return new Random().Next(1, age);
    }
}
