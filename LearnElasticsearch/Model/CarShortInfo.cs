using System;
using System.Collections.Generic;
using System.Text;

namespace LearnElasticsearch.Model
{
    public class CarShortInfo
    {
        public string DetailUrl { get; set; }
        public string CarImg { get; set; }
        public double Price { get; set; }
        public double DelPrice { get; set; }
       
        public string Title { get; set; }
        public string Tip { get; set; }
        public int BuyNum { get; set; }
        public DateTime SaveDate { get; set; }
    }

}
