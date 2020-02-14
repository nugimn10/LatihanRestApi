using System;
using System.Linq;
using System.IO;
using System.Globalization;
using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


namespace LathianRestApi
{

    public class StuffToDo : Requests
    {        public int id { get; set; }
    }

    public class Tasks{
        public List<StuffToDo> todo{get; set;}
    }

    public class Requests{
        public string title { get; set; }
        public bool done { get; set; }
    }
}