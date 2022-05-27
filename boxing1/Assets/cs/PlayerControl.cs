using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Windows.Input;
using Random = System.Random;

public class PlayerControl : MonoBehaviour
{

	Animator anim;
	
	//		area[	0	, 	0			1		2		3	]
	//array area[area_no][centre_x, centre_z, length_x, length_z]
	//area 0=home 1=boxingRing
	double[,] area = new double[3,4]{{-4.75,-7,10,14},{0,0,12.6,12.6},{0,0,8.4,8.4}};
	//double[,] area = new double[3,4]{{0,0,46,46},{0,0,12.6,12.6},{0,0,8.4,8.4}};

	float speed_effect = 1;
	float player_power = 0;
	double player_max_power = 10;
	public int atkno = 0;
	public int worldNo;
	public GameObject Target;
	public GameObject player;
	public Collider enemyForSlider;
	public double enemyHpSliderTimer;
	public GameObject FpcCam;
	public GameObject TpcCam;
	public GameObject MpcCam;
	public int camNo;
	public Canvas canvas;
	double skill3Timer = 0;
	float cTimer;
	float mySpeed=3;
	public int skillAtk;
	public Slider HpSlider;
	public Slider SpSlider;
	public Slider EnemyHpSlider;
	public double jump;
	public int playerNo;
	public int playerNum;
	public GameObject player1p;
	public GameObject player2p;

    void Start()
    {
		playerNum=PlayerPrefs.GetInt("playerNum", 1);
		skillAtk=0;
        anim = GetComponent<Animator>();
		camNo = PlayerPrefs.GetInt("playerCamera",1);
		Time.timeScale=1;
		SpSlider.maxValue = 100;
		SpSlider.minValue = 0;
		enemyHpSliderTimer=0;
		EnemyHpSlider.minValue=0;
		if(playerNo==1){
			HpSlider.maxValue = PlayerPrefs.GetInt("playerLvData",1)*50;
			HpSlider.minValue = 0;
			if(camNo==1){
				FpcCam.SetActive(true);
				TpcCam.SetActive(false);
				MpcCam.SetActive(false);
			}
			else if(camNo==2){
				FpcCam.SetActive(false);
				TpcCam.SetActive(true);
				MpcCam.SetActive(false);
			}
		}
		else if(playerNo==2){
			HpSlider.maxValue = 2000;
			HpSlider.minValue = 0;
		}
		if(playerNum==1){
			player2p.GetComponent<PlayerControl>().HpSlider.gameObject.SetActive(false);
			player2p.GetComponent<PlayerControl>().SpSlider.gameObject.SetActive(false);
			player2p.SetActive(false);
			if(camNo==1){
				FpcCam.SetActive(true);
				TpcCam.SetActive(false);
				MpcCam.SetActive(false);
			}
			else if(camNo==2){
				FpcCam.SetActive(false);
				TpcCam.SetActive(true);
				MpcCam.SetActive(false);
			}
		}
		else if(playerNum==2){
			player2p.SetActive(true);
			player2p.GetComponent<PlayerControl>().HpSlider.gameObject.SetActive(true);
			player2p.GetComponent<PlayerControl>().SpSlider.gameObject.SetActive(true);
		}
    }

    void Update()
    {
		
		if(Input.GetKeyDown(KeyCode.P)){
			if(playerNum==1){
				playerNum=2;
				player2p.SetActive(true);
				player2p.GetComponent<PlayerControl>().HpSlider.gameObject.SetActive(true);
				player2p.GetComponent<PlayerControl>().SpSlider.gameObject.SetActive(true);
				PlayerPrefs.SetInt("playerNum", 2);
				PlayerPrefs.Save();
			}
			else if(playerNum==2){
				playerNum=1;
				player2p.GetComponent<PlayerControl>().HpSlider.gameObject.SetActive(false);
				player2p.GetComponent<PlayerControl>().SpSlider.gameObject.SetActive(false);
				player2p.SetActive(false);
				if(camNo==1){
					FpcCam.SetActive(true);
					TpcCam.SetActive(false);
					MpcCam.SetActive(false);
				}
				else if(camNo==2){
					FpcCam.SetActive(false);
					TpcCam.SetActive(true);
					MpcCam.SetActive(false);
				}
				PlayerPrefs.SetInt("playerNum", 1);
				PlayerPrefs.Save();
			}
		}
		
		if(enemyHpSliderTimer>0){
			if(enemyForSlider.GetComponent<HPControl>().HP>0){
				EnemyHpSlider.gameObject.SetActive(true);
			}
			else{
				EnemyHpSlider.gameObject.SetActive(false);
			}
			enemyHpSliderTimer-=1*Time.deltaTime;
			EnemyHpSlider.maxValue=enemyForSlider.GetComponent<HPControl>().MaxHP;
			EnemyHpSlider.value=enemyForSlider.GetComponent<HPControl>().HP;
		}
		else{
			EnemyHpSlider.gameObject.SetActive(false);
		}
		HpSlider.value=GetComponent<HPControl>().HP;
		
		if(playerNum==1){
			if(Input.GetKeyDown(KeyCode.R)){
				if(camNo==2){
					FpcCam.SetActive(true);
					TpcCam.SetActive(false);
					MpcCam.SetActive(false);
					camNo=1;
					
					PlayerPrefs.SetInt("playerCamera", camNo);
					PlayerPrefs.Save();
				}
				else if(camNo==1){
					FpcCam.SetActive(false);
					TpcCam.SetActive(true);
					MpcCam.SetActive(false);
					camNo=2;
					
					PlayerPrefs.SetInt("playerCamera", camNo);
					PlayerPrefs.Save();
				}
			}
		}
		else if(playerNum==2){
			if(playerNo==1){
				FpcCam.SetActive(false);
				TpcCam.SetActive(false);
			}
			MpcCam.SetActive(true);
		}
		PlayerAtk(playerNo);
		PlayerMove(playerNo);
		
		
		//bounding
		if(transform.position.x>=(area[worldNo,0]+area[worldNo,2] / 2)){
			transform.position += new Vector3(-10*Time.deltaTime,0,0);
		}
		if(transform.position.x<=(area[worldNo,0]-area[worldNo,2] / 2)){
			transform.position += new Vector3(10*Time.deltaTime,0,0);
		}
		if(transform.position.z>=(area[worldNo,1]+area[worldNo,3] / 2)){
			transform.position += new Vector3(0,0,-10*Time.deltaTime);
		}
		if(transform.position.z<=(area[worldNo,1]-area[worldNo,3] / 2)){
			transform.position += new Vector3(0,0,10*Time.deltaTime);
		}
		
	}
	
	public void PlayerMove(int pNo){
		//--------------------------------p1-------------------------------------qweasdzxc
		if(pNo==1){
			if(Time.timeScale!=1&&GetComponent<HPControl>().HP<=0){
				transform.position+=new Vector3((transform.position.x-Target.transform.position.x)*(float)0.5*Time.deltaTime,0,(transform.position.z-Target.transform.position.z)*(float)0.5*Time.deltaTime);
			}
			else if(GetComponent<HPControl>().cantMoveTimer>0){
				GetComponent<HPControl>().cantMoveTimer-=1*Time.deltaTime;
				transform.position+=new Vector3((transform.position.x-Target.transform.position.x)*3*Time.deltaTime,0,(transform.position.z-Target.transform.position.z)*3*Time.deltaTime);
			}
			else if(Time.timeScale==1){
				if((anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_idle")||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_stand"))&&(transform.position.x<(area[worldNo,0]+area[worldNo,2] / 2)&&transform.position.x>(area[worldNo,0]-area[worldNo,2] / 2)&&transform.position.z<(area[worldNo,1]+area[worldNo,3] / 2)&&transform.position.z>(area[worldNo,1]-area[worldNo,3] / 2))){
					if(Input.GetKey(KeyCode.D)){
						transform.Rotate(Vector3.forward*(180)*Time.deltaTime);
					}
					else if(Input.GetKey(KeyCode.A)){
						transform.Rotate(Vector3.forward*(-180)*Time.deltaTime);
					}
					if(Input.GetKey(KeyCode.W)){
						transform.Translate(0, mySpeed*speed_effect*Time.deltaTime, 0);
					}
					else if(Input.GetKey(KeyCode.S)){
						transform.Translate(0, -mySpeed/2*speed_effect*Time.deltaTime, 0);
					}
					/*if(Input.GetKey(KeyCode.RightArrow)){
						transform.Rotate(Vector3.forward*(180)*Time.deltaTime);
					}
					else if(Input.GetKey(KeyCode.LeftArrow)){
						transform.Rotate(Vector3.forward*(-180)*Time.deltaTime);
					}*/
					if(Input.GetButtonDown("Jump")){
						jump=7;
					}
					else if(jump>0){
						transform.Translate(0, 0, (float)jump*Time.deltaTime);
						jump-=10*Time.deltaTime;
					}
					else{
						jump=0;
					}
				}
			}
		}
		//--------------------------------p2-------------------------------------yuihjkbnmg
		if(pNo==2){
			if(Time.timeScale!=1&&GetComponent<HPControl>().HP<=0){
				transform.position+=new Vector3((transform.position.x-Target.transform.position.x)*(float)0.5*Time.deltaTime,0,(transform.position.z-Target.transform.position.z)*(float)0.5*Time.deltaTime);
			}
			else if(GetComponent<HPControl>().cantMoveTimer>0){
				GetComponent<HPControl>().cantMoveTimer-=1*Time.deltaTime;
				transform.position+=new Vector3((transform.position.x-Target.transform.position.x)*3*Time.deltaTime,0,(transform.position.z-Target.transform.position.z)*3*Time.deltaTime);
			}
			else if(Time.timeScale==1){
				if((anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_idle")||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_stand"))&&(transform.position.x<(area[worldNo,0]+area[worldNo,2] / 2)&&transform.position.x>(area[worldNo,0]-area[worldNo,2] / 2)&&transform.position.z<(area[worldNo,1]+area[worldNo,3] / 2)&&transform.position.z>(area[worldNo,1]-area[worldNo,3] / 2))){
					if(Input.GetKey(KeyCode.K)){
						transform.Rotate(Vector3.forward*(180)*Time.deltaTime);
					}
					else if(Input.GetKey(KeyCode.H)){
						transform.Rotate(Vector3.forward*(-180)*Time.deltaTime);
					}
					if(Input.GetKey(KeyCode.U)){
						transform.Translate(0, mySpeed*speed_effect*Time.deltaTime, 0);
					}
					else if(Input.GetKey(KeyCode.J)){
						transform.Translate(0, -mySpeed/2*speed_effect*Time.deltaTime, 0);
					}
					/*if(Input.GetKey(KeyCode.RightArrow)){
						transform.Rotate(Vector3.forward*(180)*Time.deltaTime);
					}
					else if(Input.GetKey(KeyCode.LeftArrow)){
						transform.Rotate(Vector3.forward*(-180)*Time.deltaTime);
					}*/
					if(Input.GetKeyDown(KeyCode.G)){
						jump=7;
					}
					else if(jump>0){
						transform.Translate(0, 0, (float)jump*Time.deltaTime);
						jump-=10*Time.deltaTime;
					}
					else{
						jump=0;
					}
				}
			}
		}
		
	}
	
	public void PlayerAtk(int pNo){
		//--------------------------------p1-------------------------------------
		if(pNo==1){
			SpSlider.value=player_power;
			if(Input.GetKeyUp(KeyCode.C)){
				cTimer=5;
			}
			if(player_power<100 && Input.GetKey(KeyCode.C)){
				player_power += (5+cTimer)*Time.deltaTime;
				cTimer+=(25)*Time.deltaTime;
				speed_effect=0.4F;
			}
			else if(player_power>=100){
				skill3Timer+=1*Time.deltaTime;
				cTimer=5;
				if(skill3Timer>=1){
					skill3Timer=0;
					GetComponent<HPControl>().HP+=GetComponent<CharLv>().charLv;
				}
				speed_effect=0.8F;
			}
			else{
				speed_effect=1;
			}
			
			if ((Input.GetButtonDown ("Fire1")&&!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())||Input.GetKeyDown(KeyCode.Q)){
				anim.Play("playerAnimation_atkL");
			}
			if ((Input.GetButtonDown ("Fire2")&&!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())||Input.GetKeyDown(KeyCode.E)){
				anim.Play("playerAnimation_atkR");
				}
			if ((Input.GetButtonDown("Fire3")&&!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())||Input.GetKeyDown(KeyCode.Z)){
				if(player_power>75){
					canvas.gameObject.SetActive(true);
					canvas.GetComponent<SkillBtn>().LoadBtn();
					canvas.GetComponent<SkillBtn>().timer=3;
					Time.timeScale=0.1F;
					anim.Play("playerAnimation_skill1");
					player_power-=100;
				}
			}
			if (Input.GetKeyDown(KeyCode.X)){
				anim.Play("playerAnimation_skill2");
			}
			
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_atkL")
			||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_atkR")
			||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill1")
			||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill2")){
				atkno = 1;
			}
			
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill1")){
				transform.Translate(0, 2*Time.deltaTime, 0);
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill2")){
				transform.Translate(0, 5*Time.deltaTime, 0);
			}
			
			if((anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_idle"))||(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_stand"))){
				atkno = 0;
			}
		}
		//--------------------------------p2-------------------------------------
		if(pNo==2){
			SpSlider.value=player_power;
			if(Input.GetKeyUp(KeyCode.M)){
				cTimer=5;
			}
			if(player_power<100 && Input.GetKey(KeyCode.M)){
				player_power += (5+cTimer)*Time.deltaTime;
				cTimer+=(25)*Time.deltaTime;
				speed_effect=0.4F;
			}
			else if(player_power>=100){
				skill3Timer+=1*Time.deltaTime;
				cTimer=5;
				if(skill3Timer>=1){
					skill3Timer=0;
					GetComponent<HPControl>().HP+=GetComponent<CharLv>().charLv;
				}
				speed_effect=0.8F;
			}
			else{
				speed_effect=1;
			}
			
			if (Input.GetKeyDown(KeyCode.Y)){
				anim.Play("playerAnimation_atkL");
			}
			if (Input.GetKeyDown(KeyCode.I)){
				anim.Play("playerAnimation_atkR");
				}
			if (Input.GetKeyDown(KeyCode.B)){
				if(player_power>75){
					anim.Play("playerAnimation_skill1");
					player_power-=100;
				}
			}
			if (Input.GetKeyDown(KeyCode.N)){
				anim.Play("playerAnimation_skill2");
			}
			
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_atkL")
			||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_atkR")
			||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill1")
			||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill2")){
				atkno = 1;
			}
			
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill1")){
				transform.Translate(0, 2*Time.deltaTime, 0);
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill2")){
				transform.Translate(0, 5*Time.deltaTime, 0);
			}
			
			if((anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_idle"))||(anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_stand"))){
				atkno = 0;
			}
		}
	}
	
	void OnTriggerStay(Collider col){
		GameObject controller = GameObject.FindWithTag("GameController");
		
		if (col.gameObject.tag == "Enemy") {
			if(GetComponent<PlayerControl>().atkno > 0){
				controller.GetComponent<WorldController1>().Knockback(GetComponent<PlayerControl>().atkno, player,col);//col enemy1);
			}
		}
		if (col.gameObject.tag == "Sandbag") {
			controller.GetComponent<WorldController1>().Knockback(1, col.gameObject, player);
		}
	}
}
