using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Linq;

public class TouchInput : MonoBehaviour
{
    private GraphicRaycaster m_Raycaster;
    private PointerEventData m_PointerEventData;
    private EventSystem m_EventSystem;
    private bool isSwiping = false;
    private bool touchedLetter = false;
    private List<Vector3> points = new List<Vector3>();
    private List<RaycastResult> results = new List<RaycastResult>();
    public static event Action allTickedOff;

    //lineRendering
    private LineRenderer lr;

    void Start()
    {
        lr = GameObject.FindWithTag("Line").GetComponent<LineRenderer>();
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
 
    }

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
            checkLetterInteraction();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            points.Clear();
            lr.gameObject.SetActive(false);
            allTickedOff?.Invoke();
            isSwiping = false;
            if (touchedLetter)
            {
                CombatWordManager.checkWord();
            }
            touchedLetter = false;
        }
        
    }

    void checkLetterInteraction()
    {
        foreach (RaycastResult result in results)
        {
            
            lr.gameObject.SetActive(true);
            touchedLetter = true;

            if (result.gameObject.GetComponent<LetterListener>().ticked == false) //only add to points if not added yet
            {
                points.Add(result.gameObject.transform.position);
                for (int i = 0; i < points.Count; i++)
                {

                    lr.positionCount = points.Count;
                    lr.SetPositions(points.ToArray());

                }
            }
            result.gameObject.GetComponent<LetterListener>().AddLetterToCurrent(); //ticks letter true
        }
    }
}



