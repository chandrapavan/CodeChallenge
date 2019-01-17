using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertMarkets.RequestResponseInfo
{
    public interface IRequestResponseInfo
    {
    }
    public interface IRequestResponseInfo<TResult> : IRequestResponseInfo
    {

    }
    public interface IRequestResponseInfo<TPayload, TResult> : IRequestResponseInfo
    {
        TPayload Payload { get; }
    }
}
