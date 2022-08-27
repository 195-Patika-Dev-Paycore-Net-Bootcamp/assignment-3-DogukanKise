using Dogukan_Kisecuklu_Hafta_3.Context.Abstract;
using Dogukan_Kisecuklu_Hafta_3.Model.Abstract;
using Dogukan_Kisecuklu_Hafta_3.Model.Concrete;
using NHibernate;
using System.Linq;

namespace Dogukan_Kisecuklu_Hafta_3.Context.Concrete
{
    public class VehicleSession : IMapperSession<Vehicle>
    {
        private readonly ISession session;
        private ITransaction transaction;
        // ISession and ITransaction interfaces taken from the Nhibernate library.
        public VehicleSession(ISession session)
        {
            this.session = session; // Dependency injection
        }

        public void BeginTransaction()
        {
            transaction = session.BeginTransaction(); // Starting transaction process
        }

        public void CloseTransaction()
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
            //Closing transaction process
        }

        public void Commit()
        {
            transaction?.Commit(); // Commiting to transaction process
        }
        public void Rollback()
        {
            transaction.Rollback(); // Rollbacking to transaction process
        }

        public void Delete(Vehicle entity)
        {
            session.Delete(entity); // Deleting a entity which is a Vehicle 
        }

        public void Save(Vehicle entity)
        {
            session.Save(entity); // Saving a entity which is a Vehicle 
        }

        public void Update(Vehicle entity)
        {
            session.Update(entity); // Saving a entity which is a Vehicle 
        }

        public IQueryable<Vehicle> Entities => session.Query<Vehicle>();  // Selecting a entity which is a Vehicle
    }
}
