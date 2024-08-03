using TeaShop.Domain.Entities;

namespace TeaShop.Domain.Delegates
{
    public delegate bool OrderComparisonDelegate(Order left, Order right);
}
