using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System;
using System.Windows.Input;
using Random = System.Random;

public class PlayerControl : MonoBehaviour
{

	Animator anim;

	//		area[	0	, 	0			1		2		3	]
	//array area[area_no][centre_x, centre_z, length_x, length_z]
	//area 0=home 1=boxingRing 2=OpenWorld
	double[,] area = new double[3, 4] { { -4.75, -7, 100, 140}, { 0, 0, 33, 33 }, { 250, 250, 500, 500 } };
	//double[,] area = new double[3,4]{{-4.75,-7,10,14},{0,0,33,33},{250,250,500,500}};
	//double[,] area = new double[3,4]{{0,0,46,46},{0,0,12.6,12.6},{0,0,8.4,8.4}};

	public float speed_effect = 1;
	public float player_power = 0;
	public double player_max_power = 10;
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
	public double skill3Timer = 0;
	public float cTimer;
	public float mySpeed=3;
	public int skillAtk;
	public Slider HpSlider;
	public Slider SpSlider;
	public Slider EnemyHpSlider;
	public double jump;
	public int playerNo;
	public int playerNum;
	public GameObject player1p;
	public GameObject player2p;
	public GameObject throwItem;
	public GameObject throwItem2;
	public double throwTimer;
	public double moveTimer;
	public double turnTimer;

    void Start()
    {
		moveTimer=0;
		playerNum=PlayerPrefs.GetInt("playerNum", 1);
		skillAtk=0;
        anim = GetComponent<Animator>();
		camNo = PlayerPrefs.GetInt("playerCamera",1);
		Time.timeScale=1;
		SpSlider.maxValue = 100;
		SpSlider.minValue = 0;
		enemyHpSliderTimer=0;
		EnemyHpSlider.minValue=0;
		Physics.IgnoreCollision (player2p.GetComponent<Collider>(), GetComponent<Collider>());
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
		throwTimer=-1;
		canvas.gameObject.SetActive(false);
    }

    void Update()
    {
		/*if(Input.GetKeyDown(KeyCode.V)){
			if(worldNo==0){
				SceneManager.LoadScene("OpenWorld");
			}
			else if(worldNo==2){
				SceneManager.LoadScene("home");
			}
			
		}*/
		if(worldNo==2){
			if(transform.position.y<0.8){
				transform.position=new Vector3(transform.position.x,1,transform.position.z);
			}
		}
		if(Input.GetKeyDown(KeyCode.P)){
			if(playerNum==1){
				playerNum=2;
				player2p.SetActive(true);
				player2p.transform.position=transform.position;
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
		
		if(enemyHpSliderTimer>0&&enemyForSlider!=null){
			if(enemyForSlider.gameObject.GetComponent<HPControl>().HP>0){
				EnemyHpSlider.gameObject.SetActive(true);
			}
			else{
				enemyForSlider=null;
				EnemyHpSlider.gameObject.SetActive(false);
			}
			enemyHpSliderTimer-=1*Time.deltaTime;
			EnemyHpSlider.maxValue=enemyForSlider.gameObject.GetComponent<HPControl>().MaxHP;
			EnemyHpSlider.value=enemyForSlider.gameObject.GetComponent<HPControl>().HP;
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
			transform.position += new Vector3(-30*Time.deltaTime,0,0);
		}
		if(transform.position.x<=(area[worldNo,0]-area[worldNo,2] / 2)){
			transform.position += new Vector3(30*Time.deltaTime,0,0);
		}
		if(transform.position.z>=(area[worldNo,1]+area[worldNo,3] / 2)){
			transform.position += new Vector3(0,0,-30*Time.deltaTime);
		}
		if(transform.position.z<=(area[worldNo,1]-area[worldNo,3] / 2)){
			transform.position += new Vector3(0,0,30*Time.deltaTime);
		}
		
	}
	
	public void PlayerMove(int pNo){
			moveTimer-=1*Time.deltaTime;
			turnTimer-=1*Time.deltaTime;

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
				if((anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_walkR")||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_walkL")||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_walk")||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_idle")||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_stand"))&&(transform.position.x<(area[worldNo,0]+area[worldNo,2] / 2)&&transform.position.x>(area[worldNo,0]-area[worldNo,2] / 2)&&transform.position.z<(area[worldNo,1]+area[worldNo,3] / 2)&&transform.position.z>(area[worldNo,1]-area[worldNo,3] / 2))){
					if(player_power<150){
						player_power += (3)*Time.deltaTime;
					}

					if(Input.GetKey(KeyCode.D)){
						if(turnTimer<=0){
							anim.Play("playerAnimation_walk");
							transform.Rotate(Vector3.forward*(180)*Time.deltaTime);
						}
						else{
							anim.Play("playerAnimation_walkR");
							transform.Translate(-mySpeed*2*speed_effect*Time.deltaTime, 0, 0);
							turnTimer=0.1;
						}
					}
					else if(Input.GetKey(KeyCode.A)){
						if(turnTimer<=0){
							anim.Play("playerAnimation_walk");
							transform.Rotate(Vector3.forward*(-180)*Time.deltaTime);
						}
						else{
							anim.Play("playerAnimation_walkL");
							transform.Translate(mySpeed*2*speed_effect*Time.deltaTime, 0, 0);
							turnTimer=0.1;
						}
					}
					else if( (Input.GetKeyUp(KeyCode.D)) || (Input.GetKeyUp(KeyCode.A)) ){
						//if(turnTimer<=0){
							anim.Play("playerAnimation_idle");
							turnTimer=0.1;
						//}
					}
					if(Input.GetKey(KeyCode.W)){
						anim.Play("playerAnimation_walk");
						if(moveTimer<=0){
							anim.speed=1;
							transform.Translate(0, mySpeed*speed_effect*Time.deltaTime, 0);
						}
						else{
							anim.speed=2;
							transform.Translate(0, mySpeed*2*speed_effect*Time.deltaTime, 0);
							moveTimer=0.1;
						}
					}
					else if(Input.GetKey(KeyCode.S)){
						anim.Play("playerAnimation_walk");
						if(moveTimer<=0){
							anim.speed=1;
							transform.Translate(0, -mySpeed/2*speed_effect*Time.deltaTime, 0);
						}
						else{
							anim.speed=6;
							transform.Translate(0, -mySpeed*3*speed_effect*Time.deltaTime, 0);
						}
					}
					else if( (Input.GetKeyUp(KeyCode.W)) || (Input.GetKeyUp(KeyCode.S)) ){
						//if(moveTimer<=0){
							anim.Play("playerAnimation_idle");
							anim.speed=1;
							moveTimer=0.3;
						//}
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
			if(Mathf.Abs(player2p.transform.position.x-player1p.transform.position.x)>20 || Mathf.Abs(player2p.transform.position.z-player1p.transform.position.z)>20){
				player2p.transform.position=player1p.transform.position;
			}
			if(Time.timeScale!=1&&GetComponent<HPControl>().HP<=0){
				transform.position+=new Vector3((transform.position.x-Target.transform.position.x)*(float)0.5*Time.deltaTime,0,(transform.position.z-Target.transform.position.z)*(float)0.5*Time.deltaTime);
			}
			else if(GetComponent<HPControl>().cantMoveTimer>0){
				GetComponent<HPControl>().cantMoveTimer-=1*Time.deltaTime;
				transform.position+=new Vector3((transform.position.x-Target.transform.position.x)*3*Time.deltaTime,0,(transform.position.z-Target.transform.position.z)*3*Time.deltaTime);
			}
			else if(Time.timeScale==1){
				if((anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_idle")||anim.GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_stand"))&&(transform.position.x<(area[worldNo,0]+area[worldNo,2] / 2)&&transform.position.x>(area[worldNo,0]-area[worldNo,2] / 2)&&transform.position.z<(area[worldNo,1]+area[worldNo,3] / 2)&&transform.position.z>(area[worldNo,1]-area[worldNo,3] / 2))){
					if(player_power<150){
						player_power += (3)*Time.deltaTime;
					}

					if(Input.GetKey(KeyCode.K)){
						if(turnTimer<=0){
							transform.Rotate(Vector3.forward*(180)*Time.deltaTime);
						}
						else{
							transform.Translate(-mySpeed*2*speed_effect*Time.deltaTime, 0, 0);
							turnTimer=0.2;
						}
					}
					else if(Input.GetKey(KeyCode.H)){
						if(turnTimer<=0){
							transform.Rotate(Vector3.forward*(-180)*Time.deltaTime);
						}
						else{
							transform.Translate(mySpeed*2*speed_effect*Time.deltaTime, 0, 0);
							turnTimer=0.2;
						}
					}
					else if( (Input.GetKeyUp(KeyCode.K)) || (Input.GetKeyUp(KeyCode.H)) ){
						//if(turnTimer<=0){
							turnTimer=0.2;
						//}
					}
					if(Input.GetKey(KeyCode.U)){
						if(moveTimer<=0){
							transform.Translate(0, mySpeed*speed_effect*Time.deltaTime, 0);
						}
						else{
							transform.Translate(0, mySpeed*2*speed_effect*Time.deltaTime, 0);
							moveTimer=0.1;
						}
					}
					else if(Input.GetKey(KeyCode.J)){
						if(moveTimer<=0){
							transform.Translate(0, -mySpeed/2*speed_effect*Time.deltaTime, 0);
						}
						else{
							transform.Translate(0, -mySpeed*3*speed_effect*Time.deltaTime, 0);
						}
					}
					else if( (Input.GetKeyUp(KeyCode.U)) || (Input.GetKeyUp(KeyCode.J)) ){
						//if(moveTimer<=0){
							moveTimer=0.3;
						//}
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
		
		GetComponent<CharacterSkill>().CharacterSkillControl(1,0,0,gameObject);
		SpSlider.value=player_power;
		//--------------------------------p1-------------------------------------
		if(pNo==1){
			
			if(Input.GetKeyDown(KeyCode.Q)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,1,0,gameObject);
			}
			else if(Input.GetKey(KeyCode.Q)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,1,1,gameObject);
			}
			else if(Input.GetKeyUp(KeyCode.Q)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,1,2,gameObject);
			}
			
			if(Input.GetKeyDown(KeyCode.E)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,2,0,gameObject);
			}
			else if(Input.GetKey(KeyCode.E)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,2,1,gameObject);
			}
			else if(Input.GetKeyUp(KeyCode.E)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,2,2,gameObject);
			}
			
			if(Input.GetKeyDown(KeyCode.Z)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,3,0,gameObject);
			}
			else if(Input.GetKey(KeyCode.Z)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,3,1,gameObject);
			}
			else if(Input.GetKeyUp(KeyCode.Z)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,3,2,gameObject);
			}
			
			if(Input.GetKeyDown(KeyCode.X)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,4,0,gameObject);
			}
			else if(Input.GetKey(KeyCode.X)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,4,1,gameObject);
			}
			else if(Input.GetKeyUp(KeyCode.X)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,4,2,gameObject);
			}
			
			if(Input.GetKeyDown(KeyCode.C)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,5,0,gameObject);
			}
			else if(Input.GetKey(KeyCode.C)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,5,1,gameObject);
			}
			else if(Input.GetKeyUp(KeyCode.C)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,5,2,gameObject);
			}
			
		}
		
		//--------------------------------p2-------------------------------------
		if(pNo==2){
			
			if(Input.GetKeyDown(KeyCode.Y)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,1,0,gameObject);
			}
			else if(Input.GetKey(KeyCode.Y)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,1,1,gameObject);
			}
			else if(Input.GetKeyUp(KeyCode.Y)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,1,2,gameObject);
			}
			
			if(Input.GetKeyDown(KeyCode.I)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,2,0,gameObject);
			}
			else if(Input.GetKey(KeyCode.I)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,2,1,gameObject);
			}
			else if(Input.GetKeyUp(KeyCode.I)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,2,2,gameObject);
			}
			
			if(Input.GetKeyDown(KeyCode.B)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,3,0,gameObject);
			}
			else if(Input.GetKey(KeyCode.B)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,3,1,gameObject);
			}
			else if(Input.GetKeyUp(KeyCode.B)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,3,2,gameObject);
			}
			
			if(Input.GetKeyDown(KeyCode.N)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,4,0,gameObject);
			}
			else if(Input.GetKey(KeyCode.N)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,4,1,gameObject);
			}
			else if(Input.GetKeyUp(KeyCode.N)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,4,2,gameObject);
			}
			
			if(Input.GetKeyDown(KeyCode.M)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,5,0,gameObject);
			}
			else if(Input.GetKey(KeyCode.M)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,5,1,gameObject);
			}
			else if(Input.GetKeyUp(KeyCode.M)){
				GetComponent<CharacterSkill>().CharacterSkillControl(1,5,2,gameObject);
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
