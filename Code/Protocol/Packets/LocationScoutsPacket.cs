using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class LocationScoutsPacket : APPacket
{
	public override string Command => "LocationScouts";
	
	[JsonPropertyName( "locations" )]
	public List<long> Locations { get; set; } = [];
	
	[JsonPropertyName( "create_as_hint" )]
	public int CreateAsHint { get; set; } = 0;
}
