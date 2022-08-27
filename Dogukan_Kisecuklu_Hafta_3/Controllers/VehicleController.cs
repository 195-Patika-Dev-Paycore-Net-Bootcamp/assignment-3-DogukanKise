using Dogukan_Kisecuklu_Hafta_3.Context.Abstract;
using Dogukan_Kisecuklu_Hafta_3.Context.Concrete;
using Dogukan_Kisecuklu_Hafta_3.Model.Abstract;
using Dogukan_Kisecuklu_Hafta_3.Model.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dogukan_Kisecuklu_Hafta_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IMapperSession<Vehicle> session;
        private readonly IMapperSession<Container> session2;


        public VehicleController(IMapperSession<Vehicle> session, IMapperSession<Container> session2)
        {
            this.session = session;
            this.session2 = session2;
        }

        [HttpGet("GetAll")]
        public List<Vehicle> Get()
        {
            List<Vehicle> result = session.Entities.ToList();
            return result;

        }
        [HttpPost]
        public void Post([FromQuery] Vehicle vehicle)
        {
            try
            {
                session.BeginTransaction();
                session.Save(vehicle);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                
            }
            finally
            {
                session.CloseTransaction();
            }
        }

        [HttpPut]
        public ActionResult<Vehicle> Put([FromQuery] Vehicle request)
        {
            Vehicle vehicle = session.Entities.Where(x => x.id == request.id).FirstOrDefault();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                session.BeginTransaction();
                vehicle.vehicle_name = request.vehicle_name;
                vehicle.vehicle_plate = request.vehicle_plate;
                session.Update(vehicle);

                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
            }
            finally
            {
                session.CloseTransaction();
            }


            return Ok();
        }
        [HttpDelete("{id}")]
        public ActionResult<Vehicle> Delete(int id)
        {
            Vehicle vehicle = session.Entities.Where(x => x.id == id).FirstOrDefault();
            List<Container> containers = session2.Entities.Where(x => x.vehicle_id == id).ToList();
            if (vehicle == null)
            {
                return NotFound();
            }

            try
            {
                session2.BeginTransaction();
                for (int i = 0; i < containers.Count; i++)//Vehicle ile bağlantılı olan Container'lar -varsa- silme işlemi
                {
                    session2.Delete(containers[i]);
                }
                session2.Commit();
                session.BeginTransaction();
                session.Delete(vehicle);
                session.Commit();
            }
            catch (Exception ex)
            {
                session.Rollback();
                session2.Rollback();
            }
            finally
            {
                session.CloseTransaction();
                session2.CloseTransaction();
            }

            return Ok();
        }

    }
}
