using System.Text.Json.Serialization;

namespace APLib.Protocol;

// Can't be abstract because of deserialization
public class APPacket
{
	// Make sure to override this in your packet!!!
	[JsonPropertyName( "cmd" )]
	public virtual string Command { get; private set; }
}
