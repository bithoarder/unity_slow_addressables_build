using System.IO;
using UnityEditor;
using UnityEngine;

public class TexturePackerImporter : AssetPostprocessor
{
    public void OnPostprocessTexture(Texture2D texture)
    {
        if(Path.GetFileName(assetPath).Equals("atlas.png"))
        {
            TextureImporter importer = this.assetImporter as TextureImporter;

            SpriteMetaData[] metadata = new SpriteMetaData[20 * 20];
            for(int y = 0; y < 20; y++)
            {
                for(int x = 0; x < 20; x++)
                {
                    int i = x + y * 20;
                    metadata[i].name = $"{x},{y}";
                    metadata[i].rect.xMin = (float)texture.width * x / 20;
                    metadata[i].rect.yMin = (float)texture.height * y / 20;
                    metadata[i].rect.xMax = (float)texture.width * (x + 1) / 20;
                    metadata[i].rect.yMax = (float)texture.width * (y + 1) / 20;
                }
            }

            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Multiple;
            importer.spritesheet = metadata;

            EditorUtility.SetDirty(importer);
        }
    }
}
