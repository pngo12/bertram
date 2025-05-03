using WhoseTurn.DatabaseContext;
using WhoseTurn.DataModels;
using WhoseTurn.Models;
using Microsoft.EntityFrameworkCore;

namespace WhoseTurn.Repositories;

public class DeterminantRepository : IDeterminantRepository
{
    DeterminantContext _context;

    public DeterminantRepository(DeterminantContext context)
    {
        _context = context;
    }

    public async Task<List<PersonDataModel>> GetAllPersons(List<int>? personsToLoad = null)
    {
        if (personsToLoad == null)
            return await _context.Persons.ToListAsync();

        return await _context.Persons.Where(x => personsToLoad.Contains(x.Id)).ToListAsync();
    }

    public async Task UpdateOrder(PersonDataModel request)
    {
        _context.Persons.Update(request);
        await _context.SaveChangesAsync();
    }
}
