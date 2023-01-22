using NUnit.Framework;

namespace Harpoon.Application
{
    [TestFixture]
    public class CommentContentParserTest
    {

        [Test]
        public void TestParse()
        {
            const string content = "Text <script>text2</script> [b]text3[/b] text4 [b] text5 \n text6";
            const string expected = "Text &lt;script&gt;text2&lt;/script&gt; <b>text3</b> text4 <b> text5 <br/> text6</b>";

            var result = CommentContentParser.Parse(content);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestParseMaxLength()
        {
            const string content = "0123456789012345678";
            const string expected = "01234567890123";

            var result = CommentContentParser.Parse(content, 14);
            Assert.AreEqual(expected, result);
        }
        
    }
}