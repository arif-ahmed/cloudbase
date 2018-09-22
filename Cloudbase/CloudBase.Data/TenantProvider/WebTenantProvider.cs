using System;
using Cloudbase.Entities;
using CloudBase.Data.DbContext;
using Microsoft.AspNetCore.Http;

namespace CloudBase.Data.TenantProvider
{
    public class WebTenantProvider : ITenantProvider
    {
        private readonly Guid _tenantId;

        public WebTenantProvider(IHttpContextAccessor accessor,
            TenantDbContext context)
        {
            var host = accessor.HttpContext.Request.Host.Value;

            _tenantId = context.GetTenantId(host);
        }

        public Guid GetTenantId()
        {
            return _tenantId;
        }
    }
}
