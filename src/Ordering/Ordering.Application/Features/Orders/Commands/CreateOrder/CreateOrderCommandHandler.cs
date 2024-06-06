using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Models;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
    {
        IOrderRepository _orderRepository;
        IMapper _mapper;
        IEmailService _emailService;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);
            bool isOrderPlaced = await _orderRepository.AddAsync(order);
            if (isOrderPlaced)
            {
                Email email = new();
                email.Subject = "Tu orden ha sido enviada";
                email.To = order.UserName;
                email.Body = $"Estimado(a) {order.FirstName + " " + order.LastName} <br/><br/> Estamos complacidos de que recibas tu orden #{order.Id}. <br/> Gracias por preferirnos "; 
                await _emailService.SendEmailAsync(email);
            
            }
            return isOrderPlaced;
        }
    }
}
