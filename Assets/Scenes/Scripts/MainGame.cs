using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainGame : MonoBehaviour
{
    int time = 0;
	int knotGroupNum;
	int inGroupNum;
	public int answer;
    //static int point = 0;
	int idnum;
	int idx;
	int getp;
	string filePath;
    TextAsset csvFile;
	
	CapsuleCollider cc;
	Vector3 vector = Vector3.zero;
	Vector3 vectorStart;
	Vector3 vectorEnd;
	
	float mag;	
	float startPos;
	float endPos;
	Vector3 posVec;
	Rigidbody rb;
	
    public GameObject[] vertices = new GameObject[84];
	public GameObject[] vessels = new GameObject[83];
	
	public Text tx;
	public Image img;
		
	List<string[]> csvDatas = new List<string[]>();

    // Start is called before the first frame update
    void Start()
    {
	
		    img.enabled = false;
	
			knotGroupNum = Random.Range(0, 19); 

		    //knotGroupNum = 3;
			inGroupNum = Random.Range(0,99);
		    //inGroupNum = 44;
			switch(knotGroupNum){
		      case 0:
				 filePath = "problemso0";
			     answer = 0;
				 break;
				 
			  case 1:
				 filePath = "problemso11";		 				 
				 answer = 0;
				 break;

		      case 2:
			  	 filePath = "problemsx3";	  //answer0に変更
				 answer = 0;
				 break;
				 
			  case 3:
				  filePath = "problemsx4";
				 answer = 1;
				 break;

		      case 4:
				 filePath = "problemsx5";
				 answer = 1;
				 break;
				 
			  case 5:
				 filePath = "problemsx6";
				 answer = 1;
				 break;

		      case 6:
				 filePath = "problemsx7";
				 answer = 1;
				 break;
				 
			  case 7:
				 filePath = "problemsx8";		 
				 answer = 1;
				 break;

		      case 8:
				 filePath = "problemsx9"; 
				 answer = 1;
				 break;
				 
			  case 9:
				 filePath = "problemsx10";
				 answer = 1;
				 break;

		      case 10:
				 filePath = "problemso1";
				 answer = 0;
				 break;
				 
			  case 11:
				filePath = "problemso2";
				 answer = 0;
				 break;

		      case 12:
				filePath = "problemso3";
				 answer = 0;
				 break;
				 
			  case 13:
				filePath = "problemso4";
				 answer = 0;
				 break;

		      case 14:
				filePath = "problemso5";
				 answer = 0;
				 break;
				 
			  case 15:
				filePath = "problemso6";
				 answer = 0;
				 break;

		      case 16:
				filePath = "problemso7";
				 answer = 0;
				 break;
				 
			  case 17:
				filePath = "problemso8";
				 answer = 0;
				 break;

		      case 18:
				filePath = "problemso9";     //answer1に変更
				 answer = 1;
				 break;
				 
			  case 19:
				filePath = "problemso10";
				 answer = 0;
				 break;

			}

		

		        idnum = 0;
			    csvFile = Resources.Load(filePath) as TextAsset;
				StringReader reader = new StringReader(csvFile.text);
				while (reader.Peek() != -1){
					string line = reader.ReadLine();
					csvDatas.Add(line.Split(','));
				}
			    
				foreach(GameObject v in vertices){
					posVec = new Vector3(float.Parse(csvDatas[inGroupNum * 84 + idnum][3]), float.Parse(csvDatas[inGroupNum * 84 + idnum][4]), float.Parse(csvDatas[inGroupNum * 84 + idnum][5]));
					v.GetComponent<MeshRenderer>().enabled = false;
					v.transform.position = posVec;
					idnum++;
				}
				foreach(GameObject v in vessels){
				   v.GetComponent<MeshRenderer>().enabled = false;
			    }
				
				if(answer == 0){
					Debug.Log("これは解けます#" + knotGroupNum + "#" + inGroupNum );
				}else{
			    	Debug.Log("これは解けません#" + knotGroupNum + "#" + inGroupNum);
				}
				
		        PointCal pc = GameObject.Find("Point").GetComponent<PointCal>();	
				getp = pc.getPoint();
				
				tx.text = getp.ToString();
		
    }

    // Update is called once per frame
    void Update()
    {
	
	

      
		   idx = 0;
		
		   foreach (GameObject v in vertices)
		   {		
		       if(idx == 0){
			    	vectorStart = v.transform.position;
		       }
		       if(idx != 0){
			      vectorEnd = v.transform.position;
		    	   createVessel(vectorStart, vectorEnd, idx - 1);
			      vectorStart = v.transform.position;
		       }
			   idx++;
		   }

      if(time > 100){
	       foreach (GameObject v in vertices)
		   {
		      Destroy(v.GetComponent<HingeJoint> ());
			  Destroy(v.GetComponent<Rigidbody> ());
			  v.GetComponent<MeshRenderer>().enabled = true;  
		      //Rigidbody rb = v.GetComponent<Rigidbody> ();
			  //rb.isKinematic = true;
			  //rb.useGravity = false;
		   }
		   
		   foreach (GameObject v in vessels)
		   {
		      Destroy(v.GetComponent<HingeJoint> ());
			  Destroy(v.GetComponent<Rigidbody> ());
			  v.GetComponent<MeshRenderer>().enabled = true;		  
		      //Rigidbody rb = v.GetComponent<Rigidbody> ();
			  //rb.isKinematic = true;
			  //rb.useGravity = false;
		   }		   
	   }
		
		time++;
    }
	
	
	public void createVessel(Vector3 p1, Vector3 p2, int num){
        Vector3 pos = Vector3.Lerp(p1, p2, 0.5f);
		Vector3 diff = p2 - p1;
		mag = diff.magnitude;
		GameObject segObj = vessels[num];
		segObj.transform.position = pos;
		segObj.transform.up = diff;
		segObj.transform.localScale = new Vector3(0.2f, mag / 2.0f, 0.2f);
		cc = segObj.GetComponent<CapsuleCollider> ();
		cc.height = 0.1f +  mag / 2.0f ;
    }
	

	
}
