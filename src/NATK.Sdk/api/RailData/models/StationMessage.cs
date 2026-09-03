using System.Text.Json.Serialization;

internal sealed class StationMessage
{
    [JsonPropertyName("MSG_TYPE")]
    public string? MsgType { get; set; }

    [JsonPropertyName("MSG_TEXT")]
    public string? MsgText { get; set; }

    [JsonPropertyName("MSG_RICHTEXT")]
    public string? MsgRichText { get; set; }

    [JsonPropertyName("MSG_PUBDATE")]
    public string? MsgPubDate { get; set; }

    [JsonPropertyName("MSG_ID")]
    public string? MsgId { get; set; }

    [JsonPropertyName("MSG_AGENCY")]
    public string? MsgAgency { get; set; }

    [JsonPropertyName("MSG_SOURCE")]
    public string? MsgSource { get; set; }

    [JsonPropertyName("MSG_STATION_SCOPE")]
    public string? MsgStationScope { get; set; }

    [JsonPropertyName("MSG_LINE_SCOPE")]
    public string? MsgLineScope { get; set; }

    [JsonPropertyName("MSG_PUBDATE_UTC")]
    public string? MsgPubDateUtc { get; set; }

    [JsonPropertyName("MSG_URL")]
    public string? MsgUrl { get; set; }
}

public sealed class StationMessageModel
{
    public string? MsgType { get; set; }
    public string? MsgText { get; set; }
    public string? MsgRichText { get; set; }
    public string? MsgPubDate { get; set; }
    public string? MsgId { get; set; }
    public string? MsgAgency { get; set; }
    public string? MsgSource { get; set; }
    public string? MsgStationScope { get; set; }
    public string? MsgLineScope { get; set; }
    public string? MsgPubDateUtc { get; set; }
    public string? MsgUrl { get; set; }
}
