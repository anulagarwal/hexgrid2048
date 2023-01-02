using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectionTile : Tile
{
    [Header("Attributes")]
    [SerializeField] LayerMask layers;
    [SerializeField] Vector3 downPos;
    [SerializeField] float minDistance=175f;
    bool canMove = false;


    [Header("Component References")]
    [SerializeField] Tile primaryTile;
    [SerializeField] GameObject selector;



    private void Start()
    {
        origPos = transform.position;
        canMove = true;
        foreach (Tile t in GetComponentsInChildren<Tile>())
        {
//            t.Highlight(ColorManager.Instance.GetHexColor(t.hexType));
        }
    }

    private void Update()
    {
        if (GridManager.Instance.GetSelectionTile() == primaryTile && Vector2.Distance(downPos, Input.mousePosition) > minDistance && canMove)
        {
            //if mouse button (left hand side) pressed instantiate a raycast
            if (Input.GetMouseButton(0))
            {
                //create a ray cast and set it to the mouses cursor position in game
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z + 1f);
                }
            }
        }

        if(GridManager.Instance.GetSelectionTile() == primaryTile)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Switch();
            }
        }
    }
    #region Mouse events

    private void OnMouseDown()
    {
        if (GridManager.Instance.GetSelectionTile() != primaryTile && canMove)
        {
            downPos = Input.mousePosition;
            GridManager.Instance.SetSelectedTile(primaryTile);
            selector.SetActive(true);
        }
    }

    private void OnMouseUp()
    {
        if (GridManager.Instance.GetSelectionTile() == primaryTile && GridManager.Instance.GetEnteredTile()!=null && Vector2.Distance(downPos, Input.mousePosition) > minDistance && canMove)
        {
            

            if (GridManager.Instance.PlaceTile())
            {
                SelectionManager.Instance.RemoveTile(gameObject);
                Destroy(selector);
                GetComponent<BoxCollider>().enabled = false;
            }
            else
            {
                transform.DOMove(origPos, 0.25f);
            }

            selector.SetActive(false);
            GridManager.Instance.DeselectTile();
            //If can place, destroy this from selection manager and spawn new

            //transform.position = origPos;

        }
        else if(Vector2.Distance(downPos, Input.mousePosition) < 15)
        {
            Switch();
            GridManager.Instance.DeselectTile();

        }
        else
        {
            transform.DOMove(origPos, 0.25f);
            GridManager.Instance.DeselectTile();

        }
    }

    

    #endregion


    public void Switch()
    {

        //Switch all tiles by one
        //Move Hex1 -> Hex2
        //Hex2 -> Hex3
        //Hex3 -> Hex1
        Vector3 tempPos;
        Tile t1, t2, t3;
        t1 = primaryTile;
        t2 = t1.neighbors[t1.GetNeighborIndex()[0]];
        if (t2.GetNeighborIndex().Count > 0)
        {
            t3 = t2.neighbors[t2.GetNeighborIndex()[0]];
            canMove = false;
            primaryTile = t3;
            GridManager.Instance.SetSelectedTile(primaryTile);
            tempPos = t3.transform.position;

            t3.neighbors[t1.GetNeighborIndex()[0]] = t1;
            //move t3 to t1
            t3.transform.DOMove(t1.transform.position, 0.3f);

            t1.neighbors[t1.GetNeighborIndex()[0]] = null;
            t1.neighbors[t2.GetNeighborIndex()[0]] = t2;
            //move t1 to t2
            t1.transform.DOMove(t2.transform.position, 0.3f);

            t2.neighbors[t2.GetNeighborIndex()[0]] = null;
            t2.transform.DOMove(tempPos, 0.3f).OnComplete(() =>
            {
                canMove = true;

            });
        }
        else
        {
          
            canMove = false;
            primaryTile = t2;
            GridManager.Instance.SetSelectedTile(primaryTile);
            tempPos = t2.transform.position;
          
            t2.neighbors[t1.GetNeighborIndex()[0]] = t1;
            //move t1 to t2
            t2.transform.DOMove(t1.transform.position, 0.3f);


            t1.neighbors[t1.GetNeighborIndex()[0]] = null;
            t1.transform.DOMove(tempPos, 0.3f).OnComplete(() =>
            {
                canMove = true;

            });

        }
        
      
    }

    #region Neighbors
   /* public List<int> GetNeighborIndex()
    {
        List<int> i = new List<int>();
        foreach (Tile t in neighbors)
        {
            if (t != null)
            {
                i.Add(neighbors.IndexOf(t));
            }
        }
        return i;
    }
    public Tile GetNeighbor(int index)
    {
        return neighbors[index];
    }

    public List<Tile> GetNeighbors()
    {
        return neighbors;
    }
   */
    #endregion
}
