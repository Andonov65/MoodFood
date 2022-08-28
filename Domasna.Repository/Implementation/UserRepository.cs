using Domasna.Domain.Identity;
using Domasna.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domasna.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<DomasnaApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<DomasnaApplicationUser>();
        }
        public IEnumerable<DomasnaApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public DomasnaApplicationUser Get(string id)
        {
            return entities
               .Include(z => z.UserCart)
               .Include("UserCart.TicketInShoppingCarts")
               .Include("UserCart.TicketInShoppingCarts.Ticket")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(DomasnaApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(DomasnaApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(DomasnaApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
