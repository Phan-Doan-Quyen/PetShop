using System;
using System.Collections.Generic;

namespace PetShop.Models;

public partial class TbAccount
{
    public int AccountId { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Avatar { get; set; }

    public string? FullName { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public int? RoleId { get; set; }

    public string? Position { get; set; }

    public DateTime? LastLogin { get; set; }

    public bool IsActive { get; set; }

    public virtual TbRole? Role { get; set; }

    public virtual ICollection<TbBlog> TbBlogs { get; set; } = new List<TbBlog>();
}
