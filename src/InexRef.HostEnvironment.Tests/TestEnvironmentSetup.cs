﻿#region Copyright & Licence
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
using Microsoft.Extensions.DependencyInjection;

namespace InexRef.HostEnvironment.Tests
{
    public static class TestEnvironmentSetup
    {
        public static void ConfigureContainerForHostEnvironmentFlavour(ServiceCollection containerBuilder, string flavour)
        {
            HostedEnvironmentFlavour.ConfigureContainerForHostEnvironmentFlavour(containerBuilder, flavour);
        }

        private static void ImportAssemblyContaining<T>()
        {
            // workaround - does nothing, but the explicit reference to type T ensures the assembly is imported. The 
            // alternative is to dynamically load the assembly from, or copy it into the current directory. 
        }
    }
}