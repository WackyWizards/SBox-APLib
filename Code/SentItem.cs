using APLib.Protocol;

namespace APLib;

public sealed class SentItem
{
	public long Location { get; init; }
	
	public NetworkItem Item { get; init; }
}
