﻿namespace GemsOfWarsWARSTAT.Migrations
{
    using GemsOfWarsMainTypes.Model;
    using GemsOfWarsMainTypes.SubType;
    using GemsOfWarsWARSTAT.DataContext;
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddValueBaner : DbMigration
    {
        public override void Up()
        {
            // Вставка данных в новую таблицу
            using (var context = new WarDbContext())
            {
                var Red2 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Red, Count = 2 });
                var Red = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Red, Count = 1 });
                var Red_1 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Red, Count = -1 });

                var Blue2 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Blue, Count = 2 });
                var Blue = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Blue, Count = 1 });
                var Blue_1 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Blue, Count = -1 });

                var Yellow2 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Yellow, Count = 2 });
                var Yellow = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Yellow, Count = 1 });
                var Yellow_1 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Yellow, Count = -1 });

                var Brown2 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Brown, Count = 2 });
                var Brown = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Brown, Count = 1 });
                var Brown_1 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Brown, Count = -1 });

                var Green2 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Green, Count = 2 });
                var Green = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Green, Count = 1 });
                var Green_1 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Green, Count = -1 });

                var Purple2 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Purple, Count = 2 });
                var Purple = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Purple, Count = 1 });
                var Purple_1 = context.ColorsCounts.Add(new ColorsCount() { Color = ColorUnits.Purple, Count = -1 });

                var banner0 = context.BannerNames.Add(new BannerName() { Name = "Знамя Прогресса", GameId = 3001 });
                context.Banners.Add(new Banner() { BannerName = banner0, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner0, Colors = Yellow });
                var banner1 = context.BannerNames.Add(new BannerName() { Name = "Окаменевшее знамя", GameId = 3082 });
                context.Banners.Add(new Banner() { BannerName = banner1, Colors = Brown2 });
                context.Banners.Add(new Banner() { BannerName = banner1, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner1, Colors = Blue_1 });
                var banner2 = context.BannerNames.Add(new BannerName() { Name = "Грибное знамя", GameId = 3053 });
                context.Banners.Add(new Banner() { BannerName = banner2, Colors = Green2 });
                context.Banners.Add(new Banner() { BannerName = banner2, Colors = Brown });
                context.Banners.Add(new Banner() { BannerName = banner2, Colors = Red_1 });
                var banner3 = context.BannerNames.Add(new BannerName() { Name = "Знамя Пустыни", GameId = 3024 });
                context.Banners.Add(new Banner() { BannerName = banner3, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner3, Colors = Brown });
                var banner4 = context.BannerNames.Add(new BannerName() { Name = "Святое Знамя", GameId = 3014 });
                context.Banners.Add(new Banner() { BannerName = banner4, Colors = Yellow2 });
                var banner5 = context.BannerNames.Add(new BannerName() { Name = "Знамя Врат", GameId = 3057 });
                context.Banners.Add(new Banner() { BannerName = banner5, Colors = Brown2 });
                context.Banners.Add(new Banner() { BannerName = banner5, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner5, Colors = Yellow_1 });
                var banner6 = context.BannerNames.Add(new BannerName() { Name = "Знамя окуляренов", GameId = 3039 });
                context.Banners.Add(new Banner() { BannerName = banner6, Colors = Green2 });
                context.Banners.Add(new Banner() { BannerName = banner6, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner6, Colors = Brown_1 });
                var banner7 = context.BannerNames.Add(new BannerName() { Name = "Знамя улья", GameId = 3060 });
                context.Banners.Add(new Banner() { BannerName = banner7, Colors = Brown2 });
                context.Banners.Add(new Banner() { BannerName = banner7, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner7, Colors = Red_1 });
                var banner8 = context.BannerNames.Add(new BannerName() { Name = "Знамя греха", GameId = 3072 });
                context.Banners.Add(new Banner() { BannerName = banner8, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner8, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner8, Colors = Brown_1 });
                var banner9 = context.BannerNames.Add(new BannerName() { Name = "Знамя орла", GameId = 3067 });
                context.Banners.Add(new Banner() { BannerName = banner9, Colors = Brown2 });
                context.Banners.Add(new Banner() { BannerName = banner9, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner9, Colors = Purple_1 });
                var banner10 = context.BannerNames.Add(new BannerName() { Name = "Знамя ночи", GameId = 3063 });
                context.Banners.Add(new Banner() { BannerName = banner10, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner10, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner10, Colors = Yellow_1 });
                var banner11 = context.BannerNames.Add(new BannerName() { Name = "Знамя умельцев", GameId = 3045 });
                context.Banners.Add(new Banner() { BannerName = banner11, Colors = Red2 });
                context.Banners.Add(new Banner() { BannerName = banner11, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner11, Colors = Green_1 });
                var banner12 = context.BannerNames.Add(new BannerName() { Name = "Знамя метеорита", GameId = 3037 });
                context.Banners.Add(new Banner() { BannerName = banner12, Colors = Red2 });
                context.Banners.Add(new Banner() { BannerName = banner12, Colors = Brown });
                context.Banners.Add(new Banner() { BannerName = banner12, Colors = Purple_1 });
                var banner13 = context.BannerNames.Add(new BannerName() { Name = "Знамя Орков", GameId = 3018 });
                context.Banners.Add(new Banner() { BannerName = banner13, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner13, Colors = Brown });
                var banner14 = context.BannerNames.Add(new BannerName() { Name = "Знамя Вампира", GameId = 3007 });
                context.Banners.Add(new Banner() { BannerName = banner14, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner14, Colors = Purple });
                var banner15 = context.BannerNames.Add(new BannerName() { Name = "Знамя Адского Когтя", GameId = 3054 });
                context.Banners.Add(new Banner() { BannerName = banner15, Colors = Red2 });
                context.Banners.Add(new Banner() { BannerName = banner15, Colors = Purple });
                context.Banners.Add(new Banner() { BannerName = banner15, Colors = Brown_1 });
                var banner16 = context.BannerNames.Add(new BannerName() { Name = "Знамя охотника", GameId = 3048 });
                context.Banners.Add(new Banner() { BannerName = banner16, Colors = Green2 });
                context.Banners.Add(new Banner() { BannerName = banner16, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner16, Colors = Blue_1 });
                var banner17 = context.BannerNames.Add(new BannerName() { Name = "Знамя Минотавра", GameId = 3027 });
                context.Banners.Add(new Banner() { BannerName = banner17, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner17, Colors = Red });
                var banner18 = context.BannerNames.Add(new BannerName() { Name = "Знамя Песен", GameId = 3003 });
                context.Banners.Add(new Banner() { BannerName = banner18, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner18, Colors = Yellow });
                var banner19 = context.BannerNames.Add(new BannerName() { Name = "Знамя уничтожителя", GameId = 3035 });
                context.Banners.Add(new Banner() { BannerName = banner19, Colors = Brown2 });
                context.Banners.Add(new Banner() { BannerName = banner19, Colors = Purple });
                context.Banners.Add(new Banner() { BannerName = banner19, Colors = Yellow_1 });
                var banner20 = context.BannerNames.Add(new BannerName() { Name = "Знамя хитрости", GameId = 3076 });
                context.Banners.Add(new Banner() { BannerName = banner20, Colors = Red2 });
                context.Banners.Add(new Banner() { BannerName = banner20, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner20, Colors = Brown_1 });
                var banner21 = context.BannerNames.Add(new BannerName() { Name = "Бронированное знамя", GameId = 3077 });
                context.Banners.Add(new Banner() { BannerName = banner21, Colors = Green2 });
                context.Banners.Add(new Banner() { BannerName = banner21, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner21, Colors = Brown_1 });
                var banner22 = context.BannerNames.Add(new BannerName() { Name = "Знамя крови", GameId = 3074 });
                context.Banners.Add(new Banner() { BannerName = banner22, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner22, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner22, Colors = Yellow_1 });
                var banner23 = context.BannerNames.Add(new BannerName() { Name = "Знамя механизмов", GameId = 3096 });
                context.Banners.Add(new Banner() { BannerName = banner23, Colors = Yellow2 });
                context.Banners.Add(new Banner() { BannerName = banner23, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner23, Colors = Purple_1 });
                var banner24 = context.BannerNames.Add(new BannerName() { Name = "Знамя зарослей", GameId = 3073 });
                context.Banners.Add(new Banner() { BannerName = banner24, Colors = Green2 });
                context.Banners.Add(new Banner() { BannerName = banner24, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner24, Colors = Purple_1 });
                var banner25 = context.BannerNames.Add(new BannerName() { Name = "Горящее знамя", GameId = 3075 });
                context.Banners.Add(new Banner() { BannerName = banner25, Colors = Red2 });
                context.Banners.Add(new Banner() { BannerName = banner25, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner25, Colors = Blue_1 });
                var banner26 = context.BannerNames.Add(new BannerName() { Name = "Знамя охотника", GameId = 3097 });
                context.Banners.Add(new Banner() { BannerName = banner26, Colors = Green2 });
                context.Banners.Add(new Banner() { BannerName = banner26, Colors = Brown });
                context.Banners.Add(new Banner() { BannerName = banner26, Colors = Yellow_1 });
                var banner27 = context.BannerNames.Add(new BannerName() { Name = "Знамя ассасина", GameId = 3078 });
                context.Banners.Add(new Banner() { BannerName = banner27, Colors = Blue2 });
                context.Banners.Add(new Banner() { BannerName = banner27, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner27, Colors = Brown_1 });
                var banner28 = context.BannerNames.Add(new BannerName() { Name = "Знамя с клыками", GameId = 3079 });
                context.Banners.Add(new Banner() { BannerName = banner28, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner28, Colors = Brown });
                context.Banners.Add(new Banner() { BannerName = banner28, Colors = Yellow_1 });
                var banner29 = context.BannerNames.Add(new BannerName() { Name = "Пещерное знамя", GameId = 3046 });
                context.Banners.Add(new Banner() { BannerName = banner29, Colors = Brown2 });
                context.Banners.Add(new Banner() { BannerName = banner29, Colors = Purple });
                context.Banners.Add(new Banner() { BannerName = banner29, Colors = Green_1 });
                var banner30 = context.BannerNames.Add(new BannerName() { Name = "Знамя Гоблина", GameId = 3004 });
                context.Banners.Add(new Banner() { BannerName = banner30, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner30, Colors = Brown });
                var banner31 = context.BannerNames.Add(new BannerName() { Name = "Знамя заказника", GameId = 3052 });
                context.Banners.Add(new Banner() { BannerName = banner31, Colors = Green2 });
                context.Banners.Add(new Banner() { BannerName = banner31, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner31, Colors = Blue_1 });
                var banner32 = context.BannerNames.Add(new BannerName() { Name = "Знамя стража", GameId = 3042 });
                context.Banners.Add(new Banner() { BannerName = banner32, Colors = Yellow2 });
                context.Banners.Add(new Banner() { BannerName = banner32, Colors = Brown });
                context.Banners.Add(new Banner() { BannerName = banner32, Colors = Green_1 });
                var banner33 = context.BannerNames.Add(new BannerName() { Name = "Затонувшее знамя", GameId = 3069 });
                context.Banners.Add(new Banner() { BannerName = banner33, Colors = Blue2 });
                context.Banners.Add(new Banner() { BannerName = banner33, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner33, Colors = Green_1 });
                var banner34 = context.BannerNames.Add(new BannerName() { Name = "Знамя Прайда", GameId = 3005 });
                context.Banners.Add(new Banner() { BannerName = banner34, Colors = Red2 });
                var banner35 = context.BannerNames.Add(new BannerName() { Name = "Зеркальное знамя", GameId = 3062 });
                context.Banners.Add(new Banner() { BannerName = banner35, Colors = Yellow2 });
                context.Banners.Add(new Banner() { BannerName = banner35, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner35, Colors = Red_1 });
                var banner36 = context.BannerNames.Add(new BannerName() { Name = "Знамя змеев", GameId = 3064 });
                context.Banners.Add(new Banner() { BannerName = banner36, Colors = Yellow2 });
                context.Banners.Add(new Banner() { BannerName = banner36, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner36, Colors = Purple_1 });
                var banner37 = context.BannerNames.Add(new BannerName() { Name = "Знамя Темных эльфов", GameId = 3029 });
                context.Banners.Add(new Banner() { BannerName = banner37, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner37, Colors = Purple });
                var banner38 = context.BannerNames.Add(new BannerName() { Name = "Знамя щупалец", GameId = 3071 });
                context.Banners.Add(new Banner() { BannerName = banner38, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner38, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner38, Colors = Green_1 });
                var banner39 = context.BannerNames.Add(new BannerName() { Name = "Знамя кобольдов", GameId = 3051 });
                context.Banners.Add(new Banner() { BannerName = banner39, Colors = Yellow2 });
                context.Banners.Add(new Banner() { BannerName = banner39, Colors = Brown });
                context.Banners.Add(new Banner() { BannerName = banner39, Colors = Purple_1 });
                var banner40 = context.BannerNames.Add(new BannerName() { Name = "Гномье Знамя", GameId = 3012 });
                context.Banners.Add(new Banner() { BannerName = banner40, Colors = Brown2 });
                var banner41 = context.BannerNames.Add(new BannerName() { Name = "Знамя Древних Богов", GameId = 3017 });
                context.Banners.Add(new Banner() { BannerName = banner41, Colors = Purple2 });
                var banner42 = context.BannerNames.Add(new BannerName() { Name = "Знамя Нежити", GameId = 3020 });
                context.Banners.Add(new Banner() { BannerName = banner42, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner42, Colors = Purple });
                var banner43 = context.BannerNames.Add(new BannerName() { Name = "Знамя Дракона", GameId = 3019 });
                context.Banners.Add(new Banner() { BannerName = banner43, Colors = Red2 });
                context.Banners.Add(new Banner() { BannerName = banner43, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner43, Colors = Brown_1 });
                var banner44 = context.BannerNames.Add(new BannerName() { Name = "Знамя Леса", GameId = 3015 });
                context.Banners.Add(new Banner() { BannerName = banner44, Colors = Green2 });
                var banner45 = context.BannerNames.Add(new BannerName() { Name = "Знамя ледяного пламени", GameId = 3056 });
                context.Banners.Add(new Banner() { BannerName = banner45, Colors = Blue2 });
                context.Banners.Add(new Banner() { BannerName = banner45, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner45, Colors = Brown_1 });
                var banner46 = context.BannerNames.Add(new BannerName() { Name = "Знамя лабиринта", GameId = 3059 });
                context.Banners.Add(new Banner() { BannerName = banner46, Colors = Green2 });
                context.Banners.Add(new Banner() { BannerName = banner46, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner46, Colors = Purple_1 });
                var banner47 = context.BannerNames.Add(new BannerName() { Name = "Знамя Холода", GameId = 3011 });
                context.Banners.Add(new Banner() { BannerName = banner47, Colors = Blue2 });
                context.Banners.Add(new Banner() { BannerName = banner47, Colors = Purple });
                context.Banners.Add(new Banner() { BannerName = banner47, Colors = Red_1 });
                var banner48 = context.BannerNames.Add(new BannerName() { Name = "Знамя Меча", GameId = 3006 });
                context.Banners.Add(new Banner() { BannerName = banner48, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner48, Colors = Yellow });
                var banner49 = context.BannerNames.Add(new BannerName() { Name = "Звериное знамя", GameId = 3068 });
                context.Banners.Add(new Banner() { BannerName = banner49, Colors = Red2 });
                context.Banners.Add(new Banner() { BannerName = banner49, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner49, Colors = Blue_1 });
                var banner50 = context.BannerNames.Add(new BannerName() { Name = "Знамя лиса", GameId = 3084 });
                context.Banners.Add(new Banner() { BannerName = banner50, Colors = Green2 });
                context.Banners.Add(new Banner() { BannerName = banner50, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner50, Colors = Red_1 });
                var banner51 = context.BannerNames.Add(new BannerName() { Name = "Знамя безумия", GameId = 3070 });
                context.Banners.Add(new Banner() { BannerName = banner51, Colors = Yellow2 });
                context.Banners.Add(new Banner() { BannerName = banner51, Colors = Purple });
                context.Banners.Add(new Banner() { BannerName = banner51, Colors = Brown_1 });
                var banner52 = context.BannerNames.Add(new BannerName() { Name = "Львиное знамя", GameId = 3025 });
                context.Banners.Add(new Banner() { BannerName = banner52, Colors = Blue2 });
                context.Banners.Add(new Banner() { BannerName = banner52, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner52, Colors = Green_1 });
                var banner53 = context.BannerNames.Add(new BannerName() { Name = "Знамя Трезубца", GameId = 3036 });
                context.Banners.Add(new Banner() { BannerName = banner53, Colors = Blue2 });
                context.Banners.Add(new Banner() { BannerName = banner53, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner53, Colors = Yellow_1 });
                var banner54 = context.BannerNames.Add(new BannerName() { Name = "Знамя Волка", GameId = 3008 });
                context.Banners.Add(new Banner() { BannerName = banner54, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner54, Colors = Green });
                var banner55 = context.BannerNames.Add(new BannerName() { Name = "Знамя печали", GameId = 3041 });
                context.Banners.Add(new Banner() { BannerName = banner55, Colors = Blue2 });
                context.Banners.Add(new Banner() { BannerName = banner55, Colors = Brown });
                context.Banners.Add(new Banner() { BannerName = banner55, Colors = Yellow_1 });
                var banner56 = context.BannerNames.Add(new BannerName() { Name = "Элементальное знамя", GameId = 3080 });
                context.Banners.Add(new Banner() { BannerName = banner56, Colors = Brown2 });
                context.Banners.Add(new Banner() { BannerName = banner56, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner56, Colors = Yellow_1 });
                var banner57 = context.BannerNames.Add(new BannerName() { Name = "Знамя обсидиана", GameId = 3083 });
                context.Banners.Add(new Banner() { BannerName = banner57, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner57, Colors = Brown });
                context.Banners.Add(new Banner() { BannerName = banner57, Colors = Green_1 });
                var banner58 = context.BannerNames.Add(new BannerName() { Name = "Огнедышащее знамя", GameId = 3000 });
                context.Banners.Add(new Banner() { BannerName = banner58, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner58, Colors = Brown });
                var banner59 = context.BannerNames.Add(new BannerName() { Name = "Вулканическое знамя", GameId = 3044 });
                context.Banners.Add(new Banner() { BannerName = banner59, Colors = Red2 });
                context.Banners.Add(new Banner() { BannerName = banner59, Colors = Brown });
                context.Banners.Add(new Banner() { BannerName = banner59, Colors = Blue_1 });
                var banner60 = context.BannerNames.Add(new BannerName() { Name = "Знамя Бездны", GameId = 3021 });
                context.Banners.Add(new Banner() { BannerName = banner60, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner60, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner60, Colors = Yellow_1 });
                var banner61 = context.BannerNames.Add(new BannerName() { Name = "Знамя разлома", GameId = 3043 });
                context.Banners.Add(new Banner() { BannerName = banner61, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner61, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner61, Colors = Red_1 });
                var banner62 = context.BannerNames.Add(new BannerName() { Name = "Порченое знамя", GameId = 3061 });
                context.Banners.Add(new Banner() { BannerName = banner62, Colors = Yellow2 });
                context.Banners.Add(new Banner() { BannerName = banner62, Colors = Purple });
                context.Banners.Add(new Banner() { BannerName = banner62, Colors = Green_1 });
                var banner63 = context.BannerNames.Add(new BannerName() { Name = "Райское знамя", GameId = 3028 });
                context.Banners.Add(new Banner() { BannerName = banner63, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner63, Colors = Purple });
                var banner64 = context.BannerNames.Add(new BannerName() { Name = "Знамя Единорога", GameId = 3009 });
                context.Banners.Add(new Banner() { BannerName = banner64, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner64, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner64, Colors = Red_1 });
                var banner65 = context.BannerNames.Add(new BannerName() { Name = "Серебряное знамя", GameId = 3066 });
                context.Banners.Add(new Banner() { BannerName = banner65, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner65, Colors = Brown });
                context.Banners.Add(new Banner() { BannerName = banner65, Colors = Blue_1 });
                var banner66 = context.BannerNames.Add(new BannerName() { Name = "Знамя Мрака", GameId = 3022 });
                context.Banners.Add(new Banner() { BannerName = banner66, Colors = Purple });
                context.Banners.Add(new Banner() { BannerName = banner66, Colors = Brown });
                var banner67 = context.BannerNames.Add(new BannerName() { Name = "Когтистое Знамя", GameId = 3023 });
                context.Banners.Add(new Banner() { BannerName = banner67, Colors = Yellow2 });
                context.Banners.Add(new Banner() { BannerName = banner67, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner67, Colors = Blue_1 });
                var banner68 = context.BannerNames.Add(new BannerName() { Name = "Знамя слизней", GameId = 3058 });
                context.Banners.Add(new Banner() { BannerName = banner68, Colors = Blue2 });
                context.Banners.Add(new Banner() { BannerName = banner68, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner68, Colors = Red_1 });
                var banner69 = context.BannerNames.Add(new BannerName() { Name = "Знамя крыс", GameId = 3049 });
                context.Banners.Add(new Banner() { BannerName = banner69, Colors = Brown2 });
                context.Banners.Add(new Banner() { BannerName = banner69, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner69, Colors = Blue_1 });
                var banner70 = context.BannerNames.Add(new BannerName() { Name = "Знамя темных фей", GameId = 3055 });
                context.Banners.Add(new Banner() { BannerName = banner70, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner70, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner70, Colors = Green_1 });
                var banner71 = context.BannerNames.Add(new BannerName() { Name = "Знамя Сауруса", GameId = 3050 });
                context.Banners.Add(new Banner() { BannerName = banner71, Colors = Red2 });
                context.Banners.Add(new Banner() { BannerName = banner71, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner71, Colors = Brown_1 });
                var banner72 = context.BannerNames.Add(new BannerName() { Name = "Умбральное знамя", GameId = 3081 });
                context.Banners.Add(new Banner() { BannerName = banner72, Colors = Purple2 });
                context.Banners.Add(new Banner() { BannerName = banner72, Colors = Yellow });
                context.Banners.Add(new Banner() { BannerName = banner72, Colors = Brown_1 });
                var banner73 = context.BannerNames.Add(new BannerName() { Name = "Знамя медведей", GameId = 3010 });
                context.Banners.Add(new Banner() { BannerName = banner73, Colors = Brown2 });
                context.Banners.Add(new Banner() { BannerName = banner73, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner73, Colors = Purple_1 });
                var banner74 = context.BannerNames.Add(new BannerName() { Name = "Знамя гробницы", GameId = 3040 });
                context.Banners.Add(new Banner() { BannerName = banner74, Colors = Red2 });
                context.Banners.Add(new Banner() { BannerName = banner74, Colors = Purple });
                context.Banners.Add(new Banner() { BannerName = banner74, Colors = Green_1 });
                var banner75 = context.BannerNames.Add(new BannerName() { Name = "Знамя Кракена", GameId = 3026 });
                context.Banners.Add(new Banner() { BannerName = banner75, Colors = Brown2 });
                context.Banners.Add(new Banner() { BannerName = banner75, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner75, Colors = Purple_1 });
                var banner76 = context.BannerNames.Add(new BannerName() { Name = "Знамя убийства", GameId = 3065 });
                context.Banners.Add(new Banner() { BannerName = banner76, Colors = Blue2 });
                context.Banners.Add(new Banner() { BannerName = banner76, Colors = Purple });
                context.Banners.Add(new Banner() { BannerName = banner76, Colors = Yellow_1 });
                var banner77 = context.BannerNames.Add(new BannerName() { Name = "Знамя Змеи", GameId = 3016 });
                context.Banners.Add(new Banner() { BannerName = banner77, Colors = Blue });
                context.Banners.Add(new Banner() { BannerName = banner77, Colors = Red });
                var banner78 = context.BannerNames.Add(new BannerName() { Name = "Знамя Великанов", GameId = 3013 });
                context.Banners.Add(new Banner() { BannerName = banner78, Colors = Blue2 });
                var banner79 = context.BannerNames.Add(new BannerName() { Name = "Знамя с фонарем", GameId = 3030 });
                context.Banners.Add(new Banner() { BannerName = banner79, Colors = Yellow2 });
                context.Banners.Add(new Banner() { BannerName = banner79, Colors = Red });
                context.Banners.Add(new Banner() { BannerName = banner79, Colors = Green_1 });
                var banner80 = context.BannerNames.Add(new BannerName() { Name = "Паучье знамя", GameId = 3047 });
                context.Banners.Add(new Banner() { BannerName = banner80, Colors = Green2 });
                context.Banners.Add(new Banner() { BannerName = banner80, Colors = Purple });
                context.Banners.Add(new Banner() { BannerName = banner80, Colors = Red_1 });
                var banner81 = context.BannerNames.Add(new BannerName() { Name = "Летнее знамя", GameId = 3002 });
                context.Banners.Add(new Banner() { BannerName = banner81, Colors = Green2 });
                context.Banners.Add(new Banner() { BannerName = banner81, Colors = Purple });
                context.Banners.Add(new Banner() { BannerName = banner81, Colors = Blue_1 });
                var banner82 = context.BannerNames.Add(new BannerName() { Name = "Знамя духа лисы", GameId = 3090 });
                context.Banners.Add(new Banner() { BannerName = banner82, Colors = Red2 });
                context.Banners.Add(new Banner() { BannerName = banner82, Colors = Green });
                context.Banners.Add(new Banner() { BannerName = banner82, Colors = Yellow_1 });
                context.SaveChanges();
            }


        }

        public override void Down()
        {
        }
    }
}