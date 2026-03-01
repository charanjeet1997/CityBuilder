using System.Linq;
using UnityEngine;

public class RoadFixer : MonoBehaviour
{
    public GameObject deadEnd, roadStraight, corner, threeWay, fourWay;

    public void FixRoadAtPosition(PlacementManager placementManager, Vector3Int position)
    {
        var result = placementManager.GetNeighbourTypeFor(position);
        int roadCount = 0;
        roadCount = result.Where(x => x == CellType.Road).Count();
        if (roadCount <= 0 || roadCount == 1)
        {
            CreateDeadEnd(placementManager, result, position);
        }
        else if(roadCount == 2)
        {
            if (CreateStraightRoad(placementManager, result, position))
                return;
            CreateCorner(placementManager, result, position);
        }
        else if (roadCount == 3)
        {
            CreateThreeWay(placementManager, result, position);
        }
        else
        {
            CreateFourWay(placementManager, result, position);
        }
    }

    private void CreateThreeWay(PlacementManager placementManager,  CellType[] result, Vector3Int position)
    {
        if (result[1] == CellType.Road && result[2] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,threeWay,Quaternion.identity);
        }
        else if (result[0] == CellType.Road && result[2] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,threeWay,Quaternion.Euler(0,90,0));
        }
        else if(result[0] == CellType.Road && result[1] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,threeWay,Quaternion.Euler(0,180,0));
        }
        else if(result[0] == CellType.Road && result[1] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,threeWay,Quaternion.Euler(0,270,0));
        }
    }

    private void CreateFourWay(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        placementManager.ModifyPlacedStructureModel(position,fourWay,Quaternion.identity);
    }

    private void CreateCorner(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        if (result[1] == CellType.Road && result[2] == CellType.Road )
        {
            placementManager.ModifyPlacedStructureModel(position,corner,Quaternion.Euler(0,90,0));
        }
        else if (result[2] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,corner,Quaternion.Euler(0,180,0));
        }
        else if(result[0] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,corner,Quaternion.Euler(0,270,0));
        }
        else if(result[0] == CellType.Road && result[1] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,corner,Quaternion.identity);
        }
    }

    private bool CreateStraightRoad(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        if (result[0] == CellType.Road && result[2] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,roadStraight,Quaternion.identity);
            return true;
        }
        if (result[1] == CellType.Road && result[3] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,roadStraight,Quaternion.Euler(0,90,0));
            return true;
        }
        return  false;
    }

    private void CreateDeadEnd(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        if (result[1] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,deadEnd,Quaternion.Euler(0,270,0));
        }
        else if (result[2] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,deadEnd,Quaternion.identity);
        }
        else if(result[3] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,deadEnd,Quaternion.Euler(0,90,0));
        }
        else if(result[0] == CellType.Road)
        {
            placementManager.ModifyPlacedStructureModel(position,deadEnd,Quaternion.Euler(0,180,0));
        }
    }
}
