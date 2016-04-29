using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtil.Utility
{
    public class ProgressTrackerBase : IProgressTracker
    {
        #region field(s)
        protected ILog _logger;
        protected string _trnId;
        protected DateTime _dateStarted;
        protected int _totalCount;
        private Int32 _currItemIdx = -1;
        private const double _progressLogIntervalPct = 1.00;
        private int _lastTrackedProgress = 0;
        private double _progressAchievedPct = 0;
        #endregion

        public void Start(string currTrnId, int totalCount, ILog logger, DateTime dtStart)
        {
            //this.currTrnId = currTrnId;
            //this.totalCount = totalCount;
            //this.log = log;
            //this.dtStart = dtStart;

            //this.currFeedIdx = -1;
            //this.lastTrackedProgress = 0;
            //this.progressAchievedPct = 0;
            
        }

        public int TotalCount
        {
            get { return _totalCount; }
        }

        public double CurrentProgress
        {
            get { return _progressAchievedPct; }
        }

        public void ItemComplete()
        {
            //lock (currFeedIdxLock)
            //    currFeedIdx++;

            //#region Tracking the progresss
            //progressAchievedPct = (double)(((double)(currFeedIdx + 1) / totalCount) * 100);
            //if ((progressAchievedPct - lastTrackedProgress) >= progressLogIntervalPct)
            //{
            //    lastTrackedProgress = (int)progressAchievedPct;
            //    DateTime now = DateTime.Now;
            //    TimeSpan tsElapsed = now - dtStart;
            //    int secondsElapsed = (int)tsElapsed.TotalSeconds;
            //    int estimatedTotalSeconds = (int)(secondsElapsed / (progressAchievedPct / 100));
            //    TimeSpan tsETA = new TimeSpan((long)(estimatedTotalSeconds * (long)Math.Pow(10, 7)));
            //    DateTime dtETA = dtStart + tsETA;
            //    TimeSpan tsLeft = dtETA - now;
            //    //log.Info("DownloadAll()\t{0}\t{1} % complete\ttime elapsed - {2}, estimated time left - {3}, total estimated running time - {4}, ETA - {5}", currTrnId, progressAchievedPct, tsElapsed, tsLeft, tsETA, dtETA);
            //    log.Info("DownloadAll()\t{0}\t{1} % complete\telapsed - {2}, left - {3}, total - {4}, ETA - {5}", currTrnId, progressAchievedPct, tsElapsed, tsLeft, tsETA, dtETA);
            //}
            //#endregion
        }
    }
}
