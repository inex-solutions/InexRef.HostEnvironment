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

using NUnit.Framework;

namespace InexRef.HostEnvironment.Tests
{
    public abstract class NUnitTestDiscoveryFix
    {
        private static void ReferenceNUnitAssemblyToFixTestDiscovery()
        {
            // This method is never called, but it includes a reference to a type from the NUnit.Framework assembly,
            // thus forcing the assembly to be loaded
            // This fixes an issue with the specification framework test discovery.

            // FROM: https://github.com/nunit/docs/wiki/Breaking-Changes
            // A key change is that the NUnit Test Engine will not recognize a test assembly that does not reference the
            // NUnit framework directly. Normally, test assemblies use NUnit Asserts, attributes and other Types and methods.
            // However, certain third-party frameworks are designed to completely isolate the user from the
            // details of NUnit. They mediate between the test assembly and the NUnit framework in order to run tests.
            // In such a case, NUnit will indicate that the assembly either contains no tests or the proper driver could
            // not be found. To resolve this situation, simply add one NUnit attribute or other reference.
            Assert.IsTrue(true);
        }
    }
}