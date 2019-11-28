using System;
using InexRef.HostEnvironment.Hosting;
using InexRef.HostEnvironment.TestEnvironment.NUnit.Tests.Greeting;
using Microsoft.Extensions.DependencyInjection;

namespace InexRef.HostEnvironment.Tests.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var flavour = HostedEnvironment.FlavoursConfiguration.DefaultFlavour;

            var serviceCollection = new ServiceCollection();
            flavour.ConfigureContainer(serviceCollection);

            var container = serviceCollection.BuildServiceProvider();

            var greeter = container.GetService<IGreeting>();

            Console.WriteLine(greeter.Greet());
        }
    }
}
