using System.Text.Json.Serialization;

namespace APLib.Protocol.Packets;

public sealed class SyncPacket : APPacket
{
	[JsonPropertyName( "cmd" )]
	public override string Command => "Sync";
}
