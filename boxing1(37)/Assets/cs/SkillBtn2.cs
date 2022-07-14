using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBtn2 : MonoBehaviour
{
	Animator anim;
	public int num;
	public int numTest;
	public Text text;
	public Canvas canvas;
	public GameObject player;

    void Start()
    {
        anim = player.GetComponent<Animator>();
    }
	
	public void myText(){
		text.GetComponent<Text>().text=num.ToString("0");
	}
	
	public void CheckNum(){
		numTest=canvas.GetComponent<SkillBtn>().CheckNum(num);
		if(numTest==1){
			player.GetComponent<PlayerControl>().skillAtk+=1;
			canvas.GetComponent<SkillBtn>().timer+=0.5;
			if(num==5){
				Time.timeScale=1;
				canvas.gameObject.SetActive(false);
			}
			gameObject.SetActive(false);
		}
		else{
			Time.timeScale=1;
			player.GetComponent<PlayerControl>().skillAtk=0;
			anim.Play("playerAnimation_idle");
			canvas.gameObject.SetActive(false);
		}
	}
}
