using System;

namespace CloudBase.Data.TenantProvider
{
    public interface ITenantProvider
    {
        Guid GetTenantId();
    }
}