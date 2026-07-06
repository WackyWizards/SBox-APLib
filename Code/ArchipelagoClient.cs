using Sandbox;
using Sandbox.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using APLib.Protocol;
using APLib.Protocol.Packets;

namespace APLib;

public sealed partial class ArchipelagoClient : Component, IDisposable
{
	[Property]
	[Category( "Connection" )]
	public string Game { get; set; }
	
	[Property]
	[Category( "Connection" )]
	private string Host { get; set; } = "localhost";
	
	[Property]
	[Category( "Connection" )]
	private int Port { get; set; } = 38281;
	
	[Property]
	[Category( "Connection" )]
	public string SlotName { get; set; }
	
	/// <summary>
	/// Leave empty for no password.
	/// </summary>
	[Property]
	[Category( "Connection" )]
	public string Password { get; set; }
	
	[Property]
	[Category( "Connection" )]
	public bool UseSecureConnection { get; set; } = true;
	
	[Property]
	public bool Debug { get; set; } = false;
	
	[Property, ReadOnly]
	public bool IsConnected { get; private set; }
	
	[Property, ReadOnly]
	public bool IsAuthenticated { get; private set; }
	
	[Property, ReadOnly]
	public bool IsReady { get; private set; }
	
	[Property, JsonIgnore]
	public ArchipelagoSession Session { get; } = new();
	
	public ArchipelagoDataPackage DataPackage { get; } = new();
	
	private bool _connectedReceived;
	private bool _dataPackageLoaded;
	
	private readonly List<NetworkItem> _pendingItems = [];
	
	private WebSocket _socket;
	private readonly PacketDispatcher _dispatcher = new();
	
	private static readonly Logger Log = new( "APClient" );
	
	private string ConnectionUrl
	{
		get
		{
			var protocol = UseSecureConnection ? "wss" : "ws";
			
			return $"{protocol}://{Host}:{Port}";
		}
	}
	
	protected override void OnAwake()
	{
		RoomInfoReceived += OnRoomInfo;
		ConnectedReceived += OnConnectedPacket;
		ItemsReceived += OnItemsReceived;
		DataPackageReceived += OnDataPackage;
	}
	
	protected override void OnDestroy()
	{
		Dispose();
		base.OnDestroy();
	}
	
	public async Task Connect()
	{
		if ( IsConnected )
		{
			return;
		}
		
		_socket = new WebSocket();
		_socket.OnMessageReceived += MessageReceived;
		Log.Info( $"Connecting to Archipelago server at {ConnectionUrl}" );
		await _socket.Connect( ConnectionUrl );
		IsConnected = true;
		OnConnected?.Invoke();
		Log.Info( $"Connected to Archipelago server at {ConnectionUrl}" );
	}
	
	public Task Disconnect()
	{
		if ( !IsConnected )
		{
			return Task.CompletedTask;
		}
		
		IsConnected = false;
		IsAuthenticated = false;
		IsReady = false;
		_connectedReceived = false;
		_dataPackageLoaded = false;
		_pendingItems.Clear();
		Session.Reset();
		DataPackage.InternalGames.Clear();
		
		if ( _socket is not null )
		{
			_socket.OnMessageReceived -= MessageReceived;
			_socket.Dispose();
			_socket = null;
		}
		
		OnDisconnected?.Invoke();
		Log.Info( "Disconnected from Archipelago server" );
		
		return Task.CompletedTask;
	}
	
	public async Task Login()
	{
		if ( IsAuthenticated )
		{
			return;
		}
		
		var tcs = new TaskCompletionSource();
		
		void Handler()
		{
			Ready -= Handler;
			tcs.SetResult();
		}
		
		Ready += Handler;
		
		await SendAsync( new ConnectPacket
		{
			Game = Game,
			Name = SlotName,
			Password = Password,
			Id = Guid.NewGuid(),
			Tags = [],
			Version = new NetworkVersion
			{
				Major = 0, Minor = 6, Build = 7
			}
		} );
		
		await tcs.Task;
		Log.Info( $"Logged in to Archipelago game {Game}" );
	}
	
	private void MessageReceived( string json )
	{
		DebugLog( $"Received: {json}" );
		OnMessageReceived?.Invoke( json );
		
		foreach ( var packet in PacketReader.Read( json ) )
		{
			if ( packet.Command == "PrintJSON" )
			{
				var printpkt = (PrintJSONPacket)packet;
				
				foreach ( var part in printpkt.Data )
				{
					PrintReceived?.Invoke( part );
				}
			}
			
			_dispatcher.Dispatch( packet );
		}
	}
	
	public async Task SendAsync( APPacket packet )
	{
		if ( !IsConnected )
		{
			throw new InvalidOperationException( "Cannot send packets while disconnected." );
		}
		
		var json = PacketWriter.Write( packet );
		DebugLog( $"Sending: {json}" );
		await _socket.Send( json );
	}
	
	public Task Sync()
	{
		return SendAsync( new SyncPacket() );
	}
	
	public Task CheckLocation( long id )
	{
		LocationChecked?.Invoke( id );
		
		return SendAsync( new LocationChecksPacket
		{
			Locations = [id]
		} );
	}
	
	public Task CheckLocations( IEnumerable<long> ids )
	{
		var locations = ids.ToArray();
		
		foreach ( var id in locations )
		{
			LocationChecked?.Invoke( id );
		}
		
		return SendAsync( new LocationChecksPacket
		{
			Locations = locations
		} );
	}
	
	public Task CheckLocation( NetworkItem item )
	{
		return CheckLocation( item.Location );
	}
	
	public Task CheckLocations( params long[] locations )
	{
		return CheckLocations( (IEnumerable<long>)locations );
	}
	
	public Task SetGoalCompleted()
	{
		return SendAsync( new StatusUpdatePacket
		{
			Status = 30
		} );
	}
	
	private void OnRoomInfo( RoomInfoPacket packet )
	{
		Session.ServerVersion = packet.Version;
		Session.GeneratorVersion = packet.GeneratorVersion;
		Session.SeedName = packet.SeedName;
		Session.PasswordRequired = packet.PasswordRequired;
		Session.HintCost = packet.HintCost;
		Session.LocationCheckPoints = packet.LocationCheckPoints;
		Session.ServerTime = packet.Time;
		Session.InternalGames.Clear();
		
		if ( packet.Games is not null )
		{
			Session.InternalGames.AddRange( packet.Games );
		}
		
		Session.InternalTags.Clear();
		
		if ( packet.Tags is not null )
		{
			Session.InternalTags.AddRange( packet.Tags );
		}
		
		Session.InternalPermissions.Clear();
		
		if ( packet.Permissions is not null )
		{
			foreach ( var pair in packet.Permissions )
			{
				Session.InternalPermissions[pair.Key] = pair.Value;
			}
		}
		
		Session.InternalDatapackageChecksums.Clear();
		
		if ( packet.DatapackageChecksums is not null )
		{
			foreach ( var pair in packet.DatapackageChecksums )
			{
				Session.InternalDatapackageChecksums[pair.Key] = pair.Value;
			}
		}
	}
	
	private void OnConnectedPacket( ConnectedPacket packet )
	{
		IsAuthenticated = true;
		Session.Team = packet.Team;
		Session.Slot = packet.Slot;
		Session.HintPoints = packet.HintPoints;
		Session.InternalPlayers.Clear();
		
		if ( packet.Players is not null )
		{
			Session.InternalPlayers.AddRange( packet.Players );
		}
		
		Session.InternalCheckedLocations.Clear();
		
		if ( packet.CheckedLocations is not null )
		{
			foreach ( var location in packet.CheckedLocations )
			{
				Session.InternalCheckedLocations.Add( location );
			}
		}
		
		Session.InternalMissingLocations.Clear();
		
		if ( packet.MissingLocations is not null )
		{
			foreach ( var location in packet.MissingLocations )
			{
				Session.InternalMissingLocations.Add( location );
			}
		}
		
		Session.InternalSlotInfo.Clear();
		
		if ( packet.SlotInfo is not null )
		{
			foreach ( var pair in packet.SlotInfo )
			{
				Session.InternalSlotInfo[pair.Key] = pair.Value;
			}
		}
		
		Session.InternalSlotData.Clear();
		
		if ( packet.SlotData is not null )
		{
			foreach ( var pair in packet.SlotData )
			{
				Session.InternalSlotData[pair.Key] = pair.Value;
			}
		}
		
		_connectedReceived = true;
		_ = RequestDataPackage( Session.Games.ToArray() );
		TryBecomeReady();
	}
	
	private void OnItemsReceived( ReceivedItemsPacket packet )
	{
		if ( packet.Index != Session.ReceivedItems.Count )
		{
			Log.Warning( "ReceivedItems desync detected." );
			_ = Sync();
			
			return;
		}
		
		foreach ( var item in packet.Items )
		{
			Session.InternalReceivedItems.Add( item );
			
			if ( IsReady )
			{
				ItemReceived?.Invoke( item );
			}
			else
			{
				_pendingItems.Add( item );
			}
		}
	}
	
	private void OnDataPackage( DataPackagePacket packet )
	{
		foreach ( var pair in packet.Data.Games )
		{
			var game = new ArchipelagoGameData
			{
				Name = pair.Key
			};
			
			foreach ( var item in pair.Value.ItemNameToId )
			{
				game.InternalItems[item.Value] = item.Key;
			}
			
			foreach ( var location in pair.Value.LocationNameToId )
			{
				game.InternalLocations[location.Value] = location.Key;
			}
			
			DataPackage.InternalGames[pair.Key] = game;
		}
		
		_dataPackageLoaded = true;
		Log.Info( $"Loaded {DataPackage.Games.Count} games." );
		TryBecomeReady();
	}
	
	private void TryBecomeReady()
	{
		if ( IsReady )
		{
			return;
		}
		
		if ( !_connectedReceived || !_dataPackageLoaded )
		{
			return;
		}
		
		IsReady = true;
		
		foreach ( var item in _pendingItems )
		{
			ItemReceived?.Invoke( item );
		}
		
		_pendingItems.Clear();
		Ready?.Invoke();
	}
	
	public Task RequestDataPackage( params string[] games )
	{
		return SendAsync( new GetDataPackagePacket
		{
			Games = games.ToList()
		} );
	}
	
	public string GetPlayerName( int slot )
	{
		return Session.GetPlayerName( slot );
	}
	
	public string GetItemName( long itemId, int slot )
	{
		var gameName = Session.GetGame( slot );
		var game = DataPackage.GetGame( gameName );
		
		if ( game is null )
		{
			Log.Warning( $"Game '{gameName}' not found in datapackage." );
			Log.Warning( $"Games: {DataPackage.Games.Count}" );
			
			return itemId.ToString();
		}
		
		var name = game.GetItemName( itemId );
		
		if ( name is null )
		{
			Log.Warning( $"Item {itemId} not found in '{game.Name}'." );
			Log.Warning( $"Game contains {game.Items.Count} items." );
		}
		
		return name ?? itemId.ToString();
	}
	
	public string GetLocationName( long locationId, int slot )
	{
		var gameName = Session.GetGame( slot );
		var game = DataPackage.GetGame( gameName );
		
		if ( game is null )
		{
			Log.Warning( $"Game '{gameName}' not found in datapackage." );
			Log.Warning( $"Games: {DataPackage.Games.Count}" );
			
			return locationId.ToString();
		}
		
		var name = game.GetLocationName( locationId );
		
		if ( name is null )
		{
			Log.Warning( $"Location {locationId} not found in '{game.Name}'." );
			Log.Warning( $"Game contains {game.Locations.Count} locations." );
		}
		
		return name ?? locationId.ToString();
	}
	
	private void DebugLog( string message, LogType type = LogType.Info )
	{
		if ( Debug )
		{
			switch ( type )
			{
				case LogType.Info:
					Log.Info( message );
					
					break;
				case LogType.Warning:
					Log.Warning( message );
					
					break;
				case LogType.Error:
					Log.Error( message );
					
					break;
				case LogType.Trace:
					Log.Trace( message );
					
					break;
				default:
					throw new ArgumentOutOfRangeException( nameof(type), type, null );
			}
		}
	}
	
	public enum LogType
	{
		Info,
		Warning,
		Error,
		Trace
	}
	
	public void Dispose()
	{
		if ( IsConnected )
		{
			Disconnect().GetAwaiter().GetResult();
		}
	}
}
