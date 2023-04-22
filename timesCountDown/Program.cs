using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace timesCountDown
{
	internal class Program
	{
		/// <summary>
		/// 100 設計一個類別來表示一個簡單的倒數計時器，擁有設定時間、開始計時、停止計時等方法。
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Console.Write("哈囉!這是一個倒數計時器\r\n請輸入倒數時間(格式為小時:分鐘:秒):");
			string input = Console.ReadLine();

			CountDown countDown = new CountDown();
			try
			{
				countDown.times = Convert.ToDateTime(input);
			}
			catch (Exception ex)
			{
				Console.WriteLine("程式發生錯誤:" + ex.Message);
			};

			Console.Write("請輸入s開始倒數:");
			string start = Console.ReadLine();

			countDown.start(start);

			string end = Console.ReadLine();
			countDown.start(end);
		}
		class CountDown
		{
			public DateTime times { get; set; }

			public void start(string s)
			{
				bool running = true;
				if (s == "s")
				{
					TimeSpan cd = TimeSpan.Parse(times.ToString("HH:mm:ss"));
					Console.WriteLine("倒數計時開始！按任一鍵可以結束倒數!");
					do
					{
						Console.Write("\r" + cd);
						cd = cd.Subtract(TimeSpan.FromSeconds(1)); //減去一秒鐘
						Thread.Sleep(1000); //讓程式暫停執行 1 秒，也就是說每次執行倒數計時的迴圈都會等待 1 秒後才會繼續執行下一次

						if (Console.KeyAvailable) //檢查鍵盤是否有按下的鍵，如果有按下鍵則回傳 true，反之回傳 false
						{
							ConsoleKeyInfo key = Console.ReadKey(true);
							running = false;
							Console.WriteLine("\r\n倒數計時結束！");
						}
					}
					while (running);
				}
				else
				{
					Console.WriteLine("不是說了按s!?");
				}

			}
		}
	}
}

