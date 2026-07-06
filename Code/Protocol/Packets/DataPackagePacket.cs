using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class DataPackagePacket : APPacket
{
	[JsonPropertyName( "datapackage" )]
	public override string Command => "DataPackage";
	
	[JsonPropertyName( "data" )]
	public DataPackageObject Data { get; set; }
}

public sealed class DataPackageObject
{
	[JsonPropertyName( "version" )]
	public int Version { get; set; }
	
	[JsonPropertyName( "games" )]
	public Dictionary<string, GameDataPackage> Games { get; set; }
}

public sealed class GameDataPackage
{
	[JsonPropertyName( "checksum" )]
	public string Checksum { get; set; }
	
	[JsonPropertyName( "item_name_to_id" )]
	public Dictionary<string, long> ItemNameToId { get; set; }
	
	[JsonPropertyName( "location_name_to_id" )]
	public Dictionary<string, long> LocationNameToId { get; set; }
}
