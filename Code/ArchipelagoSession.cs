using System.Collections.Generic;
using System.Linq;
using APLib.Protocol;
using Sandbox;

namespace APLib;

public sealed partial class ArchipelagoSession
{
	public int Team { get; internal set; }
	
	public int Slot { get; internal set; }
	
	public string SeedName { get; internal set; } = "";
	
	public bool PasswordRequired { get; internal set; }
	
	public int HintCost { get; internal set; }
	
	public int HintPoints { get; internal set; }
	
	public int LocationCheckPoints { get; internal set; }
	
	public double ServerTime { get; internal set; }
	
	public NetworkVersion ServerVersion { get; internal set; }
	
	public NetworkVersion GeneratorVersion { get; internal set; }
	
	[Hide]
	public IReadOnlyList<string> Games => InternalGames;
	
	[Hide]
	public IReadOnlyList<string> Tags => InternalTags;
	
	[Hide]
	public IReadOnlyList<NetworkPlayer> Players => InternalPlayers;
	
	[Hide]
	public IReadOnlySet<long> CheckedLocations => InternalCheckedLocations;
	
	[Hide]
	public IReadOnlySet<long> MissingLocations => InternalMissingLocations;
	
	[Hide]
	public IReadOnlyList<NetworkItem> ReceivedItems => InternalReceivedItems;
	
	[Hide]
	public IReadOnlyDictionary<long, SentItem> SentItems => InternalSentItems;
	
	[Hide]
	public IReadOnlyDictionary<string, object> SlotData => InternalSlotData;
	
	[Hide]
	public IReadOnlyDictionary<int, NetworkSlot> SlotInfo => InternalSlotInfo;
	
	[Hide]
	public IReadOnlyDictionary<string, int> Permissions => InternalPermissions;
	
	[Hide]
	public IReadOnlyDictionary<string, string> DatapackageChecksums => InternalDatapackageChecksums;
	
	[Title( "Games" )]
	internal readonly List<string> InternalGames = [];
	
	[Title( "Tags" )]
	internal readonly List<string> InternalTags = [];
	
	[Title( "Players" )]
	internal readonly List<NetworkPlayer> InternalPlayers = [];
	
	[Title( "Checked Locations" )]
	internal readonly HashSet<long> InternalCheckedLocations = [];
	
	[Title( "Missing Locations" )]
	internal readonly HashSet<long> InternalMissingLocations = [];
	
	[Title( "Received Items" )]
	internal readonly List<NetworkItem> InternalReceivedItems = [];
	
	[Title( "Sent Items" )]
	internal readonly Dictionary<long, SentItem> InternalSentItems = [];
	
	[Hide]
	internal readonly Dictionary<string, object> InternalSlotData = [];
	
	[Title( "Slot Info" )]
	internal readonly Dictionary<int, NetworkSlot> InternalSlotInfo = [];
	
	[Title( "Permissions" )]
	internal readonly Dictionary<string, int> InternalPermissions = [];
	
	[Title( "Datapackage Checksums" )]
	internal readonly Dictionary<string, string> InternalDatapackageChecksums = [];
	
	internal void Reset()
	{
		Team = 0;
		Slot = 0;
		SeedName = "";
		PasswordRequired = false;
		HintCost = 0;
		HintPoints = 0;
		LocationCheckPoints = 0;
		ServerTime = 0;
		ServerVersion = null;
		GeneratorVersion = null;
		InternalGames.Clear();
		InternalTags.Clear();
		InternalPlayers.Clear();
		InternalCheckedLocations.Clear();
		InternalMissingLocations.Clear();
		InternalReceivedItems.Clear();
		InternalSentItems.Clear();
		InternalSlotData.Clear();
		InternalSlotInfo.Clear();
		InternalPermissions.Clear();
		InternalDatapackageChecksums.Clear();
	}
}
