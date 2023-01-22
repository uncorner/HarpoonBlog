namespace Harpoon.Core.Entities
{
    public class PostContent
    {
        public int Id { get; private set; }
        public string Data { get; private set; }

        private PostContent()
        {
        }

        public PostContent(string data)
        {
            SetData(data);
        }

        public void SetData(string data)
        {
            ArgumentHelper.EnsureNotNullOrEmpty("data", data);
            Data = data;
        }

    }
}