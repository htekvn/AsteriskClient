using System;
using System.Collections.Generic;
using System.Text;

namespace LogUtils
{
    public class StackTracer : IDisposable 
    {
        string _argsString;
        string _name;
        public StackTracer(string name)
        {
            Initialize(name, null);
        }
        
        public StackTracer(string name, object arg1)
        {
            Initialize(name, new object[] { arg1 });
        }

        public StackTracer(string name, object arg1, object arg2)
        {
            Initialize(name, new object[] { arg1, arg2 });
        }

        public StackTracer(string name, object arg1, object arg2, object arg3)
        {
            Initialize(name, new object[] { arg1, arg2, arg3 });
        }

        public StackTracer(string name, object[] argsArray)
        {
            Initialize(name, argsArray);
        }

        private void Initialize(string name, object[] argsArray)
        {

            if (Tracer.Instance.CanITrace())
            {
                _name = string.IsNullOrEmpty(name) ? "Not specified" : name;
 
                 if (argsArray != null && argsArray.Length > 0)
                 {
                     foreach (object argObj in argsArray)
                     {
                         if (null != argObj)
                         {
                             _argsString += argObj.ToString();
                             _argsString += " ";
                         }
                     }
                     if (_argsString != null)
                     {
                         _argsString = _argsString.Trim();
                         _argsString = _argsString.Replace(" ", ", ");
                     }
                 }
                 else
                 {
                     _argsString = string.Empty;
                 }
                 Tracer.Instance.StackLevel++;
                 Tracer.Instance.TraceWL3(@"<" + _name + @"( " + _argsString + @" ) >", true);
            }
        }
        
        public void Dispose()
        {
             if (Tracer.Instance.CanITrace())
             {
                 Tracer.Instance.TraceWL3(@"</" + _name + @"( " + _argsString + @" ) >", true);
                 Tracer.Instance.StackLevel--;
             }
        }
    }
}
