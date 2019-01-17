using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertMarkets.ResponseModels
{
    public class CategoryResponse
    {
        public List<string> Data { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }

    }
}
