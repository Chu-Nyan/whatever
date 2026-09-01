using System.Collections.Generic;

namespace Chunity.Core
{
    public class TextTable<T>
    {
        private static Dictionary<T, string> _texts;

        public static string Texts(T type)
        {
            return _texts[type];
        }

        public string this[T index]
        {
            get => _texts[index];
        }

        public void LoadTexts(Dictionary<T, string> texts)
        {
            _texts = texts;
        }
    }
}
