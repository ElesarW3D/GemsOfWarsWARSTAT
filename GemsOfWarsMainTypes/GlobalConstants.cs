namespace GemsOfWarsMainTypes
{
    public static class GlobalConstants
    {
        public static readonly int CountInTeam = 4;

        public static readonly string NonameUnit = "No name";
        public static readonly string TextColorUnits = "Red = 1, Brown = 2, Purple = 4, Yellow = 8, Green = 16, Blue = 32";
        public static readonly string PrintName = "Имя :";
        public static readonly string DefenceDisplayName = "{0}, {1}, {2}, {3}";
        public static readonly string DefenceTabulationDisplayName = "{0}\t{1}\t{2}\t{3}";
        public static readonly string RealStateDisName = "{0}: {1} - L:{2} из {3}";
        public static readonly string WarSplitName = "{0} Корзина: {1}";
        public static readonly string WarSplitNameTab = "{0}\n Корзина: {1}";
        public static readonly string WarSplitNameTabA = "Anabioz\n{0}\n Корзина: {1}\n Стычки:{2} \nПроцент {3:0.00}%";
        public static readonly string WarSplitNameTab2 = "{0}\n Корзина: {1}\n Стычки:{2} \nПроцент {3:0.00}%";
        public static readonly string WarSplitNameTabFull = "{0}\n Корзина: {1}\n Стычки:{2} Процент {3:0.00}%\nАнабиоз \nСтычки:{4} Процент {5:0.00}%";
        public static readonly string DefenceEfficensy = "{0}\n Корзина: {1}\n Разность\n{2:0.00}%";
        public static readonly string AttackColorCount = "Стычки({0}):{1}";

        public static readonly string NotSelect = "Не выбран(а) {0}";
        public static readonly string HasAlready = "Такой(ая) {0} уже есть";
        public static readonly string DisplayUser = "пользователь";
        public static readonly string DisplayUnit = "войско или оружие";
        public static readonly string DisplayWar = "война";
        public static readonly string DisplayGuild = "гильдия";
        public static readonly string DisplayWarGuild = "гильдия в войне";
        public static readonly string DisplayDefence = "защиты";
        public static readonly string DisplayWarDay = "защитный день";
        public static readonly string DisplayRealStat = "показатель защиты";

       
        public static readonly string DisValue = "{0} {1:P1} стычек {2}";
        public static readonly string DisValueBasket = "Корзина {0} {1:P1} стычек {2}";
        public static readonly string FullDisValueBasket = "Итого {0:P1} стычек {1}";
        public static readonly string PrintValue = "{0} \t {1:P1} \t {2}";
        public static readonly string PrintStatItem = "{0:P1} \t {1} \t";

        public static readonly string FullDisValueBasketDiff = "Итого {0:P1} стычек {1} {2}";
        public static readonly string DisValueDiff = "{0} {1:P1} стычек {2} {3}";
        public static readonly string PrintValueDiff = "{0} \t {1} \t {2:P1} \t {3} \t {4}";

        public class Messages
        {
            public static readonly string AddUser = "Вы хотите добавить пользователя \'{0}\'?";
            public static readonly string AddDefence = "Вы хотите добавить защиту {0}?";
            public static readonly string AddWar = "Вы хотите добавить войну {0}?";
            public static readonly string AddGuild = "Вы хотите добавить гильдию {0}?";
        }

        public static readonly string CloneDefeance = "Вы хотите скопировать защиту ?";
    }

    public static class LangItems 
    {
        private static IStaticNames m_langItem;
        static LangItems()
        {
            m_langItem = new RuLang();
        }

        public static string PrintName => m_langItem.PrintName;
    }

    public class RuLang : IStaticNames
    {
        public string PrintName => "Имя :";
    }

    public interface IStaticNames
    {
         string PrintName { get; }
    }
}
