using FluentAssertions;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests;

using Models;


public partial class BaseRepositoryTests
{
    //! GET ALL

    [Fact]
    public void GetAll_ShouldReturnAllTableObjects_Always()
    {
        // Arrange
        var expectedOutput = _testGenerator.Generate(10);
        _context.AddRange(expectedOutput);
        _context.SaveChanges();

        // Act
        var result = _sut.GetAll();

        // Assert
        result.Should().BeEquivalentTo(expectedOutput);
    }

    [Fact]
    public void GetAll_ShouldTrackTheReturnedObjects_Always()
    {
        // Arrange
        var expectedOutput = _testGenerator.Generate(10);
        _context.AddRange(expectedOutput);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        // Act
        _sut.GetAll().ToArray();

        // Assert
        _context.Set<Test>().Local.Count().Should().Be(10);
    }

    //! GET ALL NO TRACKING

    [Fact]
    public void GetAllNoTracking_ShouldReturnAllTableObjects_Always()
    {
        // Arrange
        var expectedOutput = _testGenerator.Generate(10);
        _context.AddRange(expectedOutput);
        _context.SaveChanges();

        // Act
        var result = _sut.GetAllNoTracking();

        // Assert
        result.Should().BeEquivalentTo(expectedOutput);
    }

    [Fact]
    public void GetAllNoTracking_ShouldNotTrackTheReturnedObjects_Always()
    {
        // Arrange
        var expectedOutput = _testGenerator.Generate(10);
        _context.AddRange(expectedOutput);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        // Act
        _sut.GetAllNoTracking().ToArray();

        // Assert
        _context.Set<Test>().Local.Count().Should().Be(0); ;
    }

    //! GET BY ID

    [Fact]
    public void GetById_ShouldReturnTheObjectRelatedToTheGivenId_WhenTheObjectExistsInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();

        // Act
        var result = _sut.GetById(3);

        // Assert
        result.Should().BeEquivalentTo(data.First(x => x.Id == 3));
    }

    [Fact]
    public void GetById_ShouldThrowInvalidOperationException_WhenTheObjectDoesNotExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();

        // Act
        var function = () => _sut.GetById(11);

        // Assert
        function.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GetById_ShouldTrackTheReturnedObject_Always()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        // Act
        _sut.GetById(3);

        // Assert
        _context.Set<Test>().Local.Count().Should().Be(1);
    }

    //! GET BY ID ASYNC

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTheObjectRelatedToTheGivenId_WhenTheObjectExistsInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();

        // Act
        var result = await _sut.GetByIdAsync(3);

        // Assert
        result.Should().BeEquivalentTo(data.First(x => x.Id == 3));
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowInvalidOperationException_WhenTheObjectDoesNotExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();

        // Act
        var function = async () => await _sut.GetByIdAsync(11);

        // Assert
        await function.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task GetByIdAsync_ShouldTrackTheReturnedObject_Always()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        // Act
        await _sut.GetByIdAsync(3);

        // Assert
        _context.Set<Test>().Local.Count().Should().Be(1);
    }

    //! GET BY ID NO TRACKING

    [Fact]
    public void GetByIdNoTracking_ShouldReturnTheObjectRelatedToTheGivenId_WhenTheObjectExistsInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();

        // Act
        var result = _sut.GetByIdNoTracking(3);

        // Assert
        result.Should().BeEquivalentTo(data.First(x => x.Id == 3));
    }

    [Fact]
    public void GetByIdNoTracking_ShouldThrowInvalidOperationException_WhenTheObjectDoesNotExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();

        // Act
        var function = () => _sut.GetByIdNoTracking(11);

        // Assert
        function.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void GetById_ShouldNotTrackTheReturnedObject_Always()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        // Act
        _sut.GetByIdNoTracking(3);

        // Assert
        _context.Set<Test>().Local.Count().Should().Be(0);
    }

    //! GET BY ID NO TRACKING ASYNC

    [Fact]
    public async Task GetByIdNoTrackingAsync_ShouldReturnTheObjectRelatedToTheGivenId_WhenTheObjectExistsInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();

        // Act
        var result = await _sut.GetByIdNoTrackingAsync(3);

        // Assert
        result.Should().BeEquivalentTo(data.First(x => x.Id == 3));
    }

    [Fact]
    public async Task GetByIdNoTrackingAsync_ShouldThrowInvalidOperationException_WhenTheObjectDoesNotExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();

        // Act
        var function = async () => await _sut.GetByIdNoTrackingAsync(11);

        // Assert
        await function.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task GetByIdNoTrackingAsync_ShouldNotTrackTheReturnedObject_Always()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        // Act
        await _sut.GetByIdNoTrackingAsync(3);

        // Assert
        _context.Set<Test>().Local.Count().Should().Be(0);
    }

    //! GET RANGE BY ID

    [Theory]
    [InlineData(-5, -1, 1)]
    [InlineData(1, 10, 2)]
    [InlineData(1, 10, 1)]
    [InlineData(-5, 5, 1)]
    [InlineData(5, 15, 1)]
    [InlineData(11, 15, 1)]
    public void GetRangeById_ShouldReturnAllObjectsByTheirIds_WhenTheIds(int startId, int endId, int step)
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        var ids = Enumerable.Range(startId, endId - startId).Select(x => (long)x).Where(x => x % step == startId % step);
        var expectedOutput = data.Where(x => ids.Contains(x.Id));

        // Act
        var result = _sut.GetRangeById(ids);

        // Assert
        result.Should().BeEquivalentTo(expectedOutput);
    }

    [Fact]
    public void GetRangeById_ShouldTrackTheReturnedObjects_Always()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        var ids = data.Select(x => x.Id);

        // Act
        var result = _sut.GetRangeById(ids).ToArray();

        // Assert
        _context.Set<Test>().Local.Count().Should().Be(ids.Count());
    }

    //! GET RANGE BY ID NO TRACKING

    [Theory]
    [InlineData(-5, -1, 1)]
    [InlineData(1, 10, 2)]
    [InlineData(1, 10, 1)]
    [InlineData(-5, 5, 1)]
    [InlineData(5, 15, 1)]
    [InlineData(11, 15, 1)]
    public void GetRangeByIdNoTracking_ShouldReturnAllObjectsByTheirIds_WhenTheIds(int startId, int endId, int step)
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        var ids = Enumerable.Range(startId, endId - startId).Select(x => (long)x).Where(x => x % step == startId % step);
        var expectedOutput = data.Where(x => ids.Contains(x.Id));

        // Act
        var result = _sut.GetRangeByIdNoTracking(ids);

        // Assert
        result.Should().BeEquivalentTo(expectedOutput);
    }

    [Fact]
    public void GetRangeByIdNoTracking_ShouldNotTrackTheReturnedObjects_Always()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        var ids = data.Select(x => x.Id);

        // Act
        var result = _sut.GetRangeByIdNoTracking(ids).ToArray();

        // Assert
        _context.Set<Test>().Local.Count().Should().Be(0);
    }
}
