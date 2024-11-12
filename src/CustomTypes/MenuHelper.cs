using System;

namespace src.CustomTypes
{
    public static class MenuHelper
    {
        /// <summary>
        /// Used to display a menu with <paramref name="options"/>
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="options"></param>
        /// <returns>The <see cref="int"/> index selected from <paramref name="options"/></returns>
        public static int DisplayMenu(Func<string, int> callback, params string[] options) /*Since we're not alloed to use System.Console outside of Program.cs we made this to reuse a menu logic*/
        {
            var index = callback.Invoke(String.Join("\n", options));
            return (index > options.Length || index < 1) ? -1 : index;
        }
    }
}
