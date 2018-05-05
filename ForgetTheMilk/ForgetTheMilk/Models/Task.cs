namespace ForgetTheMilk.Models
{
    using System;
    using System.Text.RegularExpressions;

    public class Task
    {
        public Task(string task, DateTime today)
        {
            Description = task;
            Regex reg = new Regex(@"may\s(\d)");

            bool hasDueDate = reg.IsMatch(task);

            if (hasDueDate)
            {
                Match regMatches = reg.Match(task);
                int day = Convert.ToInt32(regMatches.Groups[1].Value);
                DueDate = new DateTime(today.Year, 5, day);

                if(DueDate < today)
                {
                    DueDate = DueDate.Value.AddYears(1);
                }
            }
        }
        public string Description { get; set; }

        public DateTime? DueDate { get; set; }
    }
}