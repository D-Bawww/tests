using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    public class Node
    {
        public Transport ts;
        public int state;
        public int h1, h2;
        public Node() { }
        public Node(Transport t, int s, int hash)
        {
            ts = t;
            state = s;
            h1 = hash;
            h2 = h1;
        }
        public Node(Transport t, int s, int hash1, int hash2)
        {
            ts = t;
            state = s;
            h1 = hash1;
            h2 = hash2;
        }
    }
    public class Hashtable
    {
        static int size;
        int minSize = 1;
        int filled;
        int k;
        public Node[] table;
        public Hashtable()
        {
            size = minSize;
            table = new Node[size];
            checkSize();
        }
        public Hashtable(List<Transport> ts)
        {
            size = ts.Count;
            k = findK();
            table = new Node[size];
            for (int i = 0; i < ts.Count; i++)
                add(ts[i]);
        }
        public int hash1(Transport ts)
        {
            int hash = 0;
            int t_hash = ts.yy;
            while (t_hash > 0)
            {
                hash += t_hash % 10;
                t_hash = t_hash / 10;
            }
            for (int i = 0; i < ts.stick_name().Length; i++)
            {
                hash += Encoding.ASCII.GetBytes(ts.stick_name())[i];
            }
            return hash % size;
        }
        public int hash2(int key, int j)
        {
            return (key + j * k) % size;
        }
        public void add(Transport ts)
        {
            int key = hash1(ts);
            if (table[key] == null)
            {
                table[key] = new Node(ts, 1, key);
                filled++;
                checkSize();
            }
            else if (table[key].state == 1 && table[key].ts == ts)
                Console.WriteLine("transport already exists");
            else addColl(ts);
        }
        void addColl(Transport ts)
        {
            int t = 0;
            int key = findColl(ts, 1, ref t);
            if (key == -1)
            {
                int j = 1;
                key = hash2(hash1(ts), j);

                int s = key;
                while (table[key] != null && table[key].state == 1 && table[key].ts != ts && j <= size)
                {
                    j++;
                    key = hash2(hash1(ts), j);

                    s = key;
                    if (s == size)
                    {
                        s = 0;
                        key = 0;
                    }
                }

                if (table[key] == null)
                {
                    filled++;
                    table[key] = new Node(ts, 1, hash1(ts), key);
                    checkSize();
                }
                else if (table[key].state == 1 && table[key].ts == ts)
                {
                    Console.WriteLine("lessor already exists");
                }
                else
                {
                    filled++;
                    table[key] = new Node(ts, 1, hash1(ts), key);
                    checkSize();
                }
            }
            else Console.WriteLine("key already exists");
        }
        public void del(Transport ts)
        {
            if (table != null)
            {
                int key = hash1(ts);
                if (table[key] == null)
                    Console.WriteLine("key does not exist");
                else if (table[key].state != 2 && table[key].ts == ts)
                {
                    filled--;
                    table[key].state = 2;
                    table[key].h1 = 0;
                    checkSize();
                }
                else if (table[key].ts != ts)
                    delColl(ts);
                else Console.WriteLine("key does not exist");
            }
            else Console.WriteLine("table is empty");
        }
        void delColl(Transport ts)
        {
            int t = 0;
            int key = findColl(ts, 1, ref t);
            if (key == -1)
                Console.WriteLine("key does not exist");
            else
            {
                filled--;
                table[key].state = 2;
                checkSize();
                table[hash1(ts)].h1 = 0;
                table[key].h2 = 0;
            }
        }
        void checkSize()
        {
            if ((double)filled / (double)size >= 0.7)
                changeSize(size * 2);
            else if ((double)filled / (double)size <= 0.1 && size > minSize)
                changeSize(size / 2);
        }
        void changeSize(int s)
        {
            size = s;
            k = findK();
            Node[] nodes = new Node[s];

            for (int i = 0; i < table.Length; i++)
            {
                if (table[i] != null && table[i].state == 1)
                {
                    int c = 0;
                    int hash = hash2(hash1(table[i].ts), c);
                    int h = hash1(table[i].ts);
                    while (nodes[hash] != null && nodes[hash].state == 1)
                    {
                        c++;
                        hash = hash2(hash1(table[i].ts), c);
                    }
                    if (hash == h)
                        nodes[hash] = new Node(table[i].ts, table[i].state, h, h);
                    else
                        nodes[hash] = new Node(table[i].ts, table[i].state, h, hash);
                }
            }
            table = nodes;
            GC.Collect();
        }
        int findK()
        {
            for (int i = 2; i < size; i++)
            {
                bool found = size % i != 0;
                for (int j = 2; j < i; j++)
                    if (i % j == 0 && size % j == 0)
                        found = false;
                if (found)
                    return i;
            }
            return 1;
        }
        public int find(Transport ts, int f, ref int c)
        {
            int key = hash1(ts);
            if (table[key] != null && table[key].ts == ts)
                if (f == 1)
                    return key;
                else
                {
                    if (table[key].ts == ts)
                        return key;
                    else return -1;
                }
            else if (table[key] == null)
                return -1;
            else return findColl(ts, f, ref c);
        }
        int findColl(Transport ts, int f, ref int c)
        {
            int i = 0, key = 0;
            for (i = 1; i < size; i++)
            {
                key = hash2(hash1(ts), i);
                c++;
                if (table[key] == null)
                    break;
                if (table[key].ts == ts)
                    break;
            }

            if (table[key] == null)
                return -1;
            else if (table[key].state == 1 && table[key].ts == ts)
                if (f == 1)
                    return key;
                else
                {
                    if (table[key].ts == ts)
                        return key;
                    else return -1;
                }
            else return -1;
        }
        public List<Lessor> findLessors(List<Lessor> l, string t, int cap1, int cap2, ref int k)
        {
            List<Lessor> res = new List<Lessor>();

            for (int i = 0; i < l.Count; i++)
            {
                int key = find(new Transport("", 0, l[i].yy, l[i].mark, l[i].model), 1, ref k);
                if (key != -1)
                {
                    if ((table[key].ts.type == t) && (table[key].ts.capacity >= cap1) && (table[key].ts.capacity <= cap2))
                        res.Add(l[i]);
                }
            }
            return res;
        }
        public List<string> print()
        {
            if (table != null)
            {
                List<string> res = new List<string>();
                for (int i = 0; i < size; i++)
                {
                    string a = "";
                    if (table[i] != null && table[i].state == 1)
                    {
                        a = i + ": state = 1, hash1 = ";
                        a += table[i].h1 + ", hash2 = " + table[i].h2 + ", ";
                        a += table[i].ts.all();
                    }
                    else if (table[i] != null && table[i].state == 2)
                        a = i + ": state = 2";
                    else
                        a = i + ": state = 0";
                    res.Add(a);
                }
                return res;
            }
            else return null;
        }
    }
}
