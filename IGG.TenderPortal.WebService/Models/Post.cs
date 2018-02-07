using System;

namespace IGG.TenderPortal.WebService.Models
{
    public class Post
    {
        public int ID { get; set; } = -1;
        public int IDproject { get; set; }
        public string text { get; set; }
        public DateTime time { get; set; }
        public int from { get; set; }
        public int to { get; set; }
        public string image { get; set; }
        public int imageSize { get; set; }
        public string definition { get; set; }
    }
}