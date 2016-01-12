using UnityEngine;
using System.Collections;

public class LifeScript : MonoBehaviour {

	RectTransform rt;

	void Start () {
		rt = GetComponent<RectTransform>();
	}
	
	public void LifeDown (int ap){
		rt.sizeDelta -= new Vector2 (0,ap);
	}
}
