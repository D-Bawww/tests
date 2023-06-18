using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
	public class Unit
	{
		public Lessor lessor;
		public Unit next;
		public Unit() { }
		public Unit(Lessor l)
		{
			lessor = l;
			next = null;
		}
	}
	public class list
	{
		public Unit head;
		public list()
		{
			head = null;
		}
		public bool isEmpty()
		{
			if (head == null)
				return true;
			else return false;
		}
		public void add(Lessor ll)
		{
			Unit l = new Unit(ll);
			if (isEmpty())
				head = l;
			else
			{
				Unit a = head;
				while (a.next != null)
					a = a.next;
				a.next = l;
			}
		}
		public void del(Lessor ll)
		{
			if (isEmpty())
			{
			}
			else
			{
				if (head.lessor == ll)
					head = head.next;
				else
				{
					Unit l = head;
					while (l.next != null)
					{
						if (l.next.lessor == ll)
						{
							Unit a = l.next;
							l.next = a.next;
							break;
						}
						l = l.next;
					}
				}
				GC.Collect();
			}
		}
		public bool find(Lessor ll)
		{
			if (isEmpty())
				return false;
			else
			{
				Unit l = head;
				while ((l.lessor != ll) && (l.next != null))
					l = l.next;

				if (l.lessor == ll)
					return true;
				else return false;
			}
		}
		public int size()
		{
			if (isEmpty())
				return 0;
			else
			{
				int s = 1;
				Unit l = head;
				while (l.next != null)
				{
					l = l.next;
					s++;
				}
				return s;
			}
		}
		public List<Lessor> lessors()
		{
			List<Lessor> res = new List<Lessor>();

			if (isEmpty())
				return null;
			else
			{
				Unit a = head;
				while (a.next != null)
				{
					res.Add(a.lessor);
					a = a.next;
				}
				res.Add(a.lessor);
				return res;
			}
		}
	}
}
