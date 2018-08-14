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

        }
    }

    class Company 
    {
        //fields
        public string name {get; set;}
        public List<Warehouse> warehouse = new List<Warehouse>();

        public Company(string name)
        {
            this.name = name;
        }

    }

    class Warehouse
    {
        public string location {get;set;}
        public List<Container> container = new List<Container>();
        public int size {get; set;}

        public Warehouse(string location, int size)
        {
            this.location = location;
            this.size = size;
        }
    }

    class Container
    {
        public int size {get; set;}
        public int id {get;set;}
        public List<Item> item = new List<Item>();


        public Container(int size, int id) 
        {
            this.size = size;
            this.id = id;
        }

    }

    class Item
    {
        public string name {get;set;}
        public double price {get;set;}

        public Item(string name, double price)
        {
            this.name = name;
            this.price = price;
        }

    }


}
