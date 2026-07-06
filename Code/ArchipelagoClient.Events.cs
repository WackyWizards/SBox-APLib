using System;
using APLib.Protocol;
using APLib.Protocol.Packets;

namespace APLib;

public sealed partial class ArchipelagoClient
{
	public event Action OnConnected;
	
	public event Action OnDisconnected;
	
	public event Action<string> OnMessageReceived;
	
	public event Action<NetworkItem> ItemReceived;
	
	public event Action<PrintJSONPart> PrintReceived;
	
	public event Action<long> LocationChecked;
	
	public event Action<RoomInfoPacket> RoomInfoReceived
	{
		add => _dispatcher.RoomInfoReceived += value;
		remove => _dispatcher.RoomInfoReceived -= value;
	}
	
	public event Action<ConnectedPacket> ConnectedReceived
	{
		add => _dispatcher.ConnectedReceived += value;
		remove => _dispatcher.ConnectedReceived -= value;
	}
	
	public event Action<ConnectionRefusedPacket> ConnectionRefused
	{
		add => _dispatcher.ConnectionRefused += value;
		remove => _dispatcher.ConnectionRefused -= value;
	}
	
	public event Action<ReceivedItemsPacket> ItemsReceived
	{
		add => _dispatcher.ReceivedItems += value;
		remove => _dispatcher.ReceivedItems -= value;
	}
	
	public event Action<object> PrintJSONReceived
	{
		add => _dispatcher.PrintJSONReceived += value;
		remove => _dispatcher.PrintJSONReceived -= value;
	}
}
