using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

namespace Chunity.Core
{
    public static class AssetManager
    {
        // 동기 로드
        public static T LoadAssetSync<T>(string path)
        {
            var handle = Addressables.LoadAssetAsync<T>(path);
            handle.WaitForCompletion();
            if (handle.Status != AsyncOperationStatus.Succeeded)
                throw new System.Exception($"Addressables Load Failed.\nPath : {path}");

            return handle.Result;
        }

        // 동기 로드 후 오브젝트 생성
        public static T InstantiateAssetSync<T>(string path, string name, Transform parent) where T : Behaviour
        {
            var item = LoadAssetSync<GameObject>(path);
            var gameObj = Object.Instantiate(item, parent);
            gameObj.name = name == default ? item.name : name;
            return gameObj.GetComponent<T>();
        }
        public static T InstantiateAssetSync<T>(string path) where T : Behaviour
        {
            return InstantiateAssetSync<T>(path, default, null);
        }

        public static T InstantiateAssetSync<T>(string path, string name) where T : Behaviour
        {
            return InstantiateAssetSync<T>(path, name, null);
        }

        public static T InstantiateAssetSync<T>(string path, Transform parent) where T : Behaviour
        {
            return InstantiateAssetSync<T>(path, default, parent);
        }

        // Json 불러오기
        public static string LoadJson(string path)
        {
            return LoadAssetSync<TextAsset>(path).text;
        }
        // 비동기 로드
        public static void LoadGameObject(string path)
        {
            Addressables.LoadAssetAsync<GameObject>(path).Completed += OnLoadCompleted;
        }

        private static void OnLoadCompleted(AsyncOperationHandle<GameObject> handler)
        {
            if (handler.Status != AsyncOperationStatus.Succeeded)
                return;

            Object.Instantiate(handler.Result);
        }


        public static Sprite GetSpriteWithAtlas(string path, string name)
        {
            var spriteAtlas = LoadAssetSync<SpriteAtlas>(path);
            return spriteAtlas.GetSprite(name);
        }
    }
}
