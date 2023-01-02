
public enum UIPanelState
{
    MainMenu,
    Gameplay,
    GameWin,
    GameLose
};

public enum GameOverState
{
    Victory,
    Defeat,
    None
};
public enum GameState
{
    Main,
    InGame,
    Win,
    Lose
}

public enum BallType
{
    Red,
    Yellow
}
public enum Sound
{
    Pop,
    Win,
    Lose
}

public enum AdType
{
    Appodeal,
    GD,
    None
}

public enum RewardType
{
    Daily,
    WinMultiply
}

public enum PlayerType
{
    Noob,
    Average,
    Pro
}

public enum TileType
{    
    Empty,
    Occupied
}

public enum HexType
{
    None,
    A,
    B,
    C
}
public enum HexDirection
{
    NE, E, SE, SW, W, NW
}
public static class HexDirectionExtensions
{

    public static HexDirection Opposite(this HexDirection direction)
    {
        return (int)direction < 3 ? (direction + 3) : (direction - 3);
    }
}
public enum TileState
{
    New,
    Wall,
    Cat
}
