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
using Xunit.Extensions;

namespace InexRef.HostEnvironment.Tests.CalculatorTests
{
    public class when_adding_two_numbers : Specification
    {
        readonly Calculator calc;
        int result;
        private int count;
        public when_adding_two_numbers()
        {
            calc = new Calculator();
        }

        private int Increment()
        {
            return ++count;
        }

        protected override void Observe()
        {
            result = calc.Add(1, 2);
            Console.WriteLine("I ran: " + Increment());
        }

        [Observation]
        public void should_return_correct_result()
        {
            result.ShouldEqual(3);
        }

        [Observation]
        public void and_something_else()
        {
            result.ShouldEqual(3);
        }
    }
}