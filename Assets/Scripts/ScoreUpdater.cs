﻿using UnityEngine;
using UnityEngine.UI;

public class ScoreUpdater : MonoBehaviour
{
    public Transform PlayerTransform;
    public Text ScoreText;

	// Use this for initialization
    void Start()
    {
	}
	
	// Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
            Time.timeScale = 1;
        }
        ScoreText.text = "Score: " + PlayerTransform.position.x.ToString("####");
    }
}