using System.IO;

namespace Harpoon.Core.Entities
{
    public class Image
    {
        public int Id { get; private set; }
        public byte[] Data { get; private set; }
        public string ContentType { get; private set; }

        private Image()
        {
        }

        public Image(Stream dataStream, string contentType)
        {
            ContentType = contentType;
            // stream to bytes
            var memoryStream = new MemoryStream();
            dataStream.CopyTo(memoryStream);
            Data = memoryStream.ToArray();
        }
        
    }
}