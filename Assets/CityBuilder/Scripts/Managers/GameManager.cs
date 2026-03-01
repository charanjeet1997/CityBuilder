using System;
using SVS;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   public CameraMovement cameraMovement;
   [SerializeField] private InputManager inputManager;
   [SerializeField] private RoadManager roadManager;

   private void OnEnable()
   {
      InputManager.onMouseDown += OnMouseDown;
      InputManager.onMouseDrag += OnMouseDrag;
      InputManager.onMouseUp += OnMouseUp;

   }

   private void Update()
   {
      Vector3 cameraMovementVector = inputManager.CameraMovementVector;
      cameraMovement.MoveCamera(cameraMovementVector);
   }

   private void OnDisable()
   {
      InputManager.onMouseDown -= OnMouseDown;
      InputManager.onMouseDrag -= OnMouseDrag;
      InputManager.onMouseUp -= OnMouseUp;
   }
   private void OnMouseDown(Vector3Int pos)
   {
      Debug.Log(pos);
      roadManager.PlaceRoad(pos);
   }
   private void OnMouseDrag(Vector3Int obj)
   {
      
   }

   private void OnMouseUp()
   {
      
   }


}
