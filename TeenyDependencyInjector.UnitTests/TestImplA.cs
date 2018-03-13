namespace TeenyDependencyInjector.UnitTests
{
    internal class TestImplA : ITestInterface
    {
        public string Content { get; set; }

        public TestImplA()
        {
            Content = "Test";
        }

        public TestImplA(string constructorString, int constructorValue)
        {
            Content = constructorString + constructorValue;
        }
    }
}