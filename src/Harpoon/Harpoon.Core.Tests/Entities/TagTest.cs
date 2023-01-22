using Harpoon.Core.Entities;
using NUnit.Framework;

namespace Harpoon.Core.Tests.Entities
{
    [TestFixture]
    public class TagTest
    {

        [Test]
        public void TestNameAndIdCreation()
        {
            var tag = new Tag(" Раз Два  ");

            Assert.AreEqual("Раз Два", tag.Name);
            Assert.AreEqual("raz-dva", tag.Id);
        }

    }
}
