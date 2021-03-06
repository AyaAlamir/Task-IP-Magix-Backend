﻿using Infrastructure.Context;
using Infrastructure.Interfaces.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class Repository<T> : CrudRepository<T>, IRepository<T> where T : class
    {
        public Repository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
