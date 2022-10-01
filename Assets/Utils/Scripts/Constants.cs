using System.Reflection;

public static class Constants
{
    public const float FloatBaseValue = 0.0f;
    public const float FloatComparisionPrecision = 0.0001f;
    public const float CircleVelocity = 2.0f;
    private const  int AccessorCharacterNumberToDeleteFromPropertyName = 4;
    //Variables that need to be initialized before the game starts
    private static float _screenWidth = FloatBaseValue;
    private static float _screenHeight = FloatBaseValue;
    public const string WinLabel = "Победа";
    public const string LoseLabel = "Поражение";
    public const string ThornName = "Thorn";
    public const string CoinName = "Coin";
    
    public static float ScreenWidth
    {
        set => Utilities.SetProperty(ref _screenWidth, value,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
        get => Utilities.GetProperty(_screenWidth, 
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
    }

    public static float ScreenHeight
    {
        set => Utilities.SetProperty(ref _screenHeight, value,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
        get => Utilities.GetProperty(_screenHeight,
            MethodBase.GetCurrentMethod().Name.Substring(AccessorCharacterNumberToDeleteFromPropertyName));
    }
}
