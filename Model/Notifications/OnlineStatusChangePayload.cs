namespace StashCat.Notifications.Model;
public class OnlineStatusChangePayload
{
    public long userId { get; set; }
    public bool online { get; set; }
}