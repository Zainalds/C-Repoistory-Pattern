//Shreshtha
using SmokersTavern.Business.Interfaces;
using SmokersTavern.Data.Models;
using SmokersTavern.Model;
using SmokersTavern.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SmokersTavern.Business.Business_Logic
{
    public class SupplierBusiness : ISupplierBusiness
    {
        public IEnumerable<SupplierViewModel> GetAllSuppliers()
        {
            var item = new List<SupplierViewModel>();
            using (var suppp = new SupplierRepository())
            {
                item = suppp.GetAll().Select(x => new SupplierViewModel
                {
                    supId = x.supId,
                    name = x.name,

                    phoneNo = x.phoneNo,
                    physicalAddress = x.physicalAddress,
                    emailAddress = x.emailAddress,
                    ProductName = x.ProductName,
                    prodId = x.prodId
                }).ToList();

                using (var repository = new ProductRepository())
                {
                    item.ForEach(x => x.Product = repository.Find(y => y.Id == x.prodId).Select(model => new ProductViewModel
                    {
                        Id = model.Id,
                        ProductName = model.ProductName
                    }).FirstOrDefault());
                }
                return item;
            }

        }


        public SupplierViewModel GetById(int id)
        {
            using (var su = new SupplierRepository())
            {
                return su.Find(x => x.supId == id).Select(model => new SupplierViewModel
                {
                    supId = model.supId,
                    name = model.name,

                    phoneNo = model.phoneNo,
                    physicalAddress = model.physicalAddress,
                    emailAddress = model.emailAddress,
                    prodId = model.prodId,

                    ProductName = model.ProductName
                }).FirstOrDefault();
            }
        }

        public void Insert(SupplierViewModel model)
        {
            using (var p = new ProductRepository())
            {
                var s = new Product();
                s = p.GetById(model.prodId);
                string dummy = s.ProductName;

                using (var supp = new SupplierRepository())
                {
                    supp.Insert(new Supplier
                    {
                        name = model.name,
                        phoneNo = model.phoneNo,
                        physicalAddress = model.physicalAddress,
                        emailAddress = model.emailAddress,
                        prodId = model.prodId,
                        ProductName = dummy,
                    });
                }
            }
        }

        public void Update(SupplierViewModel model)
        {
            using (var p = new SupplierRepository())
            {
                var s = new Supplier();
                s = p.GetById(model.supId);
                if (s != null)
                {
                    s.supId = model.supId;
                    s.name = model.name;
                    s.phoneNo = model.phoneNo;
                    s.physicalAddress = model.physicalAddress;
                    s.emailAddress = model.emailAddress;
                    s.prodId = model.prodId;

                    p.Update(s);
                }
            }
        }



        public void Delete(int id)
        {
            var Repo = new SupplierArchiveRepository();
            var SupplierRepo = new SupplierRepository();

            Supplier model = SupplierRepo.GetById(id);

            if (model != null)
            {
                var ArchSupp = new ArchiveSupplier()
                {
                    SupplierId = model.supId,
                    name = model.name,
                    phoneNo = model.phoneNo,
                    physicalAddress = model.physicalAddress,
                    emailAddress = model.emailAddress,
                    ProductId = model.prodId,
                    ProductName = model.ProductName
                };

                Repo.Insert(ArchSupp);
            }

            using (var p = new SupplierRepository())
            {
                var s = new Supplier();
                s = p.GetById(id);
                if (s != null)
                {
                    p.Delete(s);
                }
            }
        }

        public IEnumerable<SupplierViewModel> GetAllProducts(int id)
        {
            using (var repository = new SupplierRepository())
            {
                return repository.Find(x => x.prodId == id).Select(model => new SupplierViewModel
                {

                    supId = model.supId,
                    name = model.name,

                    phoneNo = model.phoneNo,
                    physicalAddress = model.physicalAddress,
                    emailAddress = model.emailAddress,
                    prodId = model.prodId

                }).ToList();
            }
        }

        public ProductViewModel GetByPoductId(int id)
        {
            using (var repository = new ProductRepository())
            {
                return repository.Find(x => x.Id == id).Select(model => new ProductViewModel
                {
                    Id = model.Id,
                    ProductName = model.ProductName
                }).FirstOrDefault();
            }
        }

        public List<ProductViewModel> GetProductTypes()
        {
            using (var repository = new ProductRepository())
            {
                return repository.GetAll().Select(model => new ProductViewModel
                {
                    Id = model.Id,
                    ProductName = model.ProductName
                }).ToList();
            }
        }

        public void RestoreSupplier(int id)
        {
            var SupplierRepository = new SupplierRepository();
            var SupplierArchiveRepository = new SupplierArchiveRepository();

            ArchiveSupplier product = SupplierArchiveRepository.GetById(id);

            if (product != null)
            {
                var productRestore = new Supplier()
                {
                    name = product.name,
                    physicalAddress = product.physicalAddress,
                    emailAddress = product.emailAddress,
                    phoneNo = product.phoneNo,
                    prodId = product.ProductId,
                    ProductName = product.ProductName
                };
                SupplierRepository.Insert(productRestore);
            }
            using (var x = new SupplierArchiveRepository())
            {
                var p = new ArchiveSupplier();
                p = x.GetById(id);
                if (p != null)
                {
                    x.Delete(p);
                }
            }
        }
    }
}
