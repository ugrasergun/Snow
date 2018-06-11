using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SnowLibrary
{
    public class SnowParser : IParser
    {
        public Bar Parse(string barLine)
        {

            if(barLine == null)
            {
                throw new ArgumentNullException("barLine");
            }

            if(barLine[0] != '#')
            {
                throw new ArgumentException("BarLine should start with '#'", "barLine");
            }

            var barInfo = barLine.Split(':');

            if (barInfo.Length != 3)
            {
                throw new ArgumentException("Argument in not in a correct format", "barLine");
            }

            string name = barInfo[0].Substring(1);

            if(!(new Regex("^[a-zA-Z0-9]*$")).IsMatch(name))
            {
                throw new ArgumentException("Name Should Be Alphanumeric", "name");
            }

            KnownColor knownColor;

            if(!Enum.TryParse<KnownColor>(barInfo[1], true, out knownColor))
            {
                throw new ArgumentException(barInfo[1] + " is not a valid colour");
            }
            
            Color colour = Color.FromName(barInfo[1]);


            int value;
            if(!int.TryParse(barInfo[2], out value))
            {
                throw new ArgumentException("Bar Value should be a integer", "value");
            }
            
            Bar result = new Bar(name, colour, value);
            return result;
        }

        public List<Bar> ParseAll(string[] barLines)
        {

            if (barLines == null)
            {
                throw new ArgumentNullException("barLines");
            }
            if(barLines.Length == 0)
            {
                throw new ArgumentException("barLines collection can not be empty", "barLines");
            }

            List<Bar> result = new List<Bar>();
            foreach (var barLine in barLines)
            {
                result.Add(Parse(barLine));
            }

            return result;
        }

        
    }
}
