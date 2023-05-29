using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserContext : IDb<User, int>
    {
        private readonly GameDBContext dbContext;

        public UserContext(GameDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(User item)
        {
            try
            {
                List<Game> gamesFromDb = new List<Game>();

                foreach (Game game in item.Games)
                {
                    Game gameFromDb = dbContext.Games.Find(game.Id);
                    if (gameFromDb != null)
                    {
                        gamesFromDb.Add(gameFromDb);
                    }
                    else
                    {
                        gamesFromDb.Add(game);
                    }
                }
                item.Games = gamesFromDb;

                dbContext.Users.Add(item);
                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int key)
        {
            try
            {
                User userFromDb = Read(key);

                if (userFromDb != null)
                {
                    dbContext.Users.Remove(userFromDb);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("The user doesn't exist!");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public User Read(int key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<User> query = dbContext.Users;

                if (useNavigationalProperties)
                {
                    query = query.Include(u => u.Games);
                }

                return query.FirstOrDefault(u => u.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<User> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<User> query = dbContext.Users;

                if (useNavigationalProperties)
                {
                    query = query.Include(u => u.Games);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(User item, bool useNavigationalProperties = false)
        {
            try
            {
                dbContext.Users.Update(item);

                if (useNavigationalProperties)
                {
                    List<Game> gamesFromDb = new List<Game>();

                    foreach (Game game in item.Games)
                    {
                        Game gameFromDb = dbContext.Games.Find(game.Id);
                        if (gameFromDb != null)
                        {
                            gamesFromDb.Add(gameFromDb);
                        }
                        else
                        {
                            gamesFromDb.Add(game);
                        }
                    }

                    item.Games = gamesFromDb;
                }

                dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
