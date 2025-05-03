using WhoseTurn.Models;

namespace WhoseTurn.Managers;

public interface IDeterminantManager
{
    public Task<string> DetermineWhoPays(OrderRequestModel request);

    public Task<List<PersonResponseModel>> GetAllPersons();
}
