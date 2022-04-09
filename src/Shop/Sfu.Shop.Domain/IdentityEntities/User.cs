﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Sfu.Shop.Domain.IdentityEntities;

/// <summary>
/// Custom application user entity.
/// </summary>
public class User : IdentityUser<Guid>
{
    /// <summary>
    /// First name.
    /// </summary>
    [MaxLength(255)]
    [Required]
    public string FirstName { get; set; }

    /// <summary>
    /// Last name.
    /// </summary>
    [MaxLength(255)]
    [Required]
    public string LastName { get; set; }

    /// <summary>
    /// Full name, concat of first name and last name.
    /// </summary>
    public string FullName => Saritasa.Tools.Common.Utils.StringUtils.JoinIgnoreEmpty(FirstName, LastName);
}