namespace Webion.Licensing.Repositories;

public sealed class LicenseConfigsRepository : IDisposable
{
    private readonly LicensingDbContext _ctx = new();

    public async Task<IEnumerable<LicenseConfig>> ListAllAsync(CancellationToken cancellationToken)
    {
        var results = await _ctx.LicenseConfigs
            .Include(c => c.LicenseProperties)
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return results.Select(r => r.ToDomain());
    }

    public async Task<LicenseConfig?> GetAsync(string name, CancellationToken cancellationToken)
    {
        var config = await _ctx.LicenseConfigs
            .Include(c => c.LicenseProperties)
            .Where(c => c.Name == name)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        return config?.ToDomain();
    }

    public async Task AddAsync(LicenseConfig config, CancellationToken cancellationToken)
    {
        var exists = await _ctx.LicenseConfigs
            .Where(l => l.Name == config.Name)
            .AsNoTracking()
            .AnyAsync();

        if (exists)
            throw new InvalidOperationException("Exists");

        var dbo = new LicenseConfigDbo
        {
            Name = config.Name,
            PrivateKey = config.PrivateKey,
            PublicKey = config.PublicKey,
            AppCode = config.AppCode,
            Duration = config.Duration,
            MatchHardware = config.MatchHardware,
            LicenseProperties = config.Properties
                .Select(p => p.ToDbo())
                .ToList()
        };

        _ctx.LicenseConfigs.Add(dbo);
        await _ctx.SaveChangesAsync();
    }

    public async Task UpdateAsync(string name, LicenseConfig config, CancellationToken cancellationToken)
    {
        var existing = await _ctx.LicenseConfigs
            .Include(c => c.LicenseProperties)
            .Where(c => c.Name == name)
            .FirstOrDefaultAsync(cancellationToken);

        if (existing is null)
            throw new InvalidOperationException("not found");

        existing.Name = config.Name;
        existing.PrivateKey = config.PrivateKey;
        existing.PublicKey = config.PublicKey;
        existing.AppCode = config.AppCode;
        existing.Duration = config.Duration;
        existing.MatchHardware = config.MatchHardware;
        await _ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(string name, CancellationToken cancellationToken)
    {
        var existing = await _ctx.LicenseConfigs.FindAsync(name, cancellationToken);
        if(existing is null)
            throw new InvalidOperationException("not found");

        _ctx.LicenseConfigs.Remove(existing);
        await _ctx.SaveChangesAsync();
    }


    public void Dispose()
    {
        _ctx.Dispose();
    }
}