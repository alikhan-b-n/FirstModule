using System.Text;
using FirstModel.FirstTask.Abstract;
using FirstModule.Dtos;
using FirstModule.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using FluentAssertions;

namespace FirstModule.FirstTask.Tests;

public class EmployeeServiceTests
{
    private readonly ApplicationContext _applicationContext;
    private Mock<IApiService> _apiServiceMock;

    public EmployeeServiceTests()
    {
        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        builder.UseInMemoryDatabase(GetType().Name);

        _applicationContext = new ApplicationContext(builder.Options);
        _applicationContext.Database.EnsureDeleted();
        _applicationContext.Database.EnsureCreated();

        _apiServiceMock = new Mock<IApiService>();
    }

    [Fact]
    public async Task SaveInfo_ShouldSaveEmployeeAndSkillsEntities_PositiveTest()
    {
        //Arrange
        var name = "Arya";
        var employeeViewModel = new EmployeeDto
        {
            FullName = name,
            Skills = "Killing, shapeshifting"
        };
        _apiServiceMock
            .Setup(x => x.GetInfo(It.IsAny<string>()))
            .ReturnsAsync(employeeViewModel);
        var employeeService = new EmployeeService(_applicationContext, _apiServiceMock.Object);

        //Act
        await employeeService.SaveInfo("url");
        var employee = await _applicationContext.Employies.Where(x => x.FullName == name).FirstAsync();
        var skills = await _applicationContext.Skills.Where(x => x.EmployeeId == employee.Id).FirstAsync();

        //Assert
        Assert.Equal(employeeViewModel.FullName, employee.FullName);
        employeeViewModel.Skills.Should().BeEquivalentTo(skills.Skills);
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllSavedEmployeeAndTheirSkills_PositiveTest()
    {
        //Arrange
        var employee1 = new EmployeeEntity
        {
            FullName = "Arya"
        };
        var skills1 = new SkillEntity
        {
            EmployeeId = employee1.Id,
            Skills = "Killing, Shapeshifting"
        };
        var employee2 = new EmployeeEntity
        {
            FullName = "John"
        };
        var skills2 = new SkillEntity
        {
            EmployeeId = employee2.Id,
            Skills = "Not knowing anything, dying"
        };

        var employeeService = new EmployeeService(_applicationContext, _apiServiceMock.Object);

        //Act
        _applicationContext.Employies.Add(employee1);
        _applicationContext.Employies.Add(employee2);
        _applicationContext.Skills.Add(skills1);
        _applicationContext.Skills.Add(skills2);
        await _applicationContext.SaveChangesAsync();

        var actual = await employeeService.GetAll();

        //Assert
        List<EmployeeEntity> employeeList = new List<EmployeeEntity>() { employee1, employee2 };
        List<SkillEntity> skillList = new List<SkillEntity>() { skills1, skills2 };

        actual.Select(x => x.Skills)
            .Should()
            .BeEquivalentTo(skillList
                .Select(x => x.Skills));

        actual.Select(x => x.Id)
            .Should()
            .BeEquivalentTo(employeeList
                .Select(x => x.Id));

        actual.Select(x => x.FullName)
            .Should()
            .BeEquivalentTo(employeeList
                .Select(x => x.FullName));
    }

    [Theory]
    [InlineData("j", new[] { "John", "Jarya" })]
    [InlineData("jo", new[] { "John" })]
    public async Task GetByName_ShouldReturnListOfPeopleContainingWrittenLetters_PositiveTest(
        string input, string[] expected)
    {
        //Arrange
        var employee1 = new EmployeeEntity
        {
            FullName = "Jarya"
        };
        var skills1 = new SkillEntity
        {
            EmployeeId = employee1.Id,
            Skills = "Killing, Shapeshifting"
        };
        var employee2 = new EmployeeEntity
        {
            FullName = "John"
        };
        var skills2 = new SkillEntity
        {
            EmployeeId = employee2.Id,
            Skills = "Not knowing anything, dying"
        };

        var employeeService = new EmployeeService(_applicationContext, _apiServiceMock.Object);

        //Act
        _applicationContext.Employies.Add(employee1);
        _applicationContext.Employies.Add(employee2);
        _applicationContext.Skills.Add(skills1);
        _applicationContext.Skills.Add(skills2);
        await _applicationContext.SaveChangesAsync();

        var info = await employeeService.GetByName(input);


        //Assert
        info.Select(x => x.FullName)
            .Should()
            .BeEquivalentTo(expected);
    }

    [Theory]
    [InlineData("knowing", new[]{"John"})]
    [InlineData("killing", new[]{"John","Jarya"})]
    public async Task GetBySkills_ShouldReturnListOfPeopleContainingRequiredSkills(string input, string[] expected)
    {
        //Arrange
        var employee1 = new EmployeeEntity
        {
            FullName = "Jarya"
        };
        var skills1 = new SkillEntity
        {
            EmployeeId = employee1.Id,
            Skills = "Killing, Shapeshifting"
        };
        var employee2 = new EmployeeEntity
        {
            FullName = "John"
        };
        var skills2 = new SkillEntity
        {
            EmployeeId = employee2.Id,
            Skills = "Not knowing anything, dying, Killing"
        };

        var employeeService = new EmployeeService(_applicationContext, _apiServiceMock.Object);

        //Act
        _applicationContext.Employies.Add(employee1);
        _applicationContext.Employies.Add(employee2);
        _applicationContext.Skills.Add(skills1);
        _applicationContext.Skills.Add(skills2);
        await _applicationContext.SaveChangesAsync();

        var info = await employeeService.GetBySkills(input);


        //Assert
        info.Select(x => x.FullName)
            .Should()
            .BeEquivalentTo(expected);
    }
}