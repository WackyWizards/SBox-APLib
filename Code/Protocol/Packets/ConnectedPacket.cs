using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class ConnectedPacket : APPacket
{
	public override string Command => "Connected";
	
	[JsonPropertyName( "team" )]
	public int Team { get; set; }
	
	[JsonPropertyName( "slot" )]
	public int Slot { get; set; }
	
	[JsonPropertyName( "players" )]
	public NetworkPlayer[] Players { get; set; }
	
	[JsonPropertyName( "checked_locations" )]
	public long[] CheckedLocations { get; set; }
	
	[JsonPropertyName( "missing_locations" )]
	public long[] MissingLocations { get; set; }
	
	[JsonPropertyName( "slot_info" )]
	public Dictionary<int, NetworkSlot> SlotInfo { get; set; }
	
	[JsonPropertyName( "slot_data" )]
	public Dictionary<string, object> SlotData { get; set; }
	
	[JsonPropertyName( "hint_points" )]
	public int HintPoints { get; set; }
}
