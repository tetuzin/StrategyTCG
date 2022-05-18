// using System.Collections;
// using System.Collections.Generic;

// using UnityEngine;

// public class Canvas : MonoBehaviour
// {
//     [SerializeField]
//     Transform RootCanvasTransformOnScene;

//     private void Awake()
//     {
//         UnityEditor.SceneManagement.PrefabStage.prefabStageOpened += OnPrefabStageOpen;
//     }

//     private void OnDestroy()
//     {
//         UnityEditor.SceneManagement.PrefabStage.prefabStageOpened -= OnPrefabStageOpen;
//     }

//     private void OnPrefabStageOpen(UnityEditor.SceneManagement.PrefabStage stage)
//     {
//         var prefabContentsRoot = stage.prefabContentsRoot;
//         var rootCanvasOnPrefab = prefabContentsRoot.GetComponent<Canvas>();
//         if (rootCanvasOnPrefab != null)
//         {
//             var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(stage.prefabAssetPath);
//             var prefabRectTransform = prefab.GetComponent<RectTransform>();
//             var prefabContentsRootRectTransform = prefabContentsRoot.GetComponent<RectTransform>();

//             //CanvasをNestする
//             rootCanvasOnPrefab.transform.SetParent(RootCanvasTransformOnScene, false);

//             //崩れたRectTransformプロパティを元に戻す
//             prefabContentsRootRectTransform.anchorMin = prefabRectTransform.anchorMin;
//             prefabContentsRootRectTransform.anchorMax = prefabRectTransform.anchorMax;
//             prefabContentsRootRectTransform.anchoredPosition = prefabRectTransform.anchoredPosition;
//             prefabContentsRootRectTransform.sizeDelta = prefabRectTransform.sizeDelta;
//         }
//     }
// }
