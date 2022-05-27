using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WorldController1 : MonoBehaviour
{
	Animator anim;
	public GameObject player;
	public GameObject enemy;
    void Start()
    {
		anim = GetComponent<Animator>();
    }
	
	void FixedUpdate(){
		
		if (Input.GetKeyDown(KeyCode.G)){
			player.transform.Translate(0, 2*Time.deltaTime, 0);
		}
		
	}
	
	public void Knockback(int knNo, GameObject me, Collider other){ //for enemy
		if(knNo==0||knNo==1){
			if(other.tag=="Enemy"){
				other.GetComponent<HPControl>().cantMoveTimer=0.2;
				other.GetComponent<EnemyControl>().Me.transform.position+=new Vector3((other.GetComponent<EnemyControl>().Me.transform.position.x-me.transform.position.x)*3*Time.deltaTime,0,(other.GetComponent<EnemyControl>().Me.transform.position.z-me.transform.position.z)*3*Time.deltaTime);
			}
			else if(other.tag=="Player"){
				other.GetComponent<HPControl>().cantMoveTimer=0.1;
				other.transform.position+=new Vector3((other.transform.position.x-me.transform.position.x)*3*Time.deltaTime,0,(other.transform.position.z-me.transform.position.z)*3*Time.deltaTime);
			}
		}
	}
	
	public void Knockback(int knNo, GameObject me, GameObject other){ //for sandbag
		if(knNo==0||knNo==1){
			//other.GetComponent<HPControl>().cantMoveTimer=0.2;
			other.transform.position+=new Vector3((other.transform.position.x-me.transform.position.x)*3*Time.deltaTime,0,(other.transform.position.z-me.transform.position.z)*3*Time.deltaTime);
		}
	}
}
