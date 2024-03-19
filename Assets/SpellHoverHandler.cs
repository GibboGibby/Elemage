using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SpellHoverHandler : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private SpellInputController SpellInputController;

    [SerializeField] private SpellShape.Position position;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SpellInputController.PositionHovered(position);

    }

    public SpellShape.Position GetPosition() { return position; }

    // Start is called before the first frame update
    void Start()
    {
        if (SpellInputController == null)
        {
            SpellInputController = transform.parent.GetComponent<SpellInputController>();
            List<SpellInputController.GridPositions> gridPositions = SpellInputController.GetGridPositions();
            for (int i = 0; i < gridPositions.Count; i++)
            {
                if (gridPositions[i].image == this.GetComponent<Image>())
                {
                    position = gridPositions[i].pos;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
