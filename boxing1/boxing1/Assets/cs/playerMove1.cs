using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public class playerMove1 : MonoBehaviour
{
	//Horizontal Vertical
	Animator anim;
	double area1 = 4.2;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		GameObject enemy = GameObject.FindWithTag("Enemy");
		
		if (Input.GetButtonDown ("Fire1")){
			anim.SetTrigger("attackL");
		}
			
		if (Input.GetButtonDown ("Fire2")){
			anim.SetTrigger("attackR");
		}
		
		//transform.Translate((transform.position.x-enemy.transform.position.x)*Time.deltaTime, 0, 0);
		if(transform.position.x>=area1){
			transform.position += new Vector3(-1*Time.deltaTime,0,0);
			//transform.Translate(0, -1*Time.deltaTime, 0);
		}
		if(transform.position.x<=-area1){
			transform.position += new Vector3(1*Time.deltaTime,0,0);
			//transform.Translate(0, 1*Time.deltaTime, 0);
		}
		if(transform.position.z>=area1){
			transform.position += new Vector3(0,0,-1*Time.deltaTime);
			//transform.Translate(0, 0, -1*Time.deltaTime);
		}
		if(transform.position.z<=-area1){
			transform.position += new Vector3(0,0,1*Time.deltaTime);
			//transform.Translate(0, 0, 1*Time.deltaTime);
		}
		
		if((anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_idle")||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_stand"))&&(transform.position.x<area1&&transform.position.x>-area1&&transform.position.z<area1&&transform.position.z>-area1)){
			if(h>0.2){
				transform.Rotate(Vector3.forward*(180)*Time.deltaTime);
			}
			else if(h<-0.2){
				transform.Rotate(Vector3.forward*(-180)*Time.deltaTime);
			}
			if(v>0.2){
				transform.Translate(0, Math.Abs(transform.position.x-enemy.transform.position.x)*Time.deltaTime, 0);
			}
			else if(v<-0.2){
				transform.Translate(0, -Math.Abs(transform.position.x-enemy.transform.position.x)*Time.deltaTime, 0);
			}
		}
					
    }
}
