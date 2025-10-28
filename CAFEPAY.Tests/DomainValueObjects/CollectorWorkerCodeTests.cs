using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAFEPAY.ArqHex.Collectors.domain;
using System;

namespace CAFEPAY.Tests.Domain.ValueObjects
{
    [TestClass]
    public class CollectorWorkerCodeTests
    {
        [TestMethod]
        public void CollectorWorkerCode_Create_WithValidCode_ShouldBeCreated()
        {
            // Arrange & Act
            var workerCode = new CollectorWorkerCode("W00001");

            // Assert
            Assert.AreEqual("W00001", workerCode.GetValue());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorWorkerCode_Create_WithInvalidLength_ShouldThrowException()
        {
            // Arrange & Act
            var workerCode = new CollectorWorkerCode("W001"); // Muy corto
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorWorkerCode_Create_WithoutW_ShouldThrowException()
        {
            // Arrange & Act
            var workerCode = new CollectorWorkerCode("X00001"); // No empieza con W
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorWorkerCode_Create_WithNonDigits_ShouldThrowException()
        {
            // Arrange & Act
            var workerCode = new CollectorWorkerCode("W00A01"); // Letras en lugar de dígitos
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorWorkerCode_Create_WithAllZeros_ShouldThrowException()
        {
            // Arrange & Act
            var workerCode = new CollectorWorkerCode("W00000"); // Todos ceros
        }

        [TestMethod]
        public void CollectorWorkerCode_GetNumericPart_ShouldReturnCorrectNumber()
        {
            // Arrange
            var workerCode = new CollectorWorkerCode("W00123");

            // Act
            var numericPart = workerCode.GetNumericPart();

            // Assert
            Assert.AreEqual(123, numericPart);
        }

        [TestMethod]
        public void CollectorWorkerCode_ToString_ShouldReturnCode()
        {
            // Arrange
            var workerCode = new CollectorWorkerCode("W00456");

            // Act
            var result = workerCode.ToString();

            // Assert
            Assert.AreEqual("W00456", result);
        }

        [TestMethod]
        public void CollectorWorkerCode_Equals_WithSameCode_ShouldReturnTrue()
        {
            // Arrange
            var code1 = new CollectorWorkerCode("W00789");
            var code2 = new CollectorWorkerCode("W00789");

            // Act & Assert
            Assert.AreEqual(code1, code2);
        }

        [TestMethod]
        public void CollectorWorkerCode_Create_WithLowerCaseW_ShouldConvertToUpper()
        {
            // Arrange & Act
            var workerCode = new CollectorWorkerCode("w00123"); // w minúscula

            // Assert
            Assert.AreEqual("W00123", workerCode.GetValue()); // Debería convertir a mayúscula
        }
    }
}
