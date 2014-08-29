namespace InteractionTests
{
    public interface IPersistenceService
    {
        void Save<T>(T entityToSave) where T : InteractionDomainEntity;
    }
}