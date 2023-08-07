using Domain.Commands;
using Domain.Commands.CheckingAccountCommands;
using Domain.Commands.ViewModels;
using Domain.Converters;
using Domain.Converters.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Handlers.Base;
using Domain.Models;
using Domain.Queries;
using Domain.Queries.CheckingAccountQueries;
using Domain.Queries.ViewModel;
using Domain.Repositories;
using Domain.Repositories.Contracts;
using MediatR;
using Newtonsoft.Json;
using System.Diagnostics;
using Tools;

namespace Domain.Handlers;

public class CheckingAccountHandler : BaseHandler<CheckingAccountEntity, CheckingAccountModel, CheckingAccountConverter>,
                                      IRequestHandler<UpdateCheckingAccountCommand, CommandResult<CheckingAccountModel>>,
                                      IRequestHandler<CreateCheckingAccountCommand, CommandResult<CheckingAccountModel>>,
                                      IRequestHandler<DeleteCheckingAccountCommand, CommandResult<CheckingAccountModel>>,
                                      IRequestHandler<GetAllCheckingAccountQuery, QueryResult<CheckingAccountModel>>,
                                      IRequestHandler<GetByIdCheckingAccountQuery, QueryResult<CheckingAccountModel>>,
                                      IRequestHandler<GetCheckingAccountBalanceQuery, QueryResult<GetCheckingAccountBalanceVM>>,
                                      IRequestHandler<ExecuteTransactionCommand, CommandResult<ExecuteTransactionCommandVM>>
{

    private static IConverter<TransactionEntity, TransactionModel> TransactionConverter { get; set; }
    private static IConverter<CheckingAccountEntity, CheckingAccountModel> AccountConverter { get; set; }
    private static IConverter<IdempotencyEntity, IdempotencyModel> IdempotencyConverter { get; set; }
    private static IRepository<TransactionModel> TransactionRepository { get; set; }
    private static IRepository<CheckingAccountModel> AccountRepository { get; set; }
    private static IRepository<IdempotencyModel> IdempotencyRepository { get; set; }
    public CheckingAccountHandler(IConverter<TransactionEntity,TransactionModel> transactionConverter,
                                  IConverter<CheckingAccountEntity,CheckingAccountModel> accountConverter,
                                  IConverter<IdempotencyEntity, IdempotencyModel> idempotencyConverter,
                                  IRepository<TransactionModel> transactionRepository,
                                  IRepository<CheckingAccountModel> accountRepository,
                                  IRepository<IdempotencyModel> idempotencyRepository) : base(accountConverter, accountRepository)
    {
        TransactionConverter = transactionConverter;
        TransactionRepository = transactionRepository;
        AccountRepository = accountRepository;
        AccountConverter = accountConverter;
        IdempotencyRepository = idempotencyRepository;
        IdempotencyConverter = idempotencyConverter;
    }

    public CommandResult<CheckingAccountModel> Handle(UpdateCheckingAccountCommand command)
    {
        try
        {
            var obj = AccountRepository.GetById(command.IdCheckingAccount.ToString());
            var entity = AccountConverter.ConvertFromModelToEntity(obj) as CheckingAccountEntity;

            if (command.Active is false && entity.Active)
            {
                entity.Inactivate();
            }
            else
            {
                entity.Activate();
            }

            if (command.HolderName != entity.HolderName)
            {
                entity.UpdateName(command.HolderName);
            }

            var updated = AccountRepository.Update(AccountConverter.ConvertFromEntityToModel(entity) as CheckingAccountModel);

            if (updated)
            {
                return new CommandResult<CheckingAccountModel>(true, ESuccessMessages.OK_REQUISITON_COMPLETED_SUCCESSFULLY.ToDescription(), (CheckingAccountModel)AccountConverter.ConvertFromEntityToModel(entity));
            }
            return new CommandResult<CheckingAccountModel>(false, EErrorMessages.INTERNAL_SERVER_ERROR.ToDescription(), (CheckingAccountModel)AccountConverter.ConvertFromEntityToModel(entity));

        }
        catch (Exception ex)
        {
            return new CommandResult<CheckingAccountModel>(false, ex.InnerException.Message);
        }
    }

    public Task<CommandResult<CheckingAccountModel>> Handle(UpdateCheckingAccountCommand request, CancellationToken cancellationToken)
        => Task.FromResult(Handle(request));

    public Task<CommandResult<CheckingAccountModel>> Handle(DeleteCheckingAccountCommand request, CancellationToken cancellationToken)
        => Task.FromResult(Handle(request, request.Id));

    public Task<CommandResult<CheckingAccountModel>> Handle(CreateCheckingAccountCommand request, CancellationToken cancellationToken)
        => Task.FromResult(Handle(request));

    public Task<QueryResult<CheckingAccountModel>> Handle(GetAllCheckingAccountQuery request, CancellationToken cancellationToken)
        => Task.FromResult(Handle(request));

    public Task<QueryResult<CheckingAccountModel>> Handle(GetByIdCheckingAccountQuery request, CancellationToken cancellationToken)
        => Task.FromResult(Handle(request, request.Id));

    public Task<CommandResult<ExecuteTransactionCommandVM>> Handle(ExecuteTransactionCommand request, CancellationToken cancellationToken)
    {

        CommandResult<ExecuteTransactionCommandVM> response;
        var idempotencyResult = IdempotencyRepository.GetById(request.Id.ToString());

        if (idempotencyResult != null)
        {
            return Task.FromResult(JsonConvert.DeserializeObject<CommandResult<ExecuteTransactionCommandVM>>(idempotencyResult.resultado));
        }

        if (request.Value <= 0)
        {
            response = new CommandResult<ExecuteTransactionCommandVM>(false, EErrorMessages.INVALID_VALUE.ToDescription());
            SaveIdempotency(request, response);
            return Task.FromResult(response);
        }

        var accountModel = AccountRepository.GetById(request.IdCheckingAccount.ToString());


        if (accountModel is null)
        {
            response = new CommandResult<ExecuteTransactionCommandVM>(false, EErrorMessages.INVALID_ACCOUNT.ToDescription());
            SaveIdempotency(request, response);
            return Task.FromResult(response);
        }

        var accountEntity = AccountConverter.ConvertFromModelToEntity(accountModel);

        if (!accountEntity.Active)
        {
            response = new CommandResult<ExecuteTransactionCommandVM>(false, EErrorMessages.INACTIVE_ACCOUNT.ToDescription());
            SaveIdempotency(request, response);
            return Task.FromResult(response);
        }

        TransactionEntity entity;

        try
        {
            entity = TransactionConverter.ConvertFromCommandCreateToEntity(request);
        }
        catch (Exception ex)
        {
            response = new CommandResult<ExecuteTransactionCommandVM>(false, ex.Message);
            SaveIdempotency(request, response);
            return Task.FromResult(response);
        }

        var model = TransactionConverter.ConvertFromEntityToModel(entity);

        var save = TransactionRepository.Save(model);
        if (save > 0)
        {
            response = new CommandResult<ExecuteTransactionCommandVM>(true, ESuccessMessages.OK_REQUISITON_COMPLETED_SUCCESSFULLY.ToDescription(), new ExecuteTransactionCommandVM(Guid.Parse(model.idmovimento)));
            SaveIdempotency(request, response);
            return Task.FromResult(response);
        }

        response = new CommandResult<ExecuteTransactionCommandVM>(false, EErrorMessages.INTERNAL_SERVER_ERROR.ToDescription());
        SaveIdempotency(request, response);
        return Task.FromResult(response);
    }

    public Task<QueryResult<GetCheckingAccountBalanceVM>> Handle(GetCheckingAccountBalanceQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var transactionRepository = TransactionRepository as TransactionRepository;
            var transactionConverter = TransactionConverter as TransactionConverter;

            var checkingAccount = AccountRepository.GetById(request.Id.ToString());
            
            if (checkingAccount is null)
            {
                return Task.FromResult(new QueryResult<GetCheckingAccountBalanceVM>(false, EErrorMessages.INVALID_ACCOUNT.ToDescription()));
            }

            var accountEntity = AccountConverter.ConvertFromModelToEntity(checkingAccount) as CheckingAccountEntity;
            if (!accountEntity.Active)
            {
                return Task.FromResult(new QueryResult<GetCheckingAccountBalanceVM>(false, EErrorMessages.INACTIVE_ACCOUNT.ToDescription()));
            }

            var transactionsModels = transactionRepository.GetAllByIdCheckingAccount(request.Id.ToString());
            var transactionsEntities = transactionConverter.ConvertFromModelToEntity(transactionsModels).ToList();

            var debit = transactionsEntities.Where(x => x.Type == ETransactionType.Debit).Select(x => x.Value).Sum();
            var credit = transactionsEntities.Where(x => x.Type == ETransactionType.Credit).Select(x => x.Value).Sum();

            return Task.FromResult(new QueryResult<GetCheckingAccountBalanceVM>(true, ESuccessMessages.OK_REQUISITON_COMPLETED_SUCCESSFULLY.ToDescription(), new GetCheckingAccountBalanceVM(credit-debit, accountEntity.Number, accountEntity.HolderName)));
        }
        catch(Exception ex)
        {
            return Task.FromResult(new QueryResult<GetCheckingAccountBalanceVM>(false, EErrorMessages.INTERNAL_SERVER_ERROR.ToDescription()));
        }
    }

    private static void SaveIdempotency(ExecuteTransactionCommand request, CommandResult<ExecuteTransactionCommandVM> response)
    {
        var idempotencyEntity = new IdempotencyEntity(true, request.Id, request, response);
        IdempotencyRepository.Save(IdempotencyConverter.ConvertFromEntityToModel(idempotencyEntity));
    }
}