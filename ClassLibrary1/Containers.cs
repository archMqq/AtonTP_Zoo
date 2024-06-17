using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtonTP
{
    internal class ZooContainer
    {
        public IZoo zoo {  get; private set; }
        public IZooCostCalc calc { get; private set; }

        public ZooContainer() { }
        public ZooContainer(IZoo zoo, IZooCostCalc calc) 
        {
            this.zoo = zoo;
            this.calc = calc;
        }

        public void calcZooCost()
        {
            calc.calculateCost(zoo);
        }
    }
}
