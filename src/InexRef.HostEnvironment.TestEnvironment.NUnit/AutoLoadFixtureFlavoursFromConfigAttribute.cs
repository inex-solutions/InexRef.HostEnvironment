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

using System.Collections;
using InexRef.HostEnvironment.Hosting;
using NUnit.Framework;

namespace InexRef.HostEnvironment.TestEnvironment.NUnit
{
    public class AutoLoadFixtureFlavoursFromConfigAttribute : TestFixtureSourceAttribute
    {
        public AutoLoadFixtureFlavoursFromConfigAttribute(bool ignoreEnvironmentVariableOverrides = false) : base(typeof(FlavourConfigurationEnumerable))
        {
            if (ignoreEnvironmentVariableOverrides)
            {
                HostedEnvironment.SetFlavoursConfiguration(new FlavoursConfiguration(ignoreEnvironmentVariableOverrides: true));
            }
        }

        public class FlavourConfigurationEnumerable : IEnumerable
        {
            public IEnumerator GetEnumerator() => HostedEnvironment.FlavoursConfiguration.AvailableFlavours.GetEnumerator();
        }
    }

    //public class AutoLoadFixtureFlavoursFromConfigForTestingAttribute : TestFixtureSourceAttribute
    //{
    //    public AutoLoadFixtureFlavoursFromConfigForTestingAttribute(Type preTestSetup = null, bool ignoreEnvironmentVariableOverrides = false) : base(typeof(FlavourConfigurationEnumerable))
    //    {
    //        if (preTestSetup != null)
    //        {
    //            if (!typeof(IPreTestSetup).IsAssignableFrom(preTestSetup))
    //            {
    //                throw new TestFixtureFlavourConfigurationException($"Provided preTestSetup type ({preTestSetup.Name}) does not implement {typeof(IPreTestSetup).Name}");
    //            }

    //            var testSetupTypeCtor = preTestSetup.GetConstructor(Type.EmptyTypes);
    //            if (testSetupTypeCtor == null)
    //            {
    //                throw new TestFixtureFlavourConfigurationException($"Provided preTestSetup type ({preTestSetup.Name}) must have a parameterless constructor");
    //            }

    //            var testSetupInstance = (IPreTestSetup)testSetupTypeCtor.Invoke(new object[0]);
    //            testSetupInstance.SetupTest();
    //        }
    //    }

    //    public class FlavourConfigurationEnumerable : IEnumerable
    //    {
    //        public IEnumerator GetEnumerator() => HostedEnvironment.FlavoursConfiguration.AvailableFlavours.GetEnumerator();
    //    }
    //}
}