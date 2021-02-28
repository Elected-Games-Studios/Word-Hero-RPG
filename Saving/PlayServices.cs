using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;


public class PlayServices : MonoBehaviour
{
    public Text feedback;
    public static PlayServices Instance { get; private set; }
    const string SAVE_NAME = "WordHeroRPG";
    bool isSaving;
    bool isCloudDataLoaded = false;
    bool bGoogleCheck;
    //PlayerStats stats;

    private void Awake()
    {
        isCloudDataLoaded = false;
        Instance = this;
        DontDestroyOnLoad(gameObject);
        //Debug.Log("Today is " + System.DateTime.Today.Date);
    }

    public void Start()
    {
        if (!PlayerPrefs.HasKey(SAVE_NAME))
            PlayerPrefs.SetString(SAVE_NAME, "0");

        if (!PlayerPrefs.HasKey("IsFirstTime"))
            PlayerPrefs.SetInt("IsFirstTime", 1);

        PlayGamesClientConfiguration.Builder builder = new PlayGamesClientConfiguration.Builder().EnableSavedGames();
        PlayGamesPlatform.InitializeInstance(builder.Build());
        PlayGamesPlatform.Activate();

        SignIn();
        //GetComponent<AdManager>().Indestructable();
    }

    string GameDataToString() => SaveManager.SaveParse();

    private void SignIn()
    {
        //feedback.text = "Sign-In Run";
        Social.localUser.Authenticate((bool success) => 
        {
            //feedback.text = "Checking Authentication...";
            LoadData(); 
            if(success)
            {
                    Debug.Log("Google Sign-In has succeeded");
                    //feedback.text = "Google Sign-In has succeeded";
            }
            else
            {
                    Debug.Log("Google Sign-In Failed");
                Invoke("MoveToNextScene", 3f);
                    //feedback.text = "Google Sign-In Failed";
                    
            }
        });
        //LoadData();

    }

	private void MoveToNextScene()
	{
       // Debug.Log("Main Menu Loaded");
        //SceneManager.LoadScene(1);
	}

    #region Saved Games

    public void SaveData()
    {
        if (!isCloudDataLoaded)
        {
            //PlayerStats.Instance.SavePlayer();
            //feedback.text = ("Save Game Cloud Data is not Loaded");
        }
        if (Social.localUser.authenticated)
        {
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(SAVE_NAME,
                DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
            //feedback.text = ("Saving game to cloud");
        }
        else
        {
            //PlayerStats.Instance.SavePlayer();
            //Invoke("MoveToNextScene", 3f);
            //feedback.text = ("Offline. Saving game locally");
        }
    }

    public void LoadData()
    {
        if (Social.Active.localUser.authenticated)
        {
            isSaving = false;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(SAVE_NAME,
               DataSource.ReadCacheOrNetwork, ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
            //feedback.text = ("Load Data = Cloud Load");
        }
        else
        {
            //PlayerStats.Instance.LoadPlayer();
            //feedback.text = ("Load Data = Local Load");
        }
    }

    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            if(!isSaving)
            {
                //feedback.text = ("OnSaveGameOpened Loading");
                LoadGame(game);
            }
            else
            {
                //feedback.text = ("OnSaveGameOpened Saving");
                SaveGame(game);
            }

        }
        else
        {
            if (!isSaving)
            {
                //PlayerStats.Instance.LoadPlayer();
            }
            else
            {
                //PlayerStats.Instance.SavePlayer();
            }
        }
    }

    private void ResolveConflict(IConflictResolver resolver, ISavedGameMetadata original, byte[] originalData,
        ISavedGameMetadata unmerged, byte[] unmergedData)
    {
        if (originalData == null)
            resolver.ChooseMetadata(unmerged);
        else if (unmergedData == null)
            resolver.ChooseMetadata(original);
        else
        {
            //decoding byte data into string
            string originalStr = Encoding.ASCII.GetString(originalData);
            string unmergedStr = Encoding.ASCII.GetString(unmergedData);

            //parsing
            int originalNum = int.Parse(originalStr);
            int unmergedNum = int.Parse(unmergedStr);

            //if original score is greater than unmerged
            if (originalNum > unmergedNum)
            {
                resolver.ChooseMetadata(original);
                return;
            }
            //else (unmerged score is greater than original)
            else if (unmergedNum > originalNum)
            {
                resolver.ChooseMetadata(unmerged);
                return;
            }
            //if return doesn't get called, original and unmerged are identical
            //we can keep either one
            resolver.ChooseMetadata(original);
        }
    }

    private void LoadGame(ISavedGameMetadata game)
    {
        ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private void SaveGame(ISavedGameMetadata game)
    {
        string stringToSave = GameDataToString();

        PlayerPrefs.SetString(SAVE_NAME, stringToSave);

        byte[] dataToSave = Encoding.ASCII.GetBytes(stringToSave);

        SavedGameMetadataUpdate update = new SavedGameMetadataUpdate.Builder().Build();
        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, dataToSave,
            OnSavedGameDataWritten);
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] savedData)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            if (savedData != null)
            {
                string cloudDataString = Encoding.ASCII.GetString(savedData);
                if (cloudDataString != null)
                {
                    if (cloudDataString != "")
                    {
                        SaveManager.LoadSplit(cloudDataString);
                        isCloudDataLoaded = true;
                    }
                    else
                    {
                        WordDatav2.LoadManagerData("");
                        CharectorStats.LoadManagerData("");
                    }
                }
                else
                {
                    WordDatav2.LoadManagerData("");
                    CharectorStats.LoadManagerData("");
                }
            }
            else
            {
                WordDatav2.LoadManagerData("");
                CharectorStats.LoadManagerData("");
            }
        }
    }

    private void OnSavedGameDataWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        //feedback.text = ("Game Has been successfully saved to the cloud!");
    }

#endregion /Saved Games

    #region Achievements


    public void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, (bool success) => { });
    }

    public void IncrementAchievement(string id, int stepsToIncrement)
    {
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
    }
    #endregion /Achievements
}
