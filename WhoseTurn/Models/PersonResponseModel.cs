namespace WhoseTurn.Models;

public class PersonResponseModel : PersonBase
{    public int TotalTimesPaid { get; set; }
    public decimal TotalAmountPaid { get; set; }
}
