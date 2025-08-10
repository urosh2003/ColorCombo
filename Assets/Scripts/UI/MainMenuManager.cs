using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;
    public GameObject credits;
    [SerializeField] Texture2D mainMenuCursor;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(mainMenuCursor, new Vector2(0,0), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnMenu()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        credits.SetActive(false);
    }
    public void TurnOnSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
        credits.SetActive(false);
    }
    public void TurnOnCredits()
    {
        mainMenu.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(true);
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
