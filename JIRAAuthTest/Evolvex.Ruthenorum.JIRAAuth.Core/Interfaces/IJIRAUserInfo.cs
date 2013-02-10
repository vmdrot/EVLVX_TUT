using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces
{
    public interface IJIRAUserInfo
    {
        string self { get; }
        string name { get; }
        string email { get; }
        string displayName { get; }
        bool active { get; }
        string timeZone { get; }
        List<string> groups { get; }
        Dictionary<String, String> avatarUrls { get; }
    }
}
