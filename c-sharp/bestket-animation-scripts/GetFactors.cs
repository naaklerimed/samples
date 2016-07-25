using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;


public class GetFactors : MonoBehaviour {
	public GameObject upperLegL;
	public GameObject lowerLegL;
	public GameObject upperLegR;
	public GameObject lowerLegR;
	public GameObject upperArmL;
	public GameObject lowerArmL;
	public GameObject upperArmR;
	public GameObject lowerArmR;
	public GameObject leftFoot;
	public GameObject rightFoot;
    	public GameObject center;
    	public GameObject hoopDist;
    	public GameObject fronthoop;
    	public GameObject head;
    	public GameObject ftline;
    	public GameObject body;
    //data to be set
    	public float lowerLegAngle;
    	public float upperLegAngle;
    	public float lowerArmAngle;
    	public float upperArmAngle;
    	public float footAngle;
    	private float shooterHeight;
    	private Transform[] children = null;
    	string _fileName = "C:\\Users\\"+System.Environment.UserName+"\\Documents\\BestKet\\FactorData.txt";

//reads and sets data from factorData.txt
    public void readFile()
    {

        List<string> lines = new List<string>();
	

        using (StreamReader r = new StreamReader(_fileName))
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
				lines.Add(line);
            }
        }
       
        upperArmAngle = float.Parse(lines[1]);
	lowerArmAngle = float.Parse(lines[0]);
	upperLegAngle = float.Parse(lines[3]);
	lowerLegAngle = float.Parse(lines[2]);
        footAngle = float.Parse(lines[4]);
        shooterHeight = float.Parse(lines[5]);
        

    }
	// Use this for initialization
	void Start () {

        readFile();

		upperLegL=GameObject.FindGameObjectWithTag ("UpperLegL");
		lowerLegL = GameObject.FindGameObjectWithTag ("LowerLegL");
		upperLegR=GameObject.FindGameObjectWithTag ("UpperLegR");
		lowerLegR = GameObject.FindGameObjectWithTag ("LowerLegR");
		upperArmL=GameObject.FindGameObjectWithTag ("UpperArmL");
		lowerArmL = GameObject.FindGameObjectWithTag ("LowerArmL");
		upperArmR=GameObject.FindGameObjectWithTag ("UpperArmR");
		lowerArmR = GameObject.FindGameObjectWithTag ("LowerArmR");
		leftFoot = GameObject.FindGameObjectWithTag ("LeftFoot");
		rightFoot = GameObject.FindGameObjectWithTag ("RightFoot");
        	center = GameObject.FindGameObjectWithTag("Center");
        	fronthoop = GameObject.FindGameObjectWithTag("fronthoop");
        	hoopDist = GameObject.FindGameObjectWithTag("hoopDist");
        	head = GameObject.FindGameObjectWithTag("Head");
        	ftline = GameObject.FindGameObjectWithTag("ftline");
        	body = GameObject.FindGameObjectWithTag("Body");

		lowerLegL.transform.eulerAngles = new Vector3(0f,0f, 180+upperLegAngle+lowerLegAngle);
		lowerLegR.transform.eulerAngles = new Vector3 (0f, 0f, 180+upperLegAngle+lowerLegAngle);
        	upperLegL.transform.eulerAngles = new Vector3(0f, 0f, upperLegAngle);
        	upperLegR.transform.eulerAngles = new Vector3(0f, 0f, upperLegAngle);
		upperArmL.transform.eulerAngles = new Vector3 (0f, 0f, lowerArmAngle);
		upperArmR.transform.eulerAngles = new Vector3 (0f, 0f, lowerArmAngle);
        	lowerArmL.transform.eulerAngles = new Vector3(0f, 0f, lowerArmAngle - (360f-upperArmAngle));
        	lowerArmR.transform.eulerAngles = new Vector3(0f, 0f, lowerArmAngle - (360f - upperArmAngle));
        	rightFoot.transform.eulerAngles = new Vector3(0f, 0f, 360f-(footAngle-90f)-upperLegAngle);
        	leftFoot.transform.eulerAngles = new Vector3(0f, 0f, 360f-(footAngle - 90f)-upperLegAngle);
        if (footAngle > 90.0)
        {
            center.transform.position = new Vector3(float.Parse((center.transform.position.x).ToString()), float.Parse((center.transform.position.y + 0.1).ToString()), float.Parse((center.transform.position.z).ToString()));
        }
        float hoopHeight = 315f;
        float decrement = 0.4f;
        float hoopHeightAnimation = fronthoop.transform.position.y - hoopDist.transform.position.y;
        float hoopRate = hoopHeight / hoopHeightAnimation;
        float shooterHeightAnimationComputed = shooterHeight * hoopHeightAnimation / hoopHeight;
        float shooterHeightAnimation = head.transform.position.y - decrement - ftline.transform.position.y;
        float difference = shooterHeightAnimationComputed - shooterHeightAnimation;
        Debug.Log(difference);
        center.transform.position += new Vector3(0, difference, 0);
        
	}

	// Update is called once per frame
	void Update () {
       
    if (Input.GetKeyDown (KeyCode.P)) {  
    Application.LoadLevel (0);  
    }
    if (Input.GetKeyDown(KeyCode.Q))
    {
        Application.Quit();
    }
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
        using (StreamWriter writer = new StreamWriter(_fileName))
        {
            writer.WriteLine(lowerArmAngle+1f);
            writer.WriteLine(upperArmAngle);
            writer.WriteLine(lowerLegAngle);
            writer.WriteLine(upperLegAngle);
            writer.WriteLine(footAngle);
            writer.WriteLine(shooterHeight); 
        }
        Application.LoadLevel(0);
    }
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
        using (StreamWriter writer = new StreamWriter(_fileName))
        {
            writer.WriteLine(lowerArmAngle - 1f);
            writer.WriteLine(upperArmAngle);
            writer.WriteLine(lowerLegAngle);
            writer.WriteLine(upperLegAngle);
            writer.WriteLine(footAngle);
            writer.WriteLine(shooterHeight);
        }
        Application.LoadLevel(0);
    }
    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
        using (StreamWriter writer = new StreamWriter(_fileName))
        {
            writer.WriteLine(lowerArmAngle);
            writer.WriteLine(upperArmAngle+1f);
            writer.WriteLine(lowerLegAngle);
            writer.WriteLine(upperLegAngle);
            writer.WriteLine(footAngle);
            writer.WriteLine(shooterHeight);
        }
        Application.LoadLevel(0);
    }
    if (Input.GetKeyDown(KeyCode.Alpha4))
    {
        using (StreamWriter writer = new StreamWriter(_fileName))
        {
            writer.WriteLine(lowerArmAngle);
            writer.WriteLine(upperArmAngle-1f);
            writer.WriteLine(lowerLegAngle);
            writer.WriteLine(upperLegAngle);
            writer.WriteLine(footAngle);
            writer.WriteLine(shooterHeight);
        }
        Application.LoadLevel(0);
    }
    if (Input.GetKeyDown(KeyCode.Alpha5))
    {
        using (StreamWriter writer = new StreamWriter(_fileName))
        {
            writer.WriteLine(lowerArmAngle);
            writer.WriteLine(upperArmAngle);
            writer.WriteLine(lowerLegAngle+1f);
            writer.WriteLine(upperLegAngle);
            writer.WriteLine(footAngle);
            writer.WriteLine(shooterHeight);
        }
        Application.LoadLevel(0);
    }
    if (Input.GetKeyDown(KeyCode.Alpha6))
    {
        using (StreamWriter writer = new StreamWriter(_fileName))
        {
            writer.WriteLine(lowerArmAngle);
            writer.WriteLine(upperArmAngle);
            writer.WriteLine(lowerLegAngle -1f);
            writer.WriteLine(upperLegAngle);
            writer.WriteLine(footAngle);
            writer.WriteLine(shooterHeight);
        }
        Application.LoadLevel(0);
    }
    if (Input.GetKeyDown(KeyCode.Alpha7))
    {
        using (StreamWriter writer = new StreamWriter(_fileName))
        {
            writer.WriteLine(lowerArmAngle);
            writer.WriteLine(upperArmAngle);
            writer.WriteLine(lowerLegAngle);
            writer.WriteLine(upperLegAngle+1f);
            writer.WriteLine(footAngle);
            writer.WriteLine(shooterHeight);
        }
        Application.LoadLevel(0);
    }
    if (Input.GetKeyDown(KeyCode.Alpha8))
    {
        using (StreamWriter writer = new StreamWriter(_fileName))
        {
            writer.WriteLine(lowerArmAngle);
            writer.WriteLine(upperArmAngle);
            writer.WriteLine(lowerLegAngle);
            writer.WriteLine(upperLegAngle-1f);
            writer.WriteLine(footAngle);
            writer.WriteLine(shooterHeight);
        }
        Application.LoadLevel(0);
    }
    if (Input.GetKeyDown(KeyCode.Alpha9))
    {
        using (StreamWriter writer = new StreamWriter(_fileName))
        {
            writer.WriteLine(lowerArmAngle + 1f);
            writer.WriteLine(upperArmAngle);
            writer.WriteLine(lowerLegAngle);
            writer.WriteLine(upperLegAngle);
            writer.WriteLine(footAngle+1f);
            writer.WriteLine(shooterHeight);
        }
        Application.LoadLevel(0);
    }
    if (Input.GetKeyDown(KeyCode.Alpha0))
    {
        using (StreamWriter writer = new StreamWriter(_fileName))
        {
            writer.WriteLine(lowerArmAngle);
            writer.WriteLine(upperArmAngle);
            writer.WriteLine(lowerLegAngle);
            writer.WriteLine(upperLegAngle);
            writer.WriteLine(footAngle-1f);
            writer.WriteLine(shooterHeight);
        }
        Application.LoadLevel(0);
    }
    
    


	}

	public float getUpperLegAngle(){
		return upperLegAngle;
		}
}