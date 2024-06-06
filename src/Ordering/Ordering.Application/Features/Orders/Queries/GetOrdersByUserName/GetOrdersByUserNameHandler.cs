using AutoMapper;
using MediatR;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByUserName
{
    public class GetOrdersByUserNameHandler : IRequestHandler<GetOrdersByUserNameQuery, List<GetOrdersByUserNameDto>>
    {
        IOrderRepository _repository;
        IMapper _mapper;

        public GetOrdersByUserNameHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetOrdersByUserNameDto>> Handle(GetOrdersByUserNameQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repository.GetOrdersByUserName(request.UserName);
            return _mapper.Map<List<GetOrdersByUserNameDto>>(orders);
        }
    }
}
