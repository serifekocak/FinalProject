using DataAccess.Abstract;
using Entities.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntitiyFramework
{
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            // IDisposable pattern implementation of C#  -->  using içerisine yazılan nesneler işi bittikten sonra garbage collector ile bellekten atılır.
            using (NorthwindContext context = new NorthwindContext())
            {
                //Referans yakalama.
                var addedEntity = context.Entry(entity);
                // Eklenecek nesne
                addedEntity.State = EntityState.Added;
                // Ekle
                context.SaveChanges();  
            }
        }

        public void Delete(Product entity)
        {
            // IDisposable pattern implementation of C#  -->  using içerisine yazılan nesneler işi bittikten sonra garbage collector ile bellekten atılır.
            using (NorthwindContext context = new NorthwindContext())
            {
                //Referans yakalama.
                var deletedEntity = context.Entry(entity);
                // Silinecek nesne
                deletedEntity.State = EntityState.Deleted;
                // Sil
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                // paramtre olarak verilen lambdaya (filtreye) göre eşleşen veri getirilir.
                return  context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            // IDisposable pattern implementation of C#  -->  using içerisine yazılan nesneler işi bittikten sonra garbage collector ile bellekten atılır.
            using (NorthwindContext context = new NorthwindContext())
            {
                // filtre yoksa --> context.Set<Product>().ToList()  --> Product tablosundaki tüm verileri ver (Select * From Products)
                // filtre varsa --> paramtre olarak verilen lambdaya (filtreye) göre veriler getirilir.
                return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList();  
            }
        }

        public void Update(Product entity)
        {
            // IDisposable pattern implementation of C#  -->  using içerisine yazılan nesneler işi bittikten sonra garbage collector ile bellekten atılır.
            using (NorthwindContext context = new NorthwindContext())
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
