using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mifinanazas.God.Applicattion.Dtos.Res
{
    public class ResultObject<T>
    {
        public bool success { get; set; }
        public string error { get; set; }
        public T data { get; set; }
    }
}
