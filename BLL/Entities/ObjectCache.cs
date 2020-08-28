using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    /// <summary>
    /// cache de l'application
    /// </summary>
    public class ObjectCache
    {
        private MemoryCache cache;
        private static IList<Catalog> catalogs = new List<Catalog>();
        private static Root root;
        private ObjectCache()
        {
            cache = MemoryCache.Default;
        }
        internal static void Initialize(Root rootObject)
        {
            root = rootObject;
        }
        /// <summary>
        /// recupération du catalog
        /// </summary>
        public static IList<Catalog> Catalogs
        {
            get 
            {
                return root.Catalog;
            }
        }

        /// <summary>
        /// recuperation des catégories
        /// </summary>
        public static IList<Category> Categories
        {
            get { return root.Category; }
        }
        
    }
}
