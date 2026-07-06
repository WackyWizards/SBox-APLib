using System;
using System.Linq;
using System.Collections.Generic;

namespace APLib.Protocol;

public static class PacketRegistry
{
	private static Dictionary<string, Type> Packets => field ??= Build();
	
	private static Dictionary<string, Type> Build()
	{
		var dict = new Dictionary<string, Type>();
		var types = TypeLibrary.GetTypes<APPacket>().ToArray();
		
		foreach ( var type in types )
		{
			if ( type.IsAbstract )
			{
				continue;
			}
			
			var packet = TypeLibrary.Create<APPacket>( type.ClassName );
			
			if ( !string.IsNullOrEmpty( packet.Command ) )
			{
				dict[packet.Command] = type.TargetType;
			}
		}
		
		return dict;
	}
	
	public static Type Get( string cmd )
	{
		if ( !Packets.TryGetValue( cmd, out var type ) )
		{
			throw new InvalidOperationException( $"Unknown packet type '{cmd}'." );
		}
		
		return type;
	}
}
