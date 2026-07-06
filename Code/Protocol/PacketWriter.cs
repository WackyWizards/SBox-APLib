using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace APLib.Protocol;

public static class PacketWriter
{
	private static readonly JsonSerializerOptions _serializerOptions = new();
	
	public static string Write( APPacket packet )
	{
		return JsonSerializer.Serialize( new object[]
		{
			packet
		}, _serializerOptions );
	}
	
	public static string Write( IEnumerable<APPacket> packets )
	{
		return JsonSerializer.Serialize( packets.Cast<object>().ToArray(), _serializerOptions );
	}
}
