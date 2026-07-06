using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class RoomUpdatePacket : APPacket
{
	[JsonPropertyName( "cmd" )]
	public override string Command => "RoomUpdate";
	
	[JsonPropertyName( "checked_locations" )]
	public long[] CheckedLocations { get; set; }
	
	[JsonPropertyName( "players" )]
	public NetworkPlayer[] Players { get; set; }
}
