using Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data.Configurations.Base
{
    public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity<int>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder) 
        {
            builder.HasKey(x => x.Id);
        }
    }
}
