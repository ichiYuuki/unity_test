using UnityEngine;
using System.Collections;

public class ItemScript : MonoBehaviour {

	public int healPoint = 20;
	private LifeScript lifeScript;

	void Start () {
		lifeScript = GameObject.FindGameObjectWithTag ("HP").GetComponent<LifeScript> ();
	}

	void OnCollisionEnter2D(Collision2D col){
		if (col.gameObject.tag == "UnityChan") {
			lifeScript.LifeUp(healPoint);
			Destroy(gameObject);
		}
	}


	// Update is called once per frame
	void Update () {
	
	}
}
