using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellInputController : MonoBehaviour
{
    //[SerializeField] private Dictionary<SpellShape.Position, Image> gridThing;
    [System.Serializable]
    public struct GridPositions {
        public SpellShape.Position pos;
        public Image image;
    }

    [SerializeField] private List<GridPositions> positions;
    private Dictionary<SpellShape.Position, Image> positionsMap = new Dictionary<SpellShape.Position, Image>();
    private bool holdingMouse = false;

    [SerializeField] private float canvasDistance = 1;
    
    private LineRenderer lr;

    private List<SpellShape.Position> currentOrder = new List<SpellShape.Position>();

    private int currentLrIndex = 0;

    [SerializeField] KeyCode resetKey = KeyCode.E;

    public List<GridPositions> GetGridPositions() { return positions; }
    // Start is called before the first frame update
    void Start()
    {
        lr = gameObject.GetComponent<LineRenderer>();
        lr.positionCount = 0;

        for (int i = 0; i < positions.Count; i++)
        {
            positionsMap.Add(positions[i].pos, positions[i].image);
        }
        ResetOrder();
    }

    private void LateUpdate()
    {
        GenerateLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            holdingMouse = true;
        }
        else
        {
            holdingMouse = false;
        }

        //Update Line Renderer
        //GenerateLine();

        if (Input.GetMouseButtonDown(0))
        {

            for (int i = 0; i < positions.Count; i++)
            {
                /*
                if (positions[i].image.rectTransform.rect.Contains(Input.mousePosition))
                {
                    Debug.Log(positions[i].image.gameObject.name + " is under the mouse");
                }
                 */

                if (RectTransformUtility.RectangleContainsScreenPoint(positions[i].image.gameObject.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
                {
                    //Debug.Log(positions[i].image.gameObject.name + " is under the mouse");

                    if (currentOrder.Count != 0)
                    {
                        if (currentOrder.Last() == positions[i].pos)
                        {
                            return;
                        }
                    }

                    currentOrder.Add(positions[i].pos);
                    positions[i].image.color = GameManager.Instance.SelectedColor();
                }
            }
            
        }

        if (Input.GetMouseButtonDown(1) && Input.GetKey(resetKey))
        {
            ResetOrder();
        }

        if (currentOrder.Count > 1)
        {
            
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            string s = "";
            for (int i = 0; i < currentOrder.Count; i++)
            {
                s += i + ": " + SpellShape.PositionAsString(currentOrder[i]) + ", ";
            }

            Debug.Log(s);
        }
    }

    public void ResetOrder()
    {
        currentOrder.Clear();
        for (int i = 0; i < positions.Count; i++)
        {
            //Debug.Log("Clearing position " + i);
            positions[i].image.color = GameManager.Instance.UnselectedColor();
        }
    }

    public void PositionHovered(SpellShape.Position pos)
    {
        //Debug.Log(SpellShape.PositionAsString(pos));

        if (holdingMouse) AddToCurrentOrder(pos);
    }

    private void AddToCurrentOrder(SpellShape.Position pos)
    {
        if (currentOrder.Count != 0)
        {
            if (currentOrder.Last() == pos)
            {
                return;
            }
        }
        
        currentOrder.Add(pos);
        positionsMap[pos].color = GameManager.Instance.SelectedColor();

    }

    public List<SpellShape.Position> GetCurrentOrder() { return currentOrder; }

    private void GenerateLine()
    {
        lr.positionCount = (holdingMouse) ? currentOrder.Count + 1 : currentOrder.Count;

        for (int i = 0; i < currentOrder.Count; i++)
        {
            lr.SetPosition(i, positionsMap[currentOrder[i]].transform.position);
        }

        if (holdingMouse && currentOrder.Count > 0)
        {
            Vector3 thing = new Vector3(Input.mousePosition.x, Input.mousePosition.y, canvasDistance);
            Vector2 newVec = new Vector2(positionsMap[currentOrder.Last()].transform.position.x, positionsMap[currentOrder.Last()].transform.position.y);
            Vector3 objectInWorld = Camera.main.WorldToScreenPoint(positionsMap[currentOrder.Last()].transform.position);
            //Debug.Log("Distance between mouse and " + transform.parent.name + " is - " + Vector2.Distance(Input.mousePosition, objectInWorld));
            if (Mathf.Abs(Vector2.Distance(Input.mousePosition, objectInWorld)) > 100f)
            {
                lr.positionCount = currentOrder.Count;
                return;
            }
            else
            {
                lr.SetPosition(currentOrder.Count, Camera.main.ScreenToWorldPoint(thing));
            }
        }
    }
}
