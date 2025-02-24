﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess
{
    // CORE KATMANI DİĞER KATMANLARI REFERANS ALMAZ. 
    // EVRENSEL OLDUĞU İÇİN HİÇBİR PROJEYE BAĞLI OLMAMALIDIR

    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        // Expression ifadesi sayesinde " List<T> GetByCategory(int categoryId); " ifadesine gerek kalmaz
        // Filtreler vererek datanın tamamını değil belli bir kısmını getirmeye yarar.
        // filter = null ---> filtre vermesen de olur, zorunlu değil
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        // Tek bir datayı listeleme, filtre vermek zorunlu
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
