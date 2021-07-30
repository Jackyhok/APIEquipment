using APIEquipment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEquipment.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TypeController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Equipment> Get()
        {
            using (var context = new TMAEquipmentContext())
            {
                //get all type
                return context.Equipment.ToList();
            }
        }
    }
}
