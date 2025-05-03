using WhoseTurn.Models;

namespace WhoseTurn.DataModels;

public class PersonDataModel : PersonBase
{
    public int TotalTimesPaid { get; set; }

    public decimal TotalAmountPaid { get; set; }
}