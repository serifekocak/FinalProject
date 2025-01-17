using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntitiyFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        // Başka bir servis kullandığın için API katmanında injection hatası alırsın.
        // O yüzden AutoFacBusinessModule classında ICategoryService ve ICategoryDal bağımlılıklarını çöz.
        ICategoryService _categoryService;

        // Business katmanının bildiği tek şey IProductDal'dır.
        // Çalışılmak istenen framework (inMemory, EntityFramework, Dapper,..) constructor a parametre olarak verilir.
        // Bunların hepsi IProductDal nesnesidir.
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            // Iflerle kontrol etmek yerine Validasyon işlemi yapılabilir.
            // if (product.ProductName.Length < 2)
            // {
            //     // magic strings --> ErrorResult("Ürün ismi en az 2 karakter olmalıdır");
            //     return new ErrorResult(Messages.ProductNameInvalid);
            // }

            // Validasyon tüm projelerde ortak olduğu için Core katmanında tool olarak kullanılabilir.
            //var context = new ValidationContext<Product>(product);
            // ProductValidator productValidator = new ProductValidator();
            // var result = productValidator.Validate(context);
            // if (!result.IsValid)
            // {
            //     throw new ValidationException(result.Errors);
            // }

            // Aspect olarak metot dışına taşı(AOP)
            // ValidationTool.Validate(new ProductValidator(), product);


            //Bir kategoride en fazla 10 ürün olabilir.
            // Ayni iş kuralı birden fazla metotta kullanılabilir. O yüzden iş kurallarını fonksiyon olarak yazmakta fayda vardır (SOLID'e uygunluk)
            //var productCount = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;

            //if (productCount >= 10)
            //{
            //    return new ErrorResult("Ürün eklenemez");
            //}


            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    // Aynı isimde ürün eklenemez
            //    if (CheckIfProductNameIsUnique(product.ProductName).Success)
            //    {
            //        _productDal.Add(product);
            //        return new SuccessResult(Messages.ProductAdded);
            //    }
            //}
            //return new ErrorResult();

            // Yukarıdaki gibi yazmak yerine iş motoru (Core --> Utilities --> Business --> BusinessRules)kullanarak daha temiz bir kod düzeni sağlayabiiriz.
            IResult result = BusinessRules.Run(CheckIfProductNameIsUnique(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                                               CheckIfCategoryLimitExceded());

            if(result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded); 

         
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 19)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            //İş Kodları
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductListed);

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId == id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ValidationAspect))]
        public IResult Update(Product product)
        {
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }

        //Bir kategoride en fazla 30 ürün olabilir.
        // İş kurallarına ait fonksiyonlar private olmalıdır.
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            // SELECT COUNT(*) FROM Products WHERE categoryId = categoryId
            var productCount = _productDal.GetAll(p => p.CategoryId == categoryId).Count;

            if (productCount >= 30)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }
        // Aynı isimde ürün eklenemez
        private IResult CheckIfProductNameIsUnique(string productName)
        {
            var product = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (product)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        // Mevcut kategori sayısı 15'i geçmişse sisteme yeni ürün eklenmez.
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15) 
            {
                return new ErrorResult(Messages.CategoryLimitExceded); 
            }
            return new SuccessResult();
        }
    }
}
