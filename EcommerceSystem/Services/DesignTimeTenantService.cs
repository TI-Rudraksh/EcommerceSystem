using EcommerceSystem.Services.Interface;

namespace EcommerceSystem.Services;

public class DesignTimeTenantService: ITenantService
{
    public string GetCurrentTenantId()
    {
        return "migration_tenant";
    }
}