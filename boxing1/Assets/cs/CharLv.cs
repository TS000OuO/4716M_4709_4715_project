using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharLv : MonoBehaviour
{
	public int charLv;
	public int charAtkValue;
	public int charExpMax;
	public int charExp;
	
    void Update()
    {
		if(GetComponent<PlayerControl>().playerNo==1){
			if(charExp>=charExpMax){
				LoadPlayerLv.UpdatePlayerLv(1);
			}
		}
	}
}
