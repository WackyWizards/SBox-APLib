using System;
using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class ConnectPacket : APPacket
{
	public override string Command => "Connect";
	
	[JsonPropertyName( "game" )]
	public string Game { get; set; }
	
	[JsonPropertyName( "name" )]
	public string Name { get; set; }
	
	[JsonPropertyName( "password" )]
	public string Password { get; set; }
	
	[JsonPropertyName( "uuid" )]
	public Guid Id { get; set; }
	
	[JsonPropertyName( "version" )]
	public NetworkVersion Version { get; set; }
	
	[JsonPropertyName( "tags" )]
	public string[] Tags { get; set; }
	
	[JsonPropertyName( "items_handling" )]
	public int ItemsHandling { get; set; } = 7;
	
	[JsonPropertyName( "slot_data" )]
	public bool SlotData { get; set; } = true;
}
