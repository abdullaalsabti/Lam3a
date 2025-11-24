using System.Text.Json.Serialization;
namespace Lam3a.Data.Entities;

public class VehicleModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int BrandId { get; set; }
    [JsonIgnore]
    public VehicleBrand Brand { get; set; }
}
