using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCal : MonoBehaviour
{
    static int point = 0;
	Text text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // tx.text = point.ToString();  
    }
	
	public void addPoint(int getPoint){
        point += getPoint;
		 MainGame mg = GameObject.Find("GameObject").GetComponent<MainGame>();
		 text = mg.tx;
		 text.text = point.ToString();
		

    }
	
	public int getPoint(){
	   return point;
    }
	
	
}
