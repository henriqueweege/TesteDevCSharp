using System.ComponentModel;

namespace Domain.Enums;

public enum ESuccessMessages
{
    [Description("Ok: Requisição completada com sucesso.")]
    OK_REQUISITON_COMPLETED_SUCCESSFULLY = 1,

    [Description("NoContent: Requisição completada com sucesso.")]
    NO_CONTENT_REQUISITON_COMPLETED_SUCCESSFULLY = 2
}
