using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Lessor
    {
        public string phone_number { get; set; }
        public string mark { get; set; }
        public string model { get; set; }
        public int yy { get; set; }
        public int price { get; set; }
        public int period { get; set; }
        public Lessor() { }
        public Lessor(string p_n, string ma, string mo, int y, int p, int per)
        {
            phone_number = p_n;
            yy = y;
            mark = ma;
            model = mo;
            price = p;
            period = per;
        }
        public string all()
        {
            return phone_number + " " + mark + " " + model + " " + yy + " " + price + " " + period;
        }
        public static bool operator ==(Lessor a, Lessor b)
        {
            return ((a.price == b.price) && (a.period == b.period) && (a.phone_number == b.phone_number) && (a.mark == b.mark) && (a.model == b.model) && (a.yy == b.yy));
        }
        public static bool operator !=(Lessor a, Lessor b)
        {
            return ((a.price != b.price) || (a.period != b.period) || (a.phone_number != b.phone_number) || (a.mark == b.mark) || (a.model == b.model));
        }
        public override int GetHashCode()
        {
            return 0;
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Lessor)) return false;
            var pr = obj as Lessor;
            return this == pr;
        }
    }
}
