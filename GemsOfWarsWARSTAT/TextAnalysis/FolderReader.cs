using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using GemsOfWarsMainTypes.Model;

namespace GemsOfWarsWARSTAT.TextAnalysis
{
    //public class UnitReader : IEnumerable<Unit>
    //{

    //}
    //public class UserReader : IEnumerable<UnitReader>
    //{

    //}

    public class GuildReader : IDisposable /*: IEnumerable<UserReader>*/
    {
        public GuildReader(string guildStr)
        {
            GuildStr = guildStr;
        }

        public string GuildStr { get; private set; }
        public Guild Guild { get; private set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public class FolderReader : FullStepViewModel<GuildReader>
    {
        private string _root;
        public FolderReader(string root)
        {
            Debug.Assert( Directory.Exists(root));
            _root = root;
            var directorysStr = Directory.GetDirectories(root);
            var directorys = directorysStr.Select(str => new GuildReader(str)).ToList();
            InitItems(directorys);
        }
    }
}
