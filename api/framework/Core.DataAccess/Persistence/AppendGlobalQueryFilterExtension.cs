using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.DataAccess.Persistence;

/// <summary>
/// Esta classe fornece uma extensão para o ModelBuilder do Entity Framework Core,
/// permitindo aplicar filtros globais de consulta (HasQueryFilter) automaticamente
/// a todas as entidades que implementam uma determinada interface.
/// 
/// Esse recurso é útil para cenários como:
/// - Soft Delete (ex: IsDeleted == false)
/// - Multi-tenancy (ex: TenantId == currentTenantId)
/// - Regras de segurança ou visibilidade por interface
/// 
/// O filtro fornecido será aplicado globalmente a todas as entidades que implementam a interface,
/// combinando com filtros existentes se já houver algum definido para a entidade.
/// </summary>
internal static class ModelBuilderExtensions
{
    public static ModelBuilder AppendGlobalQueryFilter<TInterface>(
        this ModelBuilder modelBuilder,
        Expression<Func<TInterface, bool>> filter)
    {
        // Obtém todas as entidades que não possuem tipo base e implementam a interface TInterface
        var entidades = modelBuilder.Model.GetEntityTypes()
            .Where(e => e.BaseType is null && e.ClrType.GetInterface(typeof(TInterface).Name) is not null)
            .Select(e => e.ClrType);

        foreach (var entidade in entidades)
        {
            var parametro = Expression.Parameter(modelBuilder.Entity(entidade).Metadata.ClrType);
            var corpoFiltro = ReplacingExpressionVisitor.Replace(filter.Parameters.Single(), parametro, filter.Body);

            // Verifica se já existe um filtro global definido para a entidade
            if (modelBuilder.Entity(entidade).Metadata.GetQueryFilter() is { } filtroExistente)
            {
                var corpoFiltroExistente = ReplacingExpressionVisitor.Replace(
                    filtroExistente.Parameters.Single(), parametro, filtroExistente.Body);

                // Combina o filtro existente com o novo filtro usando operador E lógico (AND)
                corpoFiltro = Expression.AndAlso(corpoFiltroExistente, corpoFiltro);
            }

            // Aplica o filtro global final à entidade
            modelBuilder.Entity(entidade).HasQueryFilter(Expression.Lambda(corpoFiltro, parametro));
        }

        return modelBuilder;
    }
}
