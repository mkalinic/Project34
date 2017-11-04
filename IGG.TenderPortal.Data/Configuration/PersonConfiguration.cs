using System.Data.Entity.ModelConfiguration;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data.Configuration
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            ToTable("Person");

            HasKey(s => s.PersonId);                              
        }
    }
}
