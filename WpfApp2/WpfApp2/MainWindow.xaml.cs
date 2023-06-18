using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        static Hashtable ht;
        static AVL1 tr1;
        static AVL2 tr2;
        static List<Transport> tl;
        static List<Lessor> ll;
        static string str1 = "transport.txt";
        static string str2 = "lessor.txt";
        static int k = 0;
        static int t = 0;
        public MainWindow()
        {
            InitializeComponent();
            tl = rTransportFile(str1);
            ll = rLessorFile(str2);
            ht = new Hashtable(tl);
            tr1 = new AVL1(ll);
            tr2 = new AVL2(ll);

            transportGrid.ItemsSource = tl;
            lessorGrid.ItemsSource = ll;           
        }
        static List<Transport> rTransportFile(string str)
        {
            using (StreamReader a = new StreamReader(str, System.Text.Encoding.UTF8))
            {
                var f = new FileInfo(str);
                if (f.Length > 1)
                {
                    int size = int.Parse(a.ReadLine());

                    List<Transport> lines = new List<Transport>();

                    int i = 0;
                    while (i < size)
                    {
                        string l = a.ReadLine();
                        string[] stringSeparators = new string[] { "  " };
                        string[] b = l.Split(stringSeparators, StringSplitOptions.None);
                        lines.Add(new Transport(b[0], int.Parse(b[1]), int.Parse(b[2]), b[3], b[4]));
                        i++;
                    }
                    return lines;
                }
                return null;
            }
        }
        static List<Lessor> rLessorFile(string str)
        {
            using (StreamReader b = new StreamReader(str, System.Text.Encoding.UTF8))
            {
                var f = new FileInfo(str);
                if (f.Length > 1)
                {
                    int size = int.Parse(b.ReadLine());

                    List<Lessor> lines = new List<Lessor>();

                    int i = 0;
                    while (i < size)
                    {
                        string l = b.ReadLine();
                        string[] stringSeparators = new string[] {"  "};
                        string[] a = l.Split(stringSeparators, StringSplitOptions.None);
                        lines.Add(new Lessor(a[0], a[1], a[2], int.Parse(a[3]), int.Parse(a[4]), int.Parse(a[5])));
                        i++;
                    }
                    return lines;
                }
                return null;
            }
        }
        static void resFile(List<Lessor> l)
        {
            using (StreamWriter a = new StreamWriter("output.txt", false))
            {
                for (int i = 0; i < l.Count; i++)
                    a.WriteLine(l[i].phone_number + "  " + l[i].mark + "  " + l[i].model + "  " + l[i].yy + "  " + l[i].price + "  " + l[i].period);
            }
        }
        static void addLessorFile(Lessor l)
        {
            using (StreamWriter a = new StreamWriter(str2, false))
            {
                ll.Add(l);
                a.WriteLine(ll.Count);
                for (int i = 0; i < ll.Count; i++)
                    a.WriteLine(ll[i].phone_number + "  " + ll[i].mark + "  " + ll[i].model + "  " + ll[i].yy + "  " + ll[i].price + "  " + ll[i].period);
            }
        }
        static void addTransportFile(Transport ts)
        {
            using (StreamWriter a = new StreamWriter(str1, false))
            {
                tl.Add(ts);
                a.WriteLine(tl.Count);
                for (int i = 0; i < tl.Count; i++)
                    a.WriteLine(tl[i].type + "  " + tl[i].capacity + "  " + tl[i].yy + "  " + tl[i].mark + "  " + tl[i].model);
            }
        }
        static void delLessorFile(Lessor l)
        {
            using (StreamWriter a = new StreamWriter(str2, false))
            {
                ll.Remove(l);
                a.WriteLine(ll.Count);
                for (int i = 0; i < ll.Count; i++)
                    a.WriteLine(ll[i].phone_number + "  " + ll[i].mark + "  " + ll[i].model + "  " + ll[i].yy + "  " + ll[i].price + "  " + ll[i].period);
            }
        }
        static void delTransportFile(Transport ts)
        {
            using (StreamWriter a = new StreamWriter(str1, false))
            {
                tl.Remove(ts);
                a.WriteLine(tl.Count);
                for (int i = 0; i < tl.Count; i++)
                    a.WriteLine(tl[i].type + "  " + tl[i].capacity + "  " + tl[i].yy + "  " + tl[i].mark + "  " + tl[i].model);
            }
        }
        private void ofLes_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog w = new Microsoft.Win32.OpenFileDialog();

            w.DefaultExt = ".txt";
            w.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> result = w.ShowDialog();

            if (result == true)
            {
                str2 = w.FileName;
                ll = rLessorFile(str2);
                lessorGrid.ItemsSource = ll;
                if (ll != null)
                    tr1 = new AVL1(ll);
                else
                {
                    tr1 = new AVL1();
                    ll = new List<Lessor>();
                }
            }
        }
        private void ofT_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog w = new Microsoft.Win32.OpenFileDialog();

            w.DefaultExt = ".txt";
            w.Filter = "Text documents (.txt)|*.txt";
            Nullable<bool> result = w.ShowDialog();

            if (result == true)
            {
                str1 = w.FileName;
                tl = rTransportFile(str1);
                transportGrid.ItemsSource = tl;
                if (tl != null)
                {
                    ht = new Hashtable(tl);
                    str2 = "empty.txt";
                    File.Create(str2);
                    ll = new List<Lessor>();
                    tr1 = new AVL1();
                    lessorGrid.ItemsSource = null;
                }
                else
                {
                    ht = new Hashtable();
                    tl = new List<Transport>();
                }
            }
        }
        private void addLes_Click(object sender, RoutedEventArgs e)
        {
            string num = numberTextBox.Text, mar = markLesTextBox.Text, mod = modelLesTextBox.Text, yy = yearLesTextBox.Text, pr = priceTextBox.Text,
                per = periodTextBox.Text;
            bool f1 = check_lessor(num, mar, mod, yy, pr, per);
            if (f1)
            {
                if (ht.find(new Transport("", 0, int.Parse(yy), mar, mod), 1, ref t) != -1)
                {
                    Lessor landlord = new Lessor(num, mar, mod, int.Parse(yy), int.Parse(pr), int.Parse(per));
                    if (!tr1.node.find(landlord, tr1.node.head))
                    {
                        bool f = true;
                        Un1 un1 = tr1.node.head;
                        Un2 un2 = tr2.node.head;
                        tr1.node.add(landlord, ref un1, ref f);
                        f = true;
                        tr2.node.add(landlord, ref un2, ref f);
                        addLessorFile(landlord);
                        lessorGrid.ItemsSource = null;
                        lessorGrid.ItemsSource = ll;
                    }
                    else MessageBox.Show("Арендодатель с таким транспортом уже существует.", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Такой транспорт не существует.", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Неверный формат", "Data error", MessageBoxButton.OK, MessageBoxImage.Error);

            numberTextBox.Text = "";
            markLesTextBox.Text = "";
            modelLesTextBox.Text = "";
            yearLesTextBox.Text = "";
            priceTextBox.Text = "";
            periodTextBox.Text = "";
        }
        private void delLes_Click(object sender, RoutedEventArgs e)
        {
            string num = numberTextBox.Text, mar = markLesTextBox.Text, mod = modelLesTextBox.Text, yy = yearLesTextBox.Text, pr = priceTextBox.Text,
                per = periodTextBox.Text;
            bool f1 = check_lessor(num, mar, mod, yy, pr, per);
            if (f1)
            {
                Lessor landlord = new Lessor(num, mar, mod, int.Parse(yy), int.Parse(pr), int.Parse(per));
                if (tr1.node.find(landlord, tr1.node.head))
                {
                    bool f = true, fl = false;
                    tr1.node.delete(landlord, tr1.node.head, ref f, ref fl);
                    tr2.node.delete(landlord, tr2.node.head, ref f, ref fl);
                    delLessorFile(landlord);
                    lessorGrid.ItemsSource = null;
                    lessorGrid.ItemsSource = ll;
                }
                else MessageBox.Show("Арендодателя с таким транспортом не существует.", "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Неверный формат", "Data error", MessageBoxButton.OK, MessageBoxImage.Error);

            numberTextBox.Text = "";
            markLesTextBox.Text = "";
            modelLesTextBox.Text = "";
            yearLesTextBox.Text = "";
            priceTextBox.Text = "";
            periodTextBox.Text = "";
        }
        private void searchLes_Click(object sender, RoutedEventArgs e)
        {
            string a = markLesTextBox.Text, b = modelLesTextBox.Text;
            bool f = check_lessor_ts(a, b);
            if (f)
            {
                k = 0;
                List<Lessor> res = new List<Lessor>();
                tr1.node.head.findLessors(a, b, tr1.node.head, ref res, ref k);

                if (res.Count != 0)
                {
                    PopWindow pw = new PopWindow(res);
                    pw.Owner = this;
                    pw.Show();
                }
                else MessageBox.Show("Арендодателей с таким транспортом не существует.", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Неверный формат", "Data error", MessageBoxButton.OK, MessageBoxImage.Error);


            numberTextBox.Text = "";
            markLesTextBox.Text = "";
            modelLesTextBox.Text = "";
            yearLesTextBox.Text = "";
            priceTextBox.Text = "";
            periodTextBox.Text = "";
        }
        private void showLes_Click(object sender, RoutedEventArgs e)
        {
            PopWindow pw = new PopWindow(tr1);
            pw.Owner = this;
            pw.Show();
        }
        private void addTransport_Click(object sender, RoutedEventArgs e)
        {
            string typ = tsTypeList1.Text, cap = tsCapacityTextBox.Text, y = tsYearTextBox.Text,
                br = tsBrandTextBox.Text, mod = tsModelTextBox.Text;
            bool f1 = check_transport(typ, cap, y, br, mod);
            if (f1)
            {               
                Transport ts = new Transport(typ, int.Parse(cap), int.Parse(y), br, mod);
                if (ht.find(ts, 1, ref t) == -1)
                {
                    ht.add(ts);
                    addTransportFile(ts);
                    transportGrid.ItemsSource = null;
                    transportGrid.ItemsSource = tl;
                }
                else MessageBox.Show("Транспорт уже существует.", "Add error", MessageBoxButton.OK, MessageBoxImage.Error);
         
            }
            else MessageBox.Show("Неверный формат", "Data error", MessageBoxButton.OK, MessageBoxImage.Error);

            tsTypeList1.SelectedIndex = -1;
            tsCapacityTextBox.Text = "";
            tsYearTextBox.Text = "";
            tsBrandTextBox.Text = "";
            tsModelTextBox.Text = "";
        }
        private void delTransport_Click(object sender, RoutedEventArgs e)
        {
            string y = tsYearTextBox.Text, br = tsBrandTextBox.Text, mod = tsModelTextBox.Text;
            bool f1 = check_transport_ts(y, br, mod);
            if (f1)
            {
                Transport ts = new Transport("", 0, int.Parse(y), br, mod);
                if (ht.find(ts, 0, ref t) != -1)
                {
                    ht.del(ts);
                    delTransportFile(ts);
                    transportGrid.ItemsSource = null;
                    transportGrid.ItemsSource = tl;

                    List<List<Lessor>> show = new List<List<Lessor>>();
                    if (tr1.node.head != null)
                    {
                        tr1.node.head.show(tr1.node.head, ref show);
                        for (int i = 0; i < show.Count; i++)
                            if (show[i] != null)
                            {
                                if ((show[i][0].mark == br) && (show[i][0].model == mod))
                                {
                                    for (int j = show[i].Count - 1; j >= 0; j--)
                                    {
                                        delLessorFile(show[i][j]);
                                        bool f = true, fl = false;
                                        tr2.node.delete(show[i][j], tr2.node.head, ref f, ref fl);
                                        f = true;
                                        fl = false;
                                        tr1.node.delete(show[i][j], tr1.node.head, ref f, ref fl);
                                    }
                                }
                            }
                    }
                    lessorGrid.ItemsSource = null;
                    lessorGrid.ItemsSource = ll;

                }
                else MessageBox.Show("Транспорт не существует.", "Delete error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Неверный формат", "Data error", MessageBoxButton.OK, MessageBoxImage.Error);

            tsTypeList1.SelectedIndex = -1;
            tsCapacityTextBox.Text = "";
            tsYearTextBox.Text = "";
            tsBrandTextBox.Text = "";
            tsModelTextBox.Text = "";
        }
        private void searchTransport_Click(object sender, RoutedEventArgs e)
        {
            string y = tsYearTextBox.Text, br = tsBrandTextBox.Text, mod = tsModelTextBox.Text;
            bool f1 = check_transport_ts(y, br, mod);
            if (f1)
            {
                t = 1;
                int key = ht.find(new Transport("", 0, int.Parse(y), br, mod), 1, ref t);
                if (key != -1)
                {
                    PopWindow w = new PopWindow(ht.table[key].ts);
                    w.Owner = this;
                    w.Show();
                }
                else MessageBox.Show("Транспорт не существует.", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Неверный формат", "Data error", MessageBoxButton.OK, MessageBoxImage.Error);

            tsTypeList1.SelectedIndex = -1;
            tsCapacityTextBox.Text = "";
            tsYearTextBox.Text = "";
            tsBrandTextBox.Text = "";
            tsModelTextBox.Text = "";
        }
        private void showT_Click(object sender, RoutedEventArgs e)
        {
            PopWindow p = new PopWindow(ht);
            p.Owner = this;
            p.Show();
        }
        private void search_Click(object sender, RoutedEventArgs e)
        {
            string typ = tsTypeList2.Text, prFrom = priceFrom.Text, prTo = priceTo.Text,
                capFrom = capacityFrom.Text, capTo = capacityTo.Text,
                perFrom = periodFrom.Text, perTo = periodTo.Text;
            bool f1 = check_report_search(typ, capFrom, capTo, prFrom, prTo, perFrom, perTo);
            if (f1)
            {
                int prFromInt = int.Parse(prFrom), prToInt = int.Parse(prTo),
                    capFromInt = int.Parse(capFrom), capToInt = int.Parse(capTo),
                    perFromInt = int.Parse(perFrom), perToInt = int.Parse(perTo);
                k = 0;
                List<Lessor> l = new List<Lessor>();
                tr2.node.findTransport(prFromInt, prToInt, perFromInt, perToInt, tr2.node.head, ref l, ref k);
                if (l.Count != 0)
                {
                    List<Lessor> res = ht.findLessors(l, typ, capFromInt, capToInt, ref t);
                    if (res.Count != 0)
                    {
                        reportGrid.ItemsSource = null;
                        reportGrid.ItemsSource = res;
                        resFile(res);
                    }
                    else MessageBox.Show("Транспорта с таким типом и вместитмостью не существует.", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else MessageBox.Show("Арендодателей с такими промежутками стоимости и срока не существует.", "Search error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Неверный формат", "Data error", MessageBoxButton.OK, MessageBoxImage.Error);

            tsTypeList2.SelectedIndex = -1;
            priceFrom.Text = "";
            priceTo.Text = "";
            periodFrom.Text = "";
            periodTo.Text = "";
            capacityFrom.Text = "";
            capacityTo.Text = "";
        }
        private void showRep_Click(object sender, RoutedEventArgs e)
        {
            PopWindow p = new PopWindow(tr2);
            p.Owner = this;
            p.Show();
        }
        public bool check_lessor(string a, string b, string c, string d, string e, string f)
        {
            if ((check_digit(d) == false) || (check_digit(e) == false) || (check_digit(f) == false) || (a == "") || (b == "") || (c == "") || (d == "") || (e == "") || (f == ""))
                return false;
            else
            {
                if ((int.Parse(d) > 2022) || (int.Parse(d) < 1980))
                    return false;
                if ((int.Parse(e) > 100000) || (int.Parse(e) < 1))
                    return false;
                if ((int.Parse(f) > 170) || (int.Parse(f) < 1))
                    return false;
                if (IsValidPhone(a) == false)
                    return false;
                if ((b.Length > 50) || (b.Length < 1) || (IsValid(b) == false))
                    return false;
                if ((c.Length > 50) || (c.Length < 1) || (IsValid(c) == false))
                    return false;
                return true;
            }           
        }
        public bool check_transport(string a, string b, string c, string d, string e)
        {
            if ((check_digit(b) == false) || (check_digit(c) == false) || (a=="") || (b == "") || (c == "") || (d == "") || (e == ""))
                return false;
            else
            {
                for (int i = 0; i < a.Length; i++)
                    if (!IsLetter(a[i]))
                        return false;
                if ((int.Parse(b) > 30) || (int.Parse(b) < 1))
                    return false;
                if ((int.Parse(c) < 1980) || (int.Parse(c) > 2022))
                    return false;
                if ((d.Length > 50) || (d.Length < 1) || (IsValid(d) == false))
                    return false;
                if ((e.Length > 50) || (e.Length < 1) || (IsValid(e) == false))
                    return false;
                return true;
            }            
        }
        public bool check_transport_ts(string a, string b, string c)
        {
            if ((check_digit(a) == false) || (a == "") || (b == "") || (c == ""))
                return false;
            else
            {
                if ((int.Parse(a) < 1980) || (int.Parse(a) > 2022))
                    return false;
                if ((b.Length > 50) || (b.Length < 1) || (IsValid(b) == false))
                    return false;
                if ((c.Length > 50) || (c.Length < 1) || (IsValid(c) == false))
                    return false;
                return true;
            }           
        }
        public bool check_lessor_ts(string a, string b)
        {
            if ((a == "") || (b == ""))
                return false;
            else
            {
                if ((a.Length > 50) || (a.Length < 1) || (IsValid(a) == false))
                    return false;
                if ((b.Length > 50) || (b.Length < 1) || (IsValid(b) == false))
                    return false;
            }           
            return true;
        }
        public bool check_report_search(string a, string b, string c, string d, string e, string f, string g)
        {
            if ((check_digit(b) == false) || (check_digit(c) == false) || (check_digit(d) == false) || (check_digit(e) == false)
                || (check_digit(f) == false) || (check_digit(g) == false) || (a == "") || (b == "") || (c == "") || (d == "") || (e == "") || (f == "") || (g == ""))
                return false;
            else
            {
                for (int i = 0; i < a.Length; i++)
                    if (!IsLetter(a[i]))
                        return false;
                if ((int.Parse(b) < 1) || (int.Parse(b) > 30))
                    return false;
                if ((int.Parse(c) < 1) || (int.Parse(c) > 30))
                    return false;
                if (int.Parse(b) > int.Parse(c))
                    return false;
                if ((int.Parse(d) < 1) || (int.Parse(d) > 100000))
                    return false;
                if ((int.Parse(e) < 1) || (int.Parse(e) > 100000))
                    return false;
                if (int.Parse(d) > int.Parse(e))
                    return false;
                if ((int.Parse(f) < 1) || (int.Parse(f) > 170))
                    return false;
                if ((int.Parse(g) < 1) || (int.Parse(g) > 170))
                    return false;
                if (int.Parse(f) > int.Parse(g))
                    return false;
                return true;
            }           
        }
        public bool IsValidPhone(string s)
        {
            if (string.IsNullOrEmpty(s) || (s.Length != 11))
            {
                return false;
            }
            if (s[0] != '8')
                return false;
            for (int i = 1; i < s.Length; i++)
            {
                if (!char.IsDigit(s[i]))
                {
                    return false;
                }
            }
            return true;
        }
        static bool IsLetter(char ch)
        {
            return (ch >= 'A' && ch <= 'Z') || (ch >= 'a' && ch <= 'z') || (ch >= 'А' && ch <= 'Я') || (ch >= 'а' && ch <= 'я');
        }
        public static bool IsValid(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }

            for (int i = 0; i < s.Length; i++)
            {
                if (!IsLetter(s[i]) && s[i] != ' ' && !char.IsDigit(s[i]) && s[i] != '-')
                {
                    return false;
                }
            }
            return true;
        }
        public bool check_digit(string a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                if (!char.IsDigit(a[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
