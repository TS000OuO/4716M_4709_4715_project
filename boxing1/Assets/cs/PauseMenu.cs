using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public void BtnPause(){
		Time.timeScale=0;
	}
	
    public void BtnResume(){
		Time.timeScale=1;
	}
	
	public void BtnMainMenu(){
		Time.timeScale=1;
		SceneManager.LoadScene("StartMenu");
	}
}
