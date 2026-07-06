using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class LocationInfoPacket : APPacket
{
	public override string Command => "LocationInfo";
	
	[JsonPropertyName( "locations" )]
	public List<NetworkItem> Locations { get; set; } = [];
}
