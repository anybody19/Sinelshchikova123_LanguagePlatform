
namespace Sinelshchikova123_LanguagePlatform
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BD_Sinelshchikova123_LanguagePlatformEntities : DbContext
    {
        public BD_Sinelshchikova123_LanguagePlatformEntities()
            : base("name=BD_Sinelshchikova123_LanguagePlatformEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Progress> Progress { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
