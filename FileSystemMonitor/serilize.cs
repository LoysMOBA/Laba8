using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;


namespace IMT
{
    [Serializable]
    public class serilize
    {
        public string Way { get;  set; }
        public string Status { get;  set; }

        public serilize(string way, string status)
        {
            Way = way;
            Status = status;

        }
        public serilize()
        {
        }
        public override string ToString()
        {
            return Way;
        }

    }
    [Serializable]
    public class Info
    {
        public string information { get; set; }

        public Info() { }

        public Info(string information)
        {
            this.information = information;
        }
    }
}

