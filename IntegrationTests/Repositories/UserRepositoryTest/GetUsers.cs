using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UsersTBC.Domain.Entities;
using UsersTBC.Domain.Interfaces;
using UsersTBC.Insfrastructure.Helpers;
using UsersTBC.Insfrastructure.Repository;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTests.Repositories.UserRepositoryTest
{
    public class GetUsers
    {
        private readonly DataContext _context;
        private readonly UserRepository _userRepository;
        private readonly ITestOutputHelper _output;
        
       public GetUsers(ITestOutputHelper output)
        {
            _output = output;
            var dbOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlServer("Data Source=localhost; Initial Catalog=UsersTBC;Integrated Security=True; MultipleActiveResultSets=true;")
                .Options;
            
            
            _userRepository = new UserRepository(_context);
        }

        [Fact]
        public async Task GetUser()
        {
            /*//Setup DbContext and DbSet mock  
            var dbContextMock = new Mock<DataContext>();
            var dbSetMock = new Mock<DbSet<User>>();
            //dbSetMock.Setup(s => s.FindAsync(It.IsAny<Guid>())).Returns(Task.FromResult(new User()));
            Mock<IUserRepository> mockRepo = new Mock<IUserRepository>();
            //mockRepo.Setup(m => m.GetUser(8)).Returns(course);
            //var product = productRepository.GetUser(8).Result;
            var mockLeagueRepo = new UserRepository().MockIsValid(false);
            var user =await mockRepo.ge(8);
            
            _output.WriteLine($"OrderId: {user}");

            
            Assert.Equal(8, user.Id);*/
        }
    }
}
