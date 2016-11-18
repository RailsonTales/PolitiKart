using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 100, 50), "Jogar"))
        {
            Application.LoadLevel("principal");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 100, 50), "Ajuda"))
        {
            Application.LoadLevel("ajuda");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 100, 50), "Créditos"))
        {
            Application.LoadLevel("creditos");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 150, 100, 50), "Sair"))
        {
            Application.Quit();
        }
    }

}
