using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {
    public Canvas menu;
    public Button play;

	// Use this for initialization
	void Start () {
        menu = menu.GetComponent<Canvas>();
        play = play.GetComponent<Button>();
	}

    public void playPress()
    {
        Application.LoadLevel(1);
    }
}
