using Godot;

// All Godot scripts use the partial keyword, since Godot is generating the other parts of our classes for us
public partial class Ball : Area2D
{
    // [Export] attribute allows us to make our scripts configurable at design time
    [Export] public double MoveSpeed = 1000;
    [Export] public Vector2 Direction = Vector2.Left;

    private AudioStreamPlayer _bounceSound;

    private static readonly Vector2 StartingPoint = new(){ X = 640, Y = 360 };

    public override void _Ready()
    {
        // `GetNode` allows us to access elements within the Ball node tree in Godot
        _bounceSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }

    public override void _PhysicsProcess(double delta)
    {
        // If you think back to our game loop explanation, we must move the ball in a particular direction over time.
        // We propel the ball through space according to our MoveSpeed and Direction values.
        // We need to use the with keyword since the Position is a record type in C# and uses immutable fields,
        // but the Position itself can change.
        Position = Position with
        {
            X = (float)(Position.X + MoveSpeed * delta * Direction.X),
            Y = (float)(Position.Y + MoveSpeed * delta * Direction.Y)
        };
    }

    public void Reset(Vector2 direction)
    {
        Direction = direction;
        Position = StartingPoint;
    }

    public void Bounce(Vector2 direction)
    {
        Direction = direction;
        _bounceSound.Play();
    }
}