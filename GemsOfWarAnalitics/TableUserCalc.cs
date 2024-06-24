using GemsOfWarsMainTypes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarAnalitics
{
    public class TableUserCalc : BaseStatItem
    {
        public TableUserCalc(User user, List<double> points)
        {
            User = user;
            Points = points;
            Total = Points.Take(6).Average();
        }

        public User User { get; }
        public List<double> Points { get; }
        public double Total { get; }

        public string PrintValue => User.Name + "\t" + Points[0] + "\t"
                                    + Points[1] + "\t"
                                        + Points[2] + "\t"
            + Points[3] + "\t"
            + Points[4] + "\t"
            + Points[5] + "\t" + Total;

        public override string DisplayName => User.Name;
      
        public double Red => Points[0];
        public double Brown => Points[1];
        public double Purple => Points[2];
        public double Yellow => Points[3];
        public double Green => Points[4];
        public double Blue => Points[5];

        public override double WinRate => Total;

        public override int Count => 0;

        public override string WinRatePrint => PrintValue;
    }
}
