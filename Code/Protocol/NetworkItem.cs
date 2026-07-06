using System.Text.Json.Serialization;

namespace APLib.Protocol;

public sealed class NetworkItem
{
	[JsonPropertyName( "item" )]
	public long Item { get; set; }
	
	[JsonPropertyName( "location" )]
	public long Location { get; set; }
	
	[JsonPropertyName( "player" )]
	public int Player { get; set; }
	
	[JsonPropertyName( "flags" )]
	public int Flags { get; set; }
}
