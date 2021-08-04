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
    public bool exitMenuShowing = false;
    private List<Vector3> points = new List<Vector3>();
    private List<RaycastResult> results = new List<RaycastResult>();
    public static event Action AllTickedOff;
    private GameObject blankOne;
    private GameObject blankTwo;
    private List<GameObject> allHits = new List<GameObject>();
    [SerializeField]
    private CombatLogic combatLogic;
    //lineRendering
    private LineRenderer lr;

    private void Awake()
    {
        CombatWordManager.playerKilledTrigger += DeleteLine;
        
    }
    void Start()
    {
        blankOne = new GameObject();
        blankTwo = new GameObject();
        allHits.Add(blankOne);
        allHits.Add(blankTwo);
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

        if (isSwiping && combatLogic.isGameplay && exitMenuShowing == false)
        {
            checkLetterInteraction();
        }

        if (Input.GetButtonUp("Fire1"))
        {
            points.Clear();
            allHits.Clear();
            allHits.Add(blankOne);
            allHits.Add(blankTwo);
            lr.gameObject.SetActive(false);
            AllTickedOff?.Invoke();
            isSwiping = false;
            if (touchedLetter)
            {
                //Debug.Log("wtf bro. Touching Letters?");
                CombatWordManager.checkWord();
            }
            touchedLetter = false;
        }
        
    }

    void checkLetterInteraction()
    {     
        foreach (RaycastResult result in results)
        {
            var parent = result.gameObject.transform.GetComponentInParent<LetterListener>(); //dynamic reference to letter listener script

            if (result.gameObject.tag == "Letter")//includes the hitbox child object
            {
                lr.gameObject.SetActive(true);
                touchedLetter = true;
                if(!parent.ticked) //if letter has not been hit yet
                {
                    points.Add(parent.transform.position);
                    allHits.Add(parent.gameObject);
                    parent.AddLetterToCurrent();
                }
                else if(parent.transform.position == allHits[allHits.Count -2].transform.position) //if letter is the letter hit most recently
                {
                    allHits[allHits.Count - 1].GetComponent<LetterListener>().RemoveLetterFromCurrent(); //ticks false
                    allHits.RemoveAt(allHits.Count - 1);
                    points.RemoveAt(points.Count - 1);
                }
            }
            for (int i = 0; i < points.Count; i++) //reload line renderer's active positions
            {
                lr.positionCount = points.Count;
                lr.SetPositions(points.ToArray());
            }
        }
    }

    public void DeleteLine()
    {
        lr.gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        CombatWordManager.playerKilledTrigger -= DeleteLine;
    }
}



