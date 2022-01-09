using StockControl.API.Models;
using StockControl.Shared.Requests;

namespace StockControl.API.Mappers
{
    public interface ISupplierMapper
    {
        SupplierDetail Map_Supplier_To_SupplierDetail(Supplier supplier, SupplierDetail supplierDetail);
        Supplier Map_SupplierDetail_To_Supplier(SupplierDetail supplierDetail, Supplier supplier);
    }

    public class SupplierMapper : ISupplierMapper
    {
        public SupplierDetail Map_Supplier_To_SupplierDetail(Supplier supplier, SupplierDetail supplierDetail)
        {
            supplierDetail.Id = supplier.Id;
            supplierDetail.Name = supplier.Name;
            supplierDetail.Add1 = supplier.Add1;
            supplierDetail.Add2 = supplier.Add2;
            supplierDetail.Add3 = supplier.Add3;
            supplierDetail.Postcode = supplier.Postcode;
            supplierDetail.Email = supplier.Email;
            supplierDetail.Website = supplier.Website;
            supplierDetail.Contact = supplier.Contact;

            return supplierDetail;
        }

        public Supplier Map_SupplierDetail_To_Supplier(SupplierDetail supplierDetail, Supplier supplier)
        {
            if (!string.IsNullOrEmpty(supplierDetail.Id)) supplier.Id = supplierDetail.Id;

            supplier.Name = supplierDetail.Name;
            supplier.Add1 = supplierDetail.Add1;
            supplier.Add2 = supplierDetail.Add2;
            supplier.Add3 = supplierDetail.Add3;
            supplier.Postcode = supplierDetail.Postcode;
            supplier.Email = supplierDetail.Email;
            supplier.Website = supplierDetail.Website;
            supplier.Contact = supplierDetail.Contact;

            return supplier;
        }
    }
}
