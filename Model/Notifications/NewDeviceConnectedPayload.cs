namespace StashCat.Notifications.Model;

public class NewDeviceConnectedPayload
{
    public string? device_id { get; set; }
    public string? ip_address { get; set; }
}