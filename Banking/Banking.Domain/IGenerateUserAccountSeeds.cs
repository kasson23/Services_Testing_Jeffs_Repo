namespace Banking.Domain;

public interface IGenerateUserAccountSeeds
{
    int GetSeedFor(int age);
}