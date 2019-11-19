using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {     
        private readonly ILogger<ValuesController> _logger;

        private readonly DataContext _dataContext;
        public ValuesController(ILogger<ValuesController> logger, DataContext dataContext)
        {
            _logger = logger;
            _dataContext = dataContext;
        }       

        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var values =  await _dataContext.Values.ToListAsync();
            return Ok(values);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value =  await _dataContext.Values.FirstOrDefaultAsync(x=> x.Id==id);
            return Ok(value);
        }

        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        [HttpPut("{id}")]
        public void Put([FromBody] string value)
        {

        }


    }
}
