using System.Text.Json.Serialization;

namespace APLib.Protocol;

public sealed class NetworkVersion
{
	[JsonPropertyName( "major" )]
	public int Major { get; set; }
	
	[JsonPropertyName( "minor" )]
	public int Minor { get; set; }
	
	[JsonPropertyName( "build" )]
	public int Build { get; set; }
	
	[JsonPropertyName( "class" )]
	public string Class { get; set; } = "Version";
}
