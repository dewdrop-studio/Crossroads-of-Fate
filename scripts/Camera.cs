using Godot;

public partial class Camera: Camera2D{

    [Export]
	public float ZoomSpeed = 0.5f;
	[Export]
	public float MaxZoom = 1.3f;
	[Export]
	public float MinZoom = 0.7f;

	[Export]
	public float UIOffset = 10;

    
    private Control UI;
    private CanvasLayer UICanvasLayer;

    public override void _Ready()
    {
        PlayerOverworld player = GetNode<PlayerOverworld>("/root/World/Player");
        player.PositionChanged += OnPlayerPositionChanged;
        
        UI = GetNode<Control>("WorldUi");
        UICanvasLayer = UI.GetNode<CanvasLayer>("CanvasLayer");

        UICanvasLayer.Offset = new Vector2(UIOffset, UIOffset);
        UICanvasLayer.CustomViewport = GetViewport();
        UICanvasLayer.FollowViewportEnabled = true;
        UICanvasLayer.FollowViewportScale = 0.9f;

    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        float scroll = Input.GetAxis("zoom_out", "zoom_in");
		if (scroll != 0)
		{
			Vector2 zoom = Zoom;
			Vector2 mod = new Vector2(scroll, scroll) * ZoomSpeed * (float) delta;

			zoom += mod;
			Zoom = CrossroadsofFate.Vector2Extensions.Clamp(zoom, MinZoom, MaxZoom);
            

		}

    }




    public void OnPlayerPositionChanged(Vector2 position)
	{
		Position = position;

	}
}