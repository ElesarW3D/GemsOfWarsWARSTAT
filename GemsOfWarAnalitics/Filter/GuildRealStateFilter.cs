using GemsOfWarsMainTypes.Model;

namespace GemsOfWarAnalitics.Filter
{
    public class GuildRealStateFilter : RealStatFilter
    {
        private Guild _guild;

        public Guild Guild { get => _guild; set => _guild = value; }

        public override void CreatePredicate()
        {
            if (Guild == null)
            {
                _predicate = (x) => false;
            }
            else
            {
                _predicate = w => w.EnemyGuild.IsEquals(Guild);
            }
        }
       
    }
}
