public class TutFadeIn : TutorialStepper
{
    public static bool _fadeIn = false;    
    private void OnLevelWasLoaded(int level)
    {
        if (level == 7 && !_fadeIn)
        {
            PauseGame();
        }
    }
    public override void UnPauseGame()
    {
        base.UnPauseGame();
        _fadeIn = true;
    }
}
