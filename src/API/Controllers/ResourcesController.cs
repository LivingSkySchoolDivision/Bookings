using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSSD.Bookings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LSSD.Bookings.API.Services;

namespace LSSD.Bookings.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResourcesController : ControllerBase
    {
        private readonly ILogger<ResourcesController> _logger;
        private readonly ResourceService _resourceService;

        public ResourcesController(ILogger<ResourcesController> logger, ResourceService service)
        {
            _logger = logger;
            _resourceService = service;
        }

        [HttpGet]
        public IEnumerable<Resource> Get()
        {
            return _resourceService.GetEnabled();
        }
    }
}
