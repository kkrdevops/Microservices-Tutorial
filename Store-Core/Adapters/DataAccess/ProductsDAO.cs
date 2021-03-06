﻿using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Simple.Data;
using Store_Core.ReferenceData;

namespace Store_Core.Adapters.DataAccess
{
    public class ProductsDAO : IProductsDAO
    {
        private readonly dynamic _db;

        public ProductsDAO()
        {
            if (System.Web.HttpContext.Current != null)
            {
                var databasePath = System.Web.HttpContext.Current.Server.MapPath("~\\App_Data\\Store.sdf");
                _db = Database.Opener.OpenFile(databasePath);
            }
            else
            {
                var file = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase.Substring(8)), "App_Data\\Store.sdf");

                _db = Database.OpenFile(file);
            }
        }

        public dynamic BeginTransaction()
        {
            return _db.BeginTransaction();
        }

        public ProductReference Add(ProductReference newProductReference)
        {
            return _db.Products.Insert(newProductReference);
        }

        public void Clear()
        {
            _db.Products.DeleteAll();
        }

        public void Delete(int productId)
        {
            _db.Products.DeleteById(productId);
        }

        public IEnumerable<ProductReference> FindAll()
        {
            return _db.Products.All().ToList<ProductReference>();
        }

        public ProductReference FindById(int id)
        {
            return _db.Products.FindById(id);
        }

        public void Update(ProductReference productReference)
        {
            _db.Products.UpdateById(productReference);
        }

    }
}
