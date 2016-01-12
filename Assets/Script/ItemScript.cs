using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {
	public int healPoint = 20;
	public LifeScript lifeScript;
	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "UnityChan") {
			lifeScript.LifeUp(healPoint);
			Destroy(gameObject);
		}
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
