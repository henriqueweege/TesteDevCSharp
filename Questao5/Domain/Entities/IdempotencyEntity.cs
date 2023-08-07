using Domain.Entities.Contracts;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("[idempotencia]")]
public class IdempotencyEntity : IEntity
{
    public IdempotencyEntity(bool newEntity, Guid id, dynamic requisition, dynamic result)
    {
        if(newEntity)
        {
            Id = id;
            Requisition = JsonConvert.SerializeObject(requisition);
            Result = JsonConvert.SerializeObject(result);
        }
        else
        {
            Id = id;
            Requisition = requisition;
            Result = result;
        }
    }

    public Guid Id { get; set; }
    public string Requisition { get; set; }
    public string Result { get; set; }
}
