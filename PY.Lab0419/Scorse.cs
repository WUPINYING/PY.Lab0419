using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PY.Lab0419
{
	internal class Scorse
	{
		/// <summary>
		/// 050 設計一個類別來表示一個學生，擁有姓名、年齡、成績三個屬性，以及計算是否及格的方法。
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Student student = new Student();
			string name = student.name = "Tina";
			int age = student.age = 20;
			int scorse = student.scores = 60;

			Console.WriteLine($"姓名:{name}，年齡:{age}，成績{scorse}");
			Student.IsPass(scorse);
		}
		public class Student
		{
			public string name { get; set; }
			public int age { get; set; }
			public int scores { get; set; }

			public static void IsPass(int scorse)
			{
				if (scorse > 100 || scorse < 0)
				{
					Console.WriteLine("您的成績異常!");
				}
				else if (scorse >= 60)
				{
					Console.WriteLine("恭喜! 您的成績及格!");
				}
				else
				{
					Console.WriteLine("您的成績不及格!");
				}
			}
		}
	}
}
