using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtil.Utility
{
    public interface IProgressTracker
    {
        void Start(String currTrnId, int totalCount, ILog logger, DateTime dtStart);
        int TotalCount { get; }
        double CurrentProgress { get; }
        void ItemComplete();
    }
}
