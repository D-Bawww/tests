using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Transport
    {
        public string type { get; set; }
        public int capacity { get; set; }
        public int yy { get; set; }
        public string mark { get; set; }
        public string model { get; set; }
        public Transport() { }
        public Transport(string t, int c, int g, string mar, string mod)
        {
            type = t;
            capacity = c;
            yy = g;
            mark = mar;
            model = mod;
        }
        public string all()
        {
            return type + " " + capacity + " " + yy + " " + mark + " " + model;
        }
        public string stick_name()
        {
            return mark + " " + model;
        }
        public static bool operator ==(Transport a, Transport b)
        {
            return ((a.yy == b.yy) && (a.mark == b.mark) && (a.model == b.model));
        }
        public static bool operator !=(Transport a, Transport b)
        {
            return ((a.yy != b.yy) || (a.mark != b.mark) || (a.model != b.model));
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Transport)) return false;
            var pr = obj as Transport;
            return this == pr;
        }
        public override int GetHashCode()
        {
            return 0;
        }
    }
}
