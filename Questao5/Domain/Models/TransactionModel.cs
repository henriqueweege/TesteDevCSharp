using Domain.Models.Contracts;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Domain.Models;

[Table("[movimento]")]
public class TransactionModel : IModel
{
    [JsonPropertyName("Id")]
    public string idmovimento { get; set; }

    [JsonPropertyName("IdCheckingAccount")]
    public string idcontacorrente { get; set; }

    [JsonPropertyName("Date")]
    public DateTime datamovimento { get; set; }

    [JsonPropertyName("Type")]
    public string tipomovimento { get; set; }

    [JsonPropertyName("Value")]
    public double valor { get; set; }
}