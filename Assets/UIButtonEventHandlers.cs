using System.Globalization;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Handles the button events in the user interface.</summary>
public class UIButtonEventHandlers : MonoBehaviour
{
    #region Fields
    public Button musicFestivalScenarioButton;
    public Button footballScenarioButton;
    public Button homeButton;
    public DigitalRuby.WeatherMaker.WeatherMakerScript musicFestivalScenarioWeatherScript;
    public DigitalRuby.WeatherMaker.WeatherMakerScript footballScenarioWeatherScript;
    public GameObject musicFestivalScenarioGameObject;
    public GameObject footballScenarioGameObject;
    public Text musicFestivalScenarioButtonText;
    public Text footballScenarioButtonText;
    public bool isMusicFestivalScenarioButtonActive = false;
    public bool isFootballScenarioButtonActive = false;
    public bool isMusicFestivalScenarioWeatherScriptActive = false;
    public bool isFootballScenarioWeatherScriptActive = false;
    public bool isHomeButtonActive = false;
    public bool isFootballScenarioViewed = false;
    public bool isMusicFestivalScenarioViewed = false;
    public readonly string currentLocale = CultureInfo.CurrentUICulture.Name;
    public string scenarioViewed = string.Empty;
    public string scenarioViewedMessage = string.Empty;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Add event listeners to the three buttons
        musicFestivalScenarioButton = GameObject.Find("/UI Canvas/Music Festival Scenario Button").GetComponent<Button>();
        musicFestivalScenarioButton.onClick.AddListener(MusicFestivalScenarioClickedAction);

        footballScenarioButton = GameObject.Find("/UI Canvas/Football Game Scenario Button").GetComponent<Button>();
        footballScenarioButton.onClick.AddListener(FootballScenarioClickedAction);

        homeButton = GameObject.Find("/UI Canvas/Home Button").GetComponent<Button>();
        homeButton.onClick.AddListener(HomeClickedAction);
        homeButton.gameObject.SetActive(false);

        // Identify and assign the inactive weather maker prefabs
        musicFestivalScenarioButtonText = GameObject.Find("/UI Canvas/Music Festival Scenario Button/Music Festival Scenario Button Text").GetComponent<Text>();
        footballScenarioButtonText = GameObject.Find("/UI Canvas/Football Game Scenario Button/Football Game Scenario Button Text").GetComponent<Text>();
            
        musicFestivalScenarioGameObject = FindInactiveObjectByName("MusicFestivalWeatherMakerPrefab");
        if (musicFestivalScenarioGameObject != null) {
            musicFestivalScenarioWeatherScript = musicFestivalScenarioGameObject.GetComponent<DigitalRuby.WeatherMaker.WeatherMakerScript>();
        }
     
        footballScenarioGameObject = FindInactiveObjectByName("FootballGameWeatherMakerPrefab");
        if (footballScenarioGameObject != null) {
            footballScenarioWeatherScript = footballScenarioGameObject.GetComponent<DigitalRuby.WeatherMaker.WeatherMakerScript>();
        }

        // Configure scenario viewed button text based on the locale of the system
        if (currentLocale.StartsWith("en")) {
            scenarioViewed = "Scenario Viewed";
        }
        else if (currentLocale == "zh-TW") {
            scenarioViewed = "已查看的方案";
        }
        else if (currentLocale.StartsWith("zh")) {
            scenarioViewed = "已查看的方案";
        }
        else {
            scenarioViewed = "Scenario Viewed";
        }

        scenarioViewedMessage = string.Format("{0} " + Regex.Unescape(@"\u2713"), scenarioViewed);
    }

    #region User-Defined Methods
    /// <summary>Finds inactive game object by its name.</summary>
    /// <param name="name">Represents the name of the game object to find.</param>
    /// <returns>Returns a matching game object, or null if no match is found.</returns>
    GameObject FindInactiveObjectByName(string name) {
    Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
    for (int i = 0; i < objs.Length; i++)
    {
        if (objs[i].hideFlags == HideFlags.None)
        {
            if (objs[i].name == name)
            {
                return objs[i].gameObject;
            }
        }
    }
    return null;
}
    /// <summary>The event handler for the music festival scenario button.</summary>
    /// <remarks>Inactivates the music festival and football game scenario buttons, then activates the music festival 
    /// weather maker prefab, and then activates the home button.</remarks>
    void MusicFestivalScenarioClickedAction() {
        isMusicFestivalScenarioButtonActive = musicFestivalScenarioButton.gameObject.activeInHierarchy;
        musicFestivalScenarioButton.gameObject.SetActive(!isMusicFestivalScenarioButtonActive);

        isFootballScenarioButtonActive = footballScenarioButton.gameObject.activeInHierarchy;
        footballScenarioButton.gameObject.SetActive(!isFootballScenarioButtonActive);

        if (musicFestivalScenarioWeatherScript!= null) {
            isMusicFestivalScenarioWeatherScriptActive = musicFestivalScenarioWeatherScript.gameObject.activeInHierarchy;
            musicFestivalScenarioWeatherScript.gameObject.SetActive(!isMusicFestivalScenarioWeatherScriptActive);
        }

        isHomeButtonActive = homeButton.gameObject.activeInHierarchy;
        homeButton.gameObject.SetActive(!isHomeButtonActive);
     }

    /// <summary>The event handler for the football game scenario button.</summary>
    /// <remarks>Inactivates the music festival and football game scenario buttons as inactive, then activates the 
    /// football game scenario weather maker prefab, and then activates the home button.</remarks>
     void FootballScenarioClickedAction() {
        isFootballScenarioButtonActive = footballScenarioButton.gameObject.activeInHierarchy;
        footballScenarioButton.gameObject.SetActive(!isFootballScenarioButtonActive);

        isMusicFestivalScenarioButtonActive = musicFestivalScenarioButton.gameObject.activeInHierarchy;
        musicFestivalScenarioButton.gameObject.SetActive(!isMusicFestivalScenarioButtonActive);

        if (footballScenarioWeatherScript!= null) {
            isFootballScenarioWeatherScriptActive = footballScenarioWeatherScript.gameObject.activeInHierarchy;
            footballScenarioWeatherScript.gameObject.SetActive(!isFootballScenarioWeatherScriptActive);
        }

        isHomeButtonActive = homeButton.gameObject.activeInHierarchy;
        homeButton.gameObject.SetActive(!isHomeButtonActive);
     }

    /// <summary>The event handler for the home button.</summary>
    /// <remarks>Activates the music festival and football game scenario buttons and weather maker prefabs 
    /// then deactivates the last seen scenario's button and changes its text to "Scenario Viewed", 
    /// and then deactivates the home button.</remarks>
     void HomeClickedAction() {
        isMusicFestivalScenarioButtonActive = musicFestivalScenarioButton.gameObject.activeInHierarchy;
        musicFestivalScenarioButton.gameObject.SetActive(!isMusicFestivalScenarioButtonActive);

        isFootballScenarioButtonActive = footballScenarioButton.gameObject.activeInHierarchy;
        footballScenarioButton.gameObject.SetActive(!isFootballScenarioButtonActive);

        isMusicFestivalScenarioWeatherScriptActive = musicFestivalScenarioWeatherScript.gameObject.activeInHierarchy;
        if (isMusicFestivalScenarioWeatherScriptActive) {
            isMusicFestivalScenarioViewed = true;
            musicFestivalScenarioWeatherScript.gameObject.SetActive(!isMusicFestivalScenarioWeatherScriptActive);
            musicFestivalScenarioButton.enabled = false;
            
            musicFestivalScenarioButtonText.text = scenarioViewedMessage;
            if (isFootballScenarioViewed) {
                footballScenarioButtonText.text = scenarioViewedMessage;
            }
        }

        isFootballScenarioWeatherScriptActive = footballScenarioWeatherScript.gameObject.activeInHierarchy;
        if (isFootballScenarioWeatherScriptActive) {
            isFootballScenarioViewed = true;
            footballScenarioWeatherScript.gameObject.SetActive(!isFootballScenarioWeatherScriptActive);
            footballScenarioButton.enabled = false;
            
            footballScenarioButtonText.text = scenarioViewedMessage;
            if (isMusicFestivalScenarioViewed) {
                musicFestivalScenarioButtonText.text = scenarioViewedMessage;
            }
        }

        isHomeButtonActive = homeButton.gameObject.activeInHierarchy;
        homeButton.gameObject.SetActive(!isHomeButtonActive);
     }
     #endregion
}
