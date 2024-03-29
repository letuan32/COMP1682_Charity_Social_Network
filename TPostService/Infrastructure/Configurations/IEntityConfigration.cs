﻿using Microsoft.EntityFrameworkCore;

namespace TPostService.Infrastructure.Configurations;

public interface IEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
{
    
}