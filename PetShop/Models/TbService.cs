using System;
using System.Collections.Generic;

namespace PetShop.Models;

public partial class TbService
{
    public int ServiceId { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public int? CategoryServiceId { get; set; }

    public string? Description { get; set; }

    public string? Detail { get; set; }

    public string? Image { get; set; }

    public int? Price { get; set; }

    public int? PriceSale { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public bool IsNew { get; set; }

    public bool IsActive { get; set; }

    public int? Star { get; set; }

    public virtual TbServiceCategory? CategoryService { get; set; }

    public virtual ICollection<TbServiceReview> TbServiceReviews { get; set; } = new List<TbServiceReview>();
}
