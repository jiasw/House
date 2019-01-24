using DotnetSpider.Extension.Model;

namespace LearnElasticsearch
{
    public class AutoHomeShopListEntity : SpiderEntity
    {
        public string DetailUrl { get; set; }
        public string CarImg { get; set; }
        public string Price { get; set; }
        public string DelPrice { get; set; }
        public string Title { get; set; }
        public string Tip { get; set; }
        public string BuyNum { get; set; }

        public override string ToString()
        {
            return Title;
        }
    }
}
