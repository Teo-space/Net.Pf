using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using static Api.Controllers.BindingExampleController;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingExampleController : ControllerBase
    {
        public record Item(string name);

        static ConcurrentDictionary<Guid, Item> Items = new();

        public BindingExampleController()
        {
            if(Items.Count == 0)
            {
                Items[Guid.NewGuid()] = new("First");
                Items[Guid.NewGuid()] = new("Second");
                Items[Guid.NewGuid()] = new("Third");
            }
        }



        [HttpGet]
        public IEnumerable<KeyValuePair<Guid, Item>> GetAll() => Items.OrderBy(x => x.Value.name);


        [HttpGet("{Id}")]
        public Item FindById(Guid Id) => Items[Id];


        [HttpPost]
        public Guid Create([FromBody] Item item)
        {
            Guid Id = Guid.NewGuid();
            Items[Id] = item;
            return Id;
        }

        [HttpPut]
        public Guid Edit(Guid Id, [FromBody] Item item)
        {
            Items[Id] = item;
            return Id;
        }

        [HttpDelete]
        public Item? Delete(Guid Id)
        {
            if(Items.TryRemove(Id, out var value))
            {
                return value;
            }
            return default;
        }


    }
}
