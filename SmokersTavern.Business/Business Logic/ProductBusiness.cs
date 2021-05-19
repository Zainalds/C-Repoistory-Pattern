using SmokersTavern.Business.Interfaces;
using SmokersTavern.Data.Models;
using SmokersTavern.Model;
using SmokersTavern.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//Zain
namespace SmokersTavern.Business.Business_Logic
{
    public class ProductBusiness : IProductBusiness 
    {

        public void Insert(ProductViewModel model)
        {
            using (var repository = new ProductRepository())
            {
                repository.Insert(new Product
                {
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    BrandName = model.BrandName,
                    ProductColor = model.ProductColor,
                    ProductQuantity = model.ProductQuantity,
                    ProductPrice = model.ProductPrice,
                    ProductCostPrice = model.ProductCostPrice,
                    ImageUrl = model.ImageUrl,
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName,
                    BranchId=model.BranchId,
                    BranchName=model.BrandName
                });
            }
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            var products = new List<ProductViewModel>();

            using (var repository = new ProductRepository())
            {
                products = repository.GetAll().Select(model => new ProductViewModel
                {
                    Id = model.Id,
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    BrandName = model.BrandName,
                    ProductColor = model.ProductColor,
                    ProductQuantity = model.ProductQuantity,
                    ProductPrice = model.ProductPrice,
                    ProductCostPrice = model.ProductCostPrice,
                    ImageUrl = model.ImageUrl,
                    CategoryId = model.CategoryId,
                    CategoryName = model.Category.CategoryName,
                    BranchId = model.BranchId,
                    BranchName = model.Branch.BranchName

                }).ToList();
            }
            return products;
        }

        public IEnumerable<ProductArchiveViewModel> GetOutOfStock()
        {
            using (var product = new ArchiveRepository())
            {
                return product.GetAll().Where(x => x.ProductQuantity == 0)
                    .Select(x => new ProductArchiveViewModel()
                    {
                        Id = x.Id,
                        ProductName = x.ProductName,
                        ProductDescription = x.ProductDescription,
                        BrandName = x.BrandName,
                        ProductColor = x.ProductColor,
                        ProductQuantity = x.ProductQuantity,
                        ProductPrice = x.ProductPrice,
                        ProductCostPrice = x.ProductCostPrice,
                        ImageUrl = x.ImageUrl,
                        CategoryId = x.CategoryId,
                        CategoryName = x.Category.CategoryName,
                        BranchId = x.BranchId,
                        BranchName=x.BranchName

                    }).ToList();
            }
        }

        public ProductViewModel GetById(int id)
        {
            using (var repository = new ProductRepository())
            {
                return repository.Find(x => x.Id == id).Select(model => new ProductViewModel
                {
                    Id = model.Id,
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    BrandName = model.BrandName,
                    ProductColor = model.ProductColor,
                    ProductQuantity = model.ProductQuantity,
                    ProductPrice = model.ProductPrice,
                    ProductCostPrice = model.ProductCostPrice,
                    ImageUrl = model.ImageUrl,
                    CategoryId=model.CategoryId,
                    BranchId = model.BranchId
                }).FirstOrDefault();
            }
        }

        public void Update(ProductViewModel model)
        {
            using (var repository = new ProductRepository())
            {
                var p = new Product();
                p = repository.GetById(model.Id);
                if (p != null)
                {
                    p.Id = model.Id;
                    p.ProductName = model.ProductName;
                    p.ProductDescription = model.ProductDescription;
                    p.BrandName = model.BrandName;
                    p.ProductColor = model.ProductColor;
                    p.ProductQuantity = model.ProductQuantity;
                    p.ProductPrice = model.ProductPrice;
                    p.ProductCostPrice = model.ProductCostPrice;
                    p.CategoryId = model.CategoryId;
                    p.BranchId = model.BranchId;
                    repository.Update(p);
                }
            }
        }

        public void Delete(int id)
        {
            var ArchiveProductRepository = new ArchiveRepository();
            var ProductRepository = new ProductRepository();

            Product product = ProductRepository.GetById(id);

            if (product != null)
            {
                var ArchivedProduct = new Archive()
                {
                    ProductName=product.ProductName,
                    ProductDescription=product.ProductDescription,
                    BrandName=product.BrandName,
                    ProductColor=product.ProductColor,
                    ProductQuantity=product.ProductQuantity,
                    ProductPrice=product.ProductPrice,
                    ProductCostPrice = product.ProductCostPrice,
                    ImageUrl =product.ImageUrl,
                    DateDeleted=DateTime.Now,
                    CategoryId=product.CategoryId,
                    CategoryName=product.CategoryName,
                    BranchId = product.BranchId,
                    BranchName=product.BranchName

                };
                ArchiveProductRepository.Insert(ArchivedProduct);
            }
            using (var x = new ProductRepository())
            {
                var p = new Product();
                p = x.GetById(id);
                if (p != null)
                {
                    x.Delete(p);
                }
            }
        }

        public void DeleteCategory(int id)
        {
            var CategoryArchiveProductRepository = new CategoryArchiveRepository();
            var CategoryRepository = new CategoryRepository();

            Category category = CategoryRepository.GetById(id);

            if (category != null)
            {
                var ArchivedCategory = new CategoryArchive()
                {
                    CategoryName = category.CategoryName
                };
            CategoryArchiveProductRepository.Insert(ArchivedCategory);
            }
            using (var x = new CategoryRepository())
            {
                var p = new Category();
                p = x.GetById(id);
                if (p != null)
                {
                    x.Delete(p);
                }
            }
        }

        public void InsertProductArchive(ProductArchiveViewModel model)
        {
            using (var repository = new ArchiveRepository())
            {
                repository.Insert(new Archive
                {
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    BrandName = model.BrandName,
                    ProductColor = model.ProductColor,
                    ProductQuantity = model.ProductQuantity,
                    ProductPrice = model.ProductPrice,
                    ImageUrl = model.ImageUrl,
                    DateDeleted = DateTime.Now,
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName
                });
            }
        }
        
        public void InsertCategory(CategoryViewModel model)
        {
            using (var repository = new CategoryRepository())
            {
                repository.Insert(new Category
                {
                    CategoryName = model.CategoryName
                });
            }
        }
        public IEnumerable<CategoryViewModel> GetAllCategories()
        {
            var categories = new List<CategoryViewModel>();

            using (var repository = new CategoryRepository())
            {
                categories = repository.GetAll().Select(model => new CategoryViewModel
                {
                    Id = model.Id,
                    CategoryName =  model.CategoryName
                }).ToList();
            }
            return categories;
        }

        public CategoryViewModel GetByCategoryId(int id)
        {
            using (var repository = new CategoryRepository())
            {
                return repository.Find(x => x.Id == id).Select(model => new CategoryViewModel
                {
                    Id = model.Id,
                    CategoryName = model.CategoryName
                }).FirstOrDefault();
            }
        }

        public void UpdateCategory(CategoryViewModel model)
        {
            using (var repository = new CategoryRepository())
            {
                var c = new Category();
                c = repository.GetById(model.Id);
                if (c != null)
                {
                    c.Id = model.Id;
                    c.CategoryName = model.CategoryName;
                    repository.Update(c);
                }
            }
        }

        public IEnumerable<ProductArchiveViewModel> GetProductArchive()
        {
            var products = new List<ProductArchiveViewModel>();

            using (var repository = new ArchiveRepository())
            {
                products = repository.GetAll().Select(model => new ProductArchiveViewModel
                {
                    Id = model.Id,
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    BrandName = model.BrandName,
                    ProductColor = model.ProductColor,
                    ProductQuantity = model.ProductQuantity,
                    ProductPrice = model.ProductPrice,
                    ProductCostPrice = model.ProductCostPrice,
                    ImageUrl = model.ImageUrl,
                    DateDeleted=model.DateDeleted,
                    CategoryId = model.CategoryId,
                    CategoryName = model.Category.CategoryName

                }).ToList();
            }
            return products;
        }

        public IEnumerable<CategoryArchiveViewModel> GetCategoryArchive()
        {
            var products = new List<CategoryArchiveViewModel>();

            using (var repository = new CategoryArchiveRepository())
            {
                products = repository.GetAll().Select(model => new CategoryArchiveViewModel
                {
                    Id = model.Id,
                    CategoryName= model.CategoryName
                }).ToList();
            }
            return products;
        }

        public List<ProductViewModel> GetProductByCategory(int id)
        {
            using (var p = new ProductRepository())
            {

                return p.GetAll().Where(x => x.CategoryId == id)
                    .Select(x => new ProductViewModel()
                    {
                        Id = x.Id,
                        ProductName = x.ProductName,
                        ProductDescription = x.ProductDescription,
                        BrandName = x.BrandName,
                        ProductColor = x.ProductColor,
                        ProductQuantity = x.ProductQuantity,
                        ProductPrice = x.ProductPrice,
                        ProductCostPrice = x.ProductCostPrice,
                        ImageUrl = x.ImageUrl,
                        CategoryId = x.CategoryId

                    }).ToList();
            }
        }

        public ProductArchiveViewModel GetProductArchiveById(int id)
        {
            using (var repository = new ArchiveRepository())
            {
                return repository.Find(x => x.Id == id).Select(model => new ProductArchiveViewModel
                {
                    Id = model.Id,
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    BrandName = model.BrandName,
                    ProductColor = model.ProductColor,
                    ProductQuantity = model.ProductQuantity,
                    ProductPrice = model.ProductPrice,
                    ProductCostPrice = model.ProductCostPrice,
                    ImageUrl = model.ImageUrl,
                    CategoryId = model.CategoryId,
                    CategoryName = model.CategoryName
                }).FirstOrDefault();
            }
        }

        public void RestoreProduct(int id)
        {
            var ProductRepository = new ProductRepository();
            var ArchiveRepository = new ArchiveRepository();

            Archive product = ArchiveRepository.GetById(id);

            if (product != null)
            {
                var productRestore = new Product()
                {
                    ProductName = product.ProductName,
                    ProductDescription = product.ProductDescription,
                    BrandName = product.BrandName,
                    ProductColor = product.ProductColor,
                    ProductQuantity = product.ProductQuantity,
                    ProductPrice = product.ProductPrice,
                    ProductCostPrice = product.ProductCostPrice,
                    ImageUrl = product.ImageUrl,
                    CategoryId = product.CategoryId,
                    CategoryName = product.CategoryName,
                    BranchId=product.BranchId,
                    BranchName=product.BranchName
                };
                ProductRepository.Insert(productRestore);
            }
            using (var x = new ArchiveRepository())
            {
                var p = new Archive();
                p = x.GetById(id);
                if (p != null)
                {
                    x.Delete(p);
                }
            }
        }


        public List<ProductViewModel> GetStockPerBranch(int id)
        {
            using (var p = new ProductRepository())
            {

                return p.GetAll().Where(x => x.BranchId == id)
                    .Select(x => new ProductViewModel()
                    {
                        Id = x.Id,
                        ProductName = x.ProductName,
                        ProductQuantity = x.ProductQuantity,
                        ProductPrice = x.ProductPrice,
                        ProductCostPrice = x.ProductCostPrice,
                        ImageUrl = x.ImageUrl,
                        BranchName = x.Branch.BranchName

                    }).ToList();
            }
        }


        public List<ProductViewModel> GetSummaryPerUnit(int id)
        {

            using (var product = new ProductRepository())
            {
                return product.GetAll().Where(x => x.Id == id)
                   .Select(x => new ProductViewModel()
                   {

                       Id = x.Id,
                       ProductName = x.ProductName,
                       ProductQuantity = x.ProductQuantity,
                       ProductPrice = x.ProductPrice * x.ProductQuantity,
                       ImageUrl = x.ImageUrl,
                       BranchName = x.BranchName

                   }).ToList();
            }

        }

        public IEnumerable<ProductViewModel> GetPriceGraph()
        {
            var products = new List<ProductViewModel>();

            using (var repository = new ProductRepository())
            {
                products = repository.GetAll().Select(model => new ProductViewModel
                {
                    Id = model.Id,
                    ProductQuantity = model.ProductQuantity,
                    CategoryName = model.Category.CategoryName

                }).ToList();
            }
            return products;
        }

        public IEnumerable<ProductViewModel> GetStockPerBranchGraph(int id)
        {
            using (var p = new ProductRepository())
            {

                return p.GetAll().Where(x => x.BranchId == id)
                    .Select(x => new ProductViewModel()
                    {
                        Id = x.Id,
                        ProductQuantity = x.ProductQuantity,
                        CategoryName = x.Category.CategoryName,
                        BranchName = x.Branch.BranchName

                    }).ToList();
            }
        }
    }
}


