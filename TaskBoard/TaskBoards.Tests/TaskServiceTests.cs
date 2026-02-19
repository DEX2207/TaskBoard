using System.Linq.Expressions;
using AutoMapper;
using Moq;
using TaskBoard.Application.DTO;
using TaskBoard.Application.Interfaces;
using TaskBoard.Application.Services;
using TaskBoard.Domain.Entities;
using TaskBoard.Domain.Enum;
using Xunit;

namespace TaskBoards.Tests;

public class TaskServiceTests
{
    private readonly Mock<IUnitOfWork> _mockUow = new();
        private readonly Mock<IMapper> _mockMapper = new();
        private readonly TaskService _service;
    
        public TaskServiceTests()
        {
            _service = new TaskService(_mockUow.Object, _mockMapper.Object);
        }
    
        [Fact]
        public async System.Threading.Tasks.Task CreateTask_ShouldThrow_IfNotAdminOrManager()
        {
            var dto = new CreateTaskDto { SprintId = 1 };
            _mockUow.Setup(u => u.Sprints.FindAsync(It.IsAny<Expression<Func<Sprint, bool>>>()))
                .ReturnsAsync(new List<Sprint> { new Sprint { Id = 1, ProjectId = 1 } });

            _mockUow.Setup(u => u.Roles.FindAsync(It.IsAny<Expression<Func<Role, bool>>>()))
                .ReturnsAsync(new List<Role> { new Role { Roles = Roles.user } });
            
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _service.CreateTaskAsync(dto, 1));
        }
    
        [Fact]
        public async System.Threading.Tasks.Task DeleteTask_ShouldThrow_IfTaskNotFound()
        {
            _mockUow.Setup(u => u.Tasks.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Tasks?)null);
    
            await Assert.ThrowsAsync<Exception>(() => _service.DeleteTaskAsync(5, 1));
        }
}