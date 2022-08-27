using Dogukan_Kisecuklu_Hafta_3.Model.Abstract;

namespace Dogukan_Kisecuklu_Hafta_3.Model.Concrete
{
    public class Vehicle : IEntity
    {
        // Vehicle class and properties 
        public virtual long id { get; set; } 
        public virtual string vehicle_name { get; set; }
        public virtual string vehicle_plate { get; set; }

    }
}
