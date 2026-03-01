using UnityEngine;

public class StructureModel : MonoBehaviour
{
    private float yHeight;
    private GameObject structure;

    public void CreateModel(GameObject model)
    {
        if (structure == null)
        {
            structure = Instantiate(model, transform);
            yHeight = structure.transform.localScale.y;
        }
    }

    public void SwapStructure(GameObject model, Quaternion rotation)
    {
        if (structure != null)
        {
            Destroy(structure);
        }
        structure = Instantiate(model, transform);
        structure.transform.rotation = rotation;
        structure.transform.localPosition = new Vector3(0, yHeight, 0);
    }
    
}
