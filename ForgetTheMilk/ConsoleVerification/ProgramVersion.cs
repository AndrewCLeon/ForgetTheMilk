using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVerification
{
    public class ProgramVersion
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }
        public int Revision { get; set; }


        public static bool operator ==(ProgramVersion source, ProgramVersion dest)
        {
            return source.Major == dest.Major && source.Minor == dest.Minor && source.Build == dest.Build && source.Revision == dest.Revision;
        }

        public static bool operator !=(ProgramVersion source, ProgramVersion dest)
        {
            return source.Major != dest.Major || source.Minor != dest.Minor || source.Build != dest.Build || source.Revision == dest.Revision;
        }
    }
}
