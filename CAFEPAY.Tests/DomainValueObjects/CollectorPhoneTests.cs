using Microsoft.VisualStudio.TestTools.UnitTesting;
using CAFEPAY.ArqHex.Collectors.domain;
using System;

namespace CAFEPAY.Tests.Domain.ValueObjects
{
    [TestClass]
    public class CollectorPhoneTests
    {
        [TestMethod]
        public void CollectorPhone_Create_WithValidPhone_ShouldBeCreated()
        {
            // Arrange & Act
            var phone = new CollectorPhone("3001234567");

            // Assert
            Assert.AreEqual("3001234567", phone.GetValue());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorPhone_Create_WithInvalidLength_ShouldThrowException()
        {
            // Arrange & Act
            var phone = new CollectorPhone("300123456");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorPhone_Create_WithNonDigits_ShouldThrowException()
        {
            // Arrange & Act
            var phone = new CollectorPhone("300-123-456");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorPhone_Create_WithLeadingZero_ShouldThrowException()
        {
            // Arrange & Act
            var phone = new CollectorPhone("0123456789");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorPhone_Create_WithInvalidFirstDigit_ShouldThrowException()
        {
            // Arrange & Act
            var phone = new CollectorPhone("4001234567");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectorPhone_Create_WithInvalidSecondDigit_ShouldThrowException()
        {
            // Arrange & Act
            var phone = new CollectorPhone("3501234567");
        }

        [TestMethod]
        public void CollectorPhone_Create_WithLongConstructor_ShouldBeCreated()
        {
            // Arrange & Act
            var phone = new CollectorPhone(3001234567L);

            // Assert
            Assert.AreEqual("3001234567", phone.GetValue());
        }
    }
}
