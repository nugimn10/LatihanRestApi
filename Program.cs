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
using LathianRestApi;

namespace LathianRestApi
{
    [HelpOption("--hlp")]
    [Subcommand(
        typeof(ShowList),
        typeof(addList),
        typeof(updateList),
        typeof(delList),
        typeof(isDone),
        typeof(unDone),
        typeof(clarList)
    )]
    class Program
    {
         public static Task<int> Main(string[] args)
        {
            return CommandLineApplication.ExecuteAsync<Program>(args);
        }

        

    }
    

    [Command(Description="Se what you have to do ", Name="list")]
    class ShowList
    {
        public async Task OnExecuteAsync()
        {

            var RawResult = await modifier.Get("http://localhost:3000/StuffToDo");
            var result = JsonConvert.DeserializeObject<List<StuffToDo>>(RawResult);

            foreach(var i in result){
                string Done = null;
                if(i.done){
                    Done = "(DONE)";
                }
                Console.WriteLine($"{i.id}. {i.title} {Done}");
            }
            
        }

    }

    [Command(Description="what you planning to do ? ", Name="add")]
    class addList
    {
        [Argument(0)]
        public string task { get; set; }
        public async Task OnExecuteAsync()
        {
            var client = new HttpClient();
            
            var request = new Requests(){title = task, done = false};
            var data = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8, "application/json");
            var result = await client.PostAsync("http://localhost:3000/StuffToDo",data);

            Console.WriteLine(result);
         }
            
    }

    [Command(Description="did your plan change ? what are you gonna change ?", Name="patch")]
    class updateList
    {
        [Argument(0)]
        public string updateId { get; set; }
        
        [Argument(1)]
        public string task { get; set; }
        public async Task OnExecuteAsync()
        {
            var client = new HttpClient();
            var request = new {id = Convert.ToInt32(updateId),title = task, done = false};
            var data = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8, "application/json");
            var result = await client.PatchAsync($"http://localhost:3000/StuffToDo/{updateId}",data);

            Console.WriteLine(result);
         }
            
    }

    
    [Command(Description="you done someting great ", Name="done")]
    class isDone
    {
        [Argument(0)]
        public string updateId { get; set; }
        
        public async Task OnExecuteAsync()
        {
            var client = new HttpClient();
            var request = new {id = Convert.ToInt32(updateId), done = true};
            var data = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8, "application/json");
            var result = await client.PatchAsync($"http://localhost:3000/StuffToDo/{updateId}",data);

            Console.WriteLine(result);
         }
            
    }

    [Command(Description="finish what you start.", Name="undone")]
    class unDone
    {
        [Argument(0)]
        public string updateId { get; set; }
        

        public async Task OnExecuteAsync()
        {
            var client = new HttpClient();
            var request = new {id = Convert.ToInt32(updateId), done = false};
            var data = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8, "application/json");
            var result = await client.PatchAsync($"http://localhost:3000/StuffToDo/{updateId}",data);

            Console.WriteLine(result);
         }
            
    }

    [Command(Description="did you change your mind ?", Name="del")]
    class delList
    {
        [Argument(0)]
        public string delId { get; set; }
        
        public async Task OnExecuteAsync()
        {
            var client = new HttpClient();
            var request = new {id = Convert.ToInt32(delId)};
            var result = await client.DeleteAsync($"http://localhost:3000/StuffToDo/{delId}");

            Console.WriteLine(result);
         }
            
    }

    [Command(Description="clear all the list ", Name="clear")]
    class clarList
    {
        
        public async Task OnExecuteAsync()
        {   
            var confirm = Prompt.GetYesNo("are u sure to clear all the list ?,\n i am just to make sure u doing all of that, cuz you promise to your self to done that",false, ConsoleColor.DarkRed);
           
            var client = new HttpClient();
            if(confirm)
            {
                var RawResult = await modifier.Get("http://localhost:3000/StuffToDo");
                var dat = JsonConvert.DeserializeObject<List<StuffToDo>>(RawResult);
                var delId = new List<int>();
                
                foreach (var idDel in dat)
                {
                    delId.Add(idDel.id);
                }

                foreach(var idDel in delId)
                {
                    var result = await client.DeleteAsync($"http://localhost:3000/StuffToDo/{idDel}");
                }
                
            }

            

         }
            
    }

    

}



