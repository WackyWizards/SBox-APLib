using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class PrintJSONPacket : APPacket
{
	[JsonPropertyName( "PrintJSON" )]
	public override string Command => "PrintJSON";
	
	[JsonPropertyName( "data" )]
	public PrintJSONPart[] Data { get; set; } = [];
}

public sealed class PrintJSONPart
{
	[JsonPropertyName( "text" )]
	public string Text { get; set; }
}
