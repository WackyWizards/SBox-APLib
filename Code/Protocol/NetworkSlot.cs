using System.Text.Json.Serialization;

namespace APLib.Protocol;

public sealed class NetworkSlot
{
	[JsonPropertyName( "name" )]
	public string Name { get; set; }
	
	[JsonPropertyName( "game" )]
	public string Game { get; set; }
	
	[JsonPropertyName( "type" )]
	public int Type { get; set; }
	
	[JsonPropertyName( "group_members" )]
	public int[] GroupMembers { get; set; } = [];
}
