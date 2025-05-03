using Microsoft.EntityFrameworkCore;
using WhoseTurn.DataModels;

namespace WhoseTurn.DatabaseContext;

public class DeterminantContext : DbContext
{
    public DbSet<PersonDataModel> Persons { get; set; }

    public DeterminantContext(DbContextOptions<DeterminantContext> options)
        : base(options) { }
}
