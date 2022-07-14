using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementBoard : MonoBehaviour
{
	public int playerQua;
	public GameObject[] achi;
	public int[] achiCondition;
    // Start is called before the first frame update
    void Start()
    {
        playerQua=PlayerPrefs.GetInt("playerQuaLvData",1);
		//achi = new GameObject[6];
		//achiCondition = new int[6];
		
		for(int i=0;i<achi.Length;i++){
			if(playerQua>achiCondition[i]){
				achi[i].gameObject.SetActive(true);
			}
			else{
				achi[i].gameObject.SetActive(false);
			}
		}
    }
}
