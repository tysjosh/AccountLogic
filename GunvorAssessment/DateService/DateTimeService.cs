using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunvorAssessment.DateService
{
    public interface IDateTimeService
    {
        DateTimeOffset GetCurrentDateTime();
    }

    public class DateTimeService : IDateTimeService
    {

        public DateTimeOffset GetCurrentDateTime()
        {
            return DateTimeOffset.Now;
        }
    }
}
