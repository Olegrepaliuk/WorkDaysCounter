using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            if (dayCount < 1)
                throw new ArgumentException("Duration can`t be less than 1 day");

            DateTime possibleEndDate = startDate.AddDays(dayCount - 1);

            if (weekEnds == null)
                return possibleEndDate;

            for (int i = 0; i < weekEnds.Length; i++)
            {
                if (weekEnds[i].StartDate > weekEnds[i].EndDate)
                    throw new ArgumentException("Start date of weekend can`t be later than end date");

                int daysToAdd = 0;

                if ((weekEnds[i].EndDate >= startDate) && (weekEnds[i].StartDate <= possibleEndDate))
                {
                    var intervalStartDate = weekEnds[i].StartDate > startDate ? weekEnds[i].StartDate : startDate;
                    daysToAdd = (weekEnds[i].EndDate - intervalStartDate).Days + 1;
                }
                else if (weekEnds[i].StartDate > possibleEndDate)
                {
                    break;
                }

                possibleEndDate = possibleEndDate.AddDays(daysToAdd);
            }

            return possibleEndDate;
        }
    }
}
