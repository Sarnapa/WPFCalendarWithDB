using System;
using NUnit.Framework;
using WPFCalendarWithDB.ViewModel;

namespace WPFCalendarWithDBTest
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void GoingNextWeekTest()
        {
            var mainVm = new MainViewModel();
            var firstDay = mainVm.DaysList[0].Date;
            mainVm.GetNextWeek.Execute(null);
            Assert.AreEqual(firstDay.AddDays(7), mainVm.DaysList[0].Date);
        }

        [Test]
        public void GoingPrevWeekTest()
        {
            var mainVm = new MainViewModel();
            var firstDay = mainVm.DaysList[0].Date;
            mainVm.GetPrevWeek.Execute(null);
            Assert.AreEqual(firstDay.AddDays(-7), mainVm.DaysList[0].Date);
        }

        [Test]
        public void PopupMenuTest()
        {
            var mainVm = new MainViewModel();
            Assert.AreEqual(false, mainVm.IsPopup);
            mainVm.PopupCommand.Execute(null);
            Assert.AreEqual(true, mainVm.IsPopup);
            mainVm.PopupCommand.Execute(null);
            Assert.AreEqual(false, mainVm.IsPopup);
        }
    }
}
