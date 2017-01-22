using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour {

    public int lives = 1;
    public GameObject[] life_indicators;
    public GameObject life_indicator;

	// Use this for initialization
	void Start () {
        List<GameObject> li = new List<GameObject>();
		for(int i = 0; i < lives; i++)
        {
            GameObject new_life = Instantiate<GameObject>(life_indicator);
            RectTransform rt = new_life.GetComponent<RectTransform>();
            rt.SetParent(gameObject.transform);
            rt.anchorMin = life_indicator.GetComponent<RectTransform>().anchorMin;
            rt.anchorMax = life_indicator.GetComponent<RectTransform>().anchorMax;
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y + (rt.rect.height + 3)*i);
            
            li.Add(new_life);
        }
        life_indicator.SetActive(false);
        life_indicators = li.ToArray();
	}
	
    public void disableLife()
    {
        if(lives > 0)
        {
            life_indicators[--lives].SetActive(false);
        }
        
    }

	// Update is called once per frame
	void Update () {
		
	}
}
