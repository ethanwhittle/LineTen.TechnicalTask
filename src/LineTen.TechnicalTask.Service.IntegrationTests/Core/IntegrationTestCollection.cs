namespace LineTen.TechnicalTask.Service.IntegrationTests.Core
{
    [CollectionDefinition(nameof(IntegrationTestCollection))]
    public class IntegrationTestCollection : ICollectionFixture<TechnicalTaskWebApplicationFactory>
    {
    }
}