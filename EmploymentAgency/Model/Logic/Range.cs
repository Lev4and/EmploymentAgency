using System;

namespace EmploymentAgency.Model.Logic
{
    public class Range<T> where T : struct
    {
        public T BeginValue { get; private set; }

        public T EndValue { get; private set; }

        public Range(T beginValue, T endValue)
        {
            BeginValue = beginValue;
            EndValue = endValue;
        }
    }
}
