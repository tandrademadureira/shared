using MediatR;

namespace Shared.Infra.Cqrs
{
    /// <summary>
    /// BaseNotification abstract class. Inherits from the INotification interface
    /// Class responsible for abstracting inheritance for classes that will implement events
    /// </summary>
    /// <example>
    /// You must create a class that implements a Event and inherits from class BaseNotification
    /// <para>Create a SendEmailEvent class as below.</para>
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
    /// Another handle, after performing its specific tasks, will publish the event by the mediator design pattern
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
    public abstract class BaseNotification : INotification
    {
    }

}
