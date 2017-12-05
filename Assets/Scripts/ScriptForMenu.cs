using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptForMenu : MonoBehaviour {

	public Canvas quitMenu;
	public Button startText;
	public Button exitText;
    public GameObject mainMenu;
    public Canvas UI;
    public GameObject Player;
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		quitMenu.enabled = false;

        ShowMenu();

    }
    public void ShowMenu()
    {
        UI.scaleFactor *= 0;
        mainMenu.SetActive(true);
        AudioListener.pause = true;
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Player.GetComponent<PlayerController>().enabled = false;
    }

	public void ExitPress(){
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}
	public void NoPress(){

		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void StartLevel(){
        print("startlevel");
        UI.scaleFactor += 1;
        AudioListener.pause = true;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Player.GetComponent<PlayerController>().enabled = true;
        mainMenu.SetActive(false);
    }
	public void ExitGame(){
		Application.Quit ();
	}

}
