using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTimes
{
	internal class Program
	{
		/// <summary>
		/// 請練習寫一個class具備計算請假時數的method, 並寫單元測試它, 確認它是對的
		/// 計算一天總請假時數,每天上班時間是9~18點,12~13點是休息時間
		/// 若請假9 ~18點,傳回8(小時)
		/// 若請假9 ~17點,傳回7(小時)
		/// 若請假9 ~12點,傳回3(小時)
		/// 若請假9 ~13點,傳回3(小時),因為午休到13點
		/// 若請假12~14點,傳回1(小時),因為午休到13點
		/// 若請假8~18點,傳回8(小時), 因為9點之後才算上班
		/// 若請假9~23點,傳回8(小時),因為18點之後就下班了
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			Console.Write("嗨嗨!請輸入想要請假的時間\r\n請假起始日期 年/月/日:");
			string inputkStart = Console.ReadLine();
			bool okStart = DateTime.TryParse(inputkStart, out DateTime startTime);
			Leave leaveStart = new Leave(); //呼叫Leave class檢查輸入的請假開始時間
			leaveStart.LeaveStart = startTime;

			Console.Write("請假結束時間:");
			string inputEnd = Console.ReadLine();
			bool okEnd = DateTime.TryParse(inputEnd, out DateTime endTime);
			Leave leaveEnd = new Leave();//呼叫Leave class檢查輸入的請假結束時間
			leaveEnd.LeaveEnd = endTime;

			double leaveResult = Leave.GetToTalLeave(leaveStart.LeaveStart, leaveEnd.LeaveEnd);
			Console.WriteLine($"請假成功!請假開始時間:{leaveStart.LeaveStart}，請假的結束時間:{leaveEnd.LeaveEnd}，總共請假時數{leaveResult}");

			Console.Read();
		}
		public class Leave //計算請假時數
		{
			private DateTime _leaveStart;
			private DateTime _leaveEnd;
			public DateTime LeaveStart //請假開始時間
			{
				get
				{
					return _leaveStart;
				}
				set
				{
					DateTime mor = new DateTime(value.Year, value.Month, value.Day, 9, 0, 0);
					DateTime noon = new DateTime(value.Year, value.Month, value.Day, 12, 0, 0);
					DateTime aft = new DateTime(value.Year, value.Month, value.Day, 13, 0, 0);

					if (value < DateTime.Now) // 如果設置的值早於今天，拋出例外
					{
						throw new ArgumentException("請假日期不能早於今天！");
					}

					if (value.TimeOfDay < mor.TimeOfDay)//開始請假時間早於9:00上班時間就從9:00開始算
					{
						value = mor; //9:00
					}

					if (value.TimeOfDay > noon.TimeOfDay && value.TimeOfDay < aft.TimeOfDay)//開始請假時間晚於12:00、早於13:00上班時間就從13:00開始算
					{
						value = aft; //13:00
					}
					_leaveStart = value;
				}
			}

			public DateTime LeaveEnd //請假結束時間
			{
				get
				{
					return _leaveEnd;
				}
				set
				{
					DateTime mor = new DateTime(value.Year, value.Month, value.Day, 9, 0, 0);
					DateTime noon = new DateTime(value.Year, value.Month, value.Day, 12, 0, 0);
					DateTime aft = new DateTime(value.Year, value.Month, value.Day, 13, 0, 0);
					DateTime nig = new DateTime(value.Year, value.Month, value.Day, 18, 0, 0);

					if (value < DateTime.Now) // 如果設置的值早於今天或是請假時間，拋出例外
					{
						throw new ArgumentException("請假結束日期不可早於今日!");
					}
					else if (value.Date < _leaveStart.Date)
					{
						throw new ArgumentException("請假結束日期不可早於請假日期!");
					}
					else if (value.TimeOfDay < _leaveStart.TimeOfDay)
					{
						throw new ArgumentException("請假結束日期不可早於請假日期!");
					}

					if (value.TimeOfDay > noon.TimeOfDay && value.TimeOfDay <= aft.TimeOfDay)  //結束請假時間晚於12:00、早於13:00就算到12:00
					{
						value = noon;
					}

					if (value.TimeOfDay > nig.TimeOfDay)  //結束請假時間晚於18:00下班時間就算到18:00
					{
						value = nig;
					}
					_leaveEnd = value;
				}
			}

			public static double GetToTalLeave(DateTime startTime, DateTime endTime) //計算請假時間
			{
				TimeSpan actulToTalLeave = endTime - startTime;  //請假時段
				double actLeave = actulToTalLeave.TotalHours - 1;//請假時段-固定休息時段1小時
				return actLeave;
			}
		}
	}
}
