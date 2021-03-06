﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertMarkets.ResponseModels
{
    public class Subscriber
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public List<int> magazineIds { get; set; }
    }

    public class SubscribersResponse
    {
        public List<Subscriber> data { get; set; }
        public bool success { get; set; }
        public string token { get; set; }
    }
}
