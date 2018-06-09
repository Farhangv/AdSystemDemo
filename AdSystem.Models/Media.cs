namespace AdSystem.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] FileContent { get; set; }
        public string Extension { get; set; }
        public string OriginalFileName { get; set; }
        public string MimeType { get; set; }
        public int FileSize { get; set; }

        public int AdId { get; set; }
        public virtual Ad Ad { get; set; }
    }
}