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
using InexRef.HostEnvironment.Tests.Common;
using InexRef.HostEnvironment.Tests.Common.SpecificationFramework;
using Shouldly;

namespace InexRef.HostEnvironment.Tests
{
    public class when_the_default_flavour_is_not_present_in_the_hosting_flavours_list : HostedFlavourSpecificationBase
    {
        protected override void Given()
            => EnvironmentConfigurationFile.Write(@"
{
  ""HostingFlavours"": {
    ""Default"": ""Wotcha"",
    ""AvailableFlavours"": ""Hello"",
    ""HostingFlavours"": [

      {
        ""Name"": ""Hello"",
        ""ContainerBuilders"": [
          { ""Type"": ""InexRef.HostEnvironment.Tests.NullContainerConfigurationModule, InexRef.HostEnvironment.Tests"" }
        ]
      }
    ]
  }
}");

        protected override void When() => CaughtException = Catch.Exception(() => FlavoursConfiguration = HostedEnvironment.FlavoursConfiguration);

        [Then]
        public void a_HostedEnvironmentConfigurationException_is_thrown() => CaughtException.ShouldBeOfType<HostedEnvironmentConfigurationException>();
    }
}