using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyAnalogApp.Business.Exceptions
{
    public class BaseCustomException : Exception
    {
        public int ErrorCode { get; set; }
        

        public BaseCustomException()
        {

        }
        public BaseCustomException(int errorCode , string message):base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
