﻿using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the user entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class UserSpec : BaseSpec<UserSpec, User>
{
    public UserSpec(Guid id) : base(id)
    {
    }
    
    public UserSpec(ICollection<Guid> ids)
    {
        Query.Where(e => ids.Contains(e.Id));
    }

    public UserSpec(string email)
    {
        Query.Where(e => e.Email == email);
    }
}