using LeaveTimes_2;
using static LeaveTimes_2.LeaveTimes;

namespace LeaveTimes_2_Tests
{
    public class LeaveTimes
    {
        [SetUp]
        public void Setup()
        {
        }

        private DateTime dtStart = new DateTime(2023, 5, 20, 8, 0, 0);
        private DateTime dtEnd = new DateTime(2023, 5, 20, 18, 0, 0);


        [Test]
        public void GetStartTime_取得請假開始時間()
        {
            DateTime expected = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, dtStart.Hour + 1, dtStart.Minute, dtStart.Minute);
            DateTime actual = Leave.GetStartTime(dtStart);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetEndTime_取得請假結束時間()
        {
            DateTime expected = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, dtEnd.Hour, dtEnd.Minute, dtEnd.Minute);
            DateTime actual = Leave.GetEndTime(dtEnd);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetToTalLeave_計算請假時數()
        {
            int expected = -14;
            double actual = Leave.GetToTalLeave(dtStart, dtEnd);
            Assert.AreEqual(expected, actual);
        }
    }
}