using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace io.ark.core
{
    public class Verification
    {
        private List<string> errors = new List<string>();

        public override string ToString()
        {
            string response = "";
            if (errors.Count > 0)
                response = String.Join(", ", errors.ToArray());
            else
                response = "Verified";

            return response;
        }

        public void AddError(string errorDescription)
        {
            errors.Add(errorDescription);
        }
    }
}
