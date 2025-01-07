using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            // IDisposable pattern implementation of C#  -->  using içerisine yazılan nesneler işi bittikten sonra garbage collector ile bellekten atılır.
            using (TContext context = new TContext())
            {
                //Referans yakalama.
                var addedEntity = context.Entry(entity);
                // Eklenecek nesne
                addedEntity.State = EntityState.Added;
                // Ekle
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                //Referans yakalama.
                var deletedEntity = context.Entry(entity);
                // Silinecek nesne
                deletedEntity.State = EntityState.Deleted;
                // Sil
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                // paramtre olarak verilen lambdaya (filtreye) göre eşleşen veri getirilir.
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                // filtre yoksa --> context.Set<TEntity>().ToList()  --> TEntity tablosundaki tüm verileri ver (Select * From TEntitys)
                // filtre varsa --> paramtre olarak verilen lambdaya (filtreye) göre veriler getirilir.
                return filter == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                //Referans yakalama.
                var updatedEntity = context.Entry(entity);
                // Güncellenecek nesne
                updatedEntity.State = EntityState.Modified;
                // Güncelle
                context.SaveChanges();
            }
        }
    }
}
