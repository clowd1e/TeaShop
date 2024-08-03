using TeaShop.Domain.Entities;

namespace TeaShop.Domain.Delegates
{
    public delegate bool CustomerComparisonDelegate(Customer left, Customer right);
}
