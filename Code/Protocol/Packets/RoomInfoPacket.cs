using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class RoomInfoPacket : APPacket
{
	public override string Command => "RoomInfo";
	
	[JsonPropertyName( "version" )]
	public NetworkVersion Version { get; set; }
	
	[JsonPropertyName( "generator_version" )]
	public NetworkVersion GeneratorVersion { get; set; }
	
	[JsonPropertyName( "tags" )]
	public string[] Tags { get; set; }
	
	[JsonPropertyName( "password" )]
	public bool PasswordRequired { get; set; }
	
	[JsonPropertyName( "permissions" )]
	public Dictionary<string, int> Permissions { get; set; }
	
	[JsonPropertyName( "hint_cost" )]
	public int HintCost { get; set; }
	
	[JsonPropertyName( "location_check_points" )]
	public int LocationCheckPoints { get; set; }
	
	[JsonPropertyName( "games" )]
	public string[] Games { get; set; }
	
	[JsonPropertyName( "datapackage_checksums" )]
	public Dictionary<string, string> DatapackageChecksums { get; set; }
	
	[JsonPropertyName( "seed_name" )]
	public string SeedName { get; set; }
	
	[JsonPropertyName( "time" )]
	public double Time { get; set; }
}
