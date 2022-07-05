using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Infra.Cqrs
{

#pragma warning disable CS1570 // XML comment has badly formed XML
    /// <summary>
    /// BaseHandler abstract class that receives an instance of your contract and a generic object. Inherits from the IRequestHandler interface that too receives an instance of your contract and a generic object and BaseCommand abstract class
    /// Class responsible for abstracting who will be the method responsible for implementing the commands used in the mediator pattern
    /// </summary>
    /// <typeparam name="Contract">Instance of your Contract</typeparam>
    /// <typeparam name="TResponse">Instance of your generic object used in response</typeparam>
    /// <example>
    /// Create a Handler class that must inherit from the BaseHandler class
    /// <para>Create a new Handler class as below.</para>
    /// <code>
    /// public class CreateCommand
    /// {
    ///     public class CreateContract : BaseCommand<Result/>
    ///     {
    ///         public string Name { get; set; }
    ///     }
    /// 
    ///     public class Handler : BaseHandler<CreateContract, Result>
    ///     {
    ///         private readonly IExampleRepository _exampleRepositoryRepository;
    ///         private readonly IUnitOfWork _unitOfWork;
    ///
    ///     public Handler(IExampleRepository exampleRepositoryRepository, IUnitOfWork unitOfWork)
    ///     {
    ///         _exampleRepositoryRepository = exampleRepositoryRepository;
    ///         _unitOfWork = unitOfWork;
    ///     }
    ///
    ///     public async override Task<Result> Handle(CreateContract request, CancellationToken cancellationToken)
    ///     {
    ///         var resultModel = Domain.AggregateModels.Examples.CreateExample(request.Name)
    ///
    ///         if (resultModel.IsFailure)
    ///             return Result.Fail(resultModel.Error);
    ///
    ///         await _exampleRepositoryRepository.Add(resultModel.Data);
    ///         await _unitOfWork.SaveChangesAsync();
    ///
    ///         return Result.Ok();
    ///     }
    /// }
    /// </code>
    /// </example>
#pragma warning restore CS1570 // XML comment has badly formed XML

    public abstract class BaseHandler<Contract, TResponse> : IRequestHandler<Contract, TResponse>
        where Contract : BaseCommand<TResponse>
    {
        /// <summary>
        /// Abstract method responsible for handling the command
        /// </summary>
        /// <param name="request">Request of command</param>
        /// <param name="cancellationToken">Instance of CancellationToken</param>
        /// <returns></returns>
        public abstract Task<TResponse> Handle(Contract request, CancellationToken cancellationToken);

    }
}
