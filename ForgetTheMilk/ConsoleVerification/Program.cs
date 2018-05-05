using ForgetTheMilk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleVerification
{
    class Program
    {
        static void Main(string[] args)
        {
            ProgramVersion ver1 = new ProgramVersion();

            ver1.Major = 1;
            ver1.Minor = 2;
            ver1.Build = 3;
            ver1.Revision = 4;

            ProgramVersion ver2 = new ProgramVersion();

            ver2.Major = 1;
            ver2.Minor = 2;
            ver2.Build = 3;
            ver2.Revision = 4;

            bool matches = ver1 == ver2;

            while (true)
            {
                TestMayDueDateDoesWrapYear();
                TestMayDueDateDoesNotWrapYear();

                Console.ReadLine();
            }
        }



        public static void PrintOutcome(bool success, string failureMessage)
        {
            if (success)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("Error");
                Console.WriteLine(failureMessage);
            }
            Console.WriteLine();
        }

    }
}
