﻿using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class CanvasSwitcher : MonoBehaviour
{
    public CanvasType desiredCanvasType;

    CanvasDisplayManager canvasManager;
    Button menuButton;

    private void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClicked);
        canvasManager = CanvasDisplayManager.instance;
    }

    void OnButtonClicked()
    {
        gameObject.GetComponent<TileListButton>()?.SetTemp();      
        canvasManager.SwitchCanvas(desiredCanvasType); 
    }
}

















