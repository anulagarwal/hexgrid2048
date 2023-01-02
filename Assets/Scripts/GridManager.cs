using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class GridManager : MonoBehaviour
{
	[Header("Grid Generator Attributes")]
	public int width = 6;
	public int height = 6;

	public Tile cellPrefab;

	List<Tile> cells = new List<Tile>();

	[Header("Attributes")]
	[SerializeField] Tile selectedTile;
	[SerializeField] Tile enteredTile;
	[SerializeField] public float yOffsetTile;
	[SerializeField] public float baseYOffset;

	[SerializeField] public float upScaleValue;

	public Sequence seq;
	[SerializeField] public List<Tile> tempTiles;
	[SerializeField] public List<Tile> tempPath;
	[SerializeField] public Tile tempConnection;
	Tile tempTile;
	int depth;

	public static GridManager Instance = null;


	void Awake()
	{
		Application.targetFrameRate = 100;
		if (Instance != null && Instance != this)
		{
			Destroy(gameObject);
		}
		Instance = this;
		//cells = new Tile[height * width];

		for (int z = 0; z < height; z++)
		{
			for (int x = 0; x < width; x++)
			{
				cells.Add(CreateCell(x, z));
			}
		}
		SetNeighbors();
	}

	Tile CreateCell(int x, int z)
	{
		Vector3 position;

		position.x = x * (HexMetrics.innerRadius * 2f);
		position.y = 0f;
		position.z = z * (HexMetrics.outerRadius * 1.5f);
		position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);

		Tile cell = Instantiate<Tile>(cellPrefab);
		cell.transform.SetParent(transform, false);
		cell.transform.localPosition = position;
		cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
		cell.gameObject.name = "Tile " + cells.Count;

		return cell;
	}

	public void SetNeighbors()
	{
		foreach (Tile t in cells)
		{
			int b = t.coordinates.X;
			int z = t.coordinates.Z;

			if (cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z + 1) != null)
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z + 1));
			}
			else
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(null);
			}
			
			if (cells.Find(x => x.coordinates.X == b + 1 && x.coordinates.Z == z) != null)
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(cells.Find(x => x.coordinates.X == b + 1 && x.coordinates.Z == z));
			}
			else
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(null);
			}

			if (cells.Find(x => x.coordinates.X == b + 1 && x.coordinates.Z == z-1) != null)
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(cells.Find(x => x.coordinates.X == b + 1 && x.coordinates.Z == z-1));
			}
			else
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(null);
			}


			if (cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z - 1) != null)
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z - 1));
			}
			else
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(null);
			}


			if (cells.Find(x => x.coordinates.X == b - 1 && x.coordinates.Z == z ) != null)
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(cells.Find(x => x.coordinates.X == b - 1 && x.coordinates.Z == z ));
			}

			else
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(null);
			}


			if (cells.Find(x => x.coordinates.X == b - 1 && x.coordinates.Z == z+1) != null)
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(cells.Find(x => x.coordinates.X == b - 1 && x.coordinates.Z == z+1));
			}
			else
			{
				cells.Find(x => x.coordinates.X == b && x.coordinates.Z == z).SetNeighbor(null);
			}

			
					}
		

	}

	
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
        }
    }

    #region Entered Tiles
    public void SetEnteredTile(Tile t)
    {
		enteredTile = t;
        if (t = null)
        {
			CleanSelection();
        }
    }

	public Tile GetEnteredTile()
    {
		return enteredTile;
    }
	public Tile GetSelectionTile()
    {
		return selectedTile;
    }
	public void SetSelectedTile(Tile t)
	{
		selectedTile = t;
	}

	public bool PlaceTile()
    {
		if (enteredTile.CanBePlace())
		{

			 enteredTile.PlaceHexes(selectedTile);
			//tempTiles = enteredTile.CalculatePlacedHexes(selectedTile);
			return true;
		}
		return false;
	}
	public void DeselectTile()
    {        
		selectedTile = null;
		enteredTile = null;
		CleanSelection();
    }
	public async Task ShiftTo(Tile n, Tile tile)
    {

		await n.hexes[0].transform.DOShakePosition(0.3f, 0.1f).AsyncWaitForCompletion();
		await n.hexes[0].transform.DOMove(new Vector3(tile.transform.position.x, tile.transform.position.y + GridManager.Instance.baseYOffset, tile.transform.position.z), 0.1f).AsyncWaitForCompletion();
		tile.hexes[0].MergeHex(n.hexes[0]);
		n.ShiftHexesToTile();
		VibrationManager.Instance.PlayHaptic();
		SoundManager.Instance.Play(Sound.Pop);
		await tile.hexes[0].transform.DOPunchScale(transform.localScale * -1.3f, 0.2f).AsyncWaitForCompletion();
		tile.hexes[0].UpdateHexColor(tile.hexes[0].value);


	}

	public async Task ReverseShiftTo(Tile tile, Tile n)
    {

		await n.hexes[0].transform.DOShakePosition(0.3f, 0.1f).AsyncWaitForCompletion();
		await n.hexes[0].transform.DOMove(new Vector3(tile.transform.position.x, tile.transform.position.y + GridManager.Instance.baseYOffset, tile.transform.position.z), 0.1f).AsyncWaitForCompletion();
		tile.hexes[0].MergeHex(n.hexes[0]);
		n.ShiftHexesToTile();
		VibrationManager.Instance.PlayHaptic();
		SoundManager.Instance.Play(Sound.Pop);
		await tile.hexes[0].transform.DOPunchScale(transform.localScale * -1.3f, 0.2f).AsyncWaitForCompletion();
		tile.hexes[0].UpdateHexColor(tile.hexes[0].value);


	}
	#endregion

	public async Task CheckNeighborToShift(Tile tile)
	{
		List<float> f = new List<float>();
		tempPath.Add(tile);
		foreach (Tile t in tile.GetNeighbors())
		{
			depth = 0;

			if (t != null)
			{
				if (t.GetNeighbors() != null && t.hexes.Count > 0 && tile.hexes.Count>0&& t.hexes[0].value == tile.hexes[0].value)
				{
					f.Add(GetNeighborWeights(t));
					depth = 0;
				}
				else
				{
					f.Add(0);
				}
			}
            else
            {
				f.Add(0);
            }
		}		
		tempPath.Clear();
		depth = 0;

		Tile b = new Tile();
		b = tile.GetNeighbors().Find(x => x != null && x.hexes.Count > 0 && x.hexes[0].value == tile.hexes[0].value);

		Tile c = new Tile();
		c = tile.GetNeighbors().Find(x => x != null && x.hexes.Count > 0 && x.hexes[0].value == tile.hexes[0].value * 2);

		//Need to caculate which direction should the stuff be moved towards
		//If neighbor has 2^y then check if in y neighbors you can merge
		//Rule always move towards where there is a bigger number - if no reverse
		if (f.Find(x => x > 0.5f) >0.5f)
		{
			if (f.FindAll(x => x == Mathf.Max(f.ToArray())).Count >= 1)
			{
				Tile g = new Tile();
				Tile d = tile.GetNeighbors().Find(x => x != null && x.hexes.Count > 0 && x.hexes[0].value == tile.hexes[0].value * 2);
				Tile e = tile.GetNeighbors().Find(x => x != null && x.hexes.Count > 0 && x.hexes[0].value == tile.hexes[0].value);

				if (d != null)
				{
					await ShiftTo(e, tile);
					await CheckNeighborToShift(tile);
				}
                else
                {
					d = e.GetNeighbors().Find(x => x != null && x.hexes.Count > 0 && x.hexes[0].value == tile.hexes[0].value * 2);
                    if (d != null)
                    {
						await ReverseShiftTo(e, tile);
						await CheckNeighborToShift(e);
					}
                    else
					{
						int gTotal = 0;
						foreach (Tile q in e.GetNeighbors().FindAll(x => x != null && x.hexes.Count > 0))
						{
							gTotal += q.hexes[0].value;
						}
						int tTotal = 0;
						foreach (Tile q in tile.GetNeighbors().FindAll(x => x != null && x.hexes.Count > 0))
						{
							tTotal += q.hexes[0].value;
						}
						if (gTotal > tTotal)
						{
							await ReverseShiftTo(e, tile);
							await CheckNeighborToShift(e);
						}
						else
						{
							await ShiftTo(e, tile);
							await CheckNeighborToShift(tile);
						}
					}
				}
			}
			
        }
        else
        {
			if (tile.hexes.Count > 0 && tile.GetNeighbors().Find(x => x != null && x.hexes.Count > 0 && x.hexes[0].value == tile.hexes[0].value) != null )
			{
					if (c == null)
					{
						await ReverseShiftTo(b, tile);
						if (b != null)
						{
							await CheckNeighborToShift(b);
						}
					}
					else
					{
						await ShiftTo(b, tile);
						if (tile != null)
						{
							await CheckNeighborToShift(tile);
						}
					}
								
			}
        }
			f.Clear();
	}
	public float GetNeighborWeights(Tile tile)
	{
		float val = 0;
		int origDepth = depth;
        if (tile.neighbors.Find(x => x == tempTile) != null)
        {
			if(tile.neighbors.Find(x => x == tempTile).hexes.Count>0)
			if(tile.neighbors.Find(x=>x==tempTile).hexes[0].value == tempTile.hexes[0].value)
            {
				//val++;
            }
        }
		foreach (Tile x in tile.GetNeighbors())
		{
			if (x != null && x == tempConnection)
			{
				val = 0;
				continue;
			}
			if (x != null && x!=tempConnection)
			{
				if (x != tempTile && x != tempPath[depth])
				{
					if (x.hexes.Count > 0 && tile.hexes.Count > 0)
					{
						if (x.hexes[0].value == tile.hexes[0].value*2)
						{
							tempConnection = x;
							val+=1;
							if (x.GetNeighbors().FindAll(z => z != null && z.hexes.Count > 0 && z.hexes[0].value == x.hexes[0].value*2 && z != tile).Count > 0)
							{
								depth++;
								tempPath.Add(x);
								val += GetNeighborWeights(x);
							}						

						}

						if (x.GetNeighbors().Find(z=>z!=null && z.hexes.Count>0 && z.hexes[0].value >= tile.hexes[0].value * 4))
						{
							//val+=0.5f;							
						}
						if (x.hexes[0].value < tile.hexes[0].value)
						{
							//val += 0.25f;
						}
					}
				}

			}

			depth = origDepth;

		}

		tempConnection = null;

		return val;

	}

	public async void CheckForStack()
    {
		foreach (Tile tile in tempTiles)
		{
			tempTile = tile.hexes[0];					
			await CheckNeighborToShift(tile);
		}
		tempTile = null;
	}
	public void CleanSelection()
    {
		foreach (Tile t in cells)
		{
			if (t.GetState() != TileType.Occupied)
				t.DeHighlight();
		}
	}

    

    public bool CompareSelectedToEnteredTile()
    {
		CleanSelection();
		bool isPossible = false;
		List<Tile> tempSelect = new List<Tile>();
		if (enteredTile.GetState() == TileType.Empty)
		{
			isPossible = true;
			foreach (int i in selectedTile.GetNeighborIndex())
			{
				if (enteredTile.GetNeighbor(i) != null && enteredTile.GetNeighbor(i).GetState() == TileType.Empty)
				{
					isPossible = true;
					tempSelect.Add(enteredTile.GetNeighbor(i));
					foreach (int x in selectedTile.GetNeighbor(i).GetNeighborIndex())
					{
						if (enteredTile.GetNeighbor(i).GetNeighbor(x) != null && enteredTile.GetNeighbor(i).GetNeighbor(x).GetState() == TileType.Empty)
						{
							isPossible = true;
							tempSelect.Add(enteredTile.GetNeighbor(i).GetNeighbor(x));
						}
						else
						{
							isPossible = false;
						}
					}
				}
				else
				{
					isPossible = false;
				}
			}
		}
        if (isPossible)
        {
			enteredTile.Highlight(Color.green);
			foreach(Tile t in tempSelect)
            {
				t.Highlight(Color.green);
            }
        }
		return isPossible;
    }

	public void DeselectAll()
    {
		foreach (Tile t in cells)
		{
			t.DeHighlight();
		}
	}
}
