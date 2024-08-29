using System;

namespace Helpers
{
    public class Singleton<T>
    {
        private static readonly Lazy<T> lazy = new ();

        public static T Instance => lazy.Value;

        protected Singleton()
        {
        }
    }
}