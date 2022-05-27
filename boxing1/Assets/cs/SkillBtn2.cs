using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn2 : MonoBehaviour
{
	public int num;
	public int numTest;
	public Text text;
	public Canvas canvas;
	public GameObject player;
    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void myText(){
		text.GetComponent<Text>().text=num.ToString("0");
	}
	
	public void CheckNum(){
		numTest=canvas.GetComponent<SkillBtn>().CheckNum(num);
		if(numTest==1){
			player.GetComponent<PlayerControl>().skillAtk+=1;
			gameObject.SetActive(false);
		}
		else{
			Time.timeScale=1;
			player.GetComponent<PlayerControl>().skillAtk=0;
			canvas.gameObject.SetActive(false);
		}
	}
}
