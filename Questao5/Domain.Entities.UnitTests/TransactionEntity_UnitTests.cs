using Domain.Enums;

namespace Domain.Entities.UnitTests;

public class TransactionEntity_UnitTests
{
    [Fact]
    public void GivenFlagOfNewEntityasFalseAndNullDate_ShouldThrowException()
    {
        //arrange
        //act
        //assert
        Assert.Throws<ArgumentException>(() =>
        {
            new TransactionEntity(Guid.NewGuid(), Guid.NewGuid(), "C", 0, null, false);
        });
    }

    [Fact]
    public void GivenFlagOfNewEntityasTrueAndNullDate_ShouldGenerateCorrectEntity()
    {
        //arrange
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();
        //act
        var transaction = new TransactionEntity(firstId, secondId, "C", 0, null, true);

        //assert
        Assert.Equal(DateTime.Now.Date, transaction.Date.Date);
        Assert.Equal(firstId, transaction.Id);
        Assert.Equal(secondId, transaction.IdCheckingAccount);
        Assert.Equal(ETransactionType.Credit, transaction.Type);
        Assert.Equal(0, transaction.Value);
    }

    [Fact]
    public void GivenFlagOfNewEntityasFalseAndDateNotNull_ShouldGenerateEntityWithCorrectDate()
    {
        //arrange
        var date = DateTime.Now.AddDays(-50);
        var firstId = Guid.NewGuid();
        var secondId = Guid.NewGuid();
        //act
        var transaction = new TransactionEntity(firstId, secondId, "C", 0, date, false);

        //assert
        Assert.Equal(date.Date, transaction.Date.Date);
        Assert.Equal(firstId, transaction.Id);
        Assert.Equal(secondId, transaction.IdCheckingAccount);
        Assert.Equal(ETransactionType.Credit, transaction.Type);
        Assert.Equal(0, transaction.Value);
    }

    [Theory]
    [InlineData("invalid")]
    [InlineData("z")]
    [InlineData("a")]
    [InlineData("A")]
    [InlineData("Z")]
    public void GivenInvalidType_ShouldThrowException(string type)
    {
        //arrange
        //act
        //assert
        Assert.Throws<ArgumentException>(() =>
        {
            new TransactionEntity(Guid.NewGuid(), Guid.NewGuid(), type, 0, null, false);
        });
    }

    [Theory]
    [InlineData("d")]
    [InlineData("D")]
    public void GivenInvalidTypeAsD_ShouldCreateEntityWithTypeDebit(string type)
    {
        //arrange
        //act
        var transaction =  new TransactionEntity(Guid.NewGuid(), Guid.NewGuid(), type, 0, DateTime.Now, false);
        //assert
        Assert.Equal(ETransactionType.Debit, transaction.Type);
    }

    [Theory]
    [InlineData("c")]
    [InlineData("C")]
    public void GivenInvalidTypeAsC_ShouldCreateEntityWithTypeCredit(string type)
    {
        //arrange
        //act
        var transaction = new TransactionEntity(Guid.NewGuid(), Guid.NewGuid(), type, 0, DateTime.Now, false);
        //assert
        Assert.Equal(ETransactionType.Credit, transaction.Type);
    }
}
