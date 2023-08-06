namespace Test.Domain.Entities;

public class TransactionLog : BaseEntity
{
    public string IpAddress { get; set; }
    public string DeviceInfo { get; set; }
    public byte[] Request { get; set; }
    public byte[] Response { get; set; }
    public int StatusCode { get; set; }
}