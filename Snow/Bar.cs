using System.Drawing;

namespace SnowLibrary
{
    public struct Bar
    {
        public string Name { get; set; }
        public Color Colour { get; set; }
        public int Value { get; set; }

        public Bar(string name, Color colour, int value)
        {
            Name = name;
            Colour = colour;
            Value = value;
        }
    }
}
