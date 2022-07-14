using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtk : MonoBehaviour
{
	Animator anim;
	public GameObject enemy1;
	public GameObject player;    
   
	void Start()
    {
        anim = player.GetComponent<Animator>();
			

	}

	void Update()
    {
		GameObject controller = GameObject.FindWithTag("GameController");
		if(player.GetComponent<HPControl>().cantMoveTimer>0){
			controller.GetComponent<WorldController1>().Knockback(player.GetComponent<PlayerControl>().atkno, player, enemy1);
		}
	}
	
	void OnTriggerEnter (Collider col) {
		GameObject controller = GameObject.FindWithTag("GameController");

		if (col.gameObject.tag=="Enemy"){
			player.GetComponent<PlayerControl>().enemyForSlider=col;
			player.GetComponent<PlayerControl>().enemyHpSliderTimer=5;
		}
		if (col.gameObject.tag == "Enemy"&&player.GetComponent<PlayerControl>().atkno!=0&&!col.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("playerAnimation_skill1")&&Time.timeScale==1) {

			col.gameObject.GetComponent<HPControl>().Damage(player.GetComponent<CharLv>().charAtkValue+player.GetComponent<PlayerControl>().skillAtk,player);
		}
		if(col.gameObject.tag=="Sandbag"&&player.GetComponent<PlayerControl>().atkno!=0){
			col.gameObject.GetComponent<HPControl>().Damage(player.GetComponent<CharLv>().charAtkValue,player);
			player.GetComponent<PlayerControl>().enemyForSlider=col;
			player.GetComponent<PlayerControl>().enemyHpSliderTimer=3;
		}
	}
	
	void OnTriggerStay(Collider col){
		GameObject controller = GameObject.FindWithTag("GameController");


		if (col.gameObject.tag == "Enemy") {
			if(player.GetComponent<PlayerControl>().atkno > 0){
				controller.GetComponent<WorldController1>().Knockback(player.GetComponent<PlayerControl>().atkno, player,col);//col enemy1);
			}
		}
		if (col.gameObject.tag == "Sandbag") {
			controller.GetComponent<WorldController1>().Knockback(1, col.gameObject, player);
		}
	}
}
