namespace ConsoleVerification
{
    using ForgetTheMilk.Models;
    using NUnit.Framework;
    using System;
    using System.Reflection;

    public class CreateTaskTests :AssertionHelper
    {
        [Test]
        public void DescriptionAndNoDueDate()
        {
            string input = "Pickup the groceries";

            Task task = new Task(input, default(DateTime));

            Assert.AreEqual(input, task.Description);
            Assert.AreEqual(null, task.DueDate);
        }

        [Test]
        public void TestMayDueDateDoesWrapYear()
        {
            string input = "Pickup the groceries may 5";

            DateTime today = new DateTime(2015, 5, 31);

            Task task = new Task(input, today);

            Assert.AreEqual(new DateTime(2016, 5, 5), task.DueDate);
            Assert.AreEqual(input, task.Description);
        }

        [Test]
        public void TestMayDueDateDoesNotWrapYear()
        {
            string input = "Pickup the groceries may 5";

            DateTime today = new DateTime(2015, 5, 4);

            Task task = new Task(input, today);

            string descriptionShouldBe = input;
            DateTime? dueDateShouldBe = new DateTime(today.Year, 5, 5);

            bool success = task.Description == descriptionShouldBe
                            && dueDateShouldBe.Value == task.DueDate.Value;

            string failureMessage = $"Description: '{ task.Description }' should be '{ descriptionShouldBe }'"
                        + Environment.NewLine
                        + $"Due Date: { task.DueDate.Value } should be { dueDateShouldBe.Value }";

            Assert.AreEqual(dueDateShouldBe, task.DueDate);
            
        }

    }
}
