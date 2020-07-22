using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventContracts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCorePublisher
{
    [ApiController]
    [Route("[controller]")]
    public class ValueController : ControllerBase
    {
        readonly IPublishEndpoint _publishEndpoint;

        public ValueController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<ActionResult> Post(string value)
        {
            await _publishEndpoint.Publish<ValueEntered>(new
            {
                Value = value
            });

            return Ok();
        }

        [HttpGet]
        public string Get()
        {
            return "hello world";
        }
    }
}