using NUnit.Framework;

namespace Harpoon.Application
{
    [TestFixture]
    public class TagLineConverterTest
    {
        [Test]
        public void TestToTags()
        {
            const string tagLine = ";  Кафе;; молоко ;Русский язык;";

            var converter = CreateConverter();
            var items = converter.ToTags(tagLine);

            Assert.IsNotNull(items);
            Assert.AreEqual(3, items.Length);
            Assert.AreEqual("Кафе", items[0]);
            Assert.AreEqual("молоко", items[1]);
            Assert.AreEqual("Русский язык", items[2]);
        }

        [Test]
        public void TestToTagsWhenEmpty()
        {
            var converter = CreateConverter();
            var items = converter.ToTags(null);

            Assert.IsNotNull(items);
            Assert.AreEqual(0, items.Length);
        }

        [Test]
        public void TestToTagsWithoutDuplicates()
        {
            const string tagLine = "раз; два; три; раз";

            var converter = CreateConverter();
            var items = converter.ToTags(tagLine);

            Assert.IsNotNull(items);
            Assert.AreEqual(3, items.Length);
        }

        [Test]
        public void TestToTagLine()
        {
            var items = new [] {"раз", "Два", "Три"};

            var line = CreateConverter().ToTagLine(items);

            Assert.IsNotNullOrEmpty(line);
            Assert.AreEqual("Два; раз; Три", line);
        }

        [Test]
        public void TestToTagLineWhenEmpty()
        {
            var line = CreateConverter().ToTagLine(new string[] {});

            Assert.IsNotNull(line);
            Assert.IsEmpty(line);
        }

        private TagLineConverter CreateConverter()
        {
            return new TagLineConverter(';');
        }
        
    }
}