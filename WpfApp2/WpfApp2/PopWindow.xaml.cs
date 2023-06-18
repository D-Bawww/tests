using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для PopWindow.xaml
    /// </summary>
    public partial class PopWindow : Window
    {
        public PopWindow(AVL1 tr)
        {
            InitializeComponent();
            text.Text = "Арендодатели с транспортом";
            grid.Visibility = Visibility.Collapsed;
            list.Visibility = Visibility.Visible;
            show(tr.node.head, "", true);
        }
        public PopWindow(AVL2 tr)
        {
            InitializeComponent();
            text.Text = $"Арендодателей с транспортом найдено";
            grid.Visibility = Visibility.Collapsed;
            list.Visibility = Visibility.Visible;
            show(tr.node.head, "", true);
        }
        public PopWindow(List<Lessor> ll)
        {
            InitializeComponent();
            text.Text = $"Арендодателей найдено";
            grid.ItemsSource = ll;
        }
        public PopWindow(Transport ts)
        {
            InitializeComponent();
            text.Text = $"Транспорт найден";
            grid.Visibility = Visibility.Collapsed;
            list.Visibility = Visibility.Visible;
            list.Items.Add(ts.all());
        }
        public PopWindow(Hashtable ht)
        {
            InitializeComponent();
            text.Text = "Транспорт";
            List<string> res = ht.print();
            grid.Visibility = Visibility.Collapsed;
            list.Visibility = Visibility.Visible;
            for (int i = 0; i < res.Count; i++)
                list.Items.Add(res[i]);
        }

        public void show(Un1 u, string indent, bool l)
        {
            if (u != null)
            {
                string str = "";
                str += indent;
                if (l)
                {
                    str += "R----";
                    indent += " ";
                }
                else
                {
                    str += "L----";
                    indent += "| ";
                }
                list.Items.Add(str + $"{u.l.head.lessor.all()}({u.l.size()})");

                show(u.left, indent, false);
                show(u.right, indent, true);
            }
        }
        public void show(Un2 u, string indent, bool l)
        {
            if (u != null)
            {
                string str = "";
                str += indent;
                if (l)
                {
                    str += "R----";
                    indent += " ";
                }
                else
                {
                    str += "L----";
                    indent += "| ";
                }
                list.Items.Add(str + $"{u.l.head.lessor.all()}({u.l.size()})");

                show(u.left, indent, false);
                show(u.right, indent, true);
            }
        }
    }


}
