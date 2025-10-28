using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAFEPAY.ArqHex.Collectors.domain;
using System;

namespace CAFEPAY.Tests.Domain.ValueObjects
{
    [TestClass]
    public class CollectorStatusTests
    {
        [TestMethod]
        public void CollectorStatus_Create_WithActiveStatus_ShouldBeCreated()
        {
            // Arrange & Act
            var status = new CollectorStatus(1);

            // Assert
            Assert.AreEqual(1, status.GetValue());
            Assert.IsTrue(status.IsActive());
            Assert.AreEqual("Activo", status.GetStatusText());
        }

        [TestMethod]
        public void CollectorStatus_Create_WithInactiveStatus_ShouldBeCreated()
        {
            // Arrange & Act
            var status = new CollectorStatus(2);

            // Assert
            Assert.AreEqual(2, status.GetValue());
            Assert.IsTrue(status.IsInactive());
            Assert.AreEqual("Inactivo", status.GetStatusText());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorStatus_Create_WithInvalidStatus_ShouldThrowException()
        {
            // Arrange & Act
            var status = new CollectorStatus(3); // Status inválido
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorStatus_Create_WithZero_ShouldThrowException()
        {
            // Arrange & Act
            var status = new CollectorStatus(0); // Status inválido
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorStatus_Create_WithNegative_ShouldThrowException()
        {
            // Arrange & Act
            var status = new CollectorStatus(-1); // Status inválido
        }

        [TestMethod]
        public void CollectorStatus_StaticMethods_ShouldCreateCorrectStatus()
        {
            // Arrange & Act
            var active = CollectorStatus.Active();
            var inactive = CollectorStatus.Inactive();

            // Assert
            Assert.AreEqual(1, active.GetValue());
            Assert.AreEqual(2, inactive.GetValue());
            Assert.IsTrue(active.IsActive());
            Assert.IsTrue(inactive.IsInactive());
        }

        [TestMethod]
        public void CollectorStatus_ToString_ShouldReturnStatusText()
        {
            // Arrange
            var activeStatus = new CollectorStatus(1);
            var inactiveStatus = new CollectorStatus(2);

            // Act & Assert
            Assert.AreEqual("Activo", activeStatus.ToString());
            Assert.AreEqual("Inactivo", inactiveStatus.ToString());
        }

        [TestMethod]
        public void CollectorStatus_Equals_WithSameValue_ShouldReturnTrue()
        {
            // Arrange
            var status1 = new CollectorStatus(1);
            var status2 = new CollectorStatus(1);

            // Act & Assert
            Assert.AreEqual(status1, status2);
        }

        [TestMethod]
        public void CollectorStatus_GetHashCode_ShouldBeConsistent()
        {
            // Arrange
            var status = new CollectorStatus(1);

            // Act
            var hashCode1 = status.GetHashCode();
            var hashCode2 = status.GetHashCode();

            // Assert
            Assert.AreEqual(hashCode1, hashCode2);
        }
    }
}