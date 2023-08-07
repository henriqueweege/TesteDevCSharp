using Domain.Entities;
using Domain.Models;
using Newtonsoft.Json;

namespace Domain.Converters.UnitTests;

public class IdempotencyConverter_UnitTests
{
    private IdempotencyConverter Converter { get; set; }
    private Guid Id { get; set; }
    private IdempotencyModel Requisition { get; set; }
    private IdempotencyModel Result { get; set; }
    public IdempotencyConverter_UnitTests()
    {
        Converter = new IdempotencyConverter();
        Id = Guid.NewGuid();
        Requisition = new IdempotencyModel();
        Result = new IdempotencyModel();
    }

    [Fact]
    public void GivenEntity_ConvertFromEntityToModel_SouldReturnCorrectModel()
    {

        //arrange
        var entity = new IdempotencyEntity(true, Id, Requisition, Result);


        //act
        var model = Converter.ConvertFromEntityToModel(entity);

        //assert
        Assert.Equal(Id.ToString(), model.chave_idempotencia);
        Assert.Equal(JsonConvert.SerializeObject(Requisition), model.requisicao);
        Assert.Equal(JsonConvert.SerializeObject(Result), model.resultado);

    }

    [Fact]
    public void GivenModel_ConvertFromModelToEntity_SouldReturnCorrectModel()
    {

        //arrange
        var model = new IdempotencyModel()
        {
            chave_idempotencia = Id.ToString(),
            requisicao = JsonConvert.SerializeObject(Requisition),
            resultado = JsonConvert.SerializeObject(Result)

        };

        //act
        var entity = Converter.ConvertFromModelToEntity(model);

        //assert
        Assert.Equal(Id.ToString(), entity.Id.ToString());
        Assert.Equal(JsonConvert.SerializeObject(Requisition), entity.Requisition);
        Assert.Equal(JsonConvert.SerializeObject(Result), entity.Result);

    }
}
