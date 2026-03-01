using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private PlacementManager placementManager;
    [SerializeField] private RoadFixer roadFixer;
    private List<Vector3Int> temporaryPlacementPositions = new();
    private List<Vector3Int> roadPositionsToBeChecked = new();
    public GameObject roadStraight;

    public void PlaceRoad(Vector3Int position)
    {
        if (!placementManager.CheckIfPositionInBound(position))
            return;

        if (!placementManager.CheckIfPositionIsFree(position))
            return;
        temporaryPlacementPositions.Clear();
        temporaryPlacementPositions.Add(position);
        placementManager.PlaceTemporaryStructure(position, roadStraight, CellType.Road);
        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach (var roadPos in temporaryPlacementPositions)
        {
            roadFixer.FixRoadAtPosition(placementManager, roadPos);
            var neighbours = placementManager.GetNeighbourTypeFor(roadPos, CellType.Road);
            foreach (var neighbour in neighbours)
            {
                if (!roadPositionsToBeChecked.Contains(neighbour))
                    roadPositionsToBeChecked.Add(neighbour);

            }
        }

        foreach (var roadPos in roadPositionsToBeChecked)
        {
            roadFixer.FixRoadAtPosition(placementManager, roadPos);
        }
    }
}
