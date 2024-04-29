using Store.Data.Entities.OrderEntities;

namespace Store.Repository.Specification.OrderSpecification
{
    public class OrderWithItemsSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsSpecification(string buyerEmail) 
            : base(order=>order.BuyerEmail == buyerEmail)
        {
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);
            AddOrderBy(x => x.OrderDate);
            AddOrderByDir("DESC");
        }      
        public OrderWithItemsSpecification(Guid id,string buyerEmail) 
            : base(order=>order.BuyerEmail == buyerEmail && order.Id == id)
        {
            AddInclude(x => x.OrderItems);
            AddInclude(x => x.DeliveryMethod);
        }
    }
}
