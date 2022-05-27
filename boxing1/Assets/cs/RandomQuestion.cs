using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random=System.Random;
using UnityEngine.UI;

public class RandomQuestion : MonoBehaviour
{
	public int[] num;
	public int ans;
	public Text text;
	public Text text1;
	public Text text2;
	public Text text3;
	public Text text4;
	public GameObject imgTick;
	public GameObject imgX;
	Random rnd = new Random();
	public double timer;
    // Start is called before the first frame update
    void Start()
    {
        text=text.GetComponent<Text>();
		RndQues();
		timer=1;
    }

    // Update is called once per frame
    void Update()
    {
		if(timer<=0){
			imgTick.gameObject.SetActive(false);
			imgX.gameObject.SetActive(false);
			timer=3;
		}
		if(timer<2&&timer>0){
			timer-=Time.deltaTime;
		}
		
    }
	
	public void RndQues(){
		num = new int[7];
		num[0]=rnd.Next(0,10);
		num[1]=rnd.Next(0,3);
		num[2]=rnd.Next(0,10);
		ans=rnd.Next(3,7);
		for(int i=3;i<7;i++){
			num[i]=i;
		}
		if(num[1]==0){// +
			for(int i=3;i<7;i++){
				num[i]=num[0]+num[2]+i-2;
			}
			num[ans]=num[0]+num[2];
			text.text=num[0]+" + "+num[2];
		}
		else if(num[1]==1){// -
			for(int i=3;i<7;i++){
				num[i]=num[0]-num[2]+i-2;
			}
			num[ans]=num[0]-num[2];
			text.text=num[0]+" - "+num[2];
		}
		else if(num[1]==2){// *
			for(int i=3;i<7;i++){
				num[i]=num[0]*num[2]+i-2;
			}
			num[ans]=num[0]*num[2];
			text.text=num[0]+" * "+num[2];
		}
		
		text1.text = num[3].ToString();
		text2.text = num[4].ToString();
		text3.text = num[5].ToString();
		text4.text = num[6].ToString();
		/*for(int i=3;i<i+4;i++){
			text.text += "\n"+num[i];
		}*/
		
	}
	
	public void TestAns(int ansNo){
		if(ansNo==ans){
			imgTick.gameObject.SetActive(true);
			LoadPlayerLv.UpdatePlayerExp(10);
			timer=1;
			RndQues();
		}
		else{
			imgX.gameObject.SetActive(true);
			LoadPlayerLv.UpdatePlayerExp(0);
			timer=1.5;
		}
		//RndQues();
	}
}
