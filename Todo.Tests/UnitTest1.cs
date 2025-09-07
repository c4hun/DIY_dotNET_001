using Xunit;

namespace XUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestOk()
        {
            int a = 5;
            int b = 5;
            Assert.Equal(a, b);
        }


        [Fact]
        public void TestNotOk()
        {
            int a = 5;
            int b = 6;
            Assert.Equal(a, b);
        }
    }
}