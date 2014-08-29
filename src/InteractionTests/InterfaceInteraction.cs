using System;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace InteractionTests
{
    [TestFixture]
    public class InterfaceInteraction : InteractionContext<SpecialCreationService>
    {
        private InteractionDomainEntity _input;

        protected override void beforeEach()
        {
            var newGuid = Guid.NewGuid();
            _input = new InteractionDomainEntity() { Id = newGuid, Name = "test" };
            MockFor<IInitializationService>().Stub(x => x.Initialize(_input)).Return(_input);
        }
                                
        [Test]
        public void ShouldCallInitializeWithTheGivenEntity()
        {            
            ClassUnderTest.Create(_input);
            MockFor<IInitializationService>().AssertWasCalled(x=> x.Initialize(_input));
        }
    }
}
