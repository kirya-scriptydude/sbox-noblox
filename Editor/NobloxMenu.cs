using Editor;

[EditorApp( "Example App", "pregnant_woman", "Inspect Butts" )]
public class NobloxMenu : Window
{
	public NobloxMenu() {
		WindowTitle = "NOBLOX v1.0";
		Size = new Vector2(500, 500);
        
        HasMaximizeButton = false;
        MaximumSize = new Vector2(500, 500);
	}
}