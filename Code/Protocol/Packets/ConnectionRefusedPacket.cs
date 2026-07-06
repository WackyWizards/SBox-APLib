using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class ConnectionRefusedPacket : APPacket
{
	[JsonPropertyName( "cmd" )]
	public override string Command => "ConnectionRefused";
	
	[JsonPropertyName( "errors" )]
	public string[] Errors { get; set; }
}
