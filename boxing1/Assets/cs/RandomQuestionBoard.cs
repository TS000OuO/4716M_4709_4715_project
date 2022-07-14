using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomQuestionBoard : MonoBehaviour
{
	public GameObject QuestionUi;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	//gameObject.SetActive(false);
	public void OnTriggerStay(){
		QuestionUi.gameObject.SetActive(true);
	}
	
	public void OnTriggerExit(){
		//QuestionUi.GetComponent<RandomQuestion>().timer=0;
		QuestionUi.gameObject.SetActive(false);
		QuestionUi.GetComponent<RandomQuestion>().timer=0;
	}
}
