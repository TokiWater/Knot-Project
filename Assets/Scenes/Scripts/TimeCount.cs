using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class TimeCount : MonoBehaviour
{
    int time = 0;
	int countDown;
	public Text countText;
    // Start is called before the first frame update
    void Start()
    {
        countDown = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if(time == 1){
		   countText.text = countDown.ToString();
		   countDown = countDown - 1;
	     }
		 
		 if(time == 10){
		   countText.text = countDown.ToString();
		   countDown = countDown - 1;
	     }

		 if(time == 20){
		   countText.text = countDown.ToString();
		   countDown = countDown - 1;
		   
	     }
		 
		 if(time == 30){		   
		   SceneManager.LoadScene("Knot Project");
	     }	    
			
		time++;
    }
}
