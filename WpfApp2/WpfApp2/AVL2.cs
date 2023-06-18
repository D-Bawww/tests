using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
	public class AVL2
	{
		public Un2 node;
		public AVL2()
		{
			node = new Un2();
		}
		public AVL2(List<Lessor> ll)
		{
			bool f = true;
			node = new Un2();
			Un2 a = node.head;

			for (int i = 0; i < ll.Count; i++)
				node.add(ll[i], ref a, ref f);
			node.head = a;
		}
	}
	public class Un2
	{
		int k;
		public list l = new list();
		public Un2 right, left;
		public Un2 head = null;
		public Un2() { }

		public void add(Lessor ll, ref Un2 unit, ref bool f)
		{
			if (!(true)) Console.WriteLine("wrong date");
			else
			{
				if (unit == null)
				{
					Un2 t = new Un2();
					t.l.head = new Unit(ll);

					unit = t;
					f = true;
				}
				else if (ll.price == unit.l.head.lessor.price)
				{
					unit.l.add(ll);
					f = false;
				}
				else
				{
					if (ll.price < unit.l.head.lessor.price)
					{
						add(ll, ref unit.left, ref f);

						if (f)
						{
							if (unit.k == 1)
							{
								unit.k = 0;
								f = false;
							}
							else if (unit.k == 0)
							{
								unit.k = -1;
							}
							else
							{
								Un2 a = unit.left;
								if (a != null)
								{
									if (a.k == -1)
									{
										unit.left = a.right;
										a.right = unit;
										unit.k = 0;
										unit = a;
									}
									else
									{
										Un2 b = a.right;
										a.right = b.left;
										b.left = a;
										unit.left = b.right;
										b.right = unit;

										if (b.k == -1)
											unit.k = 1;
										else unit.k = 0;
										if (b.k == 1)
											a.k = -1;
										else a.k = 0;
										unit = b;
									}
									unit.k = 0;
									f = false;
								}
							}
						}
					}
					if (ll.price > unit.l.head.lessor.price)
					{
						add(ll, ref unit.right, ref f);

						if (f)
						{
							if (unit.k == -1)
							{
								unit.k = 0;
								f = false;
							}
							else if (unit.k == 0)
							{
								unit.k = 1;
							}
							else
							{
								Un2 a = unit.right;
								if (a != null)
								{
									if (a.k == 1)
									{
										unit.right = a.left;
										a.left = unit;
										unit.k = 0;
										unit = a;
									}
									else
									{
										Un2 b = a.left;
										a.left = b.right;
										b.right = a;
										unit.right = b.left;
										b.left = unit;

										if (b.k == 1)
											unit.k = -1;
										else unit.k = 0;
										if (b.k == -1)
											a.k = 1;
										else a.k = 0;
										unit = b;
									}
									unit.k = 0;
									f = false;
								}
							}
						}
					}
				}
				head = unit;
			}
		}
		public Un2 delete(Lessor ll, Un2 unit, ref bool f, ref bool fl)
		{
			if (false)
			{
				Console.WriteLine("");
				return null;
			}
			else
			{
				if (unit == null)
				{
					Console.WriteLine("tree is empty");
					return null;
				}
				else
				{
					if (ll.price < unit.l.head.lessor.price)
					{
						unit.left = delete(ll, unit.left, ref f, ref fl);
						if (f)
							balL(ref unit, ref f);
					}
					else if (ll.price > unit.l.head.lessor.price)
					{
						unit.right = delete(ll, unit.right, ref f, ref fl);
						if (f)
							balR(ref unit, ref f);
					}
					else if (unit.l.size() > 1 && !fl)
					{
						f = false;
						if (unit.l.find(ll))
							unit.l.del(ll);
					}
					else if (unit.l.find(ll))
					{
						if ((unit.left != null) && (unit.right != null))
						{
							unit.l = findMax(unit.left).l;
							fl = true;

							unit.left = delete(unit.l.head.lessor, unit.left, ref f, ref fl);
							if (f)
								balL(ref unit, ref f);
						}
						else
						{
							if (unit.left != null)
							{
								fl = false;
								unit.left.k = unit.k;
								unit = unit.left;
								if (f)
									balL(ref unit, ref f);
							}
							else if (unit.right != null)
							{
								fl = false;
								unit.right.k = unit.k;
								unit = unit.right;
								if (f)
									balR(ref unit, ref f);
							}
							else
							{
								unit = null;
								fl = false;
							}
						}
					}
					else Console.WriteLine("key do not exist");
					head = unit;
					return unit;
				}
			}
		}
		void balR(ref Un2 unit, ref bool f)
		{
			Un2 a, b;
			if (unit.k == 1)
			{
				unit.k = 0;
			}
			else if (unit.k == 0)
			{
				unit.k = -1;
				f = false;
			}
			else
			{
				a = unit.left;

				if (a.k <= 0)
				{
					unit.left = a.right;
					a.right = unit;
					if (a.k == 0)
					{
						unit.k = -1;
						a.k = 1;
						f = false;
					}
					else
					{

						unit.k = 0;
						a.k = 0;
					}
					unit = a;
				}
				else
				{
					b = a.right;
					a.right = b.left;
					b.left = a;
					unit.left = b.right;
					b.right = unit;

					if (b.k == -1)
						unit.k = 1;
					else unit.k = 0;
					if (b.k == 1)
						a.k = -1;
					else a.k = 0;

					unit = b;
					b.k = 0;
				}
			}
		}
		void balL(ref Un2 unit, ref bool f)
		{
			Un2 a, b;
			if (unit.k == -1)
			{
				unit.k = 0;
			}
			else if (unit.k == 0)
			{
				unit.k = 1;
				f = false;
			}
			else
			{
				a = unit.right;

				if (a.k >= 0)
				{
					unit.right = a.left;
					a.left = unit;
					if (a.k == 0)
					{
						unit.k = 1;
						a.k = -1;
						f = false;
					}
					else
					{
						unit.k = 0;
						a.k = 0;
					}
					unit = a;
				}
				else
				{
					b = a.left;
					a.left = b.right;
					b.right = a;
					unit.right = b.left;
					b.left = unit;

					if (b.k == 1)
						unit.k = -1;
					else unit.k = 0;
					if (b.k == -1)
						a.k = 1;
					else a.k = 0;

					unit = b;
					b.k = 0;
					head = b;
				}
			}
		}
		public Un2 findMax(Un2 u)
		{
			if (u == null)
			{
				Console.WriteLine("tree is empty");
				return null;
			}
			else
			{
				while (u.right != null)
					u = u.right;

				return u;
			}
		}
        public bool find(Lessor ll, Un2 unit)
		{
			if (false)
			{
				Console.WriteLine("wrong data");
				return false;
			}
			else
			{
				while ((unit != null) && (ll.price != unit.l.head.lessor.price))
				{
					if (ll.price < unit.l.head.lessor.price)
					{
						unit = unit.left;
					}
					else if (ll.price > unit.l.head.lessor.price)
					{
						unit = unit.right;
					}
				}
				if (unit != null && (ll.price == unit.l.head.lessor.price))
				{
					if (unit.l.find(ll))
						return true;
					else return false;
				}
				return unit != null;
			}
		}
        public void findTransport(int pr1, int pr2, int per1, int per2, Un2 unit, ref List<Lessor> res, ref int k)
		{
			if (unit != null)
			{
				if ((pr1 <= unit.l.head.lessor.price) && (pr2 >= unit.l.head.lessor.price))
                {
					List<Lessor> temp = unit.l.lessors();
					for (int i = 0; i < temp.Count; i++)
						if ((temp[i].period <= per2) && (temp[i].period >= per1))
							res.Add(temp[i]);
				}
				if (unit.l.head.lessor.price < pr2)
				{
					k++;
					findTransport(pr1, pr2, per1, per2, unit.right, ref res, ref k);
				}

				if (unit.l.head.lessor.price > pr1)
				{
					k++;
					findTransport(pr1, pr2, per1, per2, unit.left, ref res, ref k);
				}
			}
		}
		public void show(Un2 u, ref List<List<Lessor>> a)
		{
			if (!(Object.ReferenceEquals(u, null)))
			{
				if (!(Object.ReferenceEquals(u.right, null)))
					show(u.right, ref a);

				a.Add(u.l.lessors());

				if (!(Object.ReferenceEquals(u.left, null)))
					show(u.left, ref a);
			}
            else Console.WriteLine("tree is empty");
        }
    }
}
