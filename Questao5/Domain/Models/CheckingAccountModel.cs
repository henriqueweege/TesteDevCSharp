using Dapper.Contrib.Extensions;
using Domain.Models.Contracts;
using System.Text.Json.Serialization;

namespace Domain.Models;

[Table("[contacorrente]")]
public class CheckingAccountModel : IModel
{
    [ExplicitKey]
    [JsonPropertyName("Id")]
    public string idcontacorrente { get; set; }

    [JsonPropertyName("Number")]
    public int numero { get; set; }

    [JsonPropertyName("HolderName")]
    public string nome { get; set; }

    [JsonPropertyName("Active")]
    public bool ativo { get; set; }
}