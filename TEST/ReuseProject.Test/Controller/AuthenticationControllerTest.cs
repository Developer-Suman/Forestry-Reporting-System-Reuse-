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
        public async Task GetLoggedinMessage_ShouldReturnStatusOk200_WhenLoginSuccessful()
        {
            //Arrange
            var request = _fixture.Create<LogInDTO>();
            var expectedResult = _fixture.Create<TokenDTO>();
            _serviceMock.Setup(x => x.LoginService(request))
                .ReturnsAsync(new ServiceResult<TokenDTO>(true, expectedResult));

            //Act
            var result= await _sut.Login(request);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<ActionResult>();
            var okResult = result.As<ObjectResult>();

            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().Be(expectedResult);

            _serviceMock.Verify(x => x.LoginService(request), Times.Never());



        }

        public void GetErrorMessageForLogin_ShouldReturnStatus500InternalServerError_WhenLoginFailes()
        {

        }
    }
}
