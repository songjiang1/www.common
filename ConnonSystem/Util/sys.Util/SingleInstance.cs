using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sys.Util
{
    public sealed class SingleInstance<T> where T : class, new()
    {
        private static volatile T _instance = new T();
        private static readonly object mutex = new object();

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (mutex)
                    {
                        if (_instance == null)
                        {
                            _instance = new T();
                        }
                    }
                }
                return _instance;
            }
        }
    }
}
