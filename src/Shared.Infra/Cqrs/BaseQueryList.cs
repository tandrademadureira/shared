namespace Shared.Infra.Cqrs
{
#pragma warning disable CS1570 // XML comment has badly formed XML
    /// <summary>
    /// BaseQueryList abstract class that receives an instance of a generic object. Inherits from the BaseCommand abstract class which also takes an instance of a generic object
    /// Class responsible for abstracting the assembly of query return lists used in the mediator pattern
    /// </summary>
    /// <typeparam name="TResponse">Instance of the generic object used for the response</typeparam>
    /// <example>
    /// You must create a class that implements a contract and inherits from class BaseQueryList
    /// <para>Create a GetContract class as below</para>
    /// <code>
    /// public class GetQuery
    /// {
    ///     public class GetContract : BaseQueryList<Result<PagedList<Domain.AggregateModels.Example>>>
    ///     {
    ///         public string Name { get; set; }
    ///     }
    /// 
    ///     public class Handler : BaseHandler<GetContract, Result<PagedList<Domain.AggregateModels.Example>>>
    ///     {
    ///         private readonly IExampleRepository _exampleRepository;
    /// 
    ///         public Handler(IExampleRepository exampleRepository)
    ///         {
    ///             _exampleRepository = exampleRepository;
    ///         }
    /// 
    ///         public async override Task<Result<PagedList<Domain.AggregateModels.Example>>> Handle(GetContract request, CancellationToken cancellationToken)
    ///         {
    ///             return Result.Ok(await _exampleRepository.GetAllByFilterAsync(request.Name, request.Page, request.ItemsPerPage, request.OrderedAsc));
    ///         }
    ///     }
    /// }
    /// </code>
    /// </example>
#pragma warning restore CS1570 // XML comment has badly formed XML

    public abstract class BaseQueryList<TResponse> : BaseCommand<TResponse>
    {
        /// <summary>
        /// Property Page => Indicates how many items we will have per page in the response object.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Property ItemsPerPage => Indicates total of items per page in the response object. 
        /// </summary>
        public int ItemsPerPage { get; set; } = 100;

        /// <summary>
        /// Property OrderedAsc => Indicates whether the ordering of the query response will be ascending
        /// </summary>
        public bool OrderedAsc { get; set; } = true;
    }
}
