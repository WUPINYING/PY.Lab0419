using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace LeaveTimes_2
{
    public class LeaveTimes
    {
        static void Main(string[] args)
        {
            Console.Write("嗨嗨!請輸入想要請假的時間\r\n請假起始日期 年/月/日 時/分/秒:");
            string inputkStart = Console.ReadLine();
            bool okStart = DateTime.TryParse(inputkStart, out DateTime startTime);
            if (okStart)
            {
                startTime = Leave.GetStartTime(startTime);
                Console.WriteLine(startTime);
            }
            else
            {
                Console.WriteLine("格式不正確!");
            }

            Console.Write("請假結束時間 年/月/日 時/分/秒:");
            string inputEnd = Console.ReadLine();
            bool okEnd = DateTime.TryParse(inputEnd, out DateTime endTime);
            if (okEnd && endTime >= startTime)
            {
                endTime = Leave.GetStartTime(endTime);
                Console.WriteLine(endTime);
                double leaveResult = Leave.GetToTalLeave(startTime, endTime);
                Console.WriteLine($"請假成功!請假開始時間:{startTime}，請假的結束時間:{endTime}，總共請假時數{leaveResult}");
            }
            else if (okEnd && endTime <= startTime)
            {
                Console.WriteLine("請假結束時間要晚於請假開始時間!");
            }
            else
            {
                Console.WriteLine("格式不正確!");
            }
            Console.Read();
        }
        public class Leave
        {
            public static DateTime GetStartTime(DateTime dtStart)
            {
                DateTime mor = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, 9, 0, 0);
                DateTime noon = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, 12, 0, 0);
                DateTime aft = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, 13, 0, 0);

                if (dtStart < DateTime.Now) // 如果設置的值早於今天，拋出例外
                {
                    throw new ArgumentException("請假日期不能早於今天！");
                }

                if (dtStart.TimeOfDay < mor.TimeOfDay)//開始請假時間早於9:00上班時間就從9:00開始算
                {
                    dtStart = mor; //9:00
                }

                if (dtStart.TimeOfDay > noon.TimeOfDay && dtStart.TimeOfDay < aft.TimeOfDay)//開始請假時間晚於12:00、早於13:00上班時間就從13:00開始算
                {
                    dtStart = aft; //13:00
                }
                return dtStart;
            }

            public static DateTime GetEndTime(DateTime dtEnd)
            {
                DateTime mor = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, 9, 0, 0);
                DateTime noon = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, 12, 0, 0);
                DateTime aft = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, 13, 0, 0);
                DateTime nig = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, 18, 0, 0);


                if (dtEnd < DateTime.Now) // 如果設置的值早於今天或是請假時間，拋出例外
                {
                    throw new ArgumentException("請假結束日期不可早於今天!");
                }

                if (dtEnd.TimeOfDay > noon.TimeOfDay && dtEnd.TimeOfDay <= aft.TimeOfDay)  //結束請假時間晚於12:00、早於13:00就算到12:00
                {
                    dtEnd = noon;
                }

                if (dtEnd.TimeOfDay > nig.TimeOfDay)  //結束請假時間晚於18:00下班時間就算到18:00
                {
                    dtEnd = nig;
                }
                return dtEnd;
            }
            public static double GetToTalLeave(DateTime dtStart, DateTime dtEnd)
            {
                int breakTime = 1; //固定休息時段1小時
                int bkTime = 15; //未上班時段15小時(18:00-隔日9:00)
                TimeSpan actulToTalLeave = dtEnd - dtStart;  //請假時段
                double actLeave = actulToTalLeave.TotalHours - breakTime;//請假時段-休息時段
                if (actLeave > 8)
                {
                    actLeave = breakTime - bkTime; // 請假時段 - 隔日未上班時段
                }
                return actLeave;
            }
        }
    }
}
