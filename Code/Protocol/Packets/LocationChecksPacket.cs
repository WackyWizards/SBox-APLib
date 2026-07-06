using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class LocationChecksPacket : APPacket
{
	public override string Command => "LocationChecks";
	
	[JsonPropertyName( "locations" )]
	public long[] Locations { get; set; }
}
