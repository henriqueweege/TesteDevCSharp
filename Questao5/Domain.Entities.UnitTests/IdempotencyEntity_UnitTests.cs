using Domain.Models;
using Newtonsoft.Json;

namespace Domain.Entities.UnitTests;

public class IdempotencyEntity_UnitTests
{
    [Fact]
    public void GivenFlagOfNewEntityAsTrueAndEntitiesAsParams_ShouldGenerateCorrectEntity()
    {
        //arrange
        var requisition = new IdempotencyModel();
        var result = new IdempotencyModel();
        var id = Guid.NewGuid();
        
        //act
        var idempotency = new IdempotencyEntity(true, id, requisition, result);
        
        //assert
        Assert.Equal(id, idempotency.Id);
        Assert.True(idempotency.Result is not null);
        Assert.True(idempotency.Requisition is not null);
        Assert.True(JsonConvert.DeserializeObject<IdempotencyModel>(idempotency.Requisition) is not null);
        Assert.True(JsonConvert.DeserializeObject<IdempotencyModel>(idempotency.Result) is not null);
    }

    [Fact]
    public void GivenFlagOfNewEntityAsFalseAndCorrectParams_ShouldGenerateCorrectEntity()
    {
        //arrange
        var requisition = "requisition";
        var result = "result";
        var id = Guid.NewGuid();

        //act
        var idempotency = new IdempotencyEntity(true, id, requisition, result);

        //assert
        Assert.Equal(id, idempotency.Id);
        Assert.Equal(requisition, idempotency.Requisition.Replace('"', ' ').Split(' ')[1]);
        Assert.Equal(result, idempotency.Result.Replace('"', ' ').Split(' ')[1]);
    }
}
