using Ladeskab.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Ladeskab.Test.Unit_
{
    public class Tests
    {
        IDoor door;
        [SetUp]
        public void Setup()
        {
            //door = new Substitute.For<IDoor>();
        }

        [Test]
        public void LockDoor_()
        {
            Assert.Pass();
        }
    }
}