using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxingTimer : MonoBehaviour
{
	public double timer;
	double maxTimer;
	public GameObject player;
	public GameObject enemy;
	public static Text text;
	public int damage;
    void Start()
    {
		damage=1;
		text = GetComponent<Text>();
        maxTimer=30;
		timer=maxTimer;
    }

    void Update()
    {
		if(Time.timeScale==1){
			timer-=1*Time.deltaTime;
			if(timer>=maxTimer/2){
				text.text="<color=#00ff00>"+timer.ToString("00")+"</color>";
			}
			else if(timer>=0){
				text.text="<color=#ff0000>"+timer.ToString("00")+"</color>";
			}
		}
		if(timer<=-1){
			for(int i=0;i<damage;i++){
				player.GetComponent<HPControl>().Damage(2,enemy);
				player.GetComponent<HPControl>().TestHp();
				enemy.GetComponent<HPControl>().Damage(2,player);
				enemy.GetComponent<HPControl>().TestHp();
			}
			
			timer+=1;
			damage+=1;
		}
		
    }
}
