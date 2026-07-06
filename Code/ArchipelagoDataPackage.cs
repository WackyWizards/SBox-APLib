using System.Collections.Generic;

namespace APLib;

public sealed class ArchipelagoDataPackage
{
	public int Version { get; internal set; }
	
	public IReadOnlyDictionary<string, ArchipelagoGameData> Games => InternalGames;
	
	internal readonly Dictionary<string, ArchipelagoGameData> InternalGames = [];
	
	public ArchipelagoGameData GetGame( string game )
	{
		InternalGames.TryGetValue( game, out var value );
		
		return value;
	}
	
	public bool TryGetGame( string game, out ArchipelagoGameData gameData )
	{
		return InternalGames.TryGetValue( game, out gameData );
	}
	
	internal void Clear()
	{
		Version = 0;
		InternalGames.Clear();
	}
}
