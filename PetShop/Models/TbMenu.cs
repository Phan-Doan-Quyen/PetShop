using System;
using System.Collections.Generic;

namespace PetShop.Models;

public partial class TbMenu
{
    public int MenuId { get; set; }

    public string? Title { get; set; }

    public string? Alias { get; set; }

    public int? Levels { get; set; }

    public int? ParentId { get; set; }

    public int? Position { get; set; }

    public bool IsActive { get; set; }
}
