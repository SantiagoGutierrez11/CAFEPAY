using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CAFEPAY.ArqHex.Collectors.application.CollectorQueryAll;
using CAFEPAY.ArqHex.Collectors.domain;
using System.Collections.Generic;

namespace CAFEPAY.Tests.Application
{
    [TestClass]
    public class CollectorQueryAllTests
    {
        private Mock<CollectorRepository> _mockRepository;
        private CollectorQueryAll _collectorQueryAll;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<CollectorRepository>();
            _collectorQueryAll = new CollectorQueryAll(_mockRepository.Object);
        }

        [TestMethod]
        public void Execute_ShouldReturnListOfCollectors()
        {
            // Arrange
            var expectedCollectors = new List<Collector>
            {
                CreateSampleCollector("W00001", 12345678L, "Juan", "Pérez", "3001234567", 1),
                CreateSampleCollector("W00002", 87654321L, "María", "Gómez", "3101234567", 1),
                CreateSampleCollector("W00003", 13579246L, "Carlos", "López", "3201234567", 2)
            };

            _mockRepository.Setup(r => r.queryAll()).Returns(expectedCollectors);

            // Act
            var result = _collectorQueryAll.execute();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            _mockRepository.Verify(r => r.queryAll(), Times.Once);
        }

        [TestMethod]
        public void Execute_WithEmptyList_ShouldReturnEmptyList()
        {
            // Arrange
            _mockRepository.Setup(r => r.queryAll()).Returns(new List<Collector>());

            // Act
            var result = _collectorQueryAll.execute();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Execute_WithSingleCollector_ShouldReturnListWithOneElement()
        {
            // Arrange
            var expectedCollectors = new List<Collector>
            {
                CreateSampleCollector("W00001", 12345678L, "Ana", "Rodríguez", "3001234567", 1)
            };

            _mockRepository.Setup(r => r.queryAll()).Returns(expectedCollectors);

            // Act
            var result = _collectorQueryAll.execute();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Ana", result[0].firstName.GetValue());
            Assert.AreEqual("Rodríguez", result[0].lastName.GetValue());
        }

    
        [TestMethod]
        public void Execute_WithMixedStatus_ShouldReturnAllCollectors()
        {
            // Arrange
            var expectedCollectors = new List<Collector>
            {
                CreateSampleCollector("W00001", 12345678L, "Activo", "Usuario", "3001234567", 1),
                CreateSampleCollector("W00002", 87654321L, "Inactivo", "Usuario", "3101234567", 2)
            };

            _mockRepository.Setup(r => r.queryAll()).Returns(expectedCollectors);

            // Act
            var result = _collectorQueryAll.execute();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0].status.IsActive());
            Assert.IsTrue(result[1].status.IsInactive());
        }

        private Collector CreateSampleCollector(string workerCode, long id, string firstName, string lastName, string phone, int status)
        {
            return new Collector(
                new CollectorWorkerCode(workerCode),
                new CollectorId(id),
                new CollectorFirstName(firstName),
                new CollectorLastName(lastName),
                new CollectorPhone(phone),
                new CollectorStatus(status)
            );
        }
    }
}
