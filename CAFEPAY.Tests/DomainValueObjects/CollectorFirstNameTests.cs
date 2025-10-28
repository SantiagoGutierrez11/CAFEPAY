using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAFEPAY.ArqHex.Collectors.domain;
using System;

namespace CAFEPAY.Tests.Domain.ValueObjects
{
    [TestClass]
    public class CollectorFirstNameTests
    {
        [TestMethod]
        public void CollectorFirstName_Create_WithValidName_ShouldBeCreated()
        {
            // Arrange & Act
            var firstName = new CollectorFirstName("Juan Carlos");

            // Assert
            Assert.AreEqual("Juan Carlos", firstName.GetValue());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorFirstName_Create_WithEmptyName_ShouldThrowException()
        {
            // Arrange & Act
            var firstName = new CollectorFirstName("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorFirstName_Create_WithShortName_ShouldThrowException()
        {
            // Arrange & Act
            var firstName = new CollectorFirstName("Jo");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorFirstName_Create_WithLongName_ShouldThrowException()
        {
            // Arrange & Act
            var firstName = new CollectorFirstName("Este es un nombre muy largo que excede los treinta caracteres");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorFirstName_Create_WithNumbers_ShouldThrowException()
        {
            // Arrange & Act
            var firstName = new CollectorFirstName("Juan123");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorFirstName_Create_WithLeadingSpace_ShouldThrowException()
        {
            // Arrange & Act
            var firstName = new CollectorFirstName(" Juan");
        }

        [TestMethod]
        public void CollectorFirstName_Equals_WithSameValue_ShouldReturnTrue()
        {
            // Arrange
            var firstName1 = new CollectorFirstName("Maria");
            var firstName2 = new CollectorFirstName("Maria");

            // Act & Assert
            Assert.AreEqual(firstName1, firstName2);
        }
    }
}