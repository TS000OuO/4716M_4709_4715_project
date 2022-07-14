using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using Random = System.Random;

public class EnemyControl : MonoBehaviour
{
	Animator anim;
	double area1 = 6.3;
	int turn;
	public int myTurn;
	public float mySpeed=1;
	public GameObject Target;
	public GameObject Me;
	public double atkcd;
	double[,] area = new double[3,4]{{-4.75,-7,10,14},{0,0,33,33},{250,250,500,500}};
	
    void Start()
    {
        anim = GetComponent<Animator>();
		Random rnd = new Random();
		turn = rnd.Next(1,3)*2-3;
		myTurn = turn;
		atkcd=0;
		Target=GameObject.FindWithTag("Player");
    }

    void Update()
    {		
		atkcd-=1*Time.deltaTime;
		EnemyAction();
    }
	
	void EnemyAction(){
		
		float h=0;
		float v=0;
		//double area = area1;
		if((Mathf.Abs(Target.transform.position.x-Me.transform.position.x)<30)&&(Mathf.Abs(Target.transform.position.z-Me.transform.position.z)<30)){
			if(Time.timeScale!=1&&GetComponent<HPControl>().HP<=0){
				Me.transform.position+=new Vector3((Me.transform.position.x-Target.transform.position.x)*(float)0.5*Time.deltaTime,0,(Me.transform.position.z-Target.transform.position.z)*(float)0.5*Time.deltaTime);
			}
			else if(GetComponent<HPControl>().cantMoveTimer>0){
				GetComponent<HPControl>().cantMoveTimer-=1*Time.deltaTime;
				mySpeed=2;
				Me.transform.position+=new Vector3((Me.transform.position.x-Target.transform.position.x)*1*Time.deltaTime,0,(Me.transform.position.z-Target.transform.position.z)*1*Time.deltaTime);
			}
			else if(Time.timeScale==1){
				if(mySpeed<8 && WorldController1.worldNo==1){
					mySpeed+=4*(Time.deltaTime);
				}
				else if(mySpeed<4 && WorldController1.worldNo==2){
					mySpeed+=2*(Time.deltaTime);
				}
				if(Target.GetComponent<CharLv>().charLv>0){
					//Me.transform.Translate(-mySpeed*Time.deltaTime, 0, 0);
					Me.transform.Translate(0,0,mySpeed*Time.deltaTime);
				}
				/*{
					if(transform.position.x>=(area-0.5)||transform.position.x<=-(area-0.5)||transform.position.z>=(area-0.5)||transform.position.z<=-(area-0.5)){
						Me.transform.Rotate(Vector3.up*(turn*180)*Time.deltaTime);
					}
					else{
						Random rnd = new Random();
						turn = (rnd.Next(1,5)*2-5);
						myTurn = turn;
					}
					Me.transform.Translate(-mySpeed*Time.deltaTime,0, 0);
				}*/
			}
		}
		transform.localPosition=new Vector3(0f,0f,0f);
		
		/*if(transform.position.x>=area){
			Me.transform.position += new Vector3(-10*Time.deltaTime,0,0);
		}
		if(transform.position.x<=-area){
			Me.transform.position += new Vector3(10*Time.deltaTime,0,0);
		}
		if(transform.position.z>=area){
			Me.transform.position += new Vector3(0,0,-10*Time.deltaTime);
		}
		if(transform.position.z<=-area){
			Me.transform.position += new Vector3(0,0,10*Time.deltaTime);
		}*/
		if(transform.position.x>=(area[WorldController1.worldNo,0]+area[WorldController1.worldNo,2] / 2)){
			transform.position += new Vector3(-30*Time.deltaTime,0,0);
		}
		if(transform.position.x<=(area[WorldController1.worldNo,0]-area[WorldController1.worldNo,2] / 2)){
			transform.position += new Vector3(30*Time.deltaTime,0,0);
		}
		if(transform.position.z>=(area[WorldController1.worldNo,1]+area[WorldController1.worldNo,3] / 2)){
			transform.position += new Vector3(0,0,-30*Time.deltaTime);
		}
		if(transform.position.z<=(area[WorldController1.worldNo,1]-area[WorldController1.worldNo,3] / 2)){
			transform.position += new Vector3(0,0,30*Time.deltaTime);
		}
	}
	
	void OnTriggerStay (Collider col) {
		if (col.gameObject.tag == "Player") {
			if(Time.timeScale==1){
				if(atkcd<=0){
					Random rnd = new Random();
					if(rnd.Next(1,7)>=3){
						anim.SetTrigger("skill1");
					}
					else if(rnd.Next(1,7)>=2){
						anim.SetTrigger("attackL");
					}
					else{
						anim.SetTrigger("attackR");
					}
					atkcd=2;
				}
			}
			mySpeed=0;
		}
	}
	
	void OnTriggerExit (Collider col) {
		if (col.gameObject.tag == "Player") {
			mySpeed = 4;
		}
	}
}
