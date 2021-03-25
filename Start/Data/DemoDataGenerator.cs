using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace UserManagement.Data
{
    /// <summary>
    /// Provides methods for filling the database with demo data
    /// </summary>
    public class DemoDataGenerator
    {
        private readonly UserManagementDataContext dc;

        public DemoDataGenerator(UserManagementDataContext dc)
        {
            this.dc = dc;
        }

        /// <summary>
        /// Delete all data in the database
        /// </summary>
        /// <returns></returns>
        public async Task ClearAll()
        {
            dc.Users.RemoveRange(await dc.Users.ToArrayAsync());
            await dc.SaveChangesAsync();
        }

        /// <summary>
        /// Fill database with demo data
        /// </summary>
        public async Task Fill()
        {
            #region Add some users
            User foo, john, jane;
            dc.Users.Add(foo = new User
            {
                NameIdentifier = "foo.bar",
                FirstName = "Foo",
                LastName = "Bar",
                Email = "foo.bar@acme.corp"
            });

            dc.Users.Add(john = new User
            {
                NameIdentifier = "john.doe",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@acme.corp"
            });

            dc.Users.Add(jane = new User
            {
                NameIdentifier = "jane.doe",
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@acme.corp"
            });
            #endregion

            #region Add some groups
            // Add code to generate demo groups here
            Group gachi, yep, clock;
            dc.Groups.Add(gachi = new Group
            {
                Name = "Gachi Bois",
                Users = new List<User>(new User[] {foo, john})
            });
            dc.Groups.Add(yep = new Group
            {
                Name = "Yeppers",
                Users = new List<User>(new User[] {jane})
            });
            dc.Groups.Add(clock = new Group
            {
                Name = "Clox",
                Users = new List<User>()
            });
            #endregion

            await dc.SaveChangesAsync();
        }
    }
}
