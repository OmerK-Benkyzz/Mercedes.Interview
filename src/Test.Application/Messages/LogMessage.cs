namespace Test.Application.Messages;

public class LogMessage
{
    public string Request { get; set; }
    public string Response { get; set; }
    public string DeviceInfo { get; set; }
    public string IpAddress { get; set; }
    public int StatusCode { get; set; }
}