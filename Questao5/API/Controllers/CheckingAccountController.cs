using Domain.Commands;
using Domain.Commands.CheckingAccountCommands;
using Domain.Models;
using Domain.Queries;
using Domain.Queries.CheckingAccountQueries;
using Domain.Repositories;
using Domain.Repositories.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CheckingAccountController : ControllerBase
{

    /// <summary>
    /// Create a transaction.
    /// </summary>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status200OK)]
    public IActionResult ExecuteTransaction([FromBody] ExecuteTransactionCommand command, [FromServices] IMediator mediator)
        => Return(mediator.Send(command).Result);

    /// <summary>
    /// Get checking account balance.
    /// </summary>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status200OK)]
    public IActionResult GetBalance([FromQuery] Guid id, [FromServices] IMediator mediator)
        => Return(mediator.Send(new GetCheckingAccountBalanceQuery() { Id = id }).Result);

    #region CRUD

    /// <summary>
    /// Create a new Checking Account.
    /// </summary>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status200OK)]
    public IActionResult Save([FromBody] CreateCheckingAccountCommand command, [FromServices] IMediator mediator)
        => Return(mediator.Send(command).Result);

    /// <summary>
    /// Returns all Checnking Accounts
    /// </summary>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(QueryResult<CheckingAccountModel>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(QueryResult<CheckingAccountModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(QueryResult<CheckingAccountModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(QueryResult<CheckingAccountModel>), StatusCodes.Status204NoContent)]
    public IActionResult GetAll([FromServices] IMediator mediator)
        => Return(mediator.Send(new GetAllCheckingAccountQuery()).Result);


    /// <summary>
    /// Return Checking Account that have the id provided.
    /// </summary>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(QueryResult<CheckingAccountModel>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(QueryResult<CheckingAccountModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(QueryResult<CheckingAccountModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(QueryResult<CheckingAccountModel>), StatusCodes.Status204NoContent)]
    public IActionResult GetById([FromQuery] Guid id, [FromServices] IMediator mediator)
        => Return(mediator.Send(new GetByIdCheckingAccountQuery() { Id = id }).Result);


    /// <summary>
    /// Updates a Checking Account.
    /// </summary>
    [HttpPatch]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status204NoContent)]
    public IActionResult Update([FromQuery] UpdateCheckingAccountCommand command, [FromServices] IMediator mediator)
        => Return(mediator.Send(command).Result);

    /// <summary>
    /// Exclude a Checking Account.
    /// </summary>
    [HttpDelete]
    [Produces("application/json")]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CommandResult<CheckingAccountModel>), StatusCodes.Status204NoContent)]
    public IActionResult Delete([FromQuery] DeleteCheckingAccountCommand command, [FromServices] IMediator mediator)
        => Return(mediator.Send(command).Result);


    #endregion


    private IActionResult Return(dynamic result)
    {
        if (result.Message.Contains("Ok"))
        {
            return Ok(result);
        }
        else if (result.Message.Contains("BadRequest"))
        {
            return BadRequest(result);
        }
        else if (result.Message.Contains("NoContent"))
        {
            return StatusCode(StatusCodes.Status204NoContent, result);
        }

        return StatusCode(StatusCodes.Status500InternalServerError, result);
    }
}