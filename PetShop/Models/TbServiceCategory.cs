using System;
using System.Collections.Generic;

namespace PetShop.Models;

public partial class TbServiceCategory
{
    public int CategoryServiceId { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public string? Icon { get; set; }

    public int? Position { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<TbService> TbServices { get; set; } = new List<TbService>();
}
