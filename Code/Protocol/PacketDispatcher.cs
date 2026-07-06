using System;
using APLib.Protocol.Packets;

namespace APLib.Protocol;

public sealed class PacketDispatcher
{
	public event Action<RoomInfoPacket> RoomInfoReceived;
	
	public event Action<ConnectedPacket> ConnectedReceived;
	
	public event Action<ConnectionRefusedPacket> ConnectionRefused;
	
	public event Action<ReceivedItemsPacket> ReceivedItems;
	
	public void Dispatch( APPacket packet )
	{
		switch ( packet )
		{
			case RoomInfoPacket room:
				RoomInfoReceived?.Invoke( room );
				
				break;
			case ConnectedPacket connected:
				ConnectedReceived?.Invoke( connected );
				
				break;
			case ConnectionRefusedPacket refused:
				ConnectionRefused?.Invoke( refused );
				
				break;
			case ReceivedItemsPacket receivedItems:
				ReceivedItems?.Invoke( receivedItems );
				
				break;
		}
	}
}
