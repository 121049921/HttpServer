using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace HttpApiMethod
{

    public abstract class EntityBase : IDTO
    {

        public EntityBase() : this(DateTime.Now, DateTime.Now)
        {

        }
        public EntityBase(DateTime createDateTime, DateTime updateDateTime)
        {

            this.CreateDateTime = DateTime.Now;
            this.UpdateDateTime = DateTime.Now;

        }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            var properties = this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).ToArray();

            foreach (var i in properties)
            {
                var attr = i.GetCustomAttributes();
                foreach (var a in attr)
                {
                    var val = (a as ValidationAttribute);
                    if (val != null)
                        if (!val.IsValid(i.GetValue(this)))
                        {
                            string info = val.ErrorMessage;
                            if (string.IsNullOrWhiteSpace(info))
                                info = val.FormatErrorMessage(i.Name);
                            yield return new RuleViolation(info, i.Name);
                        }
                }
            }

        }

       
    }


}
