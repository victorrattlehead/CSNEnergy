using BLL.Entities.Interface;
using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace BLL.Entities
{
    public class Store : IStore
    {
        private Root rootCatalog;
        private double discount=0.2;

        /// <summary>
        /// permet de calculer le montant du panier
        /// </summary>
        /// <param name="basketByNames">livres à payer</param>
        /// <returns></returns>
        public double Buy(params string[] basketByNames)
        {

            CheckBasket(basketByNames);

            var prices = GetPrices(basketByNames);
            
            if (basketByNames.Length == 1)
            {
                double price;
                if (prices.TryGetValue(basketByNames[0], out price))
                    return price;
                else
                    throw new Exception("Une erreur s'est produite lors du calcul du prix de votre panier");
            }
            else if (basketByNames.Length > 1                 
                && basketByNames.Distinct().Count()==basketByNames.Count()
                && IsFromTheSameCategory(basketByNames))
            {
                return prices.Select(p => p.Value).Sum()*(1-discount);
            }
            else 
            {
                var sale = 0.95;
                var groupByProducts = CountGroupByProduct(basketByNames);
                var uniqProducts = groupByProducts.Where(p => p.Value == 1).Select(p => p.Key);
                var priceOfuniqProduct = prices.Where(pr => uniqProducts.Any(up=>pr.Key==up)).Select(pr=>pr.Value).Sum();
                double price;
                foreach(var product in groupByProducts.Where(p => p.Value > 1))
                {
                    prices.TryGetValue(product.Key, out price);
                    priceOfuniqProduct += price* sale +(product.Value - 1) * price;
                }

                return priceOfuniqProduct;
            }
        }

        /// <summary>
        /// renvoie un dictionnaire qui permet de connaitre le prix de chaque produit du panier
        /// </summary>
        /// <param name="basketByNames">panier</param>
        /// <returns>dictionnaire avec comme clé: le produit et value: le prix du produit</returns>
        private Dictionary<string, double> GetPrices(string[] basketByNames)
        {
            Dictionary<string, double> prices = new Dictionary<string, double>();
            
            var catalogs = rootCatalog.Catalog.Where(c => basketByNames.Distinct().Any(n => n.Contains(c.Name)));
            foreach(var catalog in catalogs)
            {
                prices.Add(catalog.Name, catalog.Price);
            }
            
            return prices;
        }
        /// <summary>
        /// vérifier que le panier ne contient pas des produits dont on
        /// aurait pas suffisamment de référence.
        /// </summary>
        /// <param name="basketByNames"></param>
        private void CheckBasket(string[] basketByNames)
        {
            var splitBasketDico = CountGroupByProduct(basketByNames);
            int number, maxQuantity;
            
            var exception = new NotEnoughInventoryException();

            foreach(var bookName in splitBasketDico.Keys)
            {
                maxQuantity = GetCatalog(bookName).Quantity;
                if (splitBasketDico.TryGetValue(bookName, out number) && number > maxQuantity)
                {
                    (exception.Missing as List<INameQuantity>).Add(new NameQuantity() { Name = bookName, Quantity = maxQuantity });
                }
                    
            }
            
            if(exception.Missing.Any())
                throw exception;
        }

        /// <summary>
        /// permet de connaitre le nombre d'occurence d'un produit dans le panier
        /// </summary>
        /// <param name="basketByNames"></param>
        /// <returns>renvoie un dictionaire dont la clé est le nom du livre et son nombre d'occurence dans le panier</returns>
        private IDictionary<string,int> CountGroupByProduct(string[] basketByNames)
        {
            var result = new Dictionary<string, int>();

            var dicos = basketByNames.Distinct().Select(b => new Tuple<string,int>(b, basketByNames.Where(bs => bs == b).Count()));
            foreach (var dico in dicos)
            {
                result.Add(dico.Item1, dico.Item2);
            }
            return result;
        }
        /// <summary>
        /// récupère une référence avec toutes ses infos
        /// </summary>
        /// <param name="name">nom du produit</param>
        /// <returns>la référence complete Catalog</returns>
        private Catalog GetCatalog(string name)
        {
            var product = rootCatalog.Catalog.Where(c => c.Name.Contains(name)).FirstOrDefault();
            if (product == null)
                throw new Exception("ce produit n'est pas dans le catalogue");
            return product;
        }

        /// <summary>
        /// vérifie si des produits sont tous de la meme référence.
        /// </summary>
        /// <param name="basketByNames"></param>
        /// <returns></returns>
        private bool IsFromTheSameCategory(string[] basketByNames)
        {
            Catalog catalog;
            string categoryName=string.Empty;

            foreach(var basketByName in basketByNames)
            {
                catalog = rootCatalog.Catalog.Where(c => c.Name.Contains(basketByName)).FirstOrDefault();
                if (catalog == null)
                    return false;
                if (string.IsNullOrEmpty(categoryName) && !string.IsNullOrEmpty(catalog.Category))
                    categoryName = catalog.Category;
                else if (categoryName != catalog.Category)
                    return false;

            }
            return true;
        }
        /// <summary>
        /// importe des données Json
        /// </summary>
        /// <param name="catalogAsJson"></param>
        public void Import(string catalogAsJson)
        {
            rootCatalog = JsonSerializer.Deserialize<Root>(catalogAsJson);
            ObjectCache.Initialize(rootCatalog);
        }

        /// <summary>
        /// renvoie la quantité d'un produit
        /// </summary>
        /// <param name="name">nom du produit</param>
        /// <returns></returns>
        public int Quantity(string name)
        {
            var product = GetCatalog(name);
            return product.Quantity;

        }
    }
}
