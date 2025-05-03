using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WhoseTurn.Models;

namespace WhoseTurn.DataModels;

public class OrderDataModel : OrderBase
{
    [Key]
    public int OrderId { get; set; }

    public int PersonId { get; set; }
}
