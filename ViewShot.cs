using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ViewShot : MonoBehaviour {
	Texture2D View;
	Texture2D border;
	bool shot = false;

	// Use this for initialization
	void Start () {

		//RenderTexture technique for viewshot capture
		View = new Texture2D (Screen.width, Screen.height, TextureFormat.RGB24, false);   	
	}
	
	// Update is called once per frame
	void Update () {
		//screenshot happens on mouse click
		if (Input.GetKeyUp (KeyCode.Mouse0)) {
			StartCoroutine ("Capture");
		}
	}

	//function for giving a name to every viewshot captured
	//the width and height of the screenshot are variable and can be changed as per the resolution requirement
	public static string ViewShotName(int width, int height) {
		return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png", 
			Application.dataPath, 
			width, height, 
			System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
	}
		
	void OnGUI () {

		if (shot) {
			GUI.DrawTexture (new Rect (10, 10, 60, 40), View, ScaleMode.StretchToFill);
		}

	}

	IEnumerator Capture(){
		yield return new WaitForEndOfFrame ();
		View.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0);
		View.Apply();

		//Encode texture into PNG
		byte[] bytes = View.EncodeToPNG();

		//for testing purposes, also write a file in the project folder
		string filename = ViewShotName(Screen.width, Screen.height);
		System.IO.File.WriteAllBytes(filename, bytes);
		Debug.Log(string.Format("Took screenshot to: {0}", filename));

		shot = true;
	}
}



