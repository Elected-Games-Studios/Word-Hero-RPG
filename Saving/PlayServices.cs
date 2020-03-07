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
    PlayerStats stats;

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
        GetComponent<AdManager>().Indestructable();
    }

    string GameDataToString()
    {
        if (stats == null)
        {
            stats = PlayerStats.Instance;
        }
        //feedback.text = "Game Data Writing...";
        Debug.Log("GameDataToString has Run Successfully");
        string gameDataString = stats.heroXP.ToString() + " " +
            stats.playerDamage.ToString() + " " +
            stats.pDmgLvl.ToString() + " " +
            stats.playerHealth.ToString() + " " +
            stats.pHpLvl.ToString() + " " +
            stats.playerSpeed.ToString() + " " +
            stats.pSpLvl.ToString() + " " +
            stats.playerEvade.ToString() + " " +
            stats.pEvLvl.ToString() + " " +
            stats.playerLuck.ToString() + " " +
            stats.pLkLvl.ToString() + " " +
            stats.heroLevel.ToString() + " " +
            stats.xpToLevel.ToString() + " " +
            stats.skillPoints.ToString() + " " +
            stats.xpIncrease.ToString() + " " +
            stats.healthPU.ToString() + " " +
            stats.secondWindPU.ToString() + " " +
            stats.timePU.ToString() + " " +
            stats.shieldPU.ToString() + " " +
            stats.playerGold.ToString() + " " +
            stats.activeHero.ToString() + " " +
            stats.isClassOne.ToString() + " " +
            stats.isClassTwo.ToString() + " " +
            stats.isClassThree.ToString() + " " +
            stats.isClassFour.ToString() + " " +
            stats.regionProgress.ToString() + " " +
            stats.levelProgress.ToString() + " " +
            stats.endlessRecord.ToString() + " " +
            stats.date + " " +
            stats.dailyActive.ToString();

        for(int i = 0; i < stats.levelStars.Length; i++)
        {
            gameDataString += " " + stats.levelStars[i].ToString();
        }

        //feedback.text = "Game Data Written";
        return gameDataString;
    }

    void StringToGameData(string localData)
    {
        if(stats == null)
        {
            stats = PlayerStats.Instance;
        }

        string[] gameLoadData = localData.Split(' ');
        stats.heroXP = int.Parse(gameLoadData[0]);
        stats.playerDamage = int.Parse(gameLoadData[1]);
        stats.pDmgLvl = int.Parse(gameLoadData[2]);
        stats.playerHealth = int.Parse(gameLoadData[3]);
        stats.pHpLvl = int.Parse(gameLoadData[4]);
        stats.playerSpeed = float.Parse(gameLoadData[5]);
        stats.pSpLvl = int.Parse(gameLoadData[6]);
        stats.playerEvade = int.Parse(gameLoadData[7]);
        stats.pEvLvl = int.Parse(gameLoadData[8]);
        stats.playerLuck = int.Parse(gameLoadData[9]);
        stats.pLkLvl = int.Parse(gameLoadData[10]);
        stats.heroLevel = int.Parse(gameLoadData[11]);
        stats.xpToLevel = int.Parse(gameLoadData[12]);
        stats.skillPoints = int.Parse(gameLoadData[13]);
        stats.xpIncrease = int.Parse(gameLoadData[14]);
        stats.healthPU = int.Parse(gameLoadData[15]);
        stats.secondWindPU = int.Parse(gameLoadData[16]);
        stats.timePU = int.Parse(gameLoadData[17]);
        stats.shieldPU = int.Parse(gameLoadData[18]);
        stats.playerGold = int.Parse(gameLoadData[19]);
        stats.activeHero = int.Parse(gameLoadData[20]);

        if (gameLoadData[21] == "true")
        { stats.isClassOne = true; }
        else
        { stats.isClassOne = false; }

        if (gameLoadData[22] == "true")
        { stats.isClassTwo = true; }
        else
        { stats.isClassTwo = false; }

        if (gameLoadData[23] == "true")
        { stats.isClassThree = true; }
        else
        { stats.isClassThree = false; }

        if (gameLoadData[24] == "true")
        { stats.isClassFour = true; }
        else
        { stats.isClassFour = false; }

        stats.regionProgress = int.Parse(gameLoadData[25]);
        stats.levelProgress = int.Parse(gameLoadData[26]);
        stats.endlessRecord = int.Parse(gameLoadData[27]);
        stats.date = int.Parse(gameLoadData[28]);
        //if(gameLoadData[29] == "true")
        //{ stats.dailyActive = true; }
        //else
        //{ stats.dailyActive = false; }

        for(int i = 30; i < 105; i++)
        {
            stats.levelStars[i - 30] = int.Parse(gameLoadData[i]);
        }
    }

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
        Debug.Log("Main Menu Loaded");
        SceneManager.LoadScene(1);
	}

    #region Saved Games

    public void SaveData()
    {
        if (!isCloudDataLoaded)
        {
            PlayerStats.Instance.SavePlayer();
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
            PlayerStats.Instance.SavePlayer();
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
            PlayerStats.Instance.LoadPlayer();
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
                PlayerStats.Instance.LoadPlayer();
            }
            else
            {
                PlayerStats.Instance.SavePlayer();
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
                        StringToGameData(cloudDataString);
                        isCloudDataLoaded = true;
                        Invoke("MoveToNextScene", 1f);
                    }
                    else
                    {
                        Invoke("MoveToNextScene", 3f);
                    }
                }
                else
                {
                    Invoke("MoveToNextScene", 3f);
                }
            }
            else
            {
                Invoke("MoveToNextScene", 3f);
            }

            //Debug.Log("Cloud Data Loaded. And the Game knows it");
        }
        else
        {
            Invoke("MoveToNextScene", 3f);
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
