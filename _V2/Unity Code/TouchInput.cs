using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TouchInput : MonoBehaviour
{
 
    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;
    private bool isSwiping = false;
    private bool touchedLetter = false;
    private List<RaycastResult> results = new List<RaycastResult>();
    public static event Action allTickedOff;

    // Start is called before the first frame update
    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
 
    }

    // Update is called once per frame
    void Update()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (Input.GetButtonDown("Fire1"))
        {
            isSwiping = true;                 
        }

        if (isSwiping)
        {
            foreach (RaycastResult result in results)
            {
                touchedLetter = true;
                result.gameObject.GetComponent<LetterListener>().AddLetterToCurrent();
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            allTickedOff?.Invoke();
            isSwiping = false;
            if (touchedLetter)
            {
                CombatWordManager.checkWord();
            }
            touchedLetter = false;
        }
        
    }
  
}

