namespace Lam3a.Dto;

public class ClientHomeDTO
{
    public List<VehicleOutputDTO> Vehicles { get; set; }
    public AddressDto Address { get; set; }
    public List<ServiceProviderDTO> ServiceProviders { get; set; }
    public List<ServiceDTO> Services { get; set; }
}