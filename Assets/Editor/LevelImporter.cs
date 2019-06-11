using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Experimental.AssetImporters;
using UnityEngine;

[ScriptedImporter(1, "lvl", 100)]
public class LvlImporter : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        GameObject root = new GameObject("root");

        Material spriteMaterial = new Material(Shader.Find("Sprites/Default"));

        var atlasSprites = new List<Sprite>();
        foreach(var asset in AssetDatabase.LoadAllAssetsAtPath("Assets/atlas.png"))
        {
            if(asset is Sprite sprite)
                atlasSprites.Add(sprite);
        }

        for(int i = 0; i < 1000; i++)
        {
            GameObject go = new GameObject($"sprite {i}");
            go.transform.SetParent(root.transform, false);
            go.transform.localPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0.0f);
            go.transform.localScale = new Vector3(2, 2, 2);

            SpriteRenderer spriteRenderer = go.AddComponent<SpriteRenderer>();
            spriteRenderer.material = spriteMaterial;
            spriteRenderer.sprite = atlasSprites[Random.Range(0, atlasSprites.Count)];
        }
        ctx.AddObjectToAsset("level", root);
        ctx.SetMainObject(root);
        ctx.AddObjectToAsset("material", spriteMaterial);
    }
}
