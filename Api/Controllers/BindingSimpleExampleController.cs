using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Api.Controllers.BindingExampleController;
using System.Collections.Concurrent;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingSimpleExampleController : ControllerBase
    {
        static readonly ConcurrentDictionary<string, string> Items = new();
        public BindingSimpleExampleController()
        {
            if(Items.Count == 0)
            {
                Items["first"] = "first";
                Items["secong"] = "secong";
                Items["third"] = "third";
            }
        }

        [HttpGet]
        public IEnumerable<string> GetAll() => Items.Values;
        

        [HttpGet("{item}")]
        public string? Find(string item) => Items.GetValueOrDefault(item);


        [HttpPost]
        public void Create(string item) => Items.TryAdd(item, item);


        
        [HttpPut]
        public void Edit(string item) => Items[item] = item;


        [HttpDelete]
        public void DeleteItem(string item) => Items.TryRemove(item, out var value);

    }
}
