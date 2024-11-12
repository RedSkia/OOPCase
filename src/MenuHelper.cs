using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public static class MenuHelper
    {
        /// <summary>
        /// Used to display a menu with <paramref name="options"/>
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="options"></param>
        /// <returns>The <see cref="int"/> index selected from <paramref name="options"/></returns>
        public static int DisplayMenu(Func<string, int> callback, params string[] options)
        {
            var index = callback.Invoke(String.Join("\n", options));
            return (index > options.Length || index < 1) ? -1 : index;
        }
    }
}