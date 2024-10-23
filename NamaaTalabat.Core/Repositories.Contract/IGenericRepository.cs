﻿using NamaaTalabat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamaaTalabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // GetAll ,  GetByID

        Task<IEnumerable<T>> GetAll();

        Task<T?> Get(int id); 
    }
}
