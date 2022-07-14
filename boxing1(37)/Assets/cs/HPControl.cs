using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HPControl : MonoBehaviour
{
	Animator anim;
	public int MaxHP;
	public int HP;
	public double timer;
	public double cantMoveTimer;
	public double redHpUiTimer;
	public GameObject redHpUi;
	public GameObject atkEffect;
	public int testDie;

    void Start()
    {
		testDie=0;
		anim = GetComponent<Animator>();
        timer=1.5;
		cantMoveTimer=0;
		if(gameObject.tag=="Enemy"){
			if(WorldController1.worldNo==1){
				MaxHP=PlayerPrefs.GetInt("playerQuaLvData",1)*10;
				HP=MaxHP;
			}
			else{
				MaxHP=50;
				HP=MaxHP;
			}
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
			if(tag=="Player"){
				redHpUiTimer=0.5;
			}
			if(tag=="Enemy" && atker.tag=="Player"){
				GameObject cloneAtkEffect = Instantiate(WorldController1.allAtkEffect1,transform.position, transform.rotation) as GameObject;
				Destroy(cloneAtkEffect,(float)0.3);
			}
		}
		HP-=dmg;
		if(HP<=0 && atker.tag=="Player" && Time.timeScale==1){
			if(WorldController1.worldNo==1){
				PlayerPrefs.SetInt("playerQuaLvData",PlayerPrefs.GetInt("playerQuaLvData",1)+1);
			}
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
		if(HP<1){//  HP<1
			testDie=1;
		}
		if(testDie==1){
			if(tag=="Player"){
				redHpUi.SetActive(true);
			}
			anim.Play("playerAnimation_idle");
			GetComponent<Rigidbody>().freezeRotation=false;
			transform.Rotate(Vector3.right*(90)*Time.deltaTime);
			if(WorldController1.worldNo==2){
				if(tag==("Enemy")){
					Destroy(gameObject.GetComponent<EnemyControl>().Me,1);
				}
				else{
					timer-=1*Time.deltaTime;
					if(timer<=0){
						SceneManager.LoadScene("home");
					}
				}
			}
		}
		else{//  HP>=1
			if(tag=="Player"){
				if(redHpUiTimer>0){
					redHpUiTimer-=1*Time.deltaTime;
					redHpUi.SetActive(true);
				}
				else{
					redHpUi.SetActive(false);
				}
			}
		}
		
	}
}
