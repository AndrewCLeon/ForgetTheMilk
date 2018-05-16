using ForgetTheMilk.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVerification
{
    public class LinkValidationTests : AssertionHelper
    {
        [Test]
        public void Validate_InvalidUrl_ThrowsException()
        {
            var invalidLink = "http://doesnotexistdotcom.com";

            Expect(() => new LinkValidator().Validate(invalidLink),
                Throws.Exception.With.Message.EqualTo("Invalid link " + invalidLink));
        }

        [Test]
        public void Validate_ValidUrl_ThrowsNothing()
        {
            var validLink = "http://www.google.com";

            Expect(() => new LinkValidator().Validate(validLink), Throws.Nothing);
        }
    }
}
