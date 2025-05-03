using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhoseTurn.Models;

public class PersonResponseModel : PersonBase
{    public int TotalTimesPaid { get; set; }
    public decimal TotalAmountPaid { get; set; }
}
