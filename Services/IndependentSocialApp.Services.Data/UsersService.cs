namespace IndependentSocialApp.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using IndependentSocialApp.Data.Common.Repositories;
    using IndependentSocialApp.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepo;

        public UsersService(IDeletableEntityRepository<ApplicationUser> usersRepo)
        {
            this.usersRepo = usersRepo;
        }

        public async Task<ApplicationUser> FindUserAsync(string id)
        {
            return await this.usersRepo
                .AllAsNoTracking()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
