using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTextController : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Awake()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        _text.text = "LEVEL: " + GameController.LEVEL.ToString();
    }
}
