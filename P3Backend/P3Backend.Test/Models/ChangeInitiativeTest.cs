using NUnit.Framework;
using P3Backend.Model;
using P3Backend.Model.ChangeTypes;
using P3Backend.Model.TussenTabellen;
using P3Backend.Model.Users;
using System;
using System.Diagnostics;
using Assert = Xunit.Assert;

namespace P3Backend.Test.Models {
	public class ChangeInitiativeTest {
		static DateTime validStart = DateTime.Now.AddDays(2);
		static DateTime invalidStart = DateTime.Now.AddDays(-1);
		static DateTime validEnd = DateTime.Now.AddDays(5);
		static DateTime invalidEnd = DateTime.Now.AddDays(-1);
		static Employee validEmployee = new Employee("firstNameTest",
			"lastNameTest", "email@test.com");
		static IChangeType validChangeType = new PersonalChangeType();
		static ChangeGroup validCg = new ChangeGroup("testcg");

		public ChangeInitiativeTest() {
			EmployeeChangeGroup ecg = new EmployeeChangeGroup(validEmployee, validCg);
			validEmployee.EmployeeChangeGroups.Add(ecg);
			validCg.EmployeeChangeGroups.Add(ecg);
		}

		[Test]
		public void InitializeConstructorWithValidParamsSucceeds() {
			//Arrange
			DateTime start = DateTime.Now.AddDays(1);
			Trace.WriteLine(start);
			DateTime end = DateTime.Now.AddDays(5);
			string name = "Test testing";
			string desc = "This is a test description for a test that should not fail.";
			string firstName = "Test";
			string lastName = "LastTestName";
			string email = "test@testing.com";
			Employee sponsor = new Employee(firstName, lastName, email);
			Employee e = new Employee("testEmployee", "voor Test", "testEmployee@test.com");
			IChangeType changeType = new PersonalChangeType();


			//Act
			ChangeInitiative changeInitiative = new ChangeInitiative(name, desc, start, end, sponsor, changeType, validCg);
		}

		[Test]
		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void InitializeConstructorWithInvalidNameThrowsException(string name) {
			Assert.Throws<ArgumentException>(() => new ChangeInitiative(name,
				"desc",
				validStart,
				validEnd,
				validEmployee,
				validChangeType,
				validCg));
		}

		[Test]
		[TestCase("")]
		[TestCase(" ")]
		[TestCase("1234")]
		[TestCase(null)]
		public void InitializeConstructorWithInvalidDescriptionThrowsException(string desc) {
			Assert.Throws<ArgumentException>(() => new ChangeInitiative("validName",
				desc,
				validStart,
				validEnd,
				validEmployee,
				validChangeType,
				validCg));
		}

		[Test]
		public void InitializeConstructorWithInvalidStartThrowsException() {
			Assert.Throws<ArgumentException>(() => new ChangeInitiative("test",
				"desc",
				invalidStart,
				validEnd,
				validEmployee,
				validChangeType,
				validCg));
		}

		[Test]
		public void InitializeConstructorWithInvalidEndThrowsException() {
			Assert.Throws<ArgumentException>(() => new ChangeInitiative("test",
				"desc",
				validStart,
				invalidEnd,
				validEmployee,
				validChangeType,
				validCg));
		}

		[Test]
		public void InitializeConstructorWithInvalidEmployeeThrowsException() {
			Assert.Throws<ArgumentException>(() => new ChangeInitiative("test",
				"desc",
				validStart,
				invalidEnd,
				null,
				validChangeType,
				validCg));
		}

		[Test]
		public void InitializeConstructorWithInvalidChangeTypeThrowsException() {
			Assert.Throws<ArgumentException>(() => new ChangeInitiative("test",
				"desc",
				validStart,
				invalidEnd,
				validEmployee,
				null,
				validCg));
		}
	}
}