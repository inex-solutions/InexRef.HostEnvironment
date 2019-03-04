using InexRef.HostEnvironment.Hosting;
using InexRef.HostEnvironment.Tests.Greeting;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace InexRef.HostEnvironment.Tests
{

    public class SimpleTests
    {
        private readonly ITestOutputHelper _testOutputHelper;
        private ServiceProvider Container { get; }

        public SimpleTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            var serviceCollection = new ServiceCollection();
            HostedEnvironmentFlavour.ConfigureContainerForHostEnvironmentFlavour(serviceCollection, "Hello");
            Container = serviceCollection.BuildServiceProvider();
        }

        [Fact]
        public void SimpleTest1()
        {

            foreach (var flavour in HostedEnvironmentFlavour.AvailableFlavours)
            {
                _testOutputHelper.WriteLine($"Flavour: {flavour}");
            }

            var greeting = Container.GetService<IGreeting>();
            _testOutputHelper.WriteLine($"Greeting: {greeting.Greet()}");
        }
    }
}
