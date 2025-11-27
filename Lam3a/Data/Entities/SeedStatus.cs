namespace Lam3a.Data.Entities;

public class SeedStatus
{
    public int Id { get; set; }

    // Unique name of the seed (ex: "InitialSeed", "DemoData", "AdminUserSeed")
    public string Key { get; set; } = default!;

    // Whether it finished successfully
    public bool Completed { get; set; } = true;

    // When it finished
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}