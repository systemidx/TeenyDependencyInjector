using System.Threading.Tasks;
using TeenyDependencyInjector.Interfaces;
using Moq;
using Xunit;

namespace TeenyDependencyInjector.UnitTests
{
    public class DependencyServiceTests
    {
        [Fact]
        public async Task BindTypeAndCreateInstances()
        {
            IDependencyService service = new DependencyService();
            await service.RegisterTypeAsync<ITestInterface, TestImplA>();

            ITestInterface instance = await service.CreateInstanceAsync<ITestInterface>();
            Assert.NotNull(instance);
            Assert.Equal("Test", instance.Content);
        }

        [Fact]
        public async Task BindTypeWithParametersAndCreateInstances()
        {
            IDependencyService service = new DependencyService();
            await service.RegisterTypeAsync<ITestInterface, TestImplA>("Fox", 30);

            ITestInterface instance = await service.CreateInstanceAsync<ITestInterface>();
            Assert.NotNull(instance);
            Assert.Equal("Fox30", instance.Content);
        }

        [Fact]
        public async Task BindAndRetrieveInstance()
        {
            IDependencyService service = new DependencyService();
            ITestInterface obj = new TestImplA("test", 10);

            await service.RegisterInstanceAsync<ITestInterface, TestImplA>(obj);
            ITestInterface a = await service.GetInstanceAsync<ITestInterface>();

            Assert.Equal(a.Content, obj.Content);
        }

        [Fact]
        public async Task RetrieveNamedInstance()
        {
            IDependencyService service = new DependencyService();
            ITestInterface obj = new TestImplA("test", 10);

            await service.RegisterInstanceAsync<ITestInterface, TestImplA>(obj, "mytestinstance");
            ITestInterface a = await service.GetInstanceAsync<ITestInterface>("mytestinstance");

            Assert.Equal("test10", a.Content);
        }

        [Fact]
        public async Task NamedInstancesReturnTheSameObject()
        {
            IDependencyService service = new DependencyService();
            ITestInterface obj = new TestImplA("test", 10);

            await service.RegisterInstanceAsync<ITestInterface, TestImplA>(obj, "mytestinstance");
            ITestInterface a = await service.GetInstanceAsync<ITestInterface>("mytestinstance");
            ITestInterface b = await service.GetInstanceAsync<ITestInterface>("mytestinstance");

            Assert.Equal(b, a);
        }

        [Fact]
        public async Task BindingInstanceReturnsSameObjectAfterBinding()
        {
            IDependencyService service = new DependencyService();
            ITestInterface obj = new TestImplA("test", 10);

            ITestInterface result = await service.RegisterInstanceAsync<ITestInterface, TestImplA>(obj, "mytestinstance");

            Assert.Equal(result.Content, obj.Content);
        }

        [Fact]
        public async Task RetrievingNotBoundInstanceReturnsNull()
        {
            IDependencyService service = new DependencyService();
            ITestInterface obj = new TestImplA("test", 10);

            await service.RegisterInstanceAsync<ITestInterface, TestImplA>(obj, "mytestinstance");

            ITestInterface result = await service.GetInstanceAsync<ITestInterface>("someotherinstance");
            Assert.Null(result);
        }

        [Fact]
        public async Task BoundInstancesOfSameInterfaceTypeAreNotEqual()
        {
            IDependencyService service = new DependencyService();
            ITestInterface implA = new TestImplA("test", 10);
            ITestInterface implB = new TestImplB("test", 10);

            await service.RegisterInstanceAsync<ITestInterface, TestImplA>(implA, "a");
            await service.RegisterInstanceAsync<ITestInterface, TestImplB>(implB, "b");

            ITestInterface a = await service.GetInstanceAsync<ITestInterface>("a");
            ITestInterface b = await service.GetInstanceAsync<ITestInterface>("b");

            Assert.NotEqual(a, b);
        }

        [Fact]
        public async Task ServerRunningFlagReturnsCorrectStatus()
        {
            IDependencyService utility = new DependencyService();
            ITestInterface a = await utility.RegisterInstanceAsync<ITestInterface, ITestInterface>(new Mock<ITestInterface>().Object);
            ITestInterface b = await utility.RegisterInstanceAsync<ITestInterface, ITestInterface>(new Mock<ITestInterface>().Object);

            Assert.NotNull(a);
            Assert.NotNull(b);
        }
    }
}
