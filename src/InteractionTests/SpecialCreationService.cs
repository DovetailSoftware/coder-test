using System;
using FubuCore;

namespace InteractionTests
{
    public class CreationResult
    {
        public bool Success { get; set; }
    }

    public class SpecialCreationService
    {
        private readonly IInitializationService _initializationService;
        private readonly ILoggingService _loggingService;
        private readonly IPersistenceService _persistenceService;

        public SpecialCreationService(IInitializationService initializationService, ILoggingService loggingService, IPersistenceService persistenceService)
        {
            _initializationService = initializationService;
            _loggingService = loggingService;
            _persistenceService = persistenceService;
        }

        public CreationResult Create(InteractionDomainEntity entityToCreate)
        {
            entityToCreate = _initializationService.Initialize(entityToCreate);
            _loggingService.LogDebug("Initialization Complete for: {0}".ToFormat(entityToCreate.Name));
            try
            {
                _persistenceService.Save(entityToCreate);
            }
            catch (Exception ex)
            {
                _loggingService.LogError("Error saving entity", ex);
                return new CreationResult(){Success = false};
            }
            return new CreationResult(){Success = true};
        }
    }
}