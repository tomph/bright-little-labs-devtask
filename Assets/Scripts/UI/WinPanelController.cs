using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinPanelController : MonoBehaviour
{
    [SerializeField]
    private Button _button;

    public UnityEvent ON_RESTART = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        show = false;

        _button.onClick.AddListener(OnRestart);
    }

    private void OnRestart()
    {
        ON_RESTART.Invoke();
    }

    public bool show
    {
        set
        {
            CanvasGroup cg = GetComponent<CanvasGroup>();
            cg.interactable = cg.blocksRaycasts = value;
            cg.alpha = value == true ? 1 : 0;
        }
    }
}
