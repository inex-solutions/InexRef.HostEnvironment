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
using InexRef.HostEnvironment.Tests.Common;
using InexRef.HostEnvironment.Tests.Common.SpecificationFramework;
using Shouldly;

namespace InexRef.HostEnvironment.Tests
{
    public class when_an_environment_variable_override_is_specified_but_ignoreEnvironmentVariableOverrides_is_also_set : HostedFlavourSpecificationBase
    {
        protected override void Given()
        {
            Environment.SetEnvironmentVariable("INEXREFHOSTING_HostingFlavours__Default", "Wotcha");

            EnvironmentConfigurationFile.Write(@"
{
  ""HostingFlavours"": {
    ""Default"": ""Hello"",
    ""AvailableFlavours"": ""Hello, Wotcha"",
    ""HostingFlavours"": [
      {
        ""Name"": ""Hello"",
        ""ContainerBuilders"": [
          { ""Type"": ""InexRef.HostEnvironment.Tests.NullContainerConfigurationModule, InexRef.HostEnvironment.Tests"" }
        ]
      },
{
        ""Name"": ""Wotcha"",
        ""ContainerBuilders"": [
          { ""Type"": ""InexRef.HostEnvironment.Tests.NullContainerConfigurationModule, InexRef.HostEnvironment.Tests"" }
        ]
      }
    ]
  }
}");
        }

        protected override void When() => CaughtException = Catch.Exception(() => FlavoursConfiguration = new FlavoursConfiguration(true));

        [Then]
        public void the_override_is_ignored() => FlavoursConfiguration.DefaultFlavour.Name.ShouldBe("Hello");
    }
}