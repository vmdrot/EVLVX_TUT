using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtil.Utility
{
    public interface ILog
    {
        void Debug(string msg);
        void Debug(string fmt, params object[] args);
        void Info(string msg);
        void Info(string fmt, params object[] args);
        void Warn(string msg);
        void Warn(string fmt, params object[] args);
        void Error(string msg);
        void Error(string fmt, params object[] args);
        void Fatal(string msg);
        void Fatal(string fmt, params object[] args);
    }
}
