using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPControl : MonoBehaviour
{
	Animator anim;
	public int MaxHP;
	public int HP;
	public double timer;
	public double cantMoveTimer;

    void Start()
    {
		anim = GetComponent<Animator>();
        timer=1.5;
		cantMoveTimer=0;
		if(tag=="Enemy"){
			MaxHP=PlayerPrefs.GetInt("playerQuaLvData",1)*10;
			HP=MaxHP;
		}
    }

    void Update()
    {
        TestHp();
    }
	

	public void Damage(int dmg, GameObject atker){
		
		
		if(tag=="Sandbag"){
			LoadPlayerLv.UpdatePlayerExp(1);
			HP=MaxHP;
		}
		else{
			if((anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_idle"))||(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_stand"))){
				dmg/=2;
			}
		}
		HP-=dmg;
		if(HP<=0 && atker.tag=="Player" && Time.timeScale==1){
			PlayerPrefs.SetInt("playerQuaLvData",PlayerPrefs.GetInt("playerQuaLvData",1)+1);
			LoadPlayerLv.UpdatePlayerExp(GetComponent<CharLv>().charLv*10);
		}
	}
	
	public void TestHp(){
		/*if(HP<1){
			anim.Play("playerAnimation_idle");
			GetComponent<Rigidbody>().freezeRotation=false;
			transform.Rotate(Vector3.right*(90)*Time.deltaTime);
			if(Time.timeScale>0.5){
				Time.timeScale-=1*Time.deltaTime;
			}
			else if(timer>0){
				timer-=(1/Time.timeScale)*Time.deltaTime;
			}
			else{
				Time.timeScale=0;
			}
		}*/
		if(HP<1){
			anim.Play("playerAnimation_idle");
			GetComponent<Rigidbody>().freezeRotation=false;
			transform.Rotate(Vector3.right*(90)*Time.deltaTime);
		}
	}
}
