using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Ruthenorum.JIRAAuth.Facade.Spares
{
    public class JIRAFieldsWrapper
    {
        public string expand { get; set; }
        public string startAt { get; set; }
        public string maxResults { get; set; }
        public string total { get; set; }
        public List<JIRAFieldInfo> issues { get; set; }
    }
}
