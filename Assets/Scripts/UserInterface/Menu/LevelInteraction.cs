using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace AngryBirds3D.UserInterface.Menu
{
    public class LevelInteraction : MonoBehaviour
    {
        void Update()
        {
            ServeInput();
        }

        private void ServeInput()
        {
            if (IsAnyActiveTouch())
            {
                DetectTouchType();
            }
        }

        private bool IsAnyActiveTouch()
        {
            return Touch.activeTouches.Count > 0;
        }

        private void DetectTouchType()
        {
            Touch primaryTouch = Touch.activeTouches[0];

            if (IsTapTouch(primaryTouch))
            {
                CheckLevelUITouch(primaryTouch);
            }
        }

        private bool IsTapTouch(Touch touch)
        {
            return touch.isTap;
        }

        private void CheckLevelUITouch(Touch touch)
        {
            Vector2 touchPosition = GetTouchScreenPosition(touch);

            PointerEventData pointerData = 
                CreatePointerEventDataFromTouchPosition(touchPosition);

            List<RaycastResult> results = CreateEmptyRaycastResultList();

            MakeRaycastFromCamera(pointerData, results);

            CheckForHitOnLevelUI(results);
        }

        private Vector2 GetTouchScreenPosition(Touch touch)
        {
            return touch.screenPosition;
        }

        private PointerEventData CreatePointerEventDataFromTouchPosition(
            Vector2 touchPosition)
        {
            return new PointerEventData(EventSystem.current)
            {
                position = touchPosition
            };
        }

        private List<RaycastResult> CreateEmptyRaycastResultList()
        {
            return new List<RaycastResult>();
        }

        private void MakeRaycastFromCamera(
            PointerEventData pointerData, 
            List<RaycastResult> results)
        {
            EventSystem.current.RaycastAll(pointerData, results);
        }

        private void CheckForHitOnLevelUI(List<RaycastResult> results)
        {
            foreach (var result in results)
            {
                if (IsHitLevelUI(result, out LevelInfo levelInfo))
                {
                    levelInfo.BeginLevel();
                    break;
                }
            }
        }

        private bool IsHitLevelUI(RaycastResult result, out LevelInfo levelInfo)
        {
            return result.gameObject.TryGetComponent(out levelInfo);
        }
    }
}
