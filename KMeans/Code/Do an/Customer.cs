using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Do_an
{
    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public double age { get; set; }
        public double income { get; set; }
        public int ClusterIndex { get; set; }
        public Customer(int id, string name, double age, double income)
        {
            this.id = id;
            this.name = name;
            this.age = age;
            this.income = income;
        }
    }
}
