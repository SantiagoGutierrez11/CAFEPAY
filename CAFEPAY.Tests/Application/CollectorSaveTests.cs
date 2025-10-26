using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CAFEPAY.ArqHex.Collectors.application.CollectorSave;
using CAFEPAY.ArqHex.Collectors.domain;
using System;

namespace CAFEPAY.Tests.Application
{
    [TestClass]
    public class CollectorSaveTests
    {
        private Mock<CollectorRepository> _mockRepository;
        private CollectorSave _collectorSave;

        [TestInitialize]
        public void Setup()
        {
            _mockRepository = new Mock<CollectorRepository>();
            _collectorSave = new CollectorSave(_mockRepository.Object);
        }

        [TestMethod]
        public void Execute_WithValidData_ShouldCallRepositorySave()
        {
            // Arrange
            _mockRepository.Setup(r => r.save(It.IsAny<Collector>()));

            // Act
            _collectorSave.execute("W00001", 12345678L, "Juan", "Pérez", "3001234567", 1);

            // Assert
            _mockRepository.Verify(r => r.save(It.IsAny<Collector>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Execute_WithInvalidPhone_ShouldThrowException()
        {
            // Arrange
            _mockRepository.Setup(r => r.save(It.IsAny<Collector>()));

            // Act - Teléfono inválido (no empieza con 3)
            _collectorSave.execute("W00001", 12345678L, "Juan", "Pérez", "4001234567", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Execute_WithInvalidWorkerCode_ShouldThrowException()
        {
            // Arrange
            _mockRepository.Setup(r => r.save(It.IsAny<Collector>()));

            // Act - Código de trabajador inválido
            _collectorSave.execute("X00001", 12345678L, "Juan", "Pérez", "3001234567", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Execute_WithInvalidId_ShouldThrowException()
        {
            // Arrange
            _mockRepository.Setup(r => r.save(It.IsAny<Collector>()));

            // Act - ID inválido (muy corto)
            _collectorSave.execute("W00001", 1234567L, "Juan", "Pérez", "3001234567", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Execute_WithInvalidFirstName_ShouldThrowException()
        {
            // Arrange
            _mockRepository.Setup(r => r.save(It.IsAny<Collector>()));

            // Act - Nombre inválido (muy corto)
            _collectorSave.execute("W00001", 12345678L, "Jo", "Pérez", "3001234567", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Execute_WithInvalidLastName_ShouldThrowException()
        {
            // Arrange
            _mockRepository.Setup(r => r.save(It.IsAny<Collector>()));

            // Act - Apellido inválido (con números)
            _collectorSave.execute("W00001", 12345678L, "Juan", "Pérez123", "3001234567", 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Execute_WithInvalidStatus_ShouldThrowException()
        {
            // Arrange
            _mockRepository.Setup(r => r.save(It.IsAny<Collector>()));

            // Act - Status inválido
            _collectorSave.execute("W00001", 12345678L, "Juan", "Pérez", "3001234567", 3);
        }

        [TestMethod]
        public void Execute_WithInactiveStatus_ShouldCallRepository()
        {
            // Arrange
            _mockRepository.Setup(r => r.save(It.IsAny<Collector>()));

            // Act - Status inactivo (2) es válido
            _collectorSave.execute("W00001", 12345678L, "Juan", "Pérez", "3001234567", 2);

            // Assert
            _mockRepository.Verify(r => r.save(It.IsAny<Collector>()), Times.Once);
        }

        [TestMethod]
        public void Execute_ShouldCreateCollectorWithCorrectData()
        {
            // Arrange
            Collector savedCollector = null;
            _mockRepository.Setup(r => r.save(It.IsAny<Collector>()))
                .Callback<Collector>(collector => savedCollector = collector);

            // Act
            _collectorSave.execute("W00015", 98765432L, "María", "Gómez López", "3101234567", 1);

            // Assert
            Assert.IsNotNull(savedCollector);
            Assert.AreEqual("W00015", savedCollector.workerCode.GetValue());
            Assert.AreEqual(98765432L, savedCollector.id.GetValue());
            Assert.AreEqual("María", savedCollector.firstName.GetValue());
            Assert.AreEqual("Gómez López", savedCollector.lastName.GetValue());
            Assert.AreEqual("3101234567", savedCollector.phone.GetValue());
            Assert.AreEqual(1, savedCollector.status.GetValue());
        }
    }
}

