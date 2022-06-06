using Newtonsoft.Json;
namespace back_labprog.Contracts.Frontend;

public class UploadData
{
    [JsonProperty("payload")]
    public string Payload { get; set; }
}