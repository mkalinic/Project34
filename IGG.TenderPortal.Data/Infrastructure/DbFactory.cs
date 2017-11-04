namespace IGG.TenderPortal.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        PortalEntities dbContext;

        public PortalEntities Init()
        {
            return dbContext ?? (dbContext = new PortalEntities());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
