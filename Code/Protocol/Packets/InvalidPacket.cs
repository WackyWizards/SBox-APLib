using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public class InvalidPacket : APPacket
{
	[JsonPropertyName( "cmd" )]
	public override string Command => "Invalid";
	
	[JsonPropertyName( "type" )]
	public string Type { get; set; }
	
	[JsonPropertyName( "original_cmd" )]
	public string OriginalCommand { get; set; }
	
	[JsonPropertyName( "text" )]
	public string Text { get; set; }
}
