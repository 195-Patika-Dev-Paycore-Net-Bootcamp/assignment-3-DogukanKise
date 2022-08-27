using Dogukan_Kisecuklu_Hafta_3.Context.Abstract;
using Dogukan_Kisecuklu_Hafta_3.Model.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dogukan_Kisecuklu_Hafta_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly IMapperSession<Container> session;
        public ContainerController(IMapperSession<Container> session)
        {
            this.session = session; // Dependency injection
        }

        [HttpGet("GetAll")]
        public List<Container> Get()
        {
            List<Container> result = session.Entities.ToList(); //All incoming data has been converted into a list and returned.
            return result;

        }
        [HttpGet("GetByVehicleId")]
        public List<Container> Get([FromQuery] int id)
        {
            List<Container> result = session.Entities.Where(x => x.vehicle_id == id).ToList();
            return result; // Data was returned by the ID from the user.

        }
        [HttpPost]
        public void Post([FromQuery] Container container)
        {
            try
            {
                session.BeginTransaction();
                session.Save(container); // The container_name, latitude, longitude, vehicle_id information from the user has been added to the database.
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback(); // If there is an error, the RollBack operation has been performed.

            }
            finally
            {
                session.CloseTransaction(); // The transaction was closed.
            }
        }

        [HttpPut]
        public ActionResult<Container> Put([FromQuery] Container request)
        {
            Container container = session.Entities.Where(x => x.id == request.id).FirstOrDefault();
            if (container == null)
            {
                return NotFound(); // If the ID is not in the database, NotFound is returned.
            }

            try
            {
                session.BeginTransaction();
                container.latitude = request.latitude;
                container.longitude = request.longitude;
                container.container_name = request.container_name;
                session.Update(container);
                session.Commit();

                // The information with the ID from the user has been updated.
            }
            catch (Exception ex)
            {
                session.Rollback(); // If there is an error, the RollBack operation has been performed.
            }
            finally
            {
                session.CloseTransaction(); // The transaction was closed.
            }
            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult<Container> Delete(int id)
        {
            Container container = session.Entities.Where(x => x.id == id).FirstOrDefault();
            if (container == null)
            {
                return NotFound(); // If the ID is not in the database, NotFound is returned.
            }

            try
            {
                session.BeginTransaction();
                session.Delete(container);
                session.Commit();
                //The data with the ID from the user has been deleted.
            }
            catch (Exception ex)
            {
                session.Rollback();  // If there is an error, the Rollback operation has been performed.
            }
            finally
            {
                session.CloseTransaction();// The transaction was closed.
            }

            return Ok();
        }
        [HttpGet("GetClusteredContainers")]
        public List<List<Container>> Get([FromQuery]int n, int id) 
        {
            int clusterIndex = 0;
            List<Container> containers = session.Entities.Where(x => x.vehicle_id == id).ToList();
            List<List<Container>> result = new List<List<Container>>();

            for (int i = 0; i < n; i++)
            {
                result.Add(new List<Container>());
            }
            for (int i = 0; i < containers.Count; i++)
            {
                result[clusterIndex].Add(containers[i]);
                if (clusterIndex != result.Count - 1) 
                   
                    clusterIndex++;
                else
                    clusterIndex = 0;
            }
            return result;
            // A response information is returned that divides the Containers into equal clusters according
            // to the ID and Number of Clusters (n) information from the user.
        }
    }
}
