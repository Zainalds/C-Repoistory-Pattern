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
    public class SupplierArchiveBusiness : ISupplierArchiveBusiness
    {
        public IEnumerable<SupplierArchiveViewModel> GetAllArchives()
        {

            SupplierArchiveRepository repo = new SupplierArchiveRepository();
            {
                return repo.GetAll().Select(model => new SupplierArchiveViewModel
                        {
                            Id = model.Id,
                            SupplierId = model.SupplierId,
                            name = model.name,
                            phoneNo = model.phoneNo,
                            physicalAddress = model.physicalAddress,
                            emailAddress = model.emailAddress,
                            ProductId = model.ProductId,
                            ProductName = model.ProductName
                        }).ToList();
            }
        }

        public SupplierArchiveViewModel GetById(int id)
        {

            SupplierArchiveRepository repo = new SupplierArchiveRepository();
            {
                ArchiveSupplier model = repo.GetById(id);
                var a = new SupplierArchiveViewModel();
                if (model != null)
                {
                    a.Id = model.Id;
                    a.SupplierId = model.SupplierId;
                    a.name = model.name;
                    a.phoneNo = model.phoneNo;
                    a.physicalAddress = model.physicalAddress;
                    a.emailAddress = model.emailAddress;
                    a.ProductId = model.ProductId;
                    a.ProductName = model.ProductName;
                }
                return a;
            }
        }

        public void Delete(int id)
        {
            var repo = new SupplierArchiveRepository();

            ArchiveSupplier model = repo.GetById(id);

            int SuppId = model.SupplierId;
            string SuppName = model.name;
            string SuppEmail = model.emailAddress;
            string SuppAddress = model.physicalAddress;
            string SuppNumber = model.phoneNo;
            string SuppProdName = model.ProductName;

            if (model != null)
            {
                using (var p = new SupplierRepository())
                {
                    var s = new Supplier();
                    s = p.GetById(SuppId);
                    if (s != null)
                    {
                        s.name = SuppName;
                        s.phoneNo = SuppNumber;
                        s.physicalAddress = SuppAddress;
                        s.emailAddress = SuppEmail;
                        s.ProductName = SuppProdName;
                        p.Update(s);
                    }
                }

            }
            repo.Delete(model);
        }
    }
}
