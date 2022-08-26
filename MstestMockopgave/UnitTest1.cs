using Databasetest.Controllers;
using Databasetest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using Moq;
using System.Net.Sockets;
using System.Reflection.Metadata;

namespace MstestMockopgave
{
    [TestClass]
    public class UnitTest1
    {
        private List<Cereal> list;
        private Cereal cereal;
        private Cereal cerealUpdate;
        private Mock<IJWTManagerRepository> jwtManager;
        public CerealsController _controller;
        public Mock<BallingdatabaseContext> mockContext;
        public Mock<DbSet<Cereal>> mockset;
        public List<Cereal> list2;

        [TestInitialize]
        public void Setup()
        {
            list = new List<Cereal>();
            cereal = new Cereal()  {Id = 3, Name = "SebastianMorgenmad" , Mfr = "N", Type = "H", Calories = 100, Protein = 3, Fat = 0, Sodium = 80, Fiber = 1, Carbo = 21 , Sugars = 0, Potass = -1, Vitamins = 0, Shelf = 2, Weight = 1, Cups = 1, Rating = 64533816, Picture = "C:\\Users\\KOM\\source\\repos\\Apiopgave\\CSVReader\\CerealOpgave\\SebastianMorgenmad.png" };
            cerealUpdate = new Cereal() { Id = 10, Name = "SebastianMorgenmad", Mfr = "N", Type = "H", Calories = 100, Protein = 3, Fat = 0, Sodium = 80, Fiber = 1, Carbo = 21, Sugars = 0, Potass = -1, Vitamins = 0, Shelf = 2, Weight = 1, Cups = 1, Rating = 64533816, Picture = "C:\\Users\\KOM\\source\\repos\\Apiopgave\\CSVReader\\CerealOpgave\\SebastianMorgenmad.png" };
            NotFoundObjectResult notFoundObjectResult = new NotFoundObjectResult(cereal);
            list.Add(cereal);
            list.AsQueryable();
            jwtManager = new Mock<IJWTManagerRepository> ();
            mockContext = new Mock<BallingdatabaseContext>();
            mockset = new Mock<DbSet<Cereal>>();
            list2 = new List<Cereal> { new Cereal { Id = 3, Name = "choco", Protein = 8 }, new Cereal { Id = 4, Name = "Havre", Protein = 4 } };
            mockset.As<IQueryable<Cereal>>().Setup(m => m.Provider).Returns(list2.AsQueryable().Provider);
            mockset.As<IQueryable<Cereal>>().Setup(m => m.Expression).Returns(list2.AsQueryable().Expression);
            mockset.As<IQueryable<Cereal>>().Setup(m => m.ElementType).Returns(list2.AsQueryable().ElementType);
            mockset.As<IQueryable<Cereal>>().Setup(m => m.GetEnumerator()).Returns(list2.AsQueryable().GetEnumerator());
            mockContext.Setup(m => m.Cereals).Returns(mockset.Object);
            _controller = new CerealsController(mockContext.Object, jwtManager.Object);
        }

        [TestMethod]
        public void GetAll()
        {
           
            var output = _controller.AllCereals();

            Assert.AreEqual(output.Count(), 2);
        }

        [TestMethod]
        public void GetAllCerealDetails()
        {
            
            var output = _controller.CerealsDetails(3);

            Assert.IsNotNull(output.Result);
            Assert.IsInstanceOfType(output.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public void GetAllDetailsError()
        {
            
            var output = _controller.CerealsDetails(2);

            Assert.IsNotNull(output.Result);
            Assert.IsInstanceOfType(output.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public void GetCerealbyParameterGreatherthan()
        {
            
            int? proteinresult = 0;
          
            var output = _controller.GetCerealByparameter(null,null,null,null,null, null, null, null, null, 4, null, null, null, null, null, null, null, null, null, null , null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            foreach (var item in output)
            {
                proteinresult = item.Protein;
            }
           Assert.AreEqual(8, proteinresult);
        }

        [TestMethod]
        public void GetCerealbyParameterlesserthan()
        {
           
            int? proteinresult = 0;        
            var output = _controller.GetCerealByparameter(null, null, null, null, null, null, null, null, null, null, 5, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            foreach (var item in output)
            {
                proteinresult = item.Protein;
            } 

            Assert.AreEqual(4, proteinresult);
        }

        public void GetCerealbyParameterequal()
        {

            int? proteinresult = 0;
           
            var output = _controller.GetCerealByparameter(null, null, null, null, null, null, null, null, 4, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);

            foreach (var item in output)
            {
                proteinresult = item.Protein;
            } 

            Assert.AreEqual(4, proteinresult);
        }


        [TestMethod]
        public void CreatCerealReturnsObject()
        {

            _controller.CreateCereal(cereal); 
            mockContext.Verify(m => m.Add(It.IsAny<Cereal>()), Times.Once()); 
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateCerealReturnsOkObject()
        {
           
            var output = _controller.UpdateCereal(cereal);
            mockContext.Verify(m => m.Update(It.IsAny<Cereal>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateCerealReturnsNotFoundObject()
        {

            var output = _controller.UpdateCereal(cerealUpdate);
            mockContext.Verify(m => m.Update(It.IsAny<Cereal>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }

        [TestMethod]
        public void DeleteCerealReturnsOkObject()
        {
         
            var output = _controller.DeleteCereal(3);

            Assert.IsInstanceOfType(output.Result, typeof(OkObjectResult));
            Assert.AreEqual(list.Count, 1);
            mockset.Verify(m => m.Remove(It.IsAny<Cereal>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void DeleteCerealReturnsNotFound()
        {
           
            var output = _controller.DeleteCereal(2);

            Assert.IsInstanceOfType(output.Result, typeof(NotFoundObjectResult));
            mockset.Verify(m => m.Remove(It.IsAny<Cereal>()), Times.Never());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }


    }
}