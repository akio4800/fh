using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SelvesSoftware
{
    [Serializable]
    class ExceptionHandler:Exception
    {

         // Constructors
    public ExceptionHandler(String message) 
        : base(message) 
    { }

    // Ensure Exception is Serializable
    protected ExceptionHandler(SerializationInfo info, StreamingContext ctxt) 
        : base(info, ctxt)
    { }




    }
}
