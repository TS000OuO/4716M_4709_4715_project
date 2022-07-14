using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tpDoor : MonoBehaviour
{
	public int tpMode;
	public float tpPosX;
	public float tpPosY;
	public float tpPosZ;
	public string tpScene;
	public GameObject[] tpBoard;
	public GameObject[] tpObject = new GameObject[1];
	public double[] tpTimer = new double[1];
	public int isActive;
	public string currentScene;
	public double setTimer;
	
	void Start(){
		if(setTimer<=0){
			setTimer=1.5;
		}
		currentScene=SceneManager.GetActiveScene().name;
		isActive=1;
		for(int i=0;i<tpBoard.Length;i++){
			tpBoard[i].gameObject.SetActive(false);
		}

		if( (string.IsNullOrEmpty(tpScene)) && !(tpPosX==0 && tpPosY==0 && tpPosZ==0) && !(currentScene==tpScene) ){
			tpMode=1;
			tpBoard[1].gameObject.SetActive(true);
		}
		else if( !(string.IsNullOrEmpty(tpScene)) && (tpPosX==0 && tpPosY==0 && tpPosZ==0) && !(currentScene==tpScene) ){
			tpMode=2;
			tpBoard[2].gameObject.SetActive(true);
		}
		else if( !(string.IsNullOrEmpty(tpScene)) && !(tpPosX==0 && tpPosY==0 && tpPosZ==0) && !(currentScene==tpScene) ){
			tpMode=3;
			tpBoard[3].gameObject.SetActive(true);
		}
		else if( (currentScene==tpScene) && !(tpPosX==0 && tpPosY==0 && tpPosZ==0) ){
			tpMode=4;
			tpBoard[4].gameObject.SetActive(true);
		}
		else{
			tpBoard[0].gameObject.SetActive(true);
			isActive=0;
		}
		
	}
	
	void OnTriggerEnter(Collider col){
		if(isActive==1){
			if(col.gameObject.tag=="Player" || col.gameObject.tag=="Player2"){
				for(int i=0;i<tpObject.Length;i++){
					if( GameObject.ReferenceEquals( col.gameObject, tpObject[i] ) ){
						i=tpObject.Length;
					}
					if( i==tpObject.Length-1 ){
						Array.Resize(ref tpObject,tpObject.Length+1);
						tpObject[tpObject.Length-1]=col.gameObject;
						Array.Resize(ref tpTimer,tpObject.Length);
						tpTimer[tpTimer.Length-1]=setTimer;
					}
				}
			}
		}
	}
	
    void OnTriggerStay(Collider col){
		if(isActive==1){
			if(col.gameObject.tag=="Player" || col.gameObject.tag=="Player2"){
				
				int testObjNo = 0;
				
				if(tpMode==1){
					col.gameObject.transform.position=new Vector3(col.gameObject.transform.position.x+tpPosX*Time.deltaTime,col.gameObject.transform.position.y+tpPosY*Time.deltaTime,col.gameObject.transform.position.z+tpPosZ*Time.deltaTime);
					
				}
				else if(tpMode==2 || tpMode==3 || tpMode==4){
					for(int i=0;i<tpObject.Length;i++){
						if( GameObject.ReferenceEquals( col.gameObject, tpObject[i] ) ){
							testObjNo=i;
							i=tpObject.Length;
						}
					}
					if(tpTimer[testObjNo]<=0){
						if(tpMode==2 || tpMode==3){
							SceneManager.LoadScene(tpScene);
						}
						else if(tpMode==4){
							col.gameObject.transform.position=new Vector3(tpPosX, tpPosY, tpPosZ);
						}
					}
					else{
						if(tpMode==3){
							col.gameObject.transform.position=new Vector3(col.gameObject.transform.position.x+tpPosX*Time.deltaTime,col.gameObject.transform.position.y+tpPosY*Time.deltaTime,col.gameObject.transform.position.z+tpPosZ*Time.deltaTime);
						}
						tpTimer[testObjNo]-=Time.deltaTime;
					}
				}
			}
		}
    }
	
	void OnTriggerExit(Collider col){
		if(isActive==1){
			if(col.gameObject.tag=="Player" || col.gameObject.tag=="Player2"){
				int testObjNo = 0;
				for(int i=0;i<tpObject.Length;i++){
					if( GameObject.ReferenceEquals( col.gameObject, tpObject[i] ) ){
						testObjNo=i;
						i=tpObject.Length;
					}
				}
				tpTimer[testObjNo]=setTimer;
			}
		}
	}
}
