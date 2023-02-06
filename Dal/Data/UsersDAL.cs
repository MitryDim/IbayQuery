using Dal.Entities;


namespace Dal.Data
{
    public class UsersDAL
    {
        DatabaseContext _context;

        public UsersDAL(DatabaseContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Récupération de tous les utilisateurs
        /// </summary>
        /// <returns></returns>
        public List<UsersEntities> GetAllUsers()
        {
            return _context.Users.ToList();
        }


        public UsersEntities SearchUser(string email)
        {
            return _context.Users.Where(c => c.Email == email).FirstOrDefault();
        }

        public UsersEntities SearchUser(int id)
        {
            return _context.Users.Where(c => c.Id == id).FirstOrDefault();
        }




        public UsersEntities? Insert(UsersEntities user)
        {

            _context.Users.AddAsync(user);
            _context.SaveChanges();
            return user;

        }

        public bool? Update(UsersEntities user)
        {
            var currentdata = _context.Users.Where(u => u.Id == user.Id).FirstOrDefault();

            if (currentdata == null)
            {
                return null;
            }
            try
            {
                _context.Entry(currentdata).CurrentValues.SetValues(user);
                _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(UsersEntities user)
        {
            try
            {
                _context.Remove(user);
                _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Erreur when delete user ! ");
            }
            return true;
        }
    }
}
