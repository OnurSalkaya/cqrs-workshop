using MediatR;
using OrderApiCQS.Domain.Data.Repositories;
using OrderApiCQS.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderApiCQS.Domain.Handlers.EventHandlers
{
    public class NotifyCustomerWhenOrderShippedHandler : INotificationHandler<OrderShippedEvent>
    {
        private readonly IUserRepository _userRepository;

        public NotifyCustomerWhenOrderShippedHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(OrderShippedEvent notification, CancellationToken cancellationToken)
        {
            //Get user from userId
            string userName = _userRepository.GetUserNameFake(notification.UserId);

            Console.WriteLine($"Dear {userName}, your order was shipped successfully. Thank you for choosing us!");

            await Task.Yield();
        }
    }
}
