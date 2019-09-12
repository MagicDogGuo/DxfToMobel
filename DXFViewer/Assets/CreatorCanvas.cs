using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatorCanvas : MonoBehaviour {

    GameObject obj;

    [SerializeField]
    Button Btn;

    [SerializeField]
    int Amount = 0;

    // Use this for initialization
	void Start () {
        obj = this.gameObject;
        Btn.onClick.AddListener(Creator);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void Creator()
    {
        for(int i = 0; i < Amount; i++)
        {
            GameObject temp=null;
            temp = Instantiate(obj);
            temp.transform.position = new Vector3(temp.transform.position.x, temp.transform.position.y, i*100);
        }
    }
}
