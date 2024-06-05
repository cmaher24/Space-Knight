using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HealthFPlayer : MonoBehaviour
    {
        public RectTransform targetRectTransform; // The RectTransform to follow
        public Camera uiCamera; // The camera rendering the UI (usually the Canvas's camera)

        private Transform spriteTransform;

        void Awake()
        {
            spriteTransform = GetComponent<Transform>();
            if (uiCamera == null)
            {
                uiCamera = Camera.main;
            }
        }

        void Update()
        {
            if (targetRectTransform != null)
            {
                // Convert the RectTransform's position to world space
                Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, targetRectTransform.position);
                Vector3 worldPos;
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(targetRectTransform, screenPos, uiCamera, out worldPos))
                {
                    spriteTransform.position = worldPos;
                }
            }
        }
    }

