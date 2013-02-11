using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolvex.Ruthenorum.JIRAAuth.Core.Interfaces
{
    public interface ILockedUsersRepo
    {
        List<string> Users { get; }
    }
}
