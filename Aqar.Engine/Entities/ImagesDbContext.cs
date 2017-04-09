using Aqar.Engine.BusinessEntities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alfardan.Engine.Entities
{
  public class ImagesDbContext : IdentityDbContext
  {
    public ImagesDbContext()
      : base("DefaultConnection")
    {
    }

    public static ImagesDbContext Create()
    {
      return new ImagesDbContext();
    }

    public DbSet<Image> Images { get; set; }
  }

}
