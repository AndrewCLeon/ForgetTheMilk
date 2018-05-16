using System;
using System.Net;

namespace ForgetTheMilk.Models
{
    public class LinkValidator : ILinkValidator
    {
        public void Validate(string link)
        {
            var request = WebRequest.CreateHttp(link);
            request.Method = "HEAD";
            try
            {
                request.GetResponse();
            }
            catch (WebException ex)
            {
                throw new ApplicationException("Invalid link " + link);
            }
        }
    }
}