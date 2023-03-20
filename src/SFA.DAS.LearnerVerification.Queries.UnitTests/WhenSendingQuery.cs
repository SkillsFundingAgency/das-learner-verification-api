using AutoFixture;
using FluentAssertions;
using Moq;
using SFA.DAS.LearnerVerification.Queries.Exceptions;
using SFA.DAS.LearnerVerification.Queries.VerifyLearner;

namespace SFA.DAS.LearnerVerification.Queries.UnitTests
{
    public class WhenSendingQuery
    {
        private Fixture _fixture;
        private Mock<IServiceProvider> _serviceProvider;
        private QueryDispatcher _sut;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _serviceProvider = new Mock<IServiceProvider>();
            _sut = new QueryDispatcher(_serviceProvider.Object);
        }

        [Test]
        public void AndServiceNullThenQueryDispatcherExceptionIsThrown()
        {
            //Arrange
            var query = _fixture.Create<VerifyLearnerQuery>();

            //Act
            Action act = () => { _ = _sut.Send<VerifyLearnerQuery, Queries.VerifyLearner.LearnerVerification>(query); };

            //Assert
            act.Should()
                .Throw<QueryDispatcherException>()
                .WithMessage($"Unable to dispatch query 'VerifyLearnerQuery'. No matching handler found.");
        }
    }
}