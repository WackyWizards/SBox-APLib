using System.Collections.Generic;
using System.Text.Json;

namespace APLib.Protocol;

public static class PacketReader
{
	public static IEnumerable<APPacket> Read( string json )
	{
		using var doc = JsonDocument.Parse( json );
		
		foreach ( var element in doc.RootElement.EnumerateArray() )
		{
			var cmd = element.GetProperty( "cmd" ).GetString();
			var type = PacketRegistry.Get( cmd );
			var packet = (APPacket)element.Deserialize( type );
			
			yield return packet;
		}
	}
}
