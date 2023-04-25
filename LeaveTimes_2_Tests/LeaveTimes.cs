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
        public void GetStartTime_���o�а��}�l�ɶ�()
        {
            DateTime expected = new DateTime(dtStart.Year, dtStart.Month, dtStart.Day, dtStart.Hour + 1, dtStart.Minute, dtStart.Minute);
            DateTime actual = Leave.GetStartTime(dtStart);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetEndTime_���o�а������ɶ�()
        {
            DateTime expected = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, dtEnd.Hour, dtEnd.Minute, dtEnd.Minute);
            DateTime actual = Leave.GetEndTime(dtEnd);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetToTalLeave_�p��а��ɼ�()
        {
            int expected = -14;
            double actual = Leave.GetToTalLeave(dtStart, dtEnd);
            Assert.AreEqual(expected, actual);
        }
    }
}