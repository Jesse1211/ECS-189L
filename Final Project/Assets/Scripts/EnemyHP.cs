using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHP : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider sli;
    public GameObject follow;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 boxFollow = Camera.main.WorldToScreenPoint(follow.transform.position);
        sli.GetComponent<RectTransform>().position = boxFollow;
    }
}