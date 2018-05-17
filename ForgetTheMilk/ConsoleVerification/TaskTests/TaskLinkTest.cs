using ForgetTheMilk.Models;
using NUnit.Framework;
using Rhino.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleVerification
{
    public class TaskLinkTest : AssertionHelper
    {
        public class IgnoreLinkValidator : ILinkValidator
        {
            public void Validate(string link)
            {

            }
        }

        [Test]
        public void CreateTask_DescriptionWithALink_SetLink()
        {
            var link = "http://www.google.com";
            var input = "test " + link;

            ILinkValidator mockValidator = MockRepository.GenerateMock<ILinkValidator>();

            Task task = new Task(input, default(DateTime), mockValidator);

            Expect(task.Link, Is.EqualTo("http://www.google.com"));
        }

        [Test]
        public void Validate_InvalidUrl_ThrowsException()
        {
            var input = "http://doesnotexistdotcom.com";

            ILinkValidator mockValidator = MockRepository.GenerateMock<ILinkValidator>();

            mockValidator.Expect(mock => mock.Validate(input)).Throw(new ApplicationException("Invalid link " + input));

            Expect(() => new Task(input, default(DateTime), mockValidator),
                Throws.Exception.With.Message.EqualTo("Invalid link " + input));
        }
    }
}
