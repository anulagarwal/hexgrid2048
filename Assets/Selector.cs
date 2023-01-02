using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    public Tile lastTile;

    private void Update()
    {
        Vector3 start = transform.position;
        Vector3 direction = Vector3.down;
        RaycastHit hit;

        if (GridManager.Instance.GetSelectionTile() != null) {

            
            if (Physics.SphereCast(start, 0.15f, direction * 100f, out hit))
            {
                if (hit.collider.GetComponent<Tile>() != null && hit.collider.tag=="Base" && hit.collider.GetComponent<Tile>().GetState() != TileType.Occupied)
                {
                    if (GridManager.Instance.GetEnteredTile() != hit.collider.GetComponent<Tile>())
                    {
                        GridManager.Instance.SetEnteredTile(hit.collider.GetComponent<Tile>());
                        if (GridManager.Instance.CompareSelectedToEnteredTile())
                        {
                            hit.collider.GetComponent<Tile>().SetCanPlace(true);
                        }
                        lastTile = hit.collider.GetComponent<Tile>();

                    }
                }
                else
                {
                    if (GridManager.Instance.GetEnteredTile() != null && lastTile != null)
                    {
                        GridManager.Instance.SetEnteredTile(null);
                        GridManager.Instance.CleanSelection();
                        lastTile.SetCanPlace(false);
                        lastTile = null;
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        GridManager.Instance.SetEnteredTile(null);
        GridManager.Instance.CleanSelection();
    }
}
