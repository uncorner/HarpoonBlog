using NUnit.Framework;

namespace Harpoon.Application
{
    [TestFixture]
    public class PreviewProcessorTest
    {
        [Test]
        public void TestGetFirstParagraph()
        {
            const string sourceHtml =
                "<p>    </p>"
                + "< p > Первый абзац <i>как</i> вступление   < / P>"
                + "<p>Прикольная заметка</p><p>&nbsp;ttt</p>";

            const string expectedHtml = "<p> Первый абзац <i>как</i> вступление   </p>";

            var result = PreviewProcessor.GetFirstParagraph(sourceHtml);
            
            Assert.IsNotNullOrEmpty(result);
            Assert.AreEqual(expectedHtml, result);
        }

        [Test]
        public void TestGetFirstParagraphWhenEmpty()
        {
            var result = PreviewProcessor.GetFirstParagraph("    ");
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
            
            result = PreviewProcessor.GetFirstParagraph("");
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);

            result = PreviewProcessor.GetFirstParagraph(null);
            Assert.IsNotNull(result);
            Assert.IsEmpty(result);
        }

        [Test]
        public void TestGetFirstParagraphWhenNotFound()
        {
            const string sourceHtml = "<h1>Здесь какой-то текст</h1>";
            var result = PreviewProcessor.GetFirstParagraph(sourceHtml);

            Assert.IsNotNullOrEmpty(result);
            Assert.AreEqual(sourceHtml, result);
        }

    }
}