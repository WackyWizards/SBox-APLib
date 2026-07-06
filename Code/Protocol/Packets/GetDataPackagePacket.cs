using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class GetDataPackagePacket : APPacket
{
	[JsonPropertyName( "getdatapackage" )]
	public override string Command => "GetDataPackage";
	
	public List<string> Games { get; set; } = [];
}
