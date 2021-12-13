using SpotifyAnalogApp.Business.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Services
{
    public class DayTimeService : IDateTimeService
    {
        public DateTime ReturnDaytimeNow()
        {
            return DateTime.Now;
        }
    }
}
