namespace ConsoleVerification
{
    using ForgetTheMilk.Models;
    using NUnit.Framework;
    using System;

    public class CreateTaskTests : AssertionHelper
    {
        [Test]
        [TestCase("Pickup the groceries")]
        public void DescriptionAndNoDueDate(string input)
        {
            Task task = new Task(input, default(DateTime));
            
            Expect(task.DueDate, Is.Null);
            Expect(task.Description, Is.EqualTo(input));
        }

        [Test]
        [TestCase("Pickup the groceries may 5 - as of 2015-05-31", 2016)]
        [TestCase("Pickup the groceries apr 5 - as of 2015-05-31", 2016)]
        public void TestMayDueDateDoesWrapYear(string input, int expectedYear)
        {
            DateTime today = new DateTime(2015, 5, 31);

            Task task = new Task(input, today);

            Expect(task.DueDate.Value.Year, Is.EqualTo(expectedYear));
        }

        [Test]
        [TestCase("Pickup the groceries may 5 - as of 2015-05-04")]
        public void TestMayDueDateDoesNotWrapYear(string input)
        {
            DateTime today = new DateTime(2015, 5, 4);

            Task task = new Task(input, today);
            
            Expect(task.Description, Is.EqualTo(input));
            Expect(task.DueDate.HasValue, Is.EqualTo(true));
            Expect(task.DueDate.Value, Is.EqualTo(new DateTime(today.Year, 5, 5)));
        }

        [Test]
        [TestCase("Groceries jan 5", 1)]
        [TestCase("Groceries feb 5", 2)]
        [TestCase("Groceries mar 5", 3)]
        [TestCase("Groceries apr 5", 4)]
        [TestCase("Groceries may 5", 5)]
        [TestCase("Groceries jun 5", 6)]
        [TestCase("Groceries jul 5", 7)]
        [TestCase("Groceries aug 5", 8)]
        [TestCase("Groceries sep 5", 9)]
        [TestCase("Groceries oct 5", 10)]
        [TestCase("Groceries nov 5", 11)]
        [TestCase("Groceries dec 5", 12)]
        public void DueDate(string input, int expectedMonth)
        {
            var task = new Task(input, default(DateTime));

            Expect(task.DueDate, Is.Not.Null);
            Expect(task.DueDate.Value.Month, Is.EqualTo(expectedMonth));
        }


        [Test]
        public void TwoDigitDay_ParseBothDigits()
        {
            var input = "Groceries apr 10";

            Task task = new Task(input, default(DateTime));

            Expect(task.DueDate.HasValue, Is.EqualTo(true));
            Expect(task.DueDate.Value.Day, Is.EqualTo(10));
        }

        [Test]
        public void DayIsPastTheLastDayOfTheMonth_DoesNotParseDueDate()
        {
            var input = "Groceries apr 44";
            Task task = new Task(input, default(DateTime));

            Expect(task.DueDate, Is.Null);
        }

        [Test]
        public void AddFeb29TaskInMarchOfYearBeforeLeapYear_ParseDueDate()
        {
            var input = "Groceries feb 29";
            DateTime today = new DateTime(2015, 5, 5);
            Task task = new Task(input, today);

            Expect(task.DueDate.Value.Day, Is.EqualTo(29));
        }

        [Test]
        [TestCase("Groceries feb 29 !1", 1)]
        [TestCase("Groceries feb 29 !10", 10)]
        public void PrioritySupplied_CanParsePriority(string input, int expectedPriority)
        {
            Task task = new Task(input, default(DateTime));

            Expect(task.Priority, Is.EqualTo(expectedPriority));
        }
    }
}
