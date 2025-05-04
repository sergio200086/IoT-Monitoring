using IoTMonitoringAPI.Models;
using IoTMonitoringAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IoTMonitoringAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorController(KafkaProducerService kafkaProducerService) : Controller
    {
        private readonly KafkaProducerService _kafkaProducerService = kafkaProducerService;

        [HttpGet("validate")]
        public ActionResult Validate()
        {

            return Ok(new { message = "Sensor API is working!" });
        }

        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] SensorData sensorData)
        {
            
            string dataString = JsonConvert.SerializeObject(sensorData);
            await _kafkaProducerService.SendMessageAsync(dataString);
            return Ok();
        }



        //[HttpPost("test")]
        //public ActionResult Test([FromBody] JsonContent message)
        //{

        //    return Ok(new { message = message });
        //}
    }
}
