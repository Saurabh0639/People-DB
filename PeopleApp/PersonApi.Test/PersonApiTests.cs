using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;
using TryingBackend.Controllers;
using TryingBackend.Models;
using TryingBackend.Repository;
using Xunit;


namespace PeopleApi.Test
{
    public class PersonApiTests
    {
        private readonly Mock<IPersonRepository> _mockrepo;
        private readonly PeopleController _controller;

        public PersonApiTests()
        {
           _mockrepo= new Mock<IPersonRepository>();
           _controller = new PeopleController(_mockrepo.Object);
        }

        private void SimulateValidation(object model)
        {
            // mimic the behaviour of the model binder which is responsible for Validating the Model
            var validationContext = new ValidationContext(model, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
            {
                this._controller.ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
            }

        }

        List<Person> personlist = new List<Person>()
        {
            new Person
            {
                Id=Guid.NewGuid(),
                FirstName= "Saurabh",
                LastName="Somani",
                Age= 22,
            },
            new Person
            {
                Id=Guid.NewGuid(),
                FirstName= "Shivam",
                LastName="Singh",
                Age= 22,
            }

        };

        IEnumerable<Person> personlist1 = new List<Person>()
        {
            new Person
            {
                Id=Guid.NewGuid(),
                FirstName= "Saurabh",
                LastName="Somani",
                Age= 22,
            },
            new Person
            {
                Id=Guid.NewGuid(),
                FirstName= "Shivam",
                LastName="Singh",
                Age= 22,
            }

        };


        //getall

        [Fact]
        public async void getall_okresult_passtest()
        {
            //Arrange
            _mockrepo.Setup(repo => repo.Getallpersons()).Returns(Task.FromResult(personlist1));

            //Act
            var result = await _controller.GetAll();

            //Assert
          //  Assert.IsType<OkObjectResult>(result.Result);
            result.Result.GetType().Should().Be(typeof(OkObjectResult));
        }


        [Fact]
        public async void getall_notfound_noperson()
        {
            //Arrange
            personlist1 = null;
            _mockrepo.Setup(repo => repo.Getallpersons()).Returns(Task.FromResult(personlist1));

            //Act
            var response = await _controller.GetAll();

            //Assert
           // Assert.IsType<NotFoundResult>(response.Result);
            response.Result.GetType().Should().Be(typeof(NotFoundResult));

        }



        //getperson(by id)
        [Fact]
        public async void getperson_okresult_test()
        {
            //Arrange
            var id = Guid.NewGuid();
            Person person = new Person();
            _mockrepo.Setup(repo => repo.Get(id)).Returns(Task.FromResult(person));

            //Act
            var response =await _controller.GetPerson(id);

            //Assert
            //Assert.IsType<Person>(response.Value);
            response.Value.GetType().Should().Be(typeof(Person));
        }

        [Fact]
        public async void get_exception_nopersons()
        {
            //ARANGE
            personlist = null;
            var id = Guid.NewGuid();
            _mockrepo.Setup(repo => repo.Get(id)).Throws(new Exception());

            //ACT
            var response = await _controller.GetPerson(id);
            var persons = ((ObjectResult)response.Result).StatusCode;

            //ASSERT
            Assert.Equal(500, persons);

        }




        //delete unit cases
        [Fact]
        public void Deleteperson_okresult_passtest()
        {
            var id = Guid.NewGuid();
            Person person = new Person
            {
                Id = id,
                FirstName = "xyz",
                LastName = "abc",
                Age = 23
            };
            _mockrepo.Setup((repo => repo.DeletePerson(id)));

            var response = _controller.DeletePerson(id);

            Assert.IsType<NoContentResult>(response);
        }
        [Fact]
        public void Delete_exception_nopersons()
        {
            //ARANGE
            personlist = null;
            var id = Guid.NewGuid();
            _mockrepo.Setup(repo => repo.DeletePerson(id)).Throws(new Exception());

            //ACT
            var response = _controller.DeletePerson(id);
            var persons = ((ObjectResult)response).StatusCode;

            //ASSERT
            Assert.Equal(500, persons);

        }




        //createperson
        [Fact]
        public async void Createperson_okresult_success()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Person person = new Person
            {
                Id = id,
                FirstName = "xyz",
                LastName = "abc",
                Age = 23
            };
            _mockrepo.Setup(repo => repo.CreatePerson(person)).Returns(Task.FromResult(person));

            //act
            var result = await _controller.Addperson(person);

            //assert
            result.Result.GetType().Should().Be(typeof(CreatedAtActionResult));
        }

        [Fact]
        public async void TestCreateOperation_WhenFirstNameIsInvalid()
        {
            //Arrange
            Guid id = Guid.NewGuid();
            Person person = new Person
            {
                Id = id,
                FirstName = "x",
                LastName = "abc",
                Age = 23
            };
            _mockrepo.Setup(p => p.CreatePerson(person)).Returns(Task.FromResult(person));
            //Act
            SimulateValidation(person);
            var result = await _controller.Addperson(person);

            //Assert
            result.Result.GetType().Should().Be(typeof(BadRequestResult));
        }




        //updateperson
        [Fact]
        public async void updateperson_okresult_passtest()
        {
            Person person = new Person();
            var id = person.Id;
            _mockrepo.Setup(p => p.UpdatePerson(person)).Returns(Task.FromResult(person));

            var result = await _controller.UpdateOwner(id, person);

            result.GetType().Should().Be(typeof(NoContentResult));

        }

        [Fact]
        public async void TestUpdate_personisnull_passtest()
        {
            Person person = new Person();
            person = null;
            _mockrepo.Setup(p => p.UpdatePerson(person)).Returns(Task.FromResult(person));

            var result = await _controller.UpdateOwner(Guid.NewGuid(), person);

            result.GetType().Should().Be(typeof(NotFoundResult));
        }




    }
}