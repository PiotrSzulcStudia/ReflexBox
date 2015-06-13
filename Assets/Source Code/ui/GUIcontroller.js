#pragma strict

import UnityEngine;
import UnityEngine.UI;

public var testButtuon : Button;
public var myText : Text;

public var ScoreText : Text;
public var LivesText : Text;

function Start () {
	myText = testButtuon.GetComponentInChildren(Text);
	myText.text = "HelloWorld";
}

function UpdateScore(score) {
	ScoreText.text = "Score: " + score;
}

function UpdateLives(lives) {
	LivesText.text = "Lives: " + lives;
}

function Update () {
}