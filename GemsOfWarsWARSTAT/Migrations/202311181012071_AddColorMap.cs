namespace GemsOfWarsWARSTAT.Migrations
{
    using GemsOfWarsMainTypes.Extension;
    using GemsOfWarsMainTypes.Model;
    using GemsOfWarsMainTypes.SubType;
    using GemsOfWarsWARSTAT.DataContext;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public partial class AddColorMap : DbMigration
    {
        public override void Up()
        {
            // Вставка данных в новую таблицу
            using (var context = new WarDbContext())
            {
                var allColor = ColorUnits.All.GetByOneColor().ToArray();
                var combine = GetCombineOf((ColorUnits[]) allColor.Clone(),0, allColor);
                foreach (var item in combine)
                {
                    var colorMap = new WarColorMap(item);
                    context.WarColorMaps.Add(colorMap);
                }
                context.SaveChanges();
            }
        }

        private IEnumerable<ColorUnits[]> GetCombineOf(ColorUnits[] permutation, int position, ColorUnits[] origin)
        {
            if (position == permutation.Length)
            {
                yield return (ColorUnits[])permutation.Clone();
                yield break;
            }

            for (int i = 1; i <= permutation.Length; i++)
            {
                var index = Array.IndexOf(permutation, origin[i - 1], 0, position);
                if (index == -1)
                {
                    permutation[position] = origin[i - 1];
                    foreach (var item in GetCombineOf(permutation, position + 1, origin))
                    {
                        yield return item;
                    }
                }
            }
        }

        public override void Down()
        {
        }
    }
}
