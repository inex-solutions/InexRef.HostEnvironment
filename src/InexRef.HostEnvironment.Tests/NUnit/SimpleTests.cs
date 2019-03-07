#region Copyright & Licence
// The MIT License (MIT)
// 
// Copyright 2018-2019 INEX Solutions Ltd
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software
// and associated documentation files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using InexRef.HostEnvironment.Hosting;
using InexRef.HostEnvironment.TestEnvironment.NUnit;
using InexRef.HostEnvironment.Tests.Greeting;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using Shouldly;

namespace InexRef.HostEnvironment.Tests.NUnit
{
    [TestFixtureSourceFlavours]
    public class SimpleTests
    {
        private ServiceProvider Container { get; }

        private string HostingFlavour { get; }

        public SimpleTests(string hostingFlavour)
        {
            HostingFlavour = hostingFlavour;
            var serviceCollection = new ServiceCollection();
            HostedEnvironmentFlavour.ConfigureContainerForHostEnvironmentFlavour(serviceCollection, hostingFlavour);
            Container = serviceCollection.BuildServiceProvider();
        }

        [Test]
        public void WriteGreeting()
        {
            foreach (var flavour in HostedEnvironmentFlavour.AvailableFlavours)
            {
                Console.WriteLine($"Flavour: {flavour}");
            }

            var greetingService = Container.GetService<IGreeting>();
            var greeting = greetingService.Greet();
            Console.WriteLine($"Greeting: {greeting}");
            greeting.ShouldBe(HostingFlavour);
        }
    }
}