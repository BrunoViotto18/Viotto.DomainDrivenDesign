using FluentAssertions;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests;


public partial class BaseRepositoryTests
{
    //! CREATE

    [Fact]
    public void Create_ShouldCreateAndSaveTheObjectInTheDatabase_WhenTheObjectPrimaryKeyIsNotSet()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();

        // Act
        _sut.Create(obj);
        _context.ChangeTracker.Clear();

        // Assert
        _context.Test.Count().Should().Be(1);
        _context.Test.First().Should().Be(obj);
    }

    [Fact]
    public void Create_ShouldUpdateTheOriginalObjectId_WhenTheObjectIsSuccesfullySavedToTheDatabase()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();

        // Act
        _sut.Create(obj);
        _context.ChangeTracker.Clear();

        // Assert
        _context.Test.First().Should().Be(obj);
    }

    //! CREATE ASYNC

    [Fact]
    public async Task CreateAsync_ShouldCreateAndSaveTheObjectInTheDatabase_WhenTheObjectPrimaryKeyIsNotSet()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();

        // Act
        await _sut.CreateAsync(obj);
        _context.ChangeTracker.Clear();

        // Assert
        _context.Test.Count().Should().Be(1);
        _context.Test.First().Should().Be(obj);
    }

    [Fact]
    public async Task CreateAsync_ShouldUpdateTheOriginalObjectId_WhenTheObjectIsSuccesfullySavedToTheDatabase()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();

        // Act
        await _sut.CreateAsync(obj);
        _context.ChangeTracker.Clear();

        // Assert
        _context.Test.First().Should().Be(obj);
    }

    //! CREATE RANGE

    [Fact]
    public void CreateRange_ShouldCreateAndSaveMultipleObjectsInTheDatabase_WhenEveryObjectPrimaryKeyIsNotSet()
    {
        // Arrange
        var data = _testGenerator.Generate(10);

        // Act
        _sut.CreateRange(data);
        _context.ChangeTracker.Clear();

        // Assert
        _context.Test.Count().Should().Be(10);
        _context.Test.Should().BeEquivalentTo(data);
    }

    [Fact]
    public void CreateRange_ShouldUpdateTheOriginalObjectsId_WhenTheObjectsAreSavedSuccessfully()
    {
        // Arrange
        var data = _testGenerator.Generate(10);

        // Act
        _sut.CreateRange(data);
        _context.ChangeTracker.Clear();

        // Assert
        _context.Test.Should().BeEquivalentTo(data);
    }

    //! CREATE RANGE ASYNC

    [Fact]
    public async Task CreateRangeAsync_ShouldCreateAndSaveMultipleObjectsInTheDatabase_WhenEveryObjectPrimaryKeyIsNotSet()
    {
        // Arrange
        var data = _testGenerator.Generate(10);

        // Act
        await _sut.CreateRangeAsync(data);
        _context.ChangeTracker.Clear();

        // Assert
        _context.Test.Count().Should().Be(10);
        _context.Test.Should().BeEquivalentTo(data);
    }

    [Fact]
    public async Task CreateRangeAsync_ShouldUpdateTheOriginalObjectsId_WhenTheObjectsAreSavedSuccessfully()
    {
        // Arrange
        var data = _testGenerator.Generate(10);

        // Act
        await _sut.CreateRangeAsync(data);
        _context.ChangeTracker.Clear();

        // Assert
        _context.Test.Should().BeEquivalentTo(data);
    }
}
