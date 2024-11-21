using System.IO;
using UnityEngine;

namespace util
{
    public static class AssetUtil
    {
        public static string GenerateAssetKey(string fullPath)
        {
            var prefabName = Path.GetFileName(fullPath);
            var assetType = Path.GetExtension(fullPath);
            var curDirectory = new DirectoryInfo(Path.GetDirectoryName(fullPath)).Name;
            
            prefabName = prefabName.Replace(assetType, "");
            assetType = assetType.TrimStart('.');

            return string.Join("_", new string[] { assetType, curDirectory, prefabName }).ToLower();
        }
    }
}