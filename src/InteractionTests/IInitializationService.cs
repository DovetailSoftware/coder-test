namespace InteractionTests
{
    public interface IInitializationService
    {
        T Initialize<T>(T entityToInitialize) where T : InteractionDomainEntity;
    }
}