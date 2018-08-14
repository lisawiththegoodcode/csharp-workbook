using System;
using System.Collections.Generic;

namespace Rainforest
{
    class Program
    {
        static void Main(string[] args)
        {
            //instantiate a new company called Rainforest
            Company Rainforest = new Company("Rainforest");

            //build some warehouses in a few diff cities
            Warehouse ATX = new Warehouse("Austin", 5);
            Warehouse HTX = new Warehouse("Houston", 10);
            Warehouse SATX = new Warehouse("San Antonio", 8);
            Warehouse DTX = new Warehouse("Dallas", 12);

            //add warehouses to your company
            Rainforest.warehouse.Add(ATX);
            Rainforest.warehouse.Add(HTX);
            Rainforest.warehouse.Add(SATX);
            Rainforest.warehouse.Add(DTX);

            //build containers for each warehouse
            Container ATX0 = new Container(20, 000);
            Container ATX1 = new Container(20, 001);
            Container ATX2 = new Container(20, 002);

            Container HTX0 = new Container(20, 000);
            Container HTX1 = new Container(20, 001);
            Container HTX2 = new Container(20, 002);

            Container SATX0 = new Container(20, 000);
            Container SATX1 = new Container(20, 001);
            Container SATX2 = new Container(20, 002);

            Container DTX0 = new Container(20, 000);
            Container DTX1 = new Container(20, 001);
            Container DTX2 = new Container(20, 002);

            //add containers to each warehouse
            ATX.container.Add(ATX0);
            ATX.container.Add(ATX1);
            ATX.container.Add(ATX2);

            HTX.container.Add(HTX0);
            HTX.container.Add(HTX1);
            HTX.container.Add(HTX2);

            SATX.container.Add(SATX0);
            SATX.container.Add(SATX1);
            SATX.container.Add(SATX2);

            DTX.container.Add(DTX0);
            DTX.container.Add(DTX1);
            DTX.container.Add(DTX2);

            //create items
            Item banana = new Item("Banana", 0.19);
            Item apple = new Item("Apple", 0.89);
            Item orange = new Item("Orange", 0.59);
            Item mango = new Item("Mango", 1.50);
            Item pineapple = new Item("Pineapple", 2.99);

            //add items to each container
            ATX0.item.Add(banana);
            ATX1.item.Add(banana);
            ATX2.item.Add(banana);

            HTX0.item.Add(apple);
            HTX1.item.Add(apple);
            HTX2.item.Add(apple);

            SATX0.item.Add(mango);
            SATX1.item.Add(mango);
            SATX2.item.Add(mango);

            DTX0.item.Add(pineapple);
            DTX1.item.Add(pineapple);
            DTX2.item.Add(pineapple); 

            //create a manifest that lists your company, the location of each warehouse, the containers in each warehouse, and the products in each container:
            Rainforest.printManifest();


            //build an "index" of the items, given an item, show me a container and warehouse it exists at 
            Dictionary<Item, string> index = new Dictionary<Item, string>();

            string bananaValue = Rainforest.warehouseIndex(banana);
            string appleValue = Rainforest.warehouseIndex(apple);
            string mangoValue = Rainforest.warehouseIndex(mango);
            string pineappleValue = Rainforest.warehouseIndex(pineapple);

            index.Add(banana, bananaValue);
            index.Add(apple, appleValue);
            index.Add(mango, mangoValue);
            index.Add(pineapple, pineappleValue);
            
            Console.WriteLine("");
            Console.WriteLine("This is where you can find bananas: " + index[banana]);
            Console.WriteLine("This is where you can find apples: " + index[apple]);
            Console.WriteLine("This is where you can find mangos: " + index[mango]);
            Console.WriteLine("This is where you can find pineaples: " + index[pineapple]);
        }
    }

    class Company 
    {
        //fields
        public string name {get; set;}
        public List<Warehouse> warehouse = new List<Warehouse>();

        //constructor
        public Company(string name)
        {
            this.name = name;
        }
        //method
        public void printManifest()
        {
            Console.WriteLine("Company: " + name);
            
            foreach (Warehouse location in warehouse)
            {
               location.warehouseManifest();
            }
        }

        public string warehouseIndex(Item item)
        {
            string warehouses = "";
            foreach (Warehouse location in warehouse)
            {
                if (location.containerIndex(item) != null)
                {
                    warehouses += (location.location.ToString() + " - containers: " + location.containerIndex(item) + " ");
                }
            }
            return warehouses;
        }
    }

    class Warehouse
    {
        //fields
        public string location {get;set;}
        public List<Container> container = new List<Container>();
        public int size {get; set;}

        //constructor
        public Warehouse(string location, int size)
        {
            this.location = location;
            this.size = size;
        }

        //method
        public void warehouseManifest()
        {
            Console.WriteLine("");
            Console.WriteLine("Warehouse: " + location);

            foreach (Container id in container)
            {
               id.containerManifest();
            }
        }

        public string containerIndex(Item item)
        {
            string returnValue = null;

            foreach (Container id in container)
            {
               if (id.itemIndex(item))
               {
                   returnValue += id.id.ToString() + " ";
               }
            }

            return returnValue;
        }
    }

    class Container
    {
        //fields
        public int size {get; set;}
        public int id {get;set;}
        public List<Item> item = new List<Item>();

        //constructor
        public Container(int size, int id) 
        {
            this.size = size;
            this.id = id;
        }

        //method
        public void containerManifest()
        {
            Console.WriteLine("Container: " + id);

            foreach (Item name in item)
            {
               name.itemManifest();
            }
        }

        public bool itemIndex(Item itemToIndex)
        {
            bool returnValue = false;
            foreach(Item name in item)
            {
                if (name == itemToIndex)
                {
                    returnValue = true;
                }
            }

            return returnValue;

        }

    }

    class Item
    {
        //fields
        public string name {get;set;}
        public double price {get;set;}

        //constructor
        public Item(string name, double price)
        {
            this.name = name;
            this.price = price;
        }

        //method
        public void itemManifest()
        {
            Console.WriteLine("Item: " + name);
        }

    }

}