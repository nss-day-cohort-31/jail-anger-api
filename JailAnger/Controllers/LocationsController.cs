using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using JailAnger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace JailAnger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IConfiguration _config;

        public LocationsController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: api/Locations
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Address, BusinessName, Phone FROM Locations";
                    SqlDataReader reader = await cmd.ExecuteReaderAsync();

                    List<Locations> locations = new List<Locations>();

                    while (reader.Read())
                    {
                        Locations location = new Locations
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Address = reader.GetString(reader.GetOrdinal("Address")),
                            Phone = reader.GetString(reader.GetOrdinal("Phone")),
                            BusinessName = reader.GetString(reader.GetOrdinal("BusinessName"))
                        };

                        locations.Add(location);
                    }
                    reader.Close();

                    return Ok(locations);
                }
            }
        }

        // GET: api/Locations/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Locations
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Locations/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
