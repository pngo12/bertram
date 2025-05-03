using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhoseTurn.Models;

public abstract class OrderBase
{
    public decimal Amount { get; set; }
}
