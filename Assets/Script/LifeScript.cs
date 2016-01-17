using UnityEngine;
using System.Collections;

public class LifeScript : MonoBehaviour {

	RectTransform rt;
	public GameObject UnityChan;
	public GameObject explosion;
	public Text gameOverText;
	private bool gameOver = false;

	void Start () {
		rt = GetComponent<RectTransform>();
	}

	void Update(){
		if (rt.sizeDelta.y <= 0) {
			if(gameOver == false){
				Instantiate(explosion,UnityChan.transform.position + new Vector3(0,1,0),UnityChan.transform.rotation);
			}
			GameOver();
		}
		if (gameOver) {
			gameOverText.enabled = true;
			if(Input.GetMouseButtonDown(0)){
				Application.LoadLevel("Title");
			}
		}
	}

	public void LifeDown (int ap){
		rt.sizeDelta -= new Vector2 (0,ap);
	}

	public void LifeUp(int hp){
		rt.sizeDelta += new Vector2 (0,hp);
		if (rt.sizeDelta.y > 240f) {
			rt.sizeDelta = new Vector2(51f,240f);
		}
	}
	public void GameOver(){
		gameOver = true;
		Destroy (UnityChan);
	}
}
