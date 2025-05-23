using System;
using System.Collections.Generic;

namespace PetShop.Models;

public partial class TbServiceReview
{
    public int ProductServiceId { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Image { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Detail { get; set; }

    public int? Star { get; set; }

    public int? ServiceId { get; set; }

    public bool IsActive { get; set; }

    public virtual TbService? Service { get; set; }
}
