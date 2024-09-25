using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpText : MonoBehaviour
{
    private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
