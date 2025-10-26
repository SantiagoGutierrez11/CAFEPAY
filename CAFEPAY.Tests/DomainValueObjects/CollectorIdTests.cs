using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAFEPAY.ArqHex.Collectors.domain;
using System;

namespace CAFEPAY.Tests.Domain.ValueObjects
{
    [TestClass]
    public class CollectorIdTests
    {
        [TestMethod]
        public void CollectorId_Create_WithValidId_ShouldBeCreated()
        {
            // Arrange & Act
            var id = new CollectorId(12345678L);

            // Assert
            Assert.AreEqual(12345678L, id.GetValue());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorId_Create_WithShortId_ShouldThrowException()
        {
            // Arrange & Act
            var id = new CollectorId(1234567L); // 7 dígitos
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorId_Create_WithLongId_ShouldThrowException()
        {
            // Arrange & Act
            var id = new CollectorId(12345678901L); // 11 dígitos
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorId_Create_WithLeadingZero_ShouldThrowException()
        {
            // Arrange & Act
            var id = new CollectorId(01234567L); // Empieza con 0
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorId_Create_WithAllSameDigits_ShouldThrowException()
        {
            // Arrange & Act
            var id = new CollectorId(11111111L); // Todos los dígitos iguales
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorId_Create_WithNegativeId_ShouldThrowException()
        {
            // Arrange & Act
            var id = new CollectorId(-12345678L); // Número negativo
        }

        [TestMethod]
        public void CollectorId_Create_WithMinimumLength_ShouldBeCreated()
        {
            // Arrange & Act
            var id = new CollectorId(10000000L); // 8 dígitos, mínimo permitido

            // Assert
            Assert.AreEqual(10000000L, id.GetValue());
        }

        [TestMethod]
        public void CollectorId_Create_WithMaximumLength_ShouldBeCreated()
        {
            // Arrange & Act - 10 dígitos, NO todos iguales
            var id = new CollectorId(9876543210L);

            // Assert
            Assert.AreEqual(9876543210L, id.GetValue());
        }

        [TestMethod]
        public void CollectorId_Create_WithTenDigits_ShouldBeCreated()
        {
            // Arrange & Act
            var id = new CollectorId(1234567890L);

            // Assert
            Assert.AreEqual(1234567890L, id.GetValue());
        }

        // Esta prueba documenta el comportamiento esperado
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorId_Create_WithAllNines_ShouldThrowException()
        {
            // Arrange & Act - 10 dígitos todos iguales (9s)
            var id = new CollectorId(9999999999L);
        }

        [TestMethod]
        public void CollectorId_ToString_ShouldReturnStringRepresentation()
        {
            // Arrange
            var id = new CollectorId(12345678L);

            // Act
            var result = id.ToString();

            // Assert
            Assert.AreEqual("12345678", result);
        }

        [TestMethod]
        public void CollectorId_GetValueAsString_ShouldReturnString()
        {
            // Arrange
            var id = new CollectorId(12345678L);

            // Act
            var result = id.GetValueAsString();

            // Assert
            Assert.AreEqual("12345678", result);
        }
    }
}