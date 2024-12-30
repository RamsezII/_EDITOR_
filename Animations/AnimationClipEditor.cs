#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace _EDITOR_
{
    public class AnimationClipEditor : EditorWindow
    {
        private GameObject selectedObject;
        private ModelImporterClipAnimation[] clipAnimations;

        //--------------------------------------------------------------------------------------------------------------

        [MenuItem("Assets/" + nameof(_EDITOR_) + "/" + nameof(ShowAnimationClipEditor))]
        public static void ShowAnimationClipEditor()
        {
            GetWindow<AnimationClipEditor>("Animation Clip Editor");
        }

        //--------------------------------------------------------------------------------------------------------------

        private void OnGUI()
        {
            GUILayout.Label("Animation Clip Editor", EditorStyles.boldLabel);

            // Sélectionner un GameObject
            selectedObject = (GameObject)EditorGUILayout.ObjectField("Selected Model", selectedObject, typeof(GameObject), true);

            if (selectedObject == null)
            {
                GUILayout.Label("Sélectionne un GameObject contenant un modèle avec des animations.");
                return;
            }

            // Charger les animations
            if (GUILayout.Button("Charger les Animations"))
            {
                LoadAnimations();
            }

            if (clipAnimations != null && clipAnimations.Length > 0)
            {
                GUILayout.Label($"Nombre d'animations : {clipAnimations.Length}", EditorStyles.boldLabel);

                // Liste des animations modifiables
                for (int i = 0; i < clipAnimations.Length; i++)
                {
                    GUILayout.BeginVertical("box");
                    clipAnimations[i].name = EditorGUILayout.TextField("Nom", clipAnimations[i].name);
                    clipAnimations[i].firstFrame = EditorGUILayout.FloatField("Première Frame", clipAnimations[i].firstFrame);
                    clipAnimations[i].lastFrame = EditorGUILayout.FloatField("Dernière Frame", clipAnimations[i].lastFrame);
                    clipAnimations[i].loopTime = EditorGUILayout.Toggle("Loop", clipAnimations[i].loopTime);
                    GUILayout.EndVertical();
                }

                // Bouton pour appliquer les modifications
                if (GUILayout.Button("Appliquer les Modifications"))
                {
                    ApplyAnimations();
                }
            }
        }

        private void LoadAnimations()
        {
            string path = AssetDatabase.GetAssetPath(selectedObject);
            var importer = AssetImporter.GetAtPath(path) as ModelImporter;

            if (importer != null)
            {
                clipAnimations = importer.defaultClipAnimations;
                Debug.Log($"Chargé {clipAnimations.Length} clips d'animations.");
            }
            else
            {
                Debug.LogError("Le modèle sélectionné n'a pas de ModelImporter.");
            }
        }

        private void ApplyAnimations()
        {
            if (clipAnimations == null)
            {
                Debug.LogError("Aucune animation à appliquer.");
                return;
            }

            string path = AssetDatabase.GetAssetPath(selectedObject);
            var importer = AssetImporter.GetAtPath(path) as ModelImporter;

            if (importer != null)
            {
                importer.clipAnimations = clipAnimations;
                AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                Debug.Log("Modifications appliquées avec succès !");
            }
            else
            {
                Debug.LogError("Erreur lors de l'application des animations.");
            }
        }
    }
}
#endif