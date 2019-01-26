using System.Linq;
using AtiehJobCore.Domain.Entities.Identity;

namespace AtiehJobCore.Domain.Extensions
{
    public static class UserQuery
    {
        public static IQueryable<User> SkipAndTake(
            this IQueryable<User> users, int pageIndex, int pageSize)
        {
            return users.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public static IQueryable<User> OrderByUserName(
            this IQueryable<User> users, bool isDesc = false)
        {
            return isDesc
                ? users.OrderByDescending(a => a.UserName).AsQueryable()
                : users.OrderBy(a => a.UserName).AsQueryable();
        }

        public static IQueryable<User> OrderByEmail(this IQueryable<User> users, bool isDesc = false)
        {
            return isDesc
                ? users.OrderByDescending(a => a.Email).AsQueryable()
                : users.OrderBy(a => a.Email).AsQueryable();
        }

        public static IQueryable<User> SearchByEmail(this IQueryable<User> users, string email)
        {
            return users.Where(a => a.Email.Contains(email));
        }

        public static IQueryable<User> SearchByUserName(this IQueryable<User> users, string userName)
        {
            return users.Where(a => a.UserName.Contains(userName));
        }

        public static IQueryable<User> SearchByDisplayName(
            this IQueryable<User> users, string nameForShow)
        {
            return users.Where(a => a.DisplayName.Contains(nameForShow));
        }

        public static IQueryable<User> SearchByActiveUser(this IQueryable<User> users)
        {
            return users.Where(a => a.IsActive);
        }
    }
}
