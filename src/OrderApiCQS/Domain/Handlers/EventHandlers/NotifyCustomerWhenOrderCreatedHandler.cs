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
    public class NotifyCustomerWhenOrderCreatedHandler : INotificationHandler<OrderCreatedEvent>
    {
        private readonly IUserRepository _userRepository;

        public NotifyCustomerWhenOrderCreatedHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
            //Get user from userId
            string userName = _userRepository.GetUserNameFake(notification.UserId);

            Console.WriteLine($"Dear {userName}, your order was created successfully. Thank you for choosing us!");

            await Task.Yield();
        }
    }
}
