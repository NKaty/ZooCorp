using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZooCorp.BusinessLogic.Common
{
    public class ZooConsole : IConsole
    {
        public List<string> Messages { get; set; } = new List<string>();
        public void WriteLine(string text)
        {
            Messages.Add(text);
        }
    }
}
