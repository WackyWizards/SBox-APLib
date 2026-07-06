namespace APLib.Protocol.Packets;

public sealed class SyncPacket : APPacket
{
	public override string Command => "Sync";
}
