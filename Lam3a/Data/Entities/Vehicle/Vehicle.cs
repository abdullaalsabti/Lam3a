using Lam3a.Utils;

namespace Lam3a.Data.Entities;


public class Vehicle
{
    //id
    public Vehicle(string plateNumber, int brandId, int modelId, VehicleColor color , CarType carType ,Guid clientId )
    {
        PlateNumber = plateNumber;
        BrandId = brandId;
        ModelId = modelId;
        Color = color;
        CarType = carType;
        ClientId = clientId;
    }

    public String PlateNumber { get; set; }
    //enums
    public VehicleColor Color { get; set; }
    public CarType CarType { get; set; }
    
    // Foreign Key
    public Guid ClientId { get; set; }
    public int BrandId { get; set; }
    public int ModelId { get; set; }

    // Navigation
    public Client Client { get; set; }
    public VehicleBrand Brand { get; set; }
    public VehicleModel Model { get; set; }
}