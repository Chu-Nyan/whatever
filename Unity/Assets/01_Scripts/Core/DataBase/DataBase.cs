using Chunity.Core;
using Chunity.Utility;
using System;
using Whatever.GameSystems;

namespace Whatever.Core
{
    public class DataBase : Singleton<DataBase>
    {
        public readonly WorldProfile WorldProfile;
        public readonly SkillRepository SkillRepo;

        public DataBase()
        {
            WorldProfile = AssetManager.LoadAssetSync<WorldProfile>("WorldProfile");
            SkillRepo = new();
        }

        public static K[] ConvertToData<T, K>(T[] dtos, Func<T, K> toData) where T : IHasKey
        {
            K[] result = new K[dtos[dtos.Length - 1].Key + 1];

            for (int i = 0; i < dtos.Length; i++)
            {
                result[dtos[i].Key] = toData(dtos[i]);
            }

            return result;
        }

        public static K[] ConvertToData<T, K>(string path, Func<T, K> toData) where T : IHasKey
        {
            var dtos = JsonHelper.DeserializeObject<T>(path);
            return ConvertToData(dtos, toData);
        }

        public static K[][] ConvertToArray<T, K>(T[] dtos, Func<T, K> converter) where T : IHasKey
        {
            var result = new K[dtos[dtos.Length - 1].Key + 1][];

            int i = 0;
            while (i < dtos.Length)
            {
                int start = i;
                int currentKey = dtos[start].Key;

                while (i < dtos.Length && dtos[i].Key == currentKey)
                {
                    i++;
                }

                int size = i - start;
                var array = new K[size];

                for (int j = 0; j < size; j++)
                {
                    array[j] = converter(dtos[start + j]);
                }

                result[currentKey] = array;
            }

            return result;
        }

        public static K[][] ConvertToArray<T, K>(string path, Func<T, K> converter) where T : IHasKey
        {
            var dtos = JsonHelper.DeserializeObject<T>(path);
            return ConvertToArray(dtos, converter);
        }
    }
}
