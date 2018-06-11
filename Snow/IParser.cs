using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnowLibrary
{
    public interface IParser
    {
        Bar Parse(string barLine);
        List<Bar> ParseAll(string[] barLines);
    }
}
