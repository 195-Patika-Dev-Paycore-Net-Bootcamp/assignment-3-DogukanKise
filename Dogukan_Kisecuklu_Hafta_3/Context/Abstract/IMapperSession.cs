using System.Linq;

namespace Dogukan_Kisecuklu_Hafta_3.Context.Abstract
{
    public interface IMapperSession<T> // This interface must be a generic type because it will be used in multiple Entities(Vehicle,Container).
    {
        // This interface about the Database processes.
        void BeginTransaction(); // Function to initiate the transaction
        void Commit(); // Function to commit process 
        void Rollback(); // Function the rollback.
        void CloseTransaction(); // Function to Close Transaction
        void Save(T entity); // Function to record the entity that will come from the user.
        void Update(T entity); // Function to update the entity that will come from the user.
        void Delete(T entity); // Function to delete the entity that will come from the user.
        IQueryable<T> Entities { get; } // Property to record the entity that will come from the user.
    }
}
