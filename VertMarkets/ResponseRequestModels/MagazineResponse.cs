using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertMarkets.ResponseModels
{
    public class Magazine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
    }

    public class MagazineResponse
    {
        public List<Magazine> Data { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}
