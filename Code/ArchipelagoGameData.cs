using System.Collections.Generic;

namespace APLib;

public sealed class ArchipelagoGameData
{
	public string Name { get; internal set; }
	
	public IReadOnlyDictionary<long, string> Items => InternalItems;
	
	public IReadOnlyDictionary<long, string> Locations => InternalLocations;
	
	internal readonly Dictionary<long, string> InternalItems = [];
	
	internal readonly Dictionary<long, string> InternalLocations = [];
	
	public string GetItemName( long id )
	{
		return InternalItems.TryGetValue( id, out var name ) ? name : $"Item {id}";
	}
	
	public string GetLocationName( long id )
	{
		return InternalLocations.TryGetValue( id, out var name ) ? name : $"Location {id}";
	}
}
