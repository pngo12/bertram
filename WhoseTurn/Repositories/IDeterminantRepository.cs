using WhoseTurn.DataModels;
using WhoseTurn.Models;

namespace WhoseTurn.Repositories;

public interface IDeterminantRepository
{
    Task<List<PersonDataModel>> GetAllPersons(List<int>? personsToLoad = null);

    Task UpdateOrder(PersonDataModel request);
}
