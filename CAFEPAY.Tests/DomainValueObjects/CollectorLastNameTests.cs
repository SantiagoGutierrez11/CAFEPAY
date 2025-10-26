using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAFEPAY.ArqHex.Collectors.domain;
using System;

namespace CAFEPAY.Tests.Domain.ValueObjects
{
    [TestClass]
    public class CollectorLastNameTests
    {
        [TestMethod]
        public void CollectorLastName_Create_WithValidLastName_ShouldBeCreated()
        {
            // Arrange & Act
            var lastName = new CollectorLastName("Pérez González");

            // Assert
            Assert.AreEqual("Pérez González", lastName.GetValue());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorLastName_Create_WithEmptyLastName_ShouldThrowException()
        {
            // Arrange & Act
            var lastName = new CollectorLastName("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorLastName_Create_WithNull_ShouldThrowException()
        {
            // Arrange & Act
            var lastName = new CollectorLastName(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorLastName_Create_WithShortLastName_ShouldThrowException()
        {
            // Arrange & Act
            var lastName = new CollectorLastName("Lo"); // 2 caracteres
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorLastName_Create_WithLongLastName_ShouldThrowException()
        {
            // Arrange & Act
            var lastName = new CollectorLastName("Este es un apellido muy largo que excede el límite");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorLastName_Create_WithNumbers_ShouldThrowException()
        {
            // Arrange & Act
            var lastName = new CollectorLastName("Pérez123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorLastName_Create_WithSpecialCharacters_ShouldThrowException()
        {
            // Arrange & Act
            var lastName = new CollectorLastName("Pérez@González");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorLastName_Create_WithLeadingSpace_ShouldThrowException()
        {
            // Arrange & Act
            var lastName = new CollectorLastName(" Pérez");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorLastName_Create_WithTrailingSpace_ShouldThrowException()
        {
            // Arrange & Act
            var lastName = new CollectorLastName("Pérez ");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorLastName_Create_WithMultipleSpaces_ShouldThrowException()
        {
            // Arrange & Act
            var lastName = new CollectorLastName("Pérez  González");
        }

        [TestMethod]
        public void CollectorLastName_Create_WithAccents_ShouldBeCreated()
        {
            // Arrange & Act
            var lastName = new CollectorLastName("Muñoz García");

            // Assert
            Assert.AreEqual("Muñoz García", lastName.GetValue());
        }

        [TestMethod]
        public void CollectorLastName_Create_WithApostrophe_ShouldBeCreated()
        {
            // Arrange & Act
            var lastName = new CollectorLastName("O'Connor");

            // Assert
            Assert.AreEqual("O'Connor", lastName.GetValue());
        }

        [TestMethod]
        public void CollectorLastName_Create_WithHyphen_ShouldBeCreated()
        {
            // Arrange & Act
            var lastName = new CollectorLastName("Smith-Jones");

            // Assert
            Assert.AreEqual("Smith-Jones", lastName.GetValue());
        }

        [TestMethod]
        public void CollectorLastName_Equals_WithSameValue_ShouldReturnTrue()
        {
            // Arrange
            var lastName1 = new CollectorLastName("Gómez");
            var lastName2 = new CollectorLastName("Gómez");

            // Act & Assert
            Assert.AreEqual(lastName1, lastName2);
        }

        [TestMethod]
        public void CollectorLastName_ToString_ShouldReturnLastName()
        {
            // Arrange
            var lastName = new CollectorLastName("Rodríguez");

            // Act
            var result = lastName.ToString();

            // Assert
            Assert.AreEqual("Rodríguez", result);
        }
    }
}