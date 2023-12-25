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
            result.Should().BeAssignableTo<ActionResult>();


            result.Should().BeAssignableTo<OkObjectResult>();

            result.As<ObjectResult>().Value.Should().NotBeNull().And.BeOfType(result.GetType());
            

            _serviceMock.Verify(x => x.LoginService(request), Times.Never());



        }

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


        public async Task GetAllUser_ShouldReturnStatus200OK_WhenDataFound()
        {

        }
    }
}
