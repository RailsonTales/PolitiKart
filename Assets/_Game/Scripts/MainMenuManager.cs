using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

  public void NovaPartida(){
    SceneManager.LoadScene("principal");
  }
  public void Ajuda(){
    //Application.LoadLevel("principal");
    //SceneManager.LoadScene("ajuda");
  }
  public void Creditos(){
    //SceneManager.LoadScene("creditos");
  }
  public void Sair(){
    Application.Quit();
  }

}
