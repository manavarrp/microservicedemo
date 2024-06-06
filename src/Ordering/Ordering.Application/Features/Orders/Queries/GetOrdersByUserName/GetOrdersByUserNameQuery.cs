using MediatR;

namespace Ordering.Application.Features.Orders.Queries.GetOrdersByUserName
{
    public class GetOrdersByUserNameQuery : IRequest <List<GetOrdersByUserNameDto>>
    {
        public string UserName { get; set; }
        public GetOrdersByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
