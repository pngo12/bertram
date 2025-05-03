using WhoseTurn.DataModels;
using WhoseTurn.Models;
using WhoseTurn.Repositories;


namespace WhoseTurn.Managers;

public class DeterminantManager : IDeterminantManager
{
    ILogger<DeterminantManager> _logger;
    private readonly IDeterminantRepository _data;

    public DeterminantManager(ILogger<DeterminantManager> logger, IDeterminantRepository data)
    {
        _logger = logger;
        _data = data;
    }

    public async Task<string> DetermineWhoPays(OrderRequestModel request)
    {
        try
        {
            // Load everyone in the request
            var listOfPeopleToLoad = request.Persons.Select(x => x.Id).ToList();
            
            var personsList = await _data.GetAllPersons(listOfPeopleToLoad);
            personsList = personsList.OrderBy(x => x.TotalAmountPaid).ToList();
            
            var personToPay = personsList.First();
            
            decimal total = 0.00m;
            var orderTotal = request.Persons.Sum(x => x.ItemOrderedAmount);

            for (int i = 0; i < personsList.Count - 1; i++)
            {
                var nextPerson = personsList[i+1];

                var amountToSubtract = request.Persons.Where(x => x.Id == personToPay.Id).Select(x => x.ItemOrderedAmount).First();
                total = orderTotal - amountToSubtract;

                // If someone has never paid and they were included in the order then I believe
                // it's only fair that they pay for their first time
                // This will keep happening if someone is included in the orders and hasnt paid ever (ex: new hires)
                if (personToPay.TotalTimesPaid == 0)
                {
                    await HandleUpdateOfOrders(personToPay, total);
                    return personToPay.Name;
                }

                // In the rare chance that two people are identical then we'll leave it up to chance
                if (personToPay.TotalTimesPaid == nextPerson.TotalAmountPaid
                    && personToPay.TotalAmountPaid == nextPerson.TotalAmountPaid)
                {
                    personToPay = Randomize(personToPay, nextPerson);
                    await HandleUpdateOfOrders(personToPay, total);
                    return personToPay.Name;
                }
            }
            
            await HandleUpdateOfOrders(personToPay, total);
            return personToPay.Name;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occured, unable to determine who to pay");
            throw;
        }
    }

    public async Task<List<PersonResponseModel>> GetAllPersons()
    {
        try
        {
            var everyone = await _data.GetAllPersons();

            if (everyone.Any())
            {
               return everyone.Select(x => new PersonResponseModel
               {
                Id = x.Id,
                Name = x.Name,
                TotalTimesPaid = x.TotalTimesPaid,
                TotalAmountPaid = x.TotalAmountPaid
               }).ToList();
            }

            return new List<PersonResponseModel>();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unable to get everyone");
            throw;
        }
    }

    private async Task HandleUpdateOfOrders(PersonDataModel person, decimal amount)
    {
        person.TotalAmountPaid+= amount;
        person.TotalTimesPaid++;
        await _data.UpdateOrder(person);
    }

    private PersonDataModel Randomize(PersonDataModel person1, PersonDataModel person2)
    {
        Random random = new Random();

        if (random.Next(2) == 1)
            return person1;

        return person2;
    }
}
