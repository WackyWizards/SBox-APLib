using System.Text.Json.Serialization;

namespace APLib.Protocol;

public sealed class NetworkPlayer
{
	[JsonPropertyName( "team" )]
	public int Team { get; set; }
	
	[JsonPropertyName( "slot" )]
	public int Slot { get; set; }
	
	[JsonPropertyName( "alias" )]
	public string Alias { get; set; }
	
	[JsonPropertyName( "name" )]
	public string Name { get; set; }
	
	public override string ToString()
	{
		return Alias;
	}
}
