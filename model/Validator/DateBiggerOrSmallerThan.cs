using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace model.Validator
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class DateBiggerOrSmallerThan : ValidationAttribute
    {
        private const string _defaultErrorMessage = "End date cannot be prior to start date";

        public string MinValue { get; set; }
        public string MaxValue { get; set; }

        public DateBiggerOrSmallerThan(string beginDate, string endDate) : base()
        {
            MinValue = beginDate;
            MaxValue = endDate;
        }

       

        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            object startDate = properties.Find(MinValue, true).GetValue(value);
            object endDate = properties.Find(MaxValue, true).GetValue(value);

            DateTime minDate = (DateTime)startDate;
            DateTime maxDate = (DateTime)endDate;

            DateTime d = Convert.ToDateTime(value);
           
            if (MinValue != null && d < minDate)
                return false;
            if (MaxValue != null && d > maxDate)
                return false;
            return true ;

        }
    }
}
