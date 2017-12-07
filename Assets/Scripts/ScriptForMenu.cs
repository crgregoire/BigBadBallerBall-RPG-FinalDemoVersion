using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptForMenu : MonoBehaviour {

	public Canvas quitMenu;
	public Canvas instructionMenu;

	public Button startText;
	public Button exitText;
    public GameObject mainMenu;
    public Canvas UI;
    public GameObject Player;
	void Start () {
		instructionMenu = instructionMenu.GetComponent<Canvas> ();
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();

		quitMenu.enabled = false;
		instructionMenu.enabled = false;

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
		//instructionMenu.enabled = false;
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}
		

	public void InstructionPress(){
		instructionMenu.enabled = true;
		//quitMenu.enabled = false;
		startText.enabled = false;
		exitText.enabled = false;
	}
	public void NoPress(){
		//instructionMenu.enabled = true;
		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}
	public void NoPress2(){
		instructionMenu.enabled = true;
		//quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void StartLevel(){
         print("startlevel");
        UI.scaleFactor += 1;
//		AudioListener.pause = false;
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
