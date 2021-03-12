using Moq;
using NSubstitute;
using System;

namespace CSS.UnitTest
{
    public class UnitTestBase
    {
        protected UnitTestBase()
        {

        }

        public T MockService<T>() where T : class
        {
            return Substitute.For<T>();
        }

        protected Mock<T> MyMockService<T>() where T : class
        {
            if (typeof(T).IsInterface)
            {
                return new Mock<T>();
            }
            else
            {
                throw new InvalidOperationException("You should register from an interface");
            }
        }
    }
}
