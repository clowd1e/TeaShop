using TeaShop.Domain.Entities;

namespace TeaShop.Application.Comparison
{
    public static class ComparisonExtensions
    {
        public static bool TeaDefaultComparison(Tea left, Tea right)
        {
            if (left is null || right is null)
                return false;

            return left.Name.ToCompare() == right.Name.ToCompare()
                && left.Type == right.Type;
        }

        public static bool TeaTypeDefaultComparison(TeaType left, TeaType right)
        {
            if (left is null || right is null)
                return false;

            return left.Name.ToCompare() == right.Name.ToCompare();
        }

        public static bool CustomerDefaultComparison(Customer left, Customer right)
        {
            if (left is null || right is null)
                return false;

            return left.FirstName.ToCompare() == right.FirstName.ToCompare()
                && left.LastName.ToCompare() == right.LastName.ToCompare();
        }

        public static bool OrderDefaultComparison(Order left, Order right)
        {
            if (left is null || right is null)
                return false;

            return left.Customer == right.Customer
                && left.Details == right.Details;
        }

        private static string ToCompare(this string value)
        {
            return value.Trim().ToLower();
        }
    }
}
