using Dapper.Contrib.Extensions;
using Domain.Models.Contracts;

namespace Domain.Models;

[Table("[idempotencia]")]
public class IdempotencyModel : IModel
{
    [ExplicitKey]
    public string chave_idempotencia { get; set; }
    public string requisicao { get; set; }
    public string resultado { get; set; }
}
