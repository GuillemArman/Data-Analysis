using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Obstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public int GenID()
    {
        int order1 = 1 + (int)Mathf.Log10(gameObject.transform.position.z);
        int order2 = order1 - 1 + (int)Mathf.Log10(gameObject.transform.position.x);
        int neg1 = (gameObject.transform.position.z < 0) ? 0 : 1;
        int neg2 = (gameObject.transform.position.x < 0) ? 0 : 1;

        return (int)gameObject.transform.position.z + (neg1) * (int)Mathf.Pow(10, order1) + 
              ((int)gameObject.transform.position.x) * (int)Mathf.Pow(10, order2) - 
               (Mathf.Abs(neg1 - 1)) * (int)Mathf.Pow(10, order1) + 1 * 
               (int)Mathf.Pow(10, (order2 + order2)) + (neg2 - (int)Mathf.Abs(neg2 - 1)) * 
               (int)Mathf.Pow(10, (order2 + order1));
    }
}
