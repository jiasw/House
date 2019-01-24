using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnElasticsearch.DataModel
{

    public enum DataType
    {
        File=1,
        Blog=2
    }


    public class SearchData
    {
        public string Name { get; set; }

        public DataType DataType { get; set; }

        public string Content { get; set; }

        public string SetUser { get; set; }

        public User User { get; set; }

        public List<User> Mate { get; set; }
        public string[] tags { get; set; }
    }

    public class User
    {
        public string UserName { get; set; }
        public Int32 Age { get; set; }
    }
}
