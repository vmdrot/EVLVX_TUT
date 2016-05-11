using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.VKUtilLib.YeDR
{
    public class UsrMinjustSearchResult
    {
        public bool IsFound { get; set; }
        private static UsrMinjustSearchResult _empty;
        public static UsrMinjustSearchResult Empty
        {
            get 
            {
                if (_empty == null)
                {
                    _empty = new UsrMinjustSearchResult() { IsFound = false };
                }
                return _empty;
            }
        }
    }
}
