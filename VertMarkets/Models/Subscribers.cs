using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertMarkets.Models
{
    public class Subscribers
    {
        public string Id { get; set; }
        public List<int> MagazineIds { get; set; }
    }
}
