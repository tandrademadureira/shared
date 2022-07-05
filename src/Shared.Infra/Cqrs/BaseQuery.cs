namespace Shared.Infra.Cqrs
{
#pragma warning disable CS1570 // XML comment has badly formed XML
    /// <summary>
    /// BaseQuery abstract class that receives an instance of a generic object. Inherits from the BaseCommand abstract class which also takes an instance of a generic object
    /// Class responsible for abstracting the assembly of query return used in the mediator pattern
    /// </summary>
    /// <typeparam name="TResponse">Instance of the generic object used for the response</typeparam>
    /// <example>
    /// You must create a class that implements a contract and inherits from class BaseQuery
    /// <para>Create a GetByIdContract class as below</para>
    /// <code>
    ///  public class GetByIdQuery
    ///  {
    ///     public class GetByIdContract : BaseQuery<Result<Domain.AggregateModels.Example>>
    ///     {
    ///         public Guid Id { get; set; }
    ///     }
    ///     
    ///     //The handle responsible for executing the query will inherit from BaseHandler
    ///     
    ///     public class Handler : BaseHandler<GetByIdContract, Result<Domain.AggregateModels.Example>>
    ///     {
    ///         private readonly IExampleRepository _exampleRepository;
    ///     
    ///         public Handler(IExampleRepository _exampleRepository)
    ///         {
    ///             _exampleRepository = exampleRepository;
    ///         }
    ///     
    ///         public async override Task<Result<Domain.AggregateModels.Example>> Handle(GetByIdContract request, CancellationToken cancellationToken)
    ///         {
    ///             if (request.Id.Equals(Guid.Empty))
    ///                 return Result.Fail<Domain.AggregateModels.Example>("Id can not be null");
    ///     
    ///             var model = await _exampleRepository.Get(request.Id);
    ///             if (model is null)
    ///                 return Result.Fail<Domain.AggregateModels.Example>("Example not found");
    ///     
    ///             return Result.Ok(model);
    ///         }
    ///     }
    ///  }
    /// </code>
    /// </example>
#pragma warning restore CS1570 // XML comment has badly formed XML
    public abstract class BaseQuery<TResponse> : BaseCommand<TResponse>
    {

    }
}
