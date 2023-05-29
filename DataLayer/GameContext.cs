using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class GameContext : IDb<Game, int>
    {
        private readonly GameDBContext dbContext;

        public GameContext(GameDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(Game item)
        {
            try
            {
                List<User> usersFromDb = new List<User>();
                List<Genre> genresFromDb = new List<Genre>();

                foreach (User user in item.Users)
                {
                    User userFromDb = dbContext.Users.Find(user.Id);
                    if (userFromDb != null)
                    {
                        usersFromDb.Add(userFromDb);
                    }
                    else
                    {
                        usersFromDb.Add(user);
                    }
                }

                item.Users = usersFromDb;

                foreach (Genre genre in item.Genres)
                {
                    Genre genreFromDb = dbContext.Genres.Find(genre.Id);
                    if (genreFromDb != null)
                    {
                        genresFromDb.Add(genreFromDb);
                    }
                    else
                    {
                        genresFromDb.Add(genre);
                    }
                }

                item.Genres = genresFromDb;

                dbContext.Games.Add(item);
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
                Game gameFromDb = Read(key);

                if (gameFromDb != null)
                {
                    dbContext.Games.Remove(gameFromDb);
                    dbContext.SaveChanges();
                }
                else
                {
                    throw new InvalidOperationException("Game with that key does not exist!");
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public Game Read(int key, bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Game> query = dbContext.Games;

                if (useNavigationalProperties)
                {
                    query = query
                        .Include(g => g.Users)
                        .Include(g => g.Genres);
                }

                return query.FirstOrDefault(g => g.Id == key);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Game> ReadAll(bool useNavigationalProperties = false)
        {
            try
            {
                IQueryable<Game> query = dbContext.Games;

                if (useNavigationalProperties)
                {
                    query = query
                        .Include(g => g.Users)
                        .Include(g => g.Genres);
                }

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Game item, bool useNavigationalProperties = false)
        {
            try
            {
                dbContext.Games.Update(item);

                if (useNavigationalProperties)
                {
                    List<User> usersFromDb = new List<User>();
                    List<Genre> genresFromDb = new List<Genre>();

                    foreach (User user in item.Users)
                    {
                        User userFromDb = dbContext.Users.Find(user.Id);
                        if (userFromDb != null)
                        {
                            usersFromDb.Add(userFromDb);
                        }
                        else
                        {
                            usersFromDb.Add(user);
                        }
                    }

                    item.Users = usersFromDb;

                    foreach (Genre genre in item.Genres)
                    {
                        Genre genreFromDb = dbContext.Genres.Find(genre.Id);
                        if (genreFromDb != null)
                        {
                            genresFromDb.Add(genreFromDb);
                        }
                        else
                        {
                            genresFromDb.Add(genre);
                        }
                    }

                    item.Genres = genresFromDb;
                }

                dbContext.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
