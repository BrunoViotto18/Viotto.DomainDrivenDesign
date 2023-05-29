namespace Viotto.DomainDrivenDesign.Repository.UnitTests;

using FluentAssertions;
using Models;


public partial class BaseRepositoryTests
{
    //! UPDATE

    [Fact]
    public void Update_ShouldOverwriteTheDatabaseObjectWithTheNewOne_WhenTheObjectIdExistsInTheDatabase()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        _context.Add(obj);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        var updated = new Test()
        {
            Id = obj.Id,
            Name = "Teste",
            DateOfBirth = obj.DateOfBirth,
            LuckyNumber = (obj.LuckyNumber + 10) % 100,
        };

        // Act
        _sut.Update(updated);

        // Assert
        _context.Test.First().Should().Be(updated);
    }

    [Fact]
    public void Update_ShouldThrowInvalidOperationException_WhenTheObjectIdDoesntExistInTheDatabase()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        _context.Add(obj);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        var updated = new Test()
        {
            Id = 0,
            Name = "Teste",
            DateOfBirth = obj.DateOfBirth,
            LuckyNumber = (obj.LuckyNumber + 10) % 100,
        };

        // Act
        var update = () => _sut.Update(updated);

        // Assert
        update.Should().Throw<InvalidOperationException>();
    }

    //! UPDATE ASYNC

    [Fact]
    public async Task UpdateAsync_ShouldOverwriteTheDatabaseObjectWithTheNewOne_WhenTheObjectIdExistsInTheDatabase()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        await _context.AddAsync(obj);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var updated = new Test()
        {
            Id = obj.Id,
            Name = "Teste",
            DateOfBirth = obj.DateOfBirth,
            LuckyNumber = (obj.LuckyNumber + 10) % 100,
        };

        // Act
        await _sut.UpdateAsync(updated);

        // Assert
        _context.Test.First().Should().Be(updated);
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrowInvalidOperationException_WhenTheObjectIdDoesntExistInTheDatabase()
    {
        // Arrange
        var obj = _testGenerator.Generate(1).First();
        await _context.AddAsync(obj);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var updated = new Test()
        {
            Id = 0,
            Name = "Teste",
            DateOfBirth = obj.DateOfBirth,
            LuckyNumber = (obj.LuckyNumber + 10) % 100,
        };

        // Act
        var updateAsync = async () => await _sut.UpdateAsync(updated);

        // Assert
        await updateAsync.Should().ThrowAsync<InvalidOperationException>();
    }

    //! UPDATE RANGE

    [Fact]
    public void UpdateRange_ShouldOverwriteTheDatabaseObjectsWithTheNewOnes_WhenTheObjectsIdsExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        var updated = data.Select(x => new Test()
        {
            Id = x.Id,
            Name = $"Teste_{x.Name}",
            DateOfBirth = x.DateOfBirth,
            LuckyNumber = (x.LuckyNumber + 10) % 100,
        });

        // Act
        _sut.UpdateRange(updated);

        // Assert
        _context.Test.AsEnumerable().Should().BeEquivalentTo(updated);
    }

    [Fact]
    public void UpdateRange_ShouldThrowInvalidOperationException_WhenAnyOfTheObjectsIdsDoesntExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        _context.AddRange(data);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();
        var updated = data.Select(x => new Test()
        {
            Id = 0,
            Name = $"Teste_{x.Name}",
            DateOfBirth = x.DateOfBirth,
            LuckyNumber = (x.LuckyNumber + 10) % 100,
        });

        // Act
        var updateRange = () => _sut.UpdateRange(updated);

        // Assert
        updateRange.Should().Throw<InvalidOperationException>();
    }

    //! UPDATE RANGE ASYNC

    [Fact]
    public async Task UpdateRangeAsync_ShouldOverwriteTheDatabaseObjectsWithTheNewOnes_WhenTheIdsExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var updated = data.Select(x => new Test()
        {
            Id = x.Id,
            Name = $"Teste_{x.Name}",
            DateOfBirth = x.DateOfBirth,
            LuckyNumber = (x.LuckyNumber + 10) % 100,
        });

        // Act
        await _sut.UpdateRangeAsync(updated);

        // Assert
        _context.Test.Should().BeEquivalentTo(updated);
    }

    [Fact]
    public async Task UpdateRangeAsync_ShouldThrowInvalidOperationException_WhenAnyOfTheObjectsIdsDoesntExistInTheDatabase()
    {
        // Arrange
        var data = _testGenerator.Generate(10);
        await _context.AddRangeAsync(data);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        var updated = data.Select(x => new Test()
        {
            Id = 0,
            Name = $"Teste_{x.Name}",
            DateOfBirth = x.DateOfBirth,
            LuckyNumber = (x.LuckyNumber + 10) % 100,
        });

        // Act
        var updateRangeAsync = async () => await _sut.UpdateRangeAsync(updated);

        // Assert
        await updateRangeAsync.Should().ThrowAsync<InvalidOperationException>();
    }
}
