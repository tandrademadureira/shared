using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Shared.Infra.Cqrs
{
    /// <summary>
    /// Abstract class that receives an instance of a generic object. Contains the inheritance of this from the XXX interface which also receives the instance of this generic object.
    /// Class responsible for abstracting notification events, within the meidator design pattern
    /// </summary>
    /// <example>
    /// You must create a class that implements a EventHandle and inherits from class BaseEventHandler
    /// <para>Create a SendEmailEventHandle class as below.</para>
    /// <code>
    ///  public class SendEmailEvent : BaseNotification
    ///  {
    ///      public string To { get; set; }
    ///  
    ///      public SendEmailEvent(string to)
    ///      {
    ///          To = to;
    ///      }
    ///  }
    ///  
    ///  public class SendEmailEventHandle : BaseEventHandler<SendEmailEvent/>
    ///  {
    ///      public IMediator Mediator { get; set; }
    ///  
    ///      public SendEmailEventHandle(IMediator mediator)
    ///      {
    ///          Mediator = mediator;
    ///      }
    ///  
    ///      public override async Task Handle(SendEmailEvent notification, CancellationToken cancellationToken)
    ///      {
    ///          await Mediator.Send(new SendEmailCommand.SendEmailContract() { To = notification.To });
    ///      }
    ///  }
    ///  
    ///  Another handle, after performing its specific tasks, will publish the event by the mediator design pattern
    /// 
    ///  public async override Task<Result/> Handle(UpdateContract request, CancellationToken cancellationToken)
    ///  {
    ///     // ...
    ///     // Code referring to the command or query
    ///     // ...
    ///
    ///     await _mediator.Publish(new SendEmailEvent(request.Name));
    ///
    ///     return Result.Ok();
    ///  }
    /// </code>
    /// </example>
#pragma warning restore CS1570 // XML comment has badly formed XML
    public abstract class BaseEventHandler<TEvent> : INotificationHandler<TEvent>
        where TEvent : INotification
    {
        /// <summary>
        /// Abstract method responsible for handling the event
        /// </summary>
        /// <param name="notification">Notification event</param>
        /// <param name="cancellationToken">Instance of CancellationToken</param>
        /// <returns></returns>
        public abstract Task Handle(TEvent notification, CancellationToken cancellationToken);
    }
}
