using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void NewGameBtn(){
		PlayerPrefs.DeleteAll();
		SceneManager.LoadScene("home");
	}
	
	public void OptionBtn(){
		
	}
	
	public void LoadGameBtn(){
		SceneManager.LoadScene("home");
	}
}
