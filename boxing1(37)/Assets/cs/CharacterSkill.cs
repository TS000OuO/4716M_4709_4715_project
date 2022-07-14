using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkill : MonoBehaviour
{
	Animator anim;
	
	public void CharacterSkillControl(int skillSetNo, int skillNo, int press, GameObject me){//skillNo 1=L 2=R 3=s1 4=s2 5=s3 press 0=GetKeyDown 1=GetKey 2=GetKeyUp
		
		anim = me.gameObject.GetComponent<Animator>();
		
		if(skillSetNo==1){//skillNo KEY 1=Q 2=E 3=Z 4=X 5=C
			
			if(skillNo==1){
				if (press==0){
					anim.Play("playerAnimation_atkL");
				}
			}
			
			
			if(skillNo==2){
				if (press==0){
					anim.Play("playerAnimation_atkR");
				}
			}
			
			
			if(skillNo==3){
				if (press==0){
					if(me.gameObject.GetComponent<PlayerControl>().player_power>80){
						me.gameObject.GetComponent<PlayerControl>().canvas.gameObject.SetActive(true);
						me.gameObject.GetComponent<PlayerControl>().canvas.GetComponent<SkillBtn>().LoadBtn();
						me.gameObject.GetComponent<PlayerControl>().canvas.GetComponent<SkillBtn>().timer=1.5;
						Time.timeScale=0.1F;
						anim.Play("playerAnimation_skill1");
						me.gameObject.GetComponent<PlayerControl>().player_power-=100;
					}
					else if(me.gameObject.GetComponent<PlayerControl>().player_power>30 && press==0){
						GameObject cloneThrowItem = Instantiate(me.gameObject.GetComponent<PlayerControl>().throwItem2, transform.position, transform.rotation) as GameObject;
						me.gameObject.GetComponent<PlayerControl>().player_power-=30;
						cloneThrowItem.GetComponent<Barbell>().atker=me;
						cloneThrowItem.transform.Translate(0f,2.5f,0f);
					}
					else if(me.gameObject.GetComponent<PlayerControl>().throwTimer<=0){
						anim.Play("playerAnimation_atkR");
						me.gameObject.GetComponent<PlayerControl>().throwTimer=1;
					}
				}
			}
			
			
			if(skillNo==4){
				if (press==0){
					anim.Play("playerAnimation_skill2");
				}
			}
			
			
			if(skillNo==5){
				if(me.gameObject.GetComponent<PlayerControl>().player_power<100 && press==1 && (anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_idle")||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_stand"))){
					me.gameObject.GetComponent<PlayerControl>().player_power += (5+me.gameObject.GetComponent<PlayerControl>().cTimer)*Time.deltaTime;
					me.gameObject.GetComponent<PlayerControl>().cTimer+=(25)*Time.deltaTime;
					me.gameObject.GetComponent<PlayerControl>().speed_effect=0.3F;
				}
				if(press==2){
					me.gameObject.GetComponent<PlayerControl>().cTimer=5;
				}
			}
			
			if(skillNo==0){
				if(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill1")){
					me.gameObject.transform.Translate(0, me.gameObject.GetComponent<PlayerControl>().mySpeed*(float)2*Time.deltaTime, 0);
				}
				if(me.gameObject.GetComponent<PlayerControl>().throwTimer<=0.8&&me.gameObject.GetComponent<PlayerControl>().throwTimer>0.7){
					me.gameObject.GetComponent<PlayerControl>().throwTimer=0.7;
					GameObject cloneThrowItem = Instantiate(me.gameObject.GetComponent<PlayerControl>().throwItem,transform.position, transform.rotation) as GameObject;
					cloneThrowItem.GetComponent<KnifeThrow>().atkObject[0]=me;
					cloneThrowItem.GetComponent<KnifeThrow>().atker=me;
					cloneThrowItem.transform.Translate(-0.25f,1.5f,0.25f);
				}
				if(me.gameObject.GetComponent<PlayerControl>().throwTimer>0){
					me.gameObject.GetComponent<PlayerControl>().throwTimer-=1*Time.deltaTime;
				}
				if(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill2")){
					me.gameObject.transform.Translate(0, me.gameObject.GetComponent<PlayerControl>().mySpeed*(float)1.5*Time.deltaTime, 0);
				}
				if(me.gameObject.GetComponent<PlayerControl>().player_power>=100){
					me.gameObject.GetComponent<PlayerControl>().skill3Timer+=1*Time.deltaTime;
					me.gameObject.GetComponent<PlayerControl>().cTimer=5;
					
					if(me.gameObject.GetComponent<PlayerControl>().skill3Timer>=1){
						me.gameObject.GetComponent<PlayerControl>().skill3Timer=0;
						me.gameObject.GetComponent<HPControl>().HP+=me.GetComponent<CharLv>().charLv;
					}
					me.gameObject.GetComponent<PlayerControl>().speed_effect=0.8F;
				}
				else{
					me.gameObject.GetComponent<PlayerControl>().speed_effect=1;
				}
				
				
				if(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_atkL")
				||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_atkR")
				||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill1")
				||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill2")){
					me.gameObject.GetComponent<PlayerControl>().atkno = 1;
				}
				
				
				if((anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_idle"))||(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_stand"))){
					me.gameObject.GetComponent<PlayerControl>().atkno = 0;
				}
			}
		}
	
	}
	
}
