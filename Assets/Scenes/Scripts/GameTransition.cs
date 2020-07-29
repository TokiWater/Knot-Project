using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class GameTransition : MonoBehaviour
{
    // Start is called before the first frame update
	int time;
	bool f;
	public Button btnK;
	public Button btnN;
	Image image;
	
	
	
    void Start()
    {
        time = 0;
		f = true;
		
	
    }

    // Update is called once per frame
    void Update()
    {
	    if(f == false){
			time++;
		}
		
		if(time == 200){
          SceneManager.LoadScene("BeforeGame");	 
    	}
	}
	
	void OnlickKnotButton()
	{
	    MainGame mg = GameObject.Find("GameObject").GetComponent<MainGame>();
		if(mg.answer == 1){
		   Debug.Log("正解です");
	       PointCal pc = GameObject.Find("Point").GetComponent<PointCal>();		   
		   
		   pc.addPoint(1);
		   image = mg.img;
		   image.enabled = true;
		   //mg.point = mg.point + 1;
		   
		}else{
		   Debug.Log("不正解です");
		}
		
		f = false;
		
		btnK.interactable = false;
		btnN.interactable = false;
	}
	
	void OnlickNotButton()
	{
	    MainGame mg = GameObject.Find("GameObject").GetComponent<MainGame>();
		if(mg.answer == 0){
		   Debug.Log("正解です");
		   PointCal pc = GameObject.Find("Point").GetComponent<PointCal>();		   
		   
		   pc.addPoint(1);
		   image = mg.img;
		   image.enabled = true;		   
		   //mg.point = mg.point + 1;
		   
		}else{
		   Debug.Log("不正解です");
		}
		
		f = false;
		
		btnK.interactable = false;
		btnN.interactable = false;
	}
	
	
}
