namespace Lam3a.Dto;

public class ServiceProviderDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public bool IsAvailable { get; set; }
    public decimal Rating { get; set; }
    public decimal AveragePrice { get; set; }
    public int ProjectsCount { get; set; }
}