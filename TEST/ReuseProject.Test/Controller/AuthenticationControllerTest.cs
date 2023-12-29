using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using AutoFixture;
using Reuse.Bll.Service.Interface;
using ReuseProject.Controllers;
using Reuse.DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Reuse.Bll.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ReuseProject.Test.Controller
{
    public class AuthenticationControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IAuthServices> _serviceMock;
        private readonly AuthenticateController _sut;

        public AuthenticationControllerTest()
        {
            _fixture = new Fixture();
            _serviceMock = _fixture.Freeze<Mock<IAuthServices>>();
            _sut = new AuthenticateController(_serviceMock.Object);  //creates the implementation in the memory
            
        }
        [Fact]
        public async Task GetAccessTokenAndRefreshToken_ShouldReturnStatusOk200_WhenLoginSuccessful()
        {
            //Arrange
            var request = _fixture.Create<LogInDTO>();
            var expectedResult = _fixture.Create<ServiceResult<TokenDTO>>();
            _serviceMock.Setup(x => x.LoginService(request))
                .ReturnsAsync(expectedResult);

            //Act
            var result= await _sut.Login(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.As<ObjectResult>().Value.Should().BeEquivalentTo(request);
      
            

            _serviceMock.Verify(x => x.LoginService(request), Times.Never());



        }

        [Fact]
        public async Task GetErrorMessageForLogin_ShouldReturnStatus500InternalServerError_WhenLoginFailed()
        {
            //Arrange
            var request = _fixture.Create<LogInDTO> ();
            _sut.ModelState.AddModelError("Input", "Input field is required");


            var expectedResult = _fixture.Create<ServiceResult<TokenDTO>> ();
            _serviceMock.Setup (x => x.LoginService(request)) .ReturnsAsync(expectedResult);

            //Act
            var result = await _sut.Login(request).ConfigureAwait(false); 

            //Assert
            result.Should().NotBeNull();
            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(x=>x.LoginService(request), Times.Never());

        }
        [Fact]

        public async Task GetRegisteredMessage_ShouldReturnStatus200OK_WhenRegisteredSuccessfully()
        {
            //Arrange
            var request= _fixture.Create<RegisterDTO> ();

            var expectedResult = _fixture.Create<ServiceResult<TokenDTO>> ();
            _serviceMock.Setup(x => x.RegisterServices(request)).ReturnsAsync(expectedResult);

            //Act
            var result = await _sut.Register(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<OkObjectResult>();

            result.As<ObjectResult>().Value.Should().NotBeNull().And.BeOfType(result.GetType());
            _serviceMock.Verify(x=>x.RegisterServices(request), Times.Never());

        }


        [Fact]
        public async Task GetErrorMessageForRegistered_ShouldReturnStatus500InternalServerError_WhenRegisteredFailes()
        {
            //Arrange
            var request = _fixture.Create<RegisterDTO>();
            _sut.ModelState.AddModelError("Register", "Username and password field is required");

            var expectedResult = _fixture.Create<ServiceResult<TokenDTO>>();

            _serviceMock.Setup(x => x.RegisterServices(request)).ReturnsAsync(expectedResult);

            //Act
            var result= await _sut.Register(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(x => x.RegisterServices(request), Times.Never());

        }

        [Fact]
        public async Task GetAllUser_ShouldReturnStatus200OK_WhenResultSuccess()
        {
            //Arrange
            var response = _fixture.Create<ServiceResult<List<UserDTO>>>();
            _serviceMock.Setup(x => x.GetAllUsers()).ReturnsAsync(response);

            //Act
            var result = _sut.GetAllUsers().ConfigureAwait(false);

            //Assert
            Assert.NotNull(result);
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            //result.Should().BeAssignableTo<OkObjectResult>();
            //result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(response.GetType());
            _serviceMock.Verify(x=>x.GetAllUsers(), Times.Never());


        }

        [Fact]
        public async Task GetAllUser_ShouldReturnStatus500InternalServerError_WhenResultNotSuccess()
        {
            //Arrange
            var errorResponse = _fixture.Create<string[]>();
            var usSuccessfulResponse = new ServiceResult<List<UserDTO>>(false, default, errorResponse);

            _serviceMock.Setup(x => x.GetAllUsers()).ReturnsAsync(usSuccessfulResponse);

            //Act
            var result = await _sut.GetAllUsers();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<StatusCodeResult>().Which.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            result.As<ObjectResult>().Value.Should().BeEquivalentTo(errorResponse);
            _serviceMock.Verify(x => x.GetAllUsers(), Times.Once());

        }




        [Fact]
        public async Task GetUserByBranchId_ShouldReturnStatus200OK_WhenValidinput()
        {
            //Arrange
            var response = _fixture.Create<ServiceResult<List<UserDTO>>>();
            var id = _fixture.Create<int>();

            _serviceMock.Setup(x => x.GetUserByBranchId(id)).ReturnsAsync(response);

            //Act
            var result = _sut.GetUserByBranchId(id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().NotBeNull().And.BeOfType(response.GetType());

            _serviceMock.Verify(x=>x.GetUserByBranchId(id), Times.Once());
        }

        [Fact]
        public async Task GetUserByBranchId_ShouldReturnStatus500InternalServerError_WhenNoDataFound()
        {
            //Arrange
            ServiceResult<List<UserDTO>> response = null;
            var id = _fixture.Create<int>();
            _serviceMock.Setup(x => x.GetUserByBranchId(id)).ReturnsAsync(response);

            //Act

            var result = await _sut.GetUserByBranchId(id).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<NotFoundResult>();
            _serviceMock.Verify(x => x.GetUserByBranchId(id), Times.Once());
        }

        [Fact]
        public async Task GetUserByBranchId_ShouldReturnStatus500InternalServerError_WhenResultSuccessIsFalse()
        {
            //Arrange 
            var response = _fixture.Create<ServiceResult<List<UserDTO>>>();
            var id = _fixture.Create<int>();

            _serviceMock.Setup(x => x.GetUserByBranchId(id)).ReturnsAsync(response);


            //Act
            var result = await _sut.GetUserByBranchId(id).ConfigureAwait(false);

            //Assert

            result.Should().NotBeNull();
            result.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(x => x.GetUserByBranchId(id), Times.Once());
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnBadResponse_WhenResultIsSuucess()
        {
            //Arrange
            int id = 0;
            var request = _fixture.Create<UserDTO>();
            var expectedResult = _fixture.Create<ServiceResult<UserDTO>>();
            _serviceMock.Setup(x => x.UpdateUser(request)).ReturnsAsync(expectedResult);
        

            //Act
            var result = await _sut.UpdateUser(request).ConfigureAwait(false);

            //Assert
            request.Should().NotBeNull();
            request.Should().BeAssignableTo<BadRequest>();
            request.As<ObjectResult>().StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            _serviceMock.Verify(x=>x.UpdateUser(request), Times.Never());

        }

        [Fact]
        public async Task UpdateUser_ShouldReturnStatus500InternalServerError_WhenUpdateResultFailed()
        {
            //Arrange
            var id = _fixture.Create<int>();
            var request = _fixture.Create<UserDTO>();
            var expectedResult = new ServiceResult<UserDTO>(false);
            _sut.ModelState.AddModelError("Subject", "The Subject field is required");
            _serviceMock.Setup(x=>x.UpdateUser(request)).ReturnsAsync(expectedResult);

            //Act
            var result = await _sut.UpdateUser(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<BadRequestResult>();
            _serviceMock.Verify(x => x.UpdateUser(request), Times.Never());
        }



    }
}
