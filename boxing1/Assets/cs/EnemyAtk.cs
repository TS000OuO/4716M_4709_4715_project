using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

public class EnemyAtk : MonoBehaviour
{	
	void OnTriggerEnter (Collider col) {
		GameObject enemy = GameObject.FindWithTag("Enemy");
		if ((col.gameObject.tag == "Player"||col.gameObject.tag == "Player2") && Time.timeScale==1) {
			Random rnd = new Random();
			if(PlayerPrefs.GetInt("playerQuaLvData",1)>30){
				if(PlayerPrefs.GetInt("playerQuaLvData",1)<col.GetComponent<CharLv>().charAtkValue){
					col.gameObject.GetComponent<HPControl>().Damage(rnd.Next(PlayerPrefs.GetInt("playerQuaLvData",1),col.GetComponent<CharLv>().charAtkValue),enemy);
				}
				else{
					col.gameObject.GetComponent<HPControl>().Damage(rnd.Next(col.GetComponent<CharLv>().charAtkValue,PlayerPrefs.GetInt("playerQuaLvData",1)),enemy);
				}
			}
			else{
				col.gameObject.GetComponent<HPControl>().Damage(rnd.Next(1,PlayerPrefs.GetInt("playerQuaLvData",1)+3),enemy);
			}
		}
	}
}
