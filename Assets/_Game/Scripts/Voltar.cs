using UnityEngine;
using System.Collections;

public class Voltar : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 150, 100, 50), "Voltar"))
        {
            Application.LoadLevel("menu");
        }
    }
}
