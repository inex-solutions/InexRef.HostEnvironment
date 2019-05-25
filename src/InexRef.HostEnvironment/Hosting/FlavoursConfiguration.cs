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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using InexRef.HostEnvironment.Hosting.ConfigurationElements;
using Microsoft.Extensions.Configuration;

namespace InexRef.HostEnvironment.Hosting
{
    public class FlavoursConfiguration
    {
        private HostingFlavoursConfigurationElement _hostingFlavoursConfiguration;

        public FlavoursConfiguration()
        {
            LoadConfiguration();
            VerifyConfiguration();

            var defaultFlavourConfig = _hostingFlavoursConfiguration.HostingFlavours.First(f => f.Name == _hostingFlavoursConfiguration.Default);
            var availableFlavoursConfig = _hostingFlavoursConfiguration.HostingFlavours.Where(f => _hostingFlavoursConfiguration.AvailableFlavours.SplitAndTrim(",").Contains(f.Name));

            DefaultFlavour = new HostedEnvironmentFlavour(defaultFlavourConfig);
            AvailableFlavours = availableFlavoursConfig.Select(f => new HostedEnvironmentFlavour(f)).ToArray();
        }

        private void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(new FileInfo(@"..\..\..\config.json").FullName, optional: true, reloadOnChange: false)
                .AddJsonFile(new FileInfo(@"..\..\..\flavours.json").FullName, optional: true, reloadOnChange: false);
            var flavourConfigurationRoot = builder.Build();

            _hostingFlavoursConfiguration = new HostingFlavoursConfigurationElement();
            flavourConfigurationRoot.GetSection("HostingFlavours").Bind(_hostingFlavoursConfiguration);
        }

        private void VerifyConfiguration()
        {
            var flavourConfigurationBlocks = _hostingFlavoursConfiguration.HostingFlavours.Select(f => f.Name).ToArray();
            var listedAvailableFlavours = _hostingFlavoursConfiguration.AvailableFlavours.SplitAndTrim(",").ToArray();

            if (!listedAvailableFlavours.All(f => flavourConfigurationBlocks.Contains(f)))
            {
                var msg =
                    "Hosting Flavours Configuration Error - not all specified available flavours have corresponding configuration blocks" +
                    $"flavour configuration blocks: {flavourConfigurationBlocks.ToBulletList()}\n" +
                    $"available flavours list: {AvailableFlavours.ToBulletList()}\n";
                throw new HostedEnvironmentConfigurationException(msg);
            }

            if (!listedAvailableFlavours.Contains(_hostingFlavoursConfiguration.Default))
            {
                var msg =
                    "Hosting Flavours Configuration Error - specified default flavour is not in the available flavours list" +
                    $"default flavour: {DefaultFlavour}\n" +
                    $"available flavours list: {AvailableFlavours.ToBulletList()}\n";
                throw new HostedEnvironmentConfigurationException(msg);
            }
        }

        public IEnumerable<HostedEnvironmentFlavour> AvailableFlavours { get; }

        public HostedEnvironmentFlavour DefaultFlavour { get; }
    }
}