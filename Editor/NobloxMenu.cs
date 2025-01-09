using Editor;

[EditorApp( "Noblox", "nature_people", " Import .rbxl's " )]
public class NobloxMenu : Window {
	public NobloxMenu() {
		WindowTitle = "NOBLOX v1.0";
		Size = new Vector2(500, 500);
        
        HasMaximizeButton = false;
        MaximumSize = new Vector2(500, 500);
        MinimumSize = new Vector2(500, 500);

        //  style and stuff
        Layout = Layout.Column();
        Layout.Margin = 4;
        Layout.Spacing = 4;
        SetStyles( "font-family: 'Comic Sans MS'; background-color:rgb(218, 218, 218); color: black;" );
        
        var label = Layout.Add(new Label("R O B L O X - I M P O R T E R", this));
        label.Position = new Vector2(180, 15);
        label.Size = new Vector2(150, 40);
        label.SetStyles("color: grey;");

        var title = Layout.Add(new Label("NOBLOX", this));
        title.Size = new Vector2(200, 50);
        title.Position = new Vector2(170, 52);
        title.SetStyles("font-weight: 1000; font-size: 40px;");

        var importButton = Layout.Add(new Label("IMPORT", this));
        importButton.SetStyles("background-color: rgb(0, 255, 0); font-size: 32px;");
        importButton.Alignment = Sandbox.TextFlag.Center;
        importButton.Position = new Vector2(165, 400);
        importButton.Size = new Vector2(200, 50);
        
	}
}