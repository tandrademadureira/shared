using MediatR;
using Shared.Infra.Request;
using System;

namespace Shared.Infra.Cqrs
{
#pragma warning disable CS1570 // XML comment has badly formed XML
    /// <summary>
    /// Abstract Class BaseCommand
    /// BaseCommand abstract class that receives an instance of a generic object. Inherits from the BaseRequest abstract class and IRequest interface which also takes an instance of a generic object
    /// Class responsible for abstracting the assembly of the return of the commands used in the mediator pattern
    /// </summary>
    /// <typeparam name="TResponse">Instance of the generic object used for the response</typeparam>
    /// <example>
    /// You must create a class that implements a contract and inherits from class BaseCommand
    /// <para>Create a CreateContract class as below.</para>
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
    ///         private readonly IExampleRepository _exampleRepository;
    ///         private readonly IUnitOfWork _unitOfWork;
    ///
    ///     public Handler(IExampleRepository exampleRepository, IUnitOfWork unitOfWork)
    ///     {
    ///         _exampleRepositoryRepository = exampleRepository;
    ///         _unitOfWork = unitOfWork;
    ///     }
    ///
    ///     public async override Task<Result> Handle(CreateContract request, CancellationToken cancellationToken)
    ///     {
    ///         var resultModel = Domain.AggregateModels.Examples.CreateExample(request.Name);
    ///
    ///         if (resultModel.IsFailure)
    ///             return Result.Fail(resultModel.Error);
    ///
    ///         await _exampleRepositoryRepository.Add(resultModel.Data);
    ///         await unitOfWork.SaveChangesAsync();
    ///
    ///         return Result.Ok();
    ///     }
    /// }
    /// </code>
    /// </example>
#pragma warning restore CS1570 // XML comment has badly formed XML
    [Serializable]
    public abstract class BaseCommand<TResponse> : BaseRequest, IRequest<TResponse>
    {
        /// <summary>
        /// Property Authenticated => Indicates whether the user is authenticated or not
        /// </summary>
        public bool Authenticated { get; set; }

        /// <summary>
        /// Property UserName => User name of user if is authenticated
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Property UserId => User id of user if is authenticated
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Property Roles => Roles of user if is authenticated
        /// </summary>
        public string[] Roles { get; set; }
    }
}
