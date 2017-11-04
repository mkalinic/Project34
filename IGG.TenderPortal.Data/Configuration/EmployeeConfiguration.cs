using System.Data.Entity.ModelConfiguration;
using IGG.TenderPortal.Model;

namespace IGG.TenderPortal.Data.Configuration
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            ToTable("Employee");

            HasKey(s => s.EmployeeId);

            HasRequired(s => s.Person)
            .WithOptional(e => e.Employee)
            .Map(x => x.MapKey("PersonId"));
        }
    }
}
