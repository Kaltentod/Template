using BNA.IB.WEBAPP.Domain.Entities.PlanCuentas;
using Mapster;
using BNA.IB.WEBAPP.Infrastructure.SQLServer.Models;

namespace BNA.IB.WEBAPP.Infrastructure.SQLServer.Repositories;

public class PlanCuentasRepository : IPlanCuentaRepository
{
    private readonly IBContext _context;

    public PlanCuentasRepository(IBContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IQueryable<Domain.Entities.PlanCuentas.PlanCuenta>> ListPlanCuenta(string? cuenta_Subcuenta)
    {
        if (!string.IsNullOrWhiteSpace(cuenta_Subcuenta))
            return _context.PlanCuentas.Where(x => x.CuentaSubcuenta.Equals(cuenta_Subcuenta)).ProjectToType<Domain.Entities.PlanCuentas.PlanCuenta>();

        return _context.PlanCuentas.ProjectToType<Domain.Entities.PlanCuentas.PlanCuenta>();
    }

    public IQueryable<Domain.Entities.PlanCuentas.PlanCuenta> GetCuentas()
    {
        return _context.PlanCuentas.Distinct().ProjectToType<Domain.Entities.PlanCuentas.PlanCuenta>();
    }
}