using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

using UnityEngine.SocialPlatforms;
using UnityEngine;

public class PlayServicesScript : MonoBehaviour
{
    public static PlayServicesScript Instance; //{ get; private set; }
    
    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        //Building platform and turning it on
        PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder().EnableSavedGames();
        Debug.Log("playgames configured");
        PlayGamesPlatform.InitializeInstance(builder.Build());
        Debug.Log("playgames Initialized");
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Debug.Log("playgames Activated");
        //This is Signing in.  Could put into a 'SignIn' function later.. 
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Succes");
            }
            else
            {
                Debug.Log("Login Failed");

            }
        });
    }

}
