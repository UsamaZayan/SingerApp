using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using SingerApp.DataFilters;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System;
using SingerApp.Singers;
using SingerApp.Data.DataLookups;
using System.Reflection;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace SingerApp.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class SingerAppDbContext :
    AbpDbContext<SingerAppDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    public DbSet<Singer> Singers { get; set; }
    public DbSet<Country> Countries { get; set; }

    public DbSet<SingerTranslation> SingerTranslations { get; set; }


    protected virtual bool IsActiveFilterEnabled => DataFilter?.IsEnabled<IIsActive>() ?? false;

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public SingerAppDbContext(DbContextOptions<SingerAppDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.HasPostgresExtension("uuid-ossp");
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        builder.Entity<Country>(b =>
        {
            b.ToTable("Countries");
            b.ConfigureByConvention();
            b.HasIndex(x => x.Name);
        });

        builder.Entity<SingerTranslation>(b =>
        {
            b.ToTable("SingerTranslation");
            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(SingerConsts.MaxNameLength)
                .HasAnnotation("MinLength", SingerConsts.MinNameLength);
            b.ConfigureByConvention();
            b.HasIndex(x => x.Name).IsUnique();
        });
        
        builder.Entity<Singer>(b =>
        {
            b.ToTable("Singers");
            b.HasOne(x => x.Country)
                .WithMany()
                .HasForeignKey(x => x.CountryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
        });

    }

    protected override bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType)
    {
        if (typeof(IIsActive).IsAssignableFrom(typeof(TEntity)))
        {
            return true;
        }

        return base.ShouldFilterEntity<TEntity>(entityType);
    }

    protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
    {
        var expression = base.CreateFilterExpression<TEntity>();

        if (typeof(IIsActive).IsAssignableFrom(typeof(TEntity)))
        {
            Expression<Func<TEntity, bool>> isActiveFilter =
                e => !IsActiveFilterEnabled || EF.Property<bool>(e, nameof(IIsActive.IsActive));
            expression = expression == null
                ? isActiveFilter
                : CombineExpressions(expression, isActiveFilter);
        }

        return expression;
    }
}
