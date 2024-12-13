using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;

public class MaskMapSplitter : MonoBehaviour
{
    [MenuItem("Tools/Batch Split Mask Maps")]
    public static void BatchSplitMaskMaps()
    {
        // Wähle den Ordner mit Mask Maps aus
        string folderPath = EditorUtility.OpenFolderPanel("Select Folder with Mask Maps", "Assets", "");
        if (string.IsNullOrEmpty(folderPath))
        {
            Debug.LogWarning("No folder selected.");
            return;
        }

        // Konvertiere den Pfad in einen relativen Unity-Pfad
        string relativePath = "Assets" + folderPath.Substring(Application.dataPath.Length);

        // Finde alle Texturen im Ordner
        string[] texturePaths = Directory.GetFiles(relativePath, "*.png")
            .Concat(Directory.GetFiles(relativePath, "*.jpg"))
            .Concat(Directory.GetFiles(relativePath, "*.tga"))
            .ToArray();

        if (texturePaths.Length == 0)
        {
            Debug.LogWarning("No textures found in the selected folder.");
            return;
        }

        // Verarbeite jede Textur
        foreach (string path in texturePaths)
        {
            // Lade die Textur
            Texture2D maskMap = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

            if (maskMap == null)
            {
                Debug.LogWarning($"Skipping invalid texture: {path}");
                continue;
            }

            // Erstelle neue Texturen für jeden Kanal
            Texture2D metallicTexture = new Texture2D(maskMap.width, maskMap.height);
            Texture2D aoTexture = new Texture2D(maskMap.width, maskMap.height);
            Texture2D smoothnessTexture = new Texture2D(maskMap.width, maskMap.height);

            // Extrahiere die Kanäle
            Color[] pixels = maskMap.GetPixels();
            for (int i = 0; i < pixels.Length; i++)
            {
                Color pixel = pixels[i];

                // Metallic (R)
                metallicTexture.SetPixel(i % maskMap.width, i / maskMap.width, new Color(pixel.r, pixel.r, pixel.r, 1));

                // Ambient Occlusion (G)
                aoTexture.SetPixel(i % maskMap.width, i / maskMap.width, new Color(pixel.g, pixel.g, pixel.g, 1));

                // Smoothness (A)
                smoothnessTexture.SetPixel(i % maskMap.width, i / maskMap.width, new Color(pixel.a, pixel.a, pixel.a, 1));
            }

            // Texturen anwenden
            metallicTexture.Apply();
            aoTexture.Apply();
            smoothnessTexture.Apply();

            // Speicherpfade für die neuen Texturen erstellen
            string directory = Path.GetDirectoryName(path);
            SaveTextureAsPNG(metallicTexture, Path.Combine(directory, maskMap.name + "_Metallic.png"));
            SaveTextureAsPNG(aoTexture, Path.Combine(directory, maskMap.name + "_AO.png"));
            SaveTextureAsPNG(smoothnessTexture, Path.Combine(directory, maskMap.name + "_Smoothness.png"));

            Debug.Log($"Processed Mask Map: {maskMap.name}");
        }

        AssetDatabase.Refresh();
        Debug.Log("Batch Mask Map splitting complete.");
    }

    private static void SaveTextureAsPNG(Texture2D texture, string path)
    {
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(path, bytes);
        AssetDatabase.ImportAsset(path);
    }
}