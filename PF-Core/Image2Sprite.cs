using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace PF_Core
{
    // Loosely based on https://forum.unity.com/threads/generating-sprites-dynamically-from-png-or-jpeg-files-in-c.343735/
    public class Image2Sprite
    {
        private static readonly Logger _logger = Logger.INSTANCE;

        private static readonly String icons_folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Icons/";
        private static readonly Dictionary<String, Sprite> _sprites = new Dictionary<string, Sprite>();

        public static bool Exists(String filePath)
        {
            _logger.Log($"Exists sprite {filePath}");
            return _sprites.ContainsKey(filePath) | File.Exists(icons_folder + filePath);
        }

        public static Sprite Create(String filePath)
        {
            _logger.Log($"Create sprite {filePath}");
            Sprite sprite;
            if (_sprites.ContainsKey(filePath))
            {
                _logger.Debug($"Sprite {filePath} already loaded");
                sprite = _sprites[filePath];
            }
            else
            {
                _logger.Debug($"Loading sprite {filePath}");
                var bytes = File.ReadAllBytes(icons_folder + filePath);
                var texture = new Texture2D(64, 64, TextureFormat.DXT5, false);
                texture.LoadImage(bytes);
                sprite = Sprite.Create(texture, new Rect(0, 0, 64, 64), new Vector2(0, 0));
                _sprites[filePath] = sprite;
            }

            _logger.Log($"DONE: Create sprite {filePath}");
            return sprite;
        }
    }
}
