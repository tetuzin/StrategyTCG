using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace ShunLib.Utils.Resource
{
    public class ResourceUtils
    {
        // 画像ファイルパス名からTextureを取得
        public static Texture2D GetTexture2D(string path)
        {
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(bytes);
            return texture;
        }

        // 画像ファイル名からSpriteを取得
        public static Sprite GetSprite(string path)
        {
            Texture2D texture = GetTexture2D(path);
            Rect rect = new Rect(0f, 0f, texture.width, texture.height);
            Sprite sprite = Sprite.Create(texture, rect, Vector2.zero);
            return sprite;
        }

        // 画像ファイル格納パスを返す
        public static string GetTexturePath(string path)
        {
            return "Assets/Texture/" + path + ".jpg";
        }
    }
}

