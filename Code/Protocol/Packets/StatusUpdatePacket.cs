using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class StatusUpdatePacket : APPacket
{
	[JsonPropertyName( "cmd" )]
	public override string Command => "StatusUpdate";
	
	[JsonPropertyName( "status" )]
	public int Status { get; set; }
}
