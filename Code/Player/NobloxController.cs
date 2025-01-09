using Sandbox;

[Title("Noblox - Controller"), Group("Noblox"), Description("Default character controller for Noblox.")]
public class NobloxController : Component {
	[Property, Group("Dependencies")]
	public Rigidbody Rigidbody {get; set;}
}
