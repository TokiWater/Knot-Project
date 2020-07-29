using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class RopeG : MonoBehaviour {

    public GameObject[] vertices = new GameObject[84];
	public GameObject[] vessels = new GameObject[83];
	public GameObject[] physicalEffects = new GameObject[1];

    LineRenderer line;
	bool problemCheck = true;
	int flag = 0;
	int flagS = 0;
	int flagE = 0;
	float x = 0.0f;
    float z = 0.0f;
    float spaceX = 0.3f;
	float randRadTheta = 0.0f;
	float randRadPhi = 0.0f;
	float objX = 0.0f;
	float objY = 0.0f;
    float objZ = 0.0f;
	float mag;
	float startPos;
	float endPos;
	float thrust = 400.0f;
	float test;
	Rigidbody rb;
	Wind wd;
	int startTime = 0;
	static int problemNum = 1859;
	static int seedNum = 18;
	int answer = 0; //0の時解ける1の時結び目
	CapsuleCollider cc;
	Vector3 vector = Vector3.zero;
	Vector3 vectorStart;
	Vector3 vectorEnd;
	Vector3 center = new Vector3(1.0f, 0.0f, 0.0f);
	Vector3 end = new Vector3(2.5f, 0.0f, 0.0f);
	Vector3 pull  = new Vector3(0.1f, 0.0f, 0.0f);
    Vector3 prev;
	Vector3 ppei = Vector3.zero;
    Vector3 moveDirection = Vector3.zero;
 	Vector3 check = Vector3.zero;
    Vector3 pullVectorF;
	Vector3 pullVectorB;
	Vector3 posVec;
	Vector3 thrustVec = new Vector3(1.0f, 0.4f, 0.0f);
	Vector3 thrustMVec = new Vector3(-1.0f, 0.4f, 0.0f);
	Vector3 thrustVecStart = new Vector3(0.0f, 1.0f, 0.0f);
	Vector3[] vectorStartPrev = new Vector3[79];
	Vector3[] vectorEndPrev = new Vector3[79];
	TextAsset csvFile;
	string filePath;
	
	int time = 0;
	
	List<string[]> csvDatas = new List<string[]>();
	
	
	
	
	//int spaceZ = 1;
	
    void Awake(){
		
		
		   int idnum = 0;
	    	int problemID = 40;

				csvFile = Resources.Load("problemso0") as TextAsset;
				StringReader reader = new StringReader(csvFile.text);
				while (reader.Peek() != -1){
					string line = reader.ReadLine();
					csvDatas.Add(line.Split(','));
				}
					Debug.Log("これはRopeGのスクリプtです#" + csvDatas[0][3] + "#" + csvDatas[0][4] + "#" + csvDatas[1][5]);
				foreach(GameObject v in vertices){
						posVec = new Vector3(float.Parse(csvDatas[problemID * 84 + idnum][3]), float.Parse(csvDatas[problemID * 84 + idnum][4]), float.Parse(csvDatas[problemID * 84 + idnum][5]));
					v.transform.position = posVec;
					idnum++;
				}
		

   		
	
	}


    void Start () 
    {		

    }
	
    void Update () 
    {
        int idx = 0;

		if(time == 100){

		
		   switch(seedNum){
		      case 0:
		         csvFile = Resources.Load("xSeed1") as TextAsset;
				 filePath = "problemso0.csv";
				 answer = 0;
				 break;
				 
			  case 1:
		         csvFile = Resources.Load("xSeed2") as TextAsset;
				 filePath = "problemso11.csv";		 				 
				 answer = 0;
				 break;

		      case 2:
		         csvFile = Resources.Load("xSeed3") as TextAsset;
			  	 filePath = "problemsx3.csv";	 
				 answer = 1;
				 break;
				 
			  case 3:
		         csvFile = Resources.Load("xSeed4") as TextAsset;
				  filePath = "problemsx4.csv";
				 answer = 1;
				 break;

		      case 4:
		         csvFile = Resources.Load("xSeed5") as TextAsset;
				 filePath = "problemsx5.csv";
				 answer = 1;
				 break;
				 
			  case 5:
		         csvFile = Resources.Load("xSeed6") as TextAsset;
				 filePath = "problemsx6.csv";
				 answer = 1;
				 break;

		      case 6:
		         csvFile = Resources.Load("xSeed7") as TextAsset;
				 filePath = "problemsx7.csv";
				 answer = 1;
				 break;
				 
			  case 7:
		         csvFile = Resources.Load("xSeed8") as TextAsset;
				 filePath = "problemsx8.csv";		 
				 answer = 1;
				 break;

		      case 8:
		         csvFile = Resources.Load("xSeed9") as TextAsset;
				 filePath = "problemsx9.csv"; 
				 answer = 1;
				 break;
				 
			  case 9:
		         csvFile = Resources.Load("xSeed10") as TextAsset;
				 filePath = "problemsx10.csv";
				 answer = 1;
				 break;

		      case 10:
		         csvFile = Resources.Load("oSeed1") as TextAsset;
				 filePath = "problemso1.csv";
				 answer = 0;
				 break;
				 
			  case 11:
		         csvFile = Resources.Load("oSeed2") as TextAsset;
				filePath = "problemso2.csv";
				 answer = 0;
				 break;

		      case 12:
		         csvFile = Resources.Load("oSeed3") as TextAsset;
				filePath = "problemso3.csv";
				 answer = 0;
				 break;
				 
			  case 13:
		         csvFile = Resources.Load("oSeed4") as TextAsset;
				 				 filePath = "problemso4.csv";
				 answer = 0;
				 break;

		      case 14:
		         csvFile = Resources.Load("oSeed5") as TextAsset;
				 				 filePath = "problemso5.csv";
				 answer = 0;
				 break;
				 
			  case 15:
		         csvFile = Resources.Load("oSeed6") as TextAsset;
				 				 filePath = "problemso6.csv";
				 answer = 0;
				 break;

		      case 16:
		         csvFile = Resources.Load("oSeed7") as TextAsset;
				 				 filePath = "problemso7.csv";
				 answer = 0;
				 break;
				 
			  case 17:
		         csvFile = Resources.Load("oSeed8") as TextAsset;
				 				 filePath = "problemso8.csv";
				 answer = 0;
				 break;

		      case 18:
		         csvFile = Resources.Load("oSeed9") as TextAsset;
				 				 filePath = "problemso9.csv";
				 answer = 0;
				 break;
				 
			  case 19:
		         csvFile = Resources.Load("oSeed10") as TextAsset;
				 				 filePath = "problemso10.csv";
				 answer = 0;
				 break;

           }				 
		   StringReader reader = new StringReader(csvFile.text);
		   
		   csvDatas = new List<string[]>();
		   
		   while (reader.Peek() != -1){
		       string line = reader.ReadLine();
			   csvDatas.Add(line.Split(','));
		   }
		   Debug.Log("#" + csvDatas[0][1] + "#" + csvDatas[0][2] + "#" + csvDatas[1][0]);

		   
		   flag = 1;
        }		   
		
		if (startTime == 5){
		     int idnum = 0;
		     foreach(GameObject v in vertices){
		         posVec = new Vector3(float.Parse(csvDatas[idnum][1]), float.Parse(csvDatas[idnum][2]), float.Parse(csvDatas[idnum][3]));
			     v.transform.position = posVec;
			     idnum++;
		   }		
		}

		if (startTime == 100  || startTime == 300){	
	        foreach(GameObject v in physicalEffects){
			   wd = v.GetComponent<Wind>();
			   int objNum = Random.Range(3, 80);
			   GameObject obj =  vertices[objNum];
			   
			   float windX = obj.transform.position.x;
			   float windY = 0;
			   float windZ = obj.transform.position.z;
			   Vector3 windVector = new Vector3(windX, windY, windZ);
			   v.transform.position = windVector;
			   
				if(windX < 1){
			      randRadTheta = Random.Range( - 2 * Mathf.PI / 3.0f, 2 * Mathf.PI / 3.0f); 
			   }else{
				  randRadTheta = Random.Range(  Mathf.PI / 3.0f,  5.0f * Mathf.PI / 3.0f); 
			   }	   

			   wd.velocity.x = 5 * Mathf.Cos(randRadTheta);
			   wd.velocity.z = 5 * Mathf.Sin(randRadTheta);
			   wd.velocity.y = 5;
			}		
		}			
		
		if (startTime == 200 || startTime == 380){
			foreach(GameObject v in physicalEffects){
			   wd = v.GetComponent<Wind>();
			   wd.velocity.x = 0;
			   wd.velocity.y = 0;
			   wd.velocity.z = 0;
			}
					
		}
		
		if (startTime == 600){
		   int idnum = 0;
		   
		   GameObject startObj = vertices[0];
		   float startObjX = startObj.transform.position.x;
		   GameObject startPre = vertices[1];
		   Vector3 startV = startObj.transform.position - startPre.transform.position;
		   startV.y = 0;
		   
		   GameObject endObj = vertices[83];
		   float endObjX = endObj.transform.position.x;
		   GameObject endPre = vertices[82];
		   Vector3 endV = endObj.transform.position - endPre.transform.position;
		   endV.y = 0;
		   
		   foreach(GameObject v in vertices){
		      float heightCheck = v.transform.position.y;
			  float widthCheck = v.transform.position.x;
			  Vector3 boundCheckS = v.transform.position - startObj.transform.position;
			  boundCheckS.y = 0;
			  Vector3 boundCheckE = v.transform.position - endObj.transform.position;
			  boundCheckE.y = 0;
			  
              if(heightCheck  >  0.25){
			     Debug.Log("一部高すぎます");
				 problemCheck = false;
		      }
			  
			 // if(startObjX > widthCheck || endObjX < widthCheck){
			 //    Debug.Log("はみ出ています");
			//	 problemCheck = false;
			//  }
			if(Vector3.Dot(boundCheckS.normalized, startV.normalized) > 0.4 || Vector3.Dot(boundCheckE.normalized, endV.normalized) > 0.4){
			     Debug.Log("食い込んでいます");
				 problemCheck = false;
			}
			  
		   }
		   
		   if(problemCheck == true){
		      Debug.Log("問題生成しました");
		   
		     // filePath = "problems.csv";
		   
		      foreach(GameObject v in vertices){
			      float fileX = v.transform.position.x;
			      float fileY = v.transform.position.y;
			      float fileZ = v.transform.position.z;
		          string myString = problemNum + "," + answer + "," + "#" + idnum + "#" + "," + fileX + "," + fileY + "," + fileZ + "\n";
			      File.AppendAllText(filePath, myString);	
                  idnum++;	   
		      }
			  

		     	problemNum++;
			
			   if(problemNum % 100 == 0){
			      flag = 0;
			      time = 0;
			      seedNum++;
		       }
			   
		       SceneManager.LoadScene("Knot Project");			   
			   
			   
		    }
			
			problemCheck = true;
			startTime = 0;
	    }		
		
		
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
		

		if(flag == 1 || flag == 2){
		   startTime++;
	   }
		
		
		time++;
    }
	
	void createVessel(Vector3 p1, Vector3 p2, int num){
        Vector3 pos = Vector3.Lerp(p1, p2, 0.5f);
		//Vector3 ppos = Vector3.Lerp(pp1, pp2, 0.5f);
		Vector3 diff = p2 - p1;
		mag = diff.magnitude;
		GameObject segObj = vessels[num];
	//	Rigidbody rb = segObj.GetComponent<Rigidbody> ();
		segObj.transform.position = pos;
		segObj.transform.up = diff;
		segObj.transform.localScale = new Vector3(0.2f, mag / 2.0f, 0.2f);
		cc = segObj.GetComponent<CapsuleCollider> ();
		cc.height =  mag / 2.0f ;
		//rb.velocity = ppos - pos;
    }
}
