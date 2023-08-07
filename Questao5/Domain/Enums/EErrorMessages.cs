using System.ComponentModel;

namespace Domain.Enums;

public enum EErrorMessages
{
    [Description("BadRequest: apenas contas correntes cadastradas podem receber movimentação.")]
    INVALID_ACCOUNT = 1,

    [Description("BadRequest: apenas contas correntes ativas podem receber movimentação.")]
    INACTIVE_ACCOUNT = 2,

    [Description("BadRequest: apenas valores positivos podem ser recebidos.")]
    INVALID_VALUE = 3,

    [Description("BadRequest: apenas os tipos “débito” ou “crédito” podem ser aceitos.")]
    INVALID_TYPE = 4,

    [Description("BadRequest: parametros obrigatório passado como nulo.")]
    INVALID_PARAMETER = 5,

    [Description("InternalServerError: algum erro ocorreu.")]
    INTERNAL_SERVER_ERROR = 6,

    [Description("BadRequest: algum erro ocorreu.")]
    BAD_REQUEST = 7
}
