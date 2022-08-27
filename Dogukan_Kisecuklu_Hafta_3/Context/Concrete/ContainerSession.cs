using Dogukan_Kisecuklu_Hafta_3.Context.Abstract;
using Dogukan_Kisecuklu_Hafta_3.Model.Concrete;
using NHibernate;
using System.Linq;

namespace Dogukan_Kisecuklu_Hafta_3.Context.Concrete
{
    public class ContainerSession : IMapperSession<Container>
    {
        private readonly ISession session;
        private ITransaction transaction;
        // ISession and ITransaction interfaces taken from the Nhibernate library.
        public ContainerSession(ISession session)
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

        public void Delete(Container entity)
        {
            session.Delete(entity); // Deleting a entity which is a container 
        }

        public void Save(Container entity)
        {
            session.Save(entity); // Saving a entity which is a Container 
        }

        public void Update(Container entity)
        {
            session.Update(entity); // Saving a entity which is a Container 
        }

        public IQueryable<Container> Entities => session.Query<Container>(); // Selecting a entity which is a Container
    }
}
