using Audree.Demo.Core.Models;
using Audree.Demo.Core.Models.Admin;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Audree.Demo.Infrastructure.Data
{

    public class Plcontextclass:DbContext
    {
        #region DB Context
        public Plcontextclass(DbContextOptions<Plcontextclass> options) : base(options) { }
        
        #endregion

        #region DBSET
        public DbSet<PLClub> pLClubs { get; set; }
        public DbSet<Employ> employs { get; set; }

        #endregion

    }
}
