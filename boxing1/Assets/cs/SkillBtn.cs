using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class SkillBtn : MonoBehaviour
{
	public GameObject btn;
	public int num;
	public int numTest;
	public double timer;
	GameObject[] clone = new GameObject[5];
    // Start is called before the first frame update
    void Start()
    {
		LoadBtn();
	}

    // Update is called once per frame
    void Update()
    {
        timer-=1*Time.deltaTime/Time.timeScale;
		if(timer<=0){
			Time.timeScale=1;
			gameObject.SetActive(false);
		}
    }
	
	public void LoadBtn(){
		Random rnd = new Random();
		num=1;
		numTest=1;
		for(int i=0;i<5;i++){
			Destroy(clone[i]);
			clone[i] = Instantiate(btn, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			clone[i].GetComponent<SkillBtn2>().num=num;
			clone[i].GetComponent<SkillBtn2>().myText();
			num+=1;
			clone[i].GetComponent<RectTransform>().SetParent(transform);
			clone[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(rnd.Next(-750,750),rnd.Next(-300,300));//200 100
		}
	}
	
	public int CheckNum(int num){
		if(numTest==num){
			numTest+=1;
			return 1;
		}
		else{
			return 0;
		}
	}
}
