using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class OpenWorldSpawn : MonoBehaviour
{
	public int enemyNo = 20;
	public int enemyNoCount;
	Random rnd = new Random();
	public GameObject spawnEnemyObj;
	public GameObject[] enemy;
	public int spawnStepCount;
	public double spawnTimer;
	public double setSpawnTimer;
    // Start is called before the first frame update
    void Start()
    {
		if(setSpawnTimer<10){
			setSpawnTimer=30;
		}
		spawnTimer=setSpawnTimer;
		spawnStepCount=0;
		enemy = new GameObject[enemyNo];
		for(int i=0;i<enemy.Length;i++){
			enemy[i] = Instantiate(spawnEnemyObj,transform.position, transform.rotation) as GameObject;
			//enemy[i].transform.position=new Vector3(rnd.Next(50,450),5,rnd.Next(50,450));86.5 77.5
			enemy[i].transform.position=new Vector3(rnd.Next(87-50,87+50),15,rnd.Next(77-50,77+50));
		}
		spawnStepCount+=1;
    }

    // Update is called once per frame
    void Update()
    {
		spawnTimer-=Time.deltaTime;
        for(int i=0;i<enemy.Length;i++){
			if(enemy[i].transform.position.y<0){
				enemy[i].transform.position=new Vector3(rnd.Next(87-50,87+50),rnd.Next(5,15),rnd.Next(77-50,77+50));
			}
		}
		if(spawnTimer<=0){
			spawnTimer=setSpawnTimer;
			Array.Resize(ref enemy, (spawnStepCount+1) * enemyNo);
			for(int i=spawnStepCount * enemyNo;i<enemy.Length;i++){
				enemy[i] = Instantiate(spawnEnemyObj,transform.position, transform.rotation) as GameObject;
				//enemy[i].transform.position=new Vector3(rnd.Next(50,450),5,rnd.Next(50,450));86.5 77.5
				enemy[i].transform.position=new Vector3(rnd.Next(87-50,87+50),15,rnd.Next(77-50,77+50));
				enemy[i].transform.localScale=new Vector3(1+spawnStepCount*(float)0.01,1+spawnStepCount*(float)0.01,1+spawnStepCount*(float)0.01);
			}
			spawnStepCount+=1;
		}
    }
}
