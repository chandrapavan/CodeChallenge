using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VertMarkets.ResponseRequestModels;

namespace VertMarkets.RequestResponseInfo
{
    public class AnswerRequestResponseInfo:IRequestResponseInfo<AnswerRequest, AnswerResponse>
    {
        public AnswerRequest Payload { get; private set; }
        public AnswerRequestResponseInfo(AnswerRequest payload)
        {
            Payload = payload;
        }
    }
}
