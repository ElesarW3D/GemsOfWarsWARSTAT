using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GemsOfWarsMainTypes.Model
{
    public class FileLocation : WarStatsModelViewModel, IExchange<FileLocation>
    {
        private string _path;
        public string Path
        {
            get => _path;
            set
            {
                _path = value;
                RaisePropertyChanged(nameof(Path));
            }
        }

        public override object Clone()
        {
            return PersoneClone<FileLocation>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is FileLocation unit &&
                unit.Path == Path;
        }

        public void ReadFromItem(FileLocation item)
        {
            Path = item.Path;
        }

        public void WriteToItem(FileLocation item)
        {
            item.Path = Path;
        }
    }

    public class GuildFolderLocation : FileLocation, IExchange<GuildFolderLocation>
    {
        private Guild _guild;
        public Guild Guild
        {
            get => _guild;
            set
            {
                _guild = value;
                RaisePropertyChanged(nameof(Guild));
            }
        }

        public override object Clone()
        {
            return PersoneClone<GuildFolderLocation>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is GuildFolderLocation fileLocation &&
                fileLocation.Path == Path &&
                fileLocation.Guild == Guild;
        }

        public void ReadFromItem(GuildFolderLocation item)
        {
            base.ReadFromItem(item);
            Guild = item.Guild;
        }

        public void WriteToItem(GuildFolderLocation item)
        {
            base.WriteToItem(item);
            item.Guild = Guild;
        }
    }

    public class UserFolderLocation : FileLocation, IExchange<UserFolderLocation>
    {
        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                RaisePropertyChanged(nameof(User));
            }
        }

        public override object Clone()
        {
            return PersoneClone<UserFolderLocation>();
        }

        public override bool IsEquals(object obj)
        {
            return obj is UserFolderLocation fileLocation &&
                fileLocation.Path == Path &&
                fileLocation.User == User;
        }

        public void ReadFromItem(UserFolderLocation item)
        {
            base.ReadFromItem(item);
            User = item.User;
        }

        public void WriteToItem(UserFolderLocation item)
        {
            base.WriteToItem(item);
            item.User = User;
        }
    }
}
