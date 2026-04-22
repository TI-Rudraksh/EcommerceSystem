using EcommerceSystem.Services.Interface;

namespace EcommerceSystem.Services;

public class TenantService: ITenantService
{
    public string GetCurrentTenantId()
    {
        return "tenant_2";
    }
}