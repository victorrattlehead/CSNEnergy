using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Entities
{

    public class Category
    {
        public string Name { get; set; }
        public double Discount { get; set; }
    }

    public class Catalog
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }

    public class Root
    {
        public List<Category> Category { get; set; }
        public List<Catalog> Catalog { get; set; }
    }

    //public class Rootobject
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public string[] required { get; set; }
    //    public Properties properties { get; set; }
    //}

    //public class Properties
    //{
    //    public Category Category { get; set; }
    //    public Catalog Catalog { get; set; }
    //}

    //public class Category
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public Items items { get; set; }
    //}

    //public class Items
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public string[] required { get; set; }
    //    public Properties1 properties { get; set; }
    //}

    //public class Properties1
    //{
    //    public Name Name { get; set; }
    //    public Discount Discount { get; set; }
    //}

    //public class Name
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public string _default { get; set; }
    //    public string[] examples { get; set; }
    //    public string pattern { get; set; }
    //}

    //public class Discount
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public float _default { get; set; }
    //    public float[] examples { get; set; }
    //}

    //public class Catalog
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public Items1 items { get; set; }
    //}

    //public class Items1
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public string[] required { get; set; }
    //    public Properties2 properties { get; set; }
    //}

    //public class Properties2
    //{
    //    public Name1 Name { get; set; }
    //    public Category1 Category { get; set; }
    //    public Price Price { get; set; }
    //    public Quantity Quantity { get; set; }
    //}

    //public class Name1
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public string _default { get; set; }
    //    public string[] examples { get; set; }
    //    public string pattern { get; set; }
    //}

    //public class Category1
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public string _default { get; set; }
    //    public string[] examples { get; set; }
    //    public string pattern { get; set; }
    //}

    //public class Price
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public int _default { get; set; }
    //    public int[] examples { get; set; }
    //}

    //public class Quantity
    //{
    //    public string type { get; set; }
    //    public string title { get; set; }
    //    public int _default { get; set; }
    //    public int[] examples { get; set; }
    //}

}
