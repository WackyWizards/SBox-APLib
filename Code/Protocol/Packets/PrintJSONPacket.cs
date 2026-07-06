using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class PrintJSONPacket : APPacket
{
	public override string Command => "PrintJSON";
	
	[JsonPropertyName( "data" )]
	public PrintJSONPart[] Data { get; set; } = [];
}

#nullable enable

public sealed class PrintJSONPart
{
	[JsonPropertyName( "text" )]
	public string Text { get; set; } = string.Empty;
	
	[JsonPropertyName( "type" )]
	public string? Type { get; set; }
	
	[JsonPropertyName( "color" )]
	public string? Color { get; set; }
	
	[JsonPropertyName( "flags" )]
	public int? Flags { get; set; }
	
	[JsonPropertyName( "player" )]
	public int? Player { get; set; }
	
	[JsonPropertyName( "item" )]
	public long? Item { get; set; }
	
	[JsonPropertyName( "location" )]
	public long? Location { get; set; }
}
