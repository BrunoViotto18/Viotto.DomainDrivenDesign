using FluentAssertions;

namespace Viotto.DomainDrivenDesign.Repository.UnitTests;


public partial class BaseRepositoryTests
{
    //! DELETE

    [Fact]
    public void Delete_ShouldDeleteTheObjectFromTheDatabase_WhenTheIdIsValid()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        _context.Add(obj);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        // Act
        _sut.Delete(obj);

        // Assert
        _context.Test.Should().HaveCount(0);
    }

    [Fact]
    public void Delete_ShouldThrowInvalidOperationException_WhenTheIdDoesntExistInTheDatabase()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        _context.Add(obj);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        obj.Id = 0;

        // Act
        var delete = () => _sut.Delete(obj);

        // Assert
        delete.Should().Throw<InvalidOperationException>();
    }

    //! DELETE ASYNC

    [Fact]
    public async Task DeleteAsync_ShouldDeleteTheObjectFromTheDatabase_WhenTheIdIsValid()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        await _context.AddAsync(obj);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        await _sut.DeleteAsync(obj);

        // Assert
        _context.Test.Should().HaveCount(0);
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrowInvalidOperationException_WhenTheIdDoesntExistInTheDatabase()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        await _context.AddAsync(obj);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        obj.Id = 0;

        // Act
        var delete = async () => await _sut.DeleteAsync(obj);

        // Assert
        await delete.Should().ThrowAsync<InvalidOperationException>();
    }

    //! DELETE BY ID

    [Fact]
    public void DeleteById_ShouldDeleteTheObjectFromTheDatabase_WhenTheIdIsValid()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        _context.Add(obj);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        // Act
        _sut.DeleteById(obj.Id);

        // Assert
        _context.Test.Should().HaveCount(0);
    }

    [Fact]
    public void DeleteById_ShouldThrowInvalidOperationException_WhenTheIdDoesntExistInTheDatabase()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        _context.Add(obj);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        // Act
        var delete = () => _sut.DeleteById(0);

        // Assert
        delete.Should().Throw<InvalidOperationException>();
    }

    //! DELETE BY ID ASYNC

    [Fact]
    public async Task DeleteByIdAsync_ShouldDeleteTheObjectFromTheDatabase_WhenTheIdIsValid()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        await _context.AddAsync(obj);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        await _sut.DeleteByIdAsync(obj.Id);

        // Assert
        _context.Test.Should().HaveCount(0);
    }

    [Fact]
    public async Task DeleteByIdAsync_ShouldThrowInvalidOperationException_WhenTheIdDoesntExistInTheDatabase()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        await _context.AddAsync(obj);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        var delete = async () => await _sut.DeleteByIdAsync(0);

        // Assert
        await delete.Should().ThrowAsync<InvalidOperationException>();
    }

    //! DELETE RANGE

    [Fact]
    public void DeleteRange_ShouldDeleteTheObjectFromTheDatabase_WhenTheIdsAreValid()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        // Act
        _sut.DeleteRange(data);

        // Assert
        _context.Test.Should().HaveCount(0);
    }

    [Fact]
    public void DeleteRange_ShouldThrowInvalidOperationException_WhenTheIdsDontExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        data = data.Select(x =>
        {
            x.Id += 5;
            return x;
        }).ToList();

        // Act
        var deleteRange = () => _sut.DeleteRange(data);

        // Assert
        deleteRange.Should().Throw<InvalidOperationException>();
    }

    //! DELETE RANGE ASYNC

    [Fact]
    public async Task DeleteRangeAsync_ShouldDeleteTheObjectFromTheDatabase_WhenTheIdIsValid()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();

        // Act
        await _sut.DeleteRangeAsync(data);

        // Assert
        _context.Test.Should().HaveCount(0);
    }

    [Fact]
    public async Task DeleteRangeAsync_ShouldThrowInvalidOperationException_WhenTheIdDoesntExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        data = data.Select(x =>
        {
            x.Id += 5;
            return x;
        }).ToList();

        // Act
        var deleteRangeAsync = async () => await _sut.DeleteRangeAsync(data);

        // Assert
        await deleteRangeAsync.Should().ThrowAsync<InvalidOperationException>();
    }

    //! DELETE RANGE BY ID

    [Fact]
    public void DeleteRangeById_ShouldDeleteTheObjectFromTheDatabase_WhenTheIdIsValid()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        var ids = data.Select(x => x.Id);

        // Act
        _sut.DeleteRangeById(ids);

        // Assert
        _context.Test.Should().HaveCount(0);
    }

    [Fact]
    public void DeleteRangeById_ShouldThrowInvalidOperationException_WhenTheIdDoesntExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        var ids = data.Select(x => x.Id + 5);

        // Act
        var deleteRangeById = () => _sut.DeleteRangeById(ids);

        // Assert
        deleteRangeById.Should().Throw<InvalidOperationException>();
    }

    //! DELETE RANGE BY ID ASYNC

    [Fact]
    public async Task DeleteRangeByIdAsync_ShouldDeleteTheObjectFromTheDatabase_WhenTheIdIsValid()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var ids = data.Select(x => x.Id);

        // Act
        await _sut.DeleteRangeByIdAsync(ids);

        // Assert
        _context.Test.Should().HaveCount(0);
    }

    [Fact]
    public async Task DeleteRangeByIdAsync_ShouldThrowInvalidOperationException_WhenTheIdDoesntExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var ids = data.Select(x => x.Id + 5);

        // Act
        var deleteRangeByIdAsync = async () => await _sut.DeleteRangeByIdAsync(ids);

        // Assert
        await deleteRangeByIdAsync.Should().ThrowAsync<InvalidOperationException>();
    }
}
