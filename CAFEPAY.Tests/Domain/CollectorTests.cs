using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAFEPAY.ArqHex.Collectors.domain;

namespace CAFEPAY.Tests.Domain
{
    [TestClass]
    public class CollectorTests
    {
        [TestMethod]
        public void Collector_Create_WithValidData_ShouldBeCreated()
        {
            // Arrange
            var workerCode = new CollectorWorkerCode("W00001");
            var id = new CollectorId(12345678L);
            var firstName = new CollectorFirstName("Juan");
            var lastName = new CollectorLastName("Pérez");
            var phone = new CollectorPhone("3001234567");
            var status = new CollectorStatus(1);

            // Act
            var collector = new Collector(workerCode, id, firstName, lastName, phone, status);

            // Assert
            Assert.IsNotNull(collector);
            Assert.AreEqual("W00001", collector.workerCode.GetValue());
            Assert.AreEqual(12345678L, collector.id.GetValue());
            Assert.AreEqual("Juan", collector.firstName.GetValue());
            Assert.AreEqual("Pérez", collector.lastName.GetValue());
            Assert.AreEqual("3001234567", collector.phone.GetValue());
            Assert.AreEqual(1, collector.status.GetValue());
        }

        [TestMethod]
        public void Collector_Create_WithInactiveStatus_ShouldBeCreated()
        {
            // Arrange
            var workerCode = new CollectorWorkerCode("W00002");
            var id = new CollectorId(87654321L);
            var firstName = new CollectorFirstName("María");
            var lastName = new CollectorLastName("Gómez");
            var phone = new CollectorPhone("3101234567");
            var status = new CollectorStatus(2);

            // Act
            var collector = new Collector(workerCode, id, firstName, lastName, phone, status);

            // Assert
            Assert.IsNotNull(collector);
            Assert.AreEqual("W00002", collector.workerCode.GetValue());
            Assert.AreEqual(87654321L, collector.id.GetValue());
            Assert.AreEqual("María", collector.firstName.GetValue());
            Assert.AreEqual("Gómez", collector.lastName.GetValue());
            Assert.AreEqual("3101234567", collector.phone.GetValue());
            Assert.AreEqual(2, collector.status.GetValue());
            Assert.IsTrue(collector.status.IsInactive());
        }

        [TestMethod]
        public void Collector_Create_WithComplexName_ShouldBeCreated()
        {
            // Arrange
            var workerCode = new CollectorWorkerCode("W00123");
            var id = new CollectorId(13579246L); // ID con dígitos diferentes
            var firstName = new CollectorFirstName("María");
            var lastName = new CollectorLastName("García López");
            var phone = new CollectorPhone("3201234567");
            var status = new CollectorStatus(1);

            // Act
            var collector = new Collector(workerCode, id, firstName, lastName, phone, status);

            // Assert
            Assert.IsNotNull(collector);
            Assert.AreEqual("María", collector.firstName.GetValue());
            Assert.AreEqual("García López", collector.lastName.GetValue());
        }

        [TestMethod]
        public void Collector_Properties_ShouldBeImmutable()
        {
            // Arrange
            var workerCode = new CollectorWorkerCode("W00001");
            var id = new CollectorId(12345678L);
            var firstName = new CollectorFirstName("Juan");
            var lastName = new CollectorLastName("Pérez");
            var phone = new CollectorPhone("3001234567");
            var status = new CollectorStatus(1);

            // Act
            var collector = new Collector(workerCode, id, firstName, lastName, phone, status);

            // Assert
            Assert.IsNotNull(collector.workerCode);
            Assert.IsNotNull(collector.id);
            Assert.IsNotNull(collector.firstName);
            Assert.IsNotNull(collector.lastName);
            Assert.IsNotNull(collector.phone);
            Assert.IsNotNull(collector.status);
        }

        [TestMethod]
        public void Collector_Create_MultipleInstances_ShouldBeIndependent()
        {
            // Arrange - IDs con dígitos diferentes
            var collector1 = CreateSampleCollector("W00001", 12345678L, "Ana", "Gómez", "3111111111", 1);
            var collector2 = CreateSampleCollector("W00002", 87654321L, "Carlos", "López", "3222222222", 2);

            // Act & Assert
            Assert.AreEqual("W00001", collector1.workerCode.GetValue());
            Assert.AreEqual("W00002", collector2.workerCode.GetValue());
            Assert.AreEqual("Ana", collector1.firstName.GetValue());
            Assert.AreEqual("Carlos", collector2.firstName.GetValue());
            Assert.IsTrue(collector1.status.IsActive());
            Assert.IsTrue(collector2.status.IsInactive());
        }

        [TestMethod]
        public void Collector_WithDifferentPhoneFormats_ShouldWork()
        {
            // Arrange & Act - IDs con dígitos diferentes
            var collector1 = CreateSampleCollector("W00001", 12345678L, "Juan", "Pérez", "3001234567", 1);
            var collector2 = CreateSampleCollector("W00002", 23456789L, "María", "Gómez", "3101234567", 1);
            var collector3 = CreateSampleCollector("W00003", 34567890L, "Pedro", "López", "3201234567", 1);

            // Assert
            Assert.AreEqual("3001234567", collector1.phone.GetValue());
            Assert.AreEqual("3101234567", collector2.phone.GetValue());
            Assert.AreEqual("3201234567", collector3.phone.GetValue());
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