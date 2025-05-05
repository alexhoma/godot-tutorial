using Godot;

public interface IsNpc
{
    float DifficultyMultiplier { get; set; }
    
    void IncrementDifficulty()
    {
        DifficultyMultiplier += 1.5f;
    }   
}