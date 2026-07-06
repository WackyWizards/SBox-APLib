using System.Linq;
using APLib.Protocol;

namespace APLib;

public sealed partial class ArchipelagoSession
{
	public bool HasCheckedLocation( long id )
	{
		return InternalCheckedLocations.Contains( id );
	}
	
	public bool IsMissingLocation( long id )
	{
		return InternalMissingLocations.Contains( id );
	}
	
	public bool HasReceivedItem( long itemId )
	{
		return InternalReceivedItems.Any( x => x.Item == itemId );
	}
	
	public bool IsCurrentPlayer( int slot )
	{
		return Slot == slot;
	}
	
	public NetworkPlayer GetPlayer( int slot )
	{
		return InternalPlayers.FirstOrDefault( x => x.Slot == slot );
	}
	
	public string GetPlayerName( int slot )
	{
		return GetPlayer( slot )?.Name ?? $"Player {slot}";
	}
	
	public NetworkPlayer CurrentPlayer()
	{
		return GetPlayer( Slot );
	}
	
	public NetworkItem LastReceivedItem()
	{
		return InternalReceivedItems.LastOrDefault();
	}
	
	public NetworkSlot GetSlotInfo( int slot )
	{
		InternalSlotInfo.TryGetValue( slot, out var info );
		
		return info;
	}
	
	public string GetGame( int slot )
	{
		return GetSlotInfo( slot )?.Game;
	}
	
	public NetworkSlot CurrentSlotInfo()
	{
		return GetSlotInfo( Slot );
	}
}
