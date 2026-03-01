using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    public int width, height;
    private Grid placementGrid;

    private Dictionary<Vector3Int, StructureModel> temporaryPlacedObjects = new ();

    private void Start()
    {
        placementGrid = new Grid(width, height);
    }

    public bool CheckIfPositionInBound(Vector3Int position)
    {
        return (position.x >= 0 && position.x < width && position.z >= 0 && position.z < height);
    }

    public bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionOfType(CellType.Empty, position);
    }

    private bool CheckIfPositionOfType(CellType cellType, Vector3Int position)
    {
        return placementGrid[position.x, position.z] == cellType;
    }

    public void PlaceTemporaryStructure(Vector3Int position, GameObject placementObject, CellType cellType)
    {
        placementGrid[position.x, position.z] = cellType;
        StructureModel structure = CreateNewStructureModel(position, placementObject, cellType);
        temporaryPlacedObjects.Add(position, structure);
    }

    private StructureModel CreateNewStructureModel(Vector3Int position, GameObject placementObject, CellType cellType)
    {
        GameObject structure = new GameObject(cellType.ToString());
        structure.transform.parent = placementObject.transform;
        structure.transform.localPosition = position;
        StructureModel structureModel = structure.AddComponent<StructureModel>();
        structureModel.CreateModel(placementObject);
        return structureModel;
    }

    public void ModifyPlacedStructureModel(Vector3Int position, GameObject newModel,Quaternion rotation)
    {
        if (temporaryPlacedObjects.ContainsKey(position))
        {
            temporaryPlacedObjects[position].SwapStructure(newModel, rotation);
        }
    }

    public CellType[] GetNeighbourTypeFor(Vector3Int position)
    {
        return placementGrid.GetAllAdjacentCellTypes(position.x, position.z);
    }
}
