using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeThrow : MonoBehaviour
{
	public GameObject[] atkObject = new GameObject[2*2];
	double timer;
	public int atkno;
	public GameObject atker;
    // Start is called before the first frame update
    void Start()
    {
		//transform.Translate(-0.25f,1.5f,0.25f);
		atkno=0;
        timer=1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 15*Time.deltaTime, 0);
		timer-=1*Time.deltaTime;
		if(timer<=0){
			Destroy(gameObject);
		}
    }
	
	void OnTriggerEnter(Collider col){
		for(int i=0;i<=atkno;i++){
			if(GameObject.ReferenceEquals( col.gameObject, atkObject[i])||col.gameObject.tag!=("Enemy")){
				i=4;
			}
			if(i==atkno){
				atkno+=1;
				atkObject[atkno]=col.gameObject;
				col.gameObject.GetComponent<HPControl>().Damage(10,atker);
				
			}
			if(atkno>=3){
				Destroy(gameObject);
			}
		}
	}
}
