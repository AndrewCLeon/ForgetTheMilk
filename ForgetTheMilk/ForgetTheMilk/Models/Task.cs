namespace ForgetTheMilk.Models
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    public class Task
    {
        public Task(string task, DateTime today, ILinkValidator linkValidator = null)
        {
            Description = task;
            Regex reg = new Regex(@"(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)\s(\d+)");
            bool hasDueDate = reg.IsMatch(task);
            if (hasDueDate)
            {
                Match dueDate = reg.Match(task);
                var monthInput = dueDate.Groups[1].Value;
                var month = DateTime.ParseExact(monthInput, "MMM", CultureInfo.CurrentCulture).Month;
                var day = Convert.ToInt32(dueDate.Groups[2].Value);
                var year = today.Year;
                if(month < today.Month || (month == today.Month && day < today.Day))
                {
                    year++;
                }
                if(day <= DateTime.DaysInMonth(year, month))
                {
                    DueDate = new DateTime(year, month, day);
                }
            }

            var linkPattern = new Regex(@"(http://[^\s]+)");
            var hasLink = linkPattern.IsMatch(task);
            if (hasLink)
            {
                var link = linkPattern.Match(task).Groups[1].Value;
                linkValidator.Validate(link);
                Link = link;
            }

            var priorityPattern = new Regex(@"(!\d+)");
            var hasPriority = priorityPattern.IsMatch(task);
            if (hasPriority)
            {
                var priority = priorityPattern.Match(task).Groups[1].Value.Replace("!", "");
                bool canParse = int.TryParse(priority, out int tempPriority);
                if(canParse)
                {
                    Priority = tempPriority;
                }
            }
        }

        public string Description { get; set; }

        public DateTime? DueDate { get; set; }

        public string Link { get; set; }

        public int Priority { get; set; }
    }
}