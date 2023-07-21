public class AccountParams : IAccountParams
{
    public const string AddressTemplate = "https://{0}/game/json?h={1}";
    public string RequestsAddress => string.Format(AddressTemplate, Domain, UserKey);

    public string UserKey { get; set; }
    public string Domain { get; set; }
    public string region { get; set; }
    public string origin { get; set; }
    public string instanceId { get; set; }
    public string metricsUvId { get; set; }
    public string sid { get; set; }
    public string cid { get; set; }
    public string ig_conv_last_site { get; set; }
    public string _uetsid { get; set; }
    public string _uetvid { get; set; }
}