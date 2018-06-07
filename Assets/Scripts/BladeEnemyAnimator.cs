using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeEnemyAnimator : MonoBehaviour {

	public GameObject Player;
	public Sprite baseSprite;
	public Sprite[] mySprites;
	public SpriteRenderer mySpriteRenderer;
	public float framesPerSecond;
	public float secondsPerFrame;
	public float range;
	public int currentFrame;

	// Use this for initialization
	void Start () {
		secondsPerFrame = 1/framesPerSecond;
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update () {
		if(Vector3.Distance(Player.transform.position, this.transform.position) < range){
			Invoke("NextFrame", secondsPerFrame);
		} else {
			mySpriteRenderer.sprite = baseSprite;
		}
	}

	void NextFrame() {
		if (Vector3.Distance(Player.transform.position, this.transform.position) < range){
			currentFrame = (currentFrame + 1) % mySprites.Length;
			mySpriteRenderer.sprite = mySprites[currentFrame];
			Invoke("NextFrame", secondsPerFrame);
		}
	}
}