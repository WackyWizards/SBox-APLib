using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class GetDataPackagePacket : APPacket
{
	public override string Command => "GetDataPackage";
	
	[JsonPropertyName( "games" )]
	public List<string> Games { get; set; } = [];
}
