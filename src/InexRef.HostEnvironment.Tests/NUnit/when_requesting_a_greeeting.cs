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

using InexRef.HostEnvironment.Hosting;
using InexRef.HostEnvironment.TestEnvironment.NUnit.SpecificationFramework;
using InexRef.HostEnvironment.Tests.Greeting;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace InexRef.HostEnvironment.Tests.NUnit
{
    public class when_requesting_a_greeeting : HostedFlavourTestBase
    {
        private IGreeting _greetingService;
        private string _greeting;

        public when_requesting_a_greeeting(HostedEnvironmentFlavour hostingFlavour) : base(hostingFlavour)
        {
        }

        protected override void Given() => _greetingService = Container.GetRequiredService<IGreeting>();

        protected override void When() => _greeting = _greetingService.Greet();

        [Then]
        public void the_greeting_should_match_the_host_flavour() => _greeting.ShouldBe(HostingFlavour.Name);
    }
}