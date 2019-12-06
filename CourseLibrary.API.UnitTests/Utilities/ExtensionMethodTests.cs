using CourseLibrary.API.Entities;
using CourseLibrary.API.Helpers;
using NUnit.Framework;
using System;

namespace CourseLibrary.API.UnitTests.Utilities
{
    public class ExtensionMethodTests
    {
        [Test]
        public void ExtensionMethods_GetCurrentAge_BirthdayPassed ()
        {
            //arrange
            var author = new Author()
            {
                FirstName = "FName",
                LastName = "LName",
                Id = Guid.NewGuid(),
                DateOfBirth = new DateTime(2000, 2, 1)
            };
            var today = new DateTime(2011, 2, 2);

            //act
            int age = author.DateOfBirth.GetCurrentAge(today);

            //assert
            Assert.AreEqual(11, age);
        }

        [Test]
        public void ExtensionMethods_GetCurrentAge_BirthdayToCome()
        {
            //arrange
            var author = new Author()
            {
                FirstName = "FName",
                LastName = "LName",
                Id = Guid.NewGuid(),
                DateOfBirth = new DateTime(2000, 2, 1)
            };
            var today = new DateTime(2011, 1, 31);

            //act
            int age = author.DateOfBirth.GetCurrentAge(today);

            //assert
            Assert.AreEqual(10, age);
        }
    }
}
