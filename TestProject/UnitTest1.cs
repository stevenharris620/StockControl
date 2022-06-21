using StockControl.API.Mappers;
using StockControl.API.Models;
using StockControl.Shared.Requests;

namespace TestProject
{
    public class Tests
    {
        private Supplier _supplier;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_Supplier_To_VM_Mapper()
        {
            // Arrange

            _supplier = new Supplier
            {
                Id = "id",
                Name = "Name",
                Add1 = "Add1",
                Add2 = "Add2",
                Add3 = "Add3",
                Postcode = "Postcode",
                Email = "Email",
                Website = "Website",
                Contact = "Contact"
            };

            var mapper = new SupplierMapper();

            // Act

            var vm = mapper.Map_Supplier_To_SupplierDetail(_supplier, new SupplierDetail());

            // Assert

            Assert.IsNotNull(vm);
            Assert.AreEqual(_supplier.Id,vm.Id);
            Assert.AreEqual(_supplier.Name, vm.Name);
            Assert.AreEqual( _supplier.Add1, vm.Add1);
            Assert.AreEqual(_supplier.Add2, vm.Add2);
            Assert.AreEqual( _supplier.Add3, vm.Add3);
            Assert.AreEqual(_supplier.Postcode, vm.Postcode);
            Assert.AreEqual( _supplier.Email, vm.Email);
            Assert.AreEqual( _supplier.Website, vm.Website);
            Assert.AreEqual( _supplier.Contact, vm.Contact);

        }
    }
}