using Dogukan_Kisecuklu_Hafta_3.Model.Abstract;

namespace Dogukan_Kisecuklu_Hafta_3.Model.Concrete
{
    public class Container: IEntity
    {
        // Vehicle class and properties 
        public virtual long id { get; set; }
        public virtual string container_name { get; set; }
        public virtual double latitude { get; set; }
        public virtual double longitude { get; set; }
        public virtual long vehicle_id { get; set; }

    }
}
