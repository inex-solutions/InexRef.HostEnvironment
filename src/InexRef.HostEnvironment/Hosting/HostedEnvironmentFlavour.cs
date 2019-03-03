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

using System;
using System.Collections.Generic;
using System.Linq;
using InexRef.HostEnvironment.Container;
using InexRef.HostEnvironment.Hosting.ConfigurationElements;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InexRef.HostEnvironment.Hosting
{
    public static class HostedEnvironmentFlavour
    {
        private static readonly HostingFlavoursConfigurationElement HostingFlavoursConfiguration;

        static HostedEnvironmentFlavour()
        {
            var hostingFlavours = new HostingFlavoursConfigurationElement();
            HostedEnvironmentConfiguration.ConfigurationRoot.GetSection("HostingFlavours").Bind(hostingFlavours);

            HostingFlavoursConfiguration = hostingFlavours;
            AvailableFlavours = hostingFlavours.AvailableFlavours.SplitAndTrim(",");

            VerifyConfiguration();
        }

        private static void VerifyConfiguration()
        {
            var flavourConfigurationBlocks = HostingFlavoursConfiguration.HostingFlavour.Select(f => f.Name).ToArray();

            if (!AvailableFlavours.All(f => flavourConfigurationBlocks.Contains(f)))
            {
                var msg =
                    "Hosting Flavours Configuration Error - not all specified available flavours have corresponding configuration blocks" +
                    $"flavour configuration blocks: {flavourConfigurationBlocks.ToBulletList()}\n" +
                    $"available flavours list: {AvailableFlavours.ToBulletList()}\n";
                throw new HostedEnvironmentConfigurationException(msg);
            }
        }

        public static IEnumerable<string> AvailableFlavours { get; }


        public static void ConfigureContainerForHostEnvironmentFlavour(ServiceCollection builder, string flavour)
        {
            foreach (var item in HostingFlavoursConfiguration.HostingFlavour.First(f => f.Name == flavour)
                .ContainerBuilders)
            {
                var type = Type.GetType(item.Type);
                var module = (ContainerConfigurationModule) Activator.CreateInstance(type);
                module.ConfigureContainer(builder);
            }
        }
    }
}