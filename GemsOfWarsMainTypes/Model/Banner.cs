using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarsMainTypes.Model
{
    public class Banner : WarStatsModelViewModel
    {
        public BannerName BannerName { get; set; }
        public ColorsCount Colors { get; set; }
        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return obj is Banner banner &&
                   EqualityComparer<BannerName>.Default.Equals(BannerName, banner.BannerName) &&
                   EqualityComparer<ColorsCount>.Default.Equals(Colors, banner.Colors);
        }

        public override int GetHashCode()
        {
            int hashCode = 1124220989;
            hashCode = hashCode * -1521134295 + EqualityComparer<BannerName>.Default.GetHashCode(BannerName);
            hashCode = hashCode * -1521134295 + EqualityComparer<ColorsCount>.Default.GetHashCode(Colors);
            return hashCode;
        }

        public override bool IsEquals(object obj)
        {
            return Equals(obj as Banner);
        }
    }
}
