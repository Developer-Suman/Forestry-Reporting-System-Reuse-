using AutoFixture;
using Reuse.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reuse.DAL.Test.Entity
{
    public class BranchTest
    {
        private readonly IFixture _fixture;
        public BranchTest(IFixture fixture)
        {
            _fixture = fixture;
            
        }

        [Fact]
        public void Id_ReturnAssignedValue()
        {
            //Arrange
            var id = _fixture.Create<int>();

            //Act
            var sut = new Branch
            {
                BranchId = id,
            };

            //Assert
            Assert.Equal(id, sut.BranchId);
        }
    }
}
