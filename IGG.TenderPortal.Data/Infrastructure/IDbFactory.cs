﻿using System;

namespace IGG.TenderPortal.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        PortalEntities Init();
    }
}
