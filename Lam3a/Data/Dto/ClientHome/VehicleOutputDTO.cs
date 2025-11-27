using Lam3a.Utils;

namespace Lam3a.Dto;

public class VehicleOutputDTO
{
    public String PlateNumber { get; set; }
    public String Model { get; set; }
    public String Brand  { get; set; }
    public VehicleColor Color { get; set; }
    public CarType CarType { get; set; }
}