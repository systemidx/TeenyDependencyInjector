using System;
using System.Collections.Generic;
using System.Text;

namespace TeenyDependencyInjector.UnitTests
{
    internal class TestImplB : ITestInterface
    {
        public string Content { get; set; }

        public TestImplB()
        {
            Content = "Test";
        }

        public TestImplB(string constructorString, int constructorValue)
        {
            Content = constructorString + constructorValue;
        }
    }
}
