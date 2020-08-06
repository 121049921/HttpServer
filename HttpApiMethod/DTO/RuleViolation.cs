using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpApiMethod
{
    public class RuleViolation
    {

        public string ErrorMessage { get; set; }
        public string PropertyName { get; set; }

        public RuleViolation() : this(string.Empty, string.Empty)
        {
        }

        public RuleViolation(string errorMessage, string propertyName)
        {
            ErrorMessage = errorMessage;
            PropertyName = propertyName;
        }

     
        public override string ToString()
        {
            return string.Format(ErrorMessage, PropertyName);
        }
    }
}
