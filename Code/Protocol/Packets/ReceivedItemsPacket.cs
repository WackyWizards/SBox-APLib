using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class ReceivedItemsPacket : APPacket
{
	[JsonPropertyName( "cmd" )]
	public override string Command => "ReceivedItems";
	
	[JsonPropertyName( "items" )]
	public NetworkItem[] Items { get; set; }
	
	[JsonPropertyName( "index" )]
	public int Index { get; set; }
}
