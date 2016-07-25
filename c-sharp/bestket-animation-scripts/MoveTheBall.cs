using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;

public class MoveTheBall : MonoBehaviour {
    public Vector2 _initialValues;
    public GetFactors getfactors;
    Rigidbody2D ballRigidBody;
    
	// Use this for initialization
    	string _fileName = "C:\\Users\\"+System.Environment.UserName+"\\Documents\\BestKet\\BestKetAnimationK\\Assets\\SpeedData.txt";
        List<string> lines = new List<string>();
        
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(200,0,300,150));
            GUILayout.Label("Speed X : " + _initialValues.x.ToString() + "\nSpeed Y : " + _initialValues.y.ToString() + "\nUpper Arm Angle : " + getfactors.lowerArmAngle + "\nLower Arm Angle : " + getfactors.upperArmAngle + "\nUpper Leg Angle : " + getfactors.upperLegAngle + "\nLower Leg Angle : " + getfactors.lowerLegAngle + "\nFoot Angle : " + getfactors.footAngle);
            GUILayout.EndArea();
        }
        

    void Start () 
    {
        using (StreamReader r = new StreamReader(_fileName))
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                lines.Add(line);
            }
        }
        _initialValues.x = float.Parse(lines[0]);
        _initialValues.y = float.Parse(lines[1]);

        ballRigidBody = GetComponent<Rigidbody2D>();
        ballRigidBody.AddForce(_initialValues, ForceMode2D.Impulse);
       
    }
	
	// Update is called once per frame
	void Update () {
	//moving the player
        if(Input.GetKey(KeyCode.UpArrow)){
            using (StreamWriter writer = new StreamWriter(_fileName))
            {
                writer.WriteLine(_initialValues.x);
                writer.WriteLine(_initialValues.y + 0.1);
            }
        
            Application.LoadLevel(0);
            
        }
	    if(Input.GetKeyDown(KeyCode.DownArrow)){
            using (StreamWriter writer = new StreamWriter(_fileName))
            {
                writer.WriteLine(_initialValues.x);
                writer.WriteLine(_initialValues.y - 0.1);
            }
            Application.LoadLevel(0);
            
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            using (StreamWriter writer = new StreamWriter(_fileName))
            {
                writer.WriteLine(_initialValues.x - 0.1);
                writer.WriteLine(_initialValues.y);
            }

            Application.LoadLevel(0);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            using (StreamWriter writer = new StreamWriter(_fileName))
            {
                writer.WriteLine(_initialValues.x + 0.1);
                writer.WriteLine(_initialValues.y);
            }
            Application.LoadLevel(0);

        }
	}
}
