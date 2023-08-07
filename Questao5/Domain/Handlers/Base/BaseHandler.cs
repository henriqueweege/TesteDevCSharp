using Domain.Commands;
using Domain.Commands.Contracts;
using Domain.Converters.Contracts;
using Domain.Entities.Contracts;
using Domain.Enums;
using Domain.Models.Contracts;
using Domain.Queries;
using Domain.Queries.Contracts;
using Domain.Repositories.Contracts;
using Tools;

namespace Domain.Handlers.Base;

public class BaseHandler<E, M, C> where E : class, IEntity where M : class, IModel where C : class, IConverter<E, M>
{
    private static IConverter<E, M> Converter { get; set; }
    private static IRepository<M> Repository { get; set; }
    public BaseHandler(IConverter<E, M> converter, IRepository<M> repository)
    {
        Converter = converter;
        Repository = repository;
    }

    public static CommandResult<M> Handle(ICreateCommand command)
    {
        try
        {
            var entity = Converter.ConvertFromCommandCreateToEntity(command);
            IModel model = Converter.ConvertFromEntityToModel(entity);
            var resultCreate = Repository.Save((M)model);

            if (resultCreate > 0)
            {
                return new CommandResult<M>(true, ESuccessMessages.OK_REQUISITON_COMPLETED_SUCCESSFULLY.ToDescription(), (M)model);
            }
            return new CommandResult<M>(false, EErrorMessages.INTERNAL_SERVER_ERROR.ToDescription(), (M)model);
        }
        catch (Exception ex)
        {

            return new CommandResult<M>(false, ex.InnerException.Message);
        }

    }

    public static CommandResult<M> Handle(IDeleteCommand command, Guid id)
    {
        try
        {
            var entityToDelete = Repository.GetById(id.ToString());

            if (entityToDelete == null)
            {
                return new CommandResult<M>(true, EErrorMessages.BAD_REQUEST.ToDescription());
            }

            var deleted = Repository.Delete(entityToDelete);

            if (deleted)
            {
                return new CommandResult<M>(true, ESuccessMessages.NO_CONTENT_REQUISITON_COMPLETED_SUCCESSFULLY.ToDescription());
            }
            return new CommandResult<M>(false, EErrorMessages.INTERNAL_SERVER_ERROR.ToDescription());

        }
        catch (Exception ex)
        {

            return new CommandResult<M>(false, ex.InnerException.Message);
        }
    }

    public static QueryResult<M> Handle(IGetAllQuery query)
    {
        try
        {
            var entities = Repository.GetAll();

            if (entities != null && entities.Count() > 0)
            {
                return new QueryResult<M>(true, ESuccessMessages.OK_REQUISITON_COMPLETED_SUCCESSFULLY.ToDescription(), entities);
            }
            else
            {
                return new QueryResult<M>(true, ESuccessMessages.NO_CONTENT_REQUISITON_COMPLETED_SUCCESSFULLY.ToDescription(), entities);

            }
        }
        catch (Exception ex)

        {

            return new QueryResult<M>(false, ex.InnerException.Message);
        }
    }

    public static QueryResult<M> Handle(IGetByIdQuery query, Guid id)
    {
        try
        {
            var entities = Repository.GetById(id.ToString());
            if (entities != null)
            {
                return new QueryResult<M>(true, ESuccessMessages.OK_REQUISITON_COMPLETED_SUCCESSFULLY.ToDescription(), entities);
            }
            else
            {
                return new QueryResult<M>(true, ESuccessMessages.NO_CONTENT_REQUISITON_COMPLETED_SUCCESSFULLY.ToDescription(), entities);

            }
        }
        catch (Exception ex)
        {

            return new QueryResult<M>(false, ex.InnerException.Message);
        }
    }
}
