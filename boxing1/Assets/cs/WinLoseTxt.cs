using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLoseTxt : MonoBehaviour
{
	public static Text text;
	public GameObject[] player = new GameObject[1];
	public GameObject[] enemy = new GameObject[1];
	public double timer = 1.5;
	public int testNo;
    void Start()
    {
        text = GetComponent<Text>();
		text.text="";
    }

    void Update()
    {
		player = GameObject.FindGameObjectsWithTag("Player");
		enemy = GameObject.FindGameObjectsWithTag("Enemy");
		
		for(int i=0;i<player.Length;i++){
			if(player[i].GetComponent<HPControl>().HP<1){
				WinLose(player[i]);
			}
		}
		for(int i=0;i<enemy.Length;i++){
			if(enemy[i].GetComponent<HPControl>().HP>0){
				i=enemy.Length+1;
			}
			if(i==enemy.Length-1){
				WinLose(enemy[0]);
			}
			testNo=i;
		}
		
        /*player = GameObject.FindWithTag("Player");
		enemy = GameObject.FindWithTag("Enemy");
		if(player.GetComponent<HPControl>().HP<1||enemy.GetComponent<HPControl>().HP<1){
			WinLose();
		}
		else{
			text.text="";
		}*/
    }
	
	void WinLose(GameObject loser){
		//GameObject player = GameObject.FindWithTag("Player");
		//GameObject enemy = GameObject.FindWithTag("Enemy");
		/*if(player.GetComponent<HPControl>().HP>enemy.GetComponent<HPControl>().HP){
			text.text="YOU WIN";
		}
		else{
			text.text="YOU LOSE";
		}*/
		if(loser.tag=="Enemy"){
			text.text="YOU WIN";
		}
		else if(loser.tag=="Player"){
			text.text="YOU LOSE";
		}
		if(Input.anyKeyDown){
			SceneManager.LoadScene("home");
		}
		
		if(Time.timeScale>0.5){
				Time.timeScale-=1*Time.deltaTime;
			}
			else if(timer>0){
				timer-=(1/Time.timeScale)*Time.deltaTime;
			}
			else{
				Time.timeScale=0;
			}
	}
}
