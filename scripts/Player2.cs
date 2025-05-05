using System;
using Godot;

public partial class Player2 : Area2D, HasScore, IsNpc
{
    [Export]
    private Area2D follow;

    [Export]
    private float difficulty = 0.3f;

    public int Score { get; set; }
    
    public float DifficultyMultiplier { get; set; } = 1;

    [Export]
    public Label ScoreDisplay { get; set; }

    public AudioStreamPlayer ScoreSound { get; set; }

    public override void _Ready()
    {
        ScoreSound = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
    }

    public override void _PhysicsProcess(double delta)
    {
        GD.Print("Difficulty", (difficulty * DifficultyMultiplier));
        
        Position = Position with
        {
            // don't leave the play field
            Y = Math.Clamp(
                Position.Lerp(follow.Position, (difficulty * DifficultyMultiplier) / 10).Y, 
                16, 
                GetViewportRect().Size.Y - 16
            )
        };
    }

    private void OnAreaEntered(Area2D area)
    {
        if (area is Ball ball)
        {
            var direction = new Vector2(Vector2.Left.X, (float)Random.Shared.NextDouble() * 2 - 1).Normalized();
            ball.Bounce(direction);
        }
    }
}