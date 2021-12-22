// See https://aka.ms/new-console-template for more information
using Fluentx;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using static Fluentx.Tester.Fluentester;

Console.WriteLine("Hello, World!");
var one = new One()
{
    X1 = "test value",
    X2 = 77,
    X3 = new Two()
    {
        X11 = new Three()
        {
            X21 = 99
        },
        X12 = new List<Three>()
                    {
                        new Three(){ X21 = 105 }
                    }
    },
    X4 = new List<Three>()
                {
                    new Three(){X21=203}
                },
    X5 = new Three[]
               {
                    new Three(){X21=204}, null
               },
    X6 = new Collection<Three>()
                {
                    new Three(){X21=205}
                },
    X7 = new List<Three>()
                {
                    new Three(){X21=206}
                },
    X8 = new Three[]
               {
                    new Three(){X21=207}
               },
    X9 = new Collection<Three>()
                {
                    new Three(){X21=208}
                },
    X10 = new Two()
    {
        X11 = new Three() { X21 = 301 },
        X12 = new Collection<Three>() { new Three() { X21 = 401 } }
    }
};

var another = one.DeepClone();
var s1 = JsonConvert.SerializeObject(one);
var s2 = JsonConvert.SerializeObject(another);
Console.WriteLine(another == one);
