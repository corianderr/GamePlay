using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GamePlay.DAL.Migrations
{
    public partial class AddGamesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "AverageRating", "Description", "MaxPlayTime", "MaxPlayers", "MinAge", "MinPlayTime", "MinPlayers", "Name", "NameEn", "NameRu", "PhotoPath", "YearOfRelease" },
                values: new object[,]
                {
                    { new Guid("0dcecdaa-9e42-4834-9d19-47c6773dd180"), 0.0, "<p>Дополнение представляется собой облегчение и небольшое переосмысление Границ. Добавляет смежные участки вокруг городов игроков и предоставляет механики, которые позволят за них побороться.&nbsp;</p>", 60, 7, 8, 30, 3, "7 Wonders: Conquest", "7-wonders-conquest", "7 чудес: завоевание", "https://s.tesera.ru/images/items/2270348,3/200x200xpa/photo1.png", 2023 },
                    { new Guid("107af80a-82b3-47f6-a4e7-1c8276732b86"), 0.0, null, 60, 4, 14, 45, 2, "Chaos Cove", "chaos-cove", "Бухта Хаоса", "https://s.tesera.ru/images/items/2270712,3/200x200xpa/photo1.jpg", 2024 },
                    { new Guid("1383f4d3-2c47-4893-a3b9-59bcdc16c045"), 0.0, null, 0, 0, 7, 0, 1, "Wirbel im Wald", "wirbel-im-wald", "Вирбель-им-Вальд", "https://s.tesera.ru/images/items/2270096,3/200x200xpa/photo1.jpg", 0 },
                    { new Guid("15c5ce86-6f43-4622-abec-7855addc8a89"), 0.0, "<p>Это дополнение добавляет в игру карты искусства и культуры, которые замешиваются в карты Эпох. Механика схожа со сбором научных символов.</p>", 60, 8, 7, 30, 3, "7 Wonders: Art&Culture", "7-wonders-art-culture", "7 чудес: искусство", "https://s.tesera.ru/images/items/2270189,3/200x200xpa/photo1.jpg", 2023 },
                    { new Guid("195b794b-208b-4725-b61a-24f256d5d2e6"), 0.0, null, 0, 0, 0, 0, 0, "7 Чудес: Лидеры (Второе издание)", "7-chudes-lidery-vtoroye-izdaniye", "7 Чудес: Лидеры (Второе издание)", null, 0 },
                    { new Guid("1be92ae4-dd17-4409-bdac-da72554ba2d0"), 0.0, "<p>В далеком будущем люди больше не будут населять Землю. Причина их исчезновения (или, возможно, гибели) неизвестна, но их отсутствие оставило пустоту, готовую заполниться другим разумным видом.</p>\r\n<p>&nbsp;</p>\r\n<p>На протяжении бесчисленных поколений один вид скромных медоносных пчел эволюционировал, чтобы заполнить эту пустоту. Они выросли в размерах и интеллекте, став высокоразвитым обществом. Они называют себя Mellifera, они и добились значительных технологических достижений в дополнение к тем технологиям, которые они адаптировали на руинах человечества, вплоть до космических путешествий.</p>\r\n<p>&nbsp;</p>\r\n<p>В<strong><i> Apiary</i></strong> каждый игрок контролирует 1 из 20 уникальных фракций.</p>\r\n<p>Ваша фракция начинает игру с ульем, несколькими ресурсами и рабочими пчелами. Стройте улей, исследуйте планеты, собирайте ресурсы, разрабатывайте технологии, чтобы продемонстрировать силу своей фракции (измеряемую в победных очках).</p>\r\n<p>Важ ждут испытания, грозит нехватка продуктов, и вашим рабочим пчелам остается сделать всего несколько действий, прежде чем они впадут в спячку!</p>\r\n<p>&nbsp;</p>\r\n<p>Сможете ли вы процветать или просто выжить?</p>", 90, 5, 14, 60, 1, "Apiary", "apiary", "пасека", "https://s.tesera.ru/images/items/2270555,3/200x200xpa/photo1.jpg", 2023 },
                    { new Guid("1c73c50a-c8ae-4162-b0f4-ced4cc12055b"), 0.0, "<p>Мифы - это фанатское дополнение к \"7 чудес\".</p>\r\n<p>Город жертвует ресурсами или выполняет строительные требования, чтобы призвать \"божественное благословение\" древнего божества из греческой, египетской и вавилонской мифологии.</p>\r\n<p>Дополнение содержит:<br />- 35 карт мифов, представляющих пантеон древних богов<br />- 2 новые планшеты Чудес (Итака и Тартарос)<br />- 3 карты гильдии<br />- 6 карт лидеров<br />- 1 свод правил</p>", 60, 7, 8, 30, 3, "7 Чудес: Мифы", "7-wonders-myths", "7 Чудес: Мифы", "https://s.tesera.ru/images/items/2270328,3/200x200xpa/photo1.jpg", 2012 },
                    { new Guid("21f13b5c-f3cb-43be-a38d-a17ee5c78e3e"), 0.0, null, 0, 0, 0, 0, 0, "The Number", "number", "Номер", null, 2020 },
                    { new Guid("32014b04-ed97-4493-bde1-ed00352d9872"), 0.0, null, 30, 3, 0, 20, 2, "Planet etuC", "planet-etuc", "Планета etuC", "https://s.tesera.ru/images/items/2270499,3/200x200xpa/photo1.jpg", 2023 },
                    { new Guid("37ca2c55-e7c6-4b7f-be44-300a046fc0f5"), 0.0, null, 40, 4, 0, 20, 3, "Twinkle Starship", "Twinkle-Starship", "Мерцающий звездолет", "https://s.tesera.ru/images/items/2270493,3/200x200xpa/photo1.jpg", 2020 },
                    { new Guid("3c6acb93-06f7-4eac-9c43-835db1e76130"), 0.0, null, 0, 0, 0, 0, 0, "7 Чудес: Армада", "7-chudes-armada", "7 Чудес: Армада", null, 0 },
                    { new Guid("3f48b3f6-2b79-4a65-bd59-0b2f15b530d2"), 0.0, "<p>Миниатюрный \"Древний ужас\", сделанный фанатом. Механики очень похожие, только партии гораздо быстрее. Ну и контента гораздо меньше, понятное дело. Любителям ДУ рекомендуется. Просто распечатываешь и играешь (также понадобятся кубики и миплы или фишки).</p>", 30, 4, 0, 20, 1, "Tiny Eldritch Horror", "tiny-eldritch", "Крошечный жуткий ужас", "https://s.tesera.ru/images/items/2271231,3/200x200xpa/photo1.png", 2018 },
                    { new Guid("4110d242-69c2-45a4-8766-39464a621588"), 0.0, null, 30, 5, 0, 20, 3, "Color Gangsters", "color-gangsters", "Цветные гангстеры", "https://s.tesera.ru/images/items/2270494,3/200x200xpa/photo1.jpg", 2019 },
                    { new Guid("46d53c82-16c0-4495-9b8a-81f2d31349fa"), 0.0, null, 0, 0, 0, 0, 0, "7 Wonder Ruins (fan expansion)", "7-wonder-ruins-fan-expansion", "7 чудесных руин (фанатское дополнение)", null, 0 },
                    { new Guid("51bfe9d8-49d4-4fbf-85b0-4ce915a6b877"), 0.0, "<p>В автономном продолжении <i>Mythic Mischief</i> вы играете за фракцию студентов Mythic Manor, соревнующихся за то, чтобы садовник поймал как можно больше других учеников, но не вас.</p>\r\n<p><br />Горгульи, гномы, оборотни и феи обладают собственным уникальным набором способностей, позволяющих перемещаться по полю, перемещать другие фракции на путь садовника и даже изменять его курс, перемещая живые изгороди.</p>\r\n<p><br />Игроки могут улучшать способности своей фракции на протяжении всей игры, собирая мощные фолианты со всего лабиринта из живой изгороди.<br />Победителем становится команда, первая набравшая 10 очков за шалости, или команда, набравшая наибольшее количество очков на момент, когда садовник завершит возврат всех фолиантов после обеда.</p>", 90, 4, 14, 45, 1, "Mythic Mischief Vol. II", "mythic-mischief-vol-ii", "Мифическое озорство Том. ", "https://s.tesera.ru/images/items/2270751,3/200x200xpa/photo1.jpg", 2024 },
                    { new Guid("6821b468-c35f-461f-8f19-8509c0c4bfb0"), 0.0, "<p>Данное дополнение немного изменяет механику получения очков научными зданиями, которые представленны зелеными картами. Добавляет колоду карт \"Прогресс\" со своими различными эффектами. Дополнение может быть замешано с базой или с любыми иными, очень хорошо играется в паре с новым \"Искусство и Культура\"</p>", 60, 8, 8, 30, 3, "7 Wonders: Progress", "7-wonders-progress", "7 чудес: прогресс", "https://s.tesera.ru/images/items/2270280,3/200x200xpa/photo1.jpg", 2023 },
                    { new Guid("6dbd2626-3d47-40a4-b452-a9b726b792be"), 0.0, null, 0, 0, 0, 0, 0, "7 чудес: Лидеры", "7-chudes-lidery", "7 чудес: Лидеры", null, 0 },
                    { new Guid("6e005aa8-80a2-4a57-9996-eee379f196b8"), 0.0, null, 0, 0, 0, 0, 0, "Большая бродилка: На пути к трону желаний", "bolshaya-brodilka-na-puti-k-tronu-zhelany", "Большая бродилка: На пути к трону желаний", null, 0 },
                    { new Guid("7833acb5-eb90-4c1f-82ab-9813ca5531ff"), 0.0, "<p>В Noctenburg ходит множество слухов: горожане шепчутся о том, что видели странных животных в своих домах, больницах и даже в суде, которые ведут себя странно, по-человечески. Действительно, зловещие предзнаменования...</p>\r\n<p>&nbsp;</p>\r\n<p>Получите новые способности, принимая могущественную форму животного, и остерегайтесь надвигающихся знамений в этом модульном дополнении для <i>Септимы</i>.</p>", 120, 4, 12, 50, 1, "Septima: Shapeshifting & Omens", "septima-shapeshifting-omens", "Септима: Изменение формы", "https://s.tesera.ru/images/items/2270700,3/200x200xpa/photo1.jpg", 2023 },
                    { new Guid("80a84cb7-8b13-41ea-9f07-d5e5799a1bd8"), 0.0, "<p>3D-игра с высокими ставками!</p>\r\n<p>Каждый за столом - карьерный преступник, готовящийся к выходу на пенсию. Неважно, кто вы - организатор или наемный убийца, вы хотите участвовать в как можно большем количестве дел и заработать как можно больше денег. Выйти победителем из серии ограблений с драгоценностями и получить наибольшую сумму денег, объединившись с другими грабителями для взлома хранилища. Совместными усилиями, используя крошечные руки, вы сможете физически сдвинуть \"висячие замки\" и похитить драгоценности, которые они защищают. Чем сложнее ограбление, тем больше вознаграждение - кто в итоге получит больше денег, тот и выиграл.</p>\r\n<p>Во время каждого хода игры Tiny Laser Heist один из игроков становится главным и выбирает свою команду. Игроки, не выбранные им, теперь играют против этой команды, чтобы остановить ограбление. Обе команды разыгрывают карты действий, определяющие способности команд и препятствия, с которыми они должны столкнуться во время ограбления. Затем у команды есть 90 секунд, чтобы украсть драгоценности. Если вам это удается, главный герой забирает и распределяет карты денег между членами своей команды. Игра продолжается до тех пор, пока не закончатся все деньги.</p>", 90, 6, 8, 30, 3, "Tiny Laser Heist", "tiny-laser-heist", "Крошечное лазерное ограбление", "https://s.tesera.ru/images/items/2270662,3/200x200xpa/photo1.png", 2024 },
                    { new Guid("914bed20-6025-4584-b3dc-5b3cd94be1fb"), 0.0, "<p>Цель этого расширения - улучшить взаимодействие между игроком и его соседями, добавив игровую зону между ними, а также новое чудо.<br />Границы позволят вам более точно управлять своей империей. Действительно, если ваши границы - это пределы вашей империи, то ваше чудо и ваши личные карточки представляют вашу столицу? Однако, как следует из названия, граница является общей для другого игрока и также является пределом его империи. И, как и в реальной жизни, вся инфраструктура, построенная на границе, принесет пользу двум империям.</p>", 60, 7, 8, 30, 3, "7 Wonders: Frontiers", "7-wonders-frontiers", "7 чудес: границы", "https://s.tesera.ru/images/items/2270341,3/200x200xpa/photo1.png", 2021 },
                    { new Guid("924e7d58-679a-44e1-a76b-bff9a5e5a3a4"), 0.0, null, 0, 0, 0, 0, 0, "7 Чудес: Армада (Второе издание)", "7-chudes-armada-vtoroye-izdaniye", "7 Чудес: Армада (Второе издание)", null, 0 },
                    { new Guid("9db08a35-e100-4223-860d-fe4986ddfd3f"), 0.0, null, 180, 4, 0, 180, 3, "coiffeur-jass", "coiffeur-jass", "парикмахер-джасс", "https://s.tesera.ru/images/items/2270491,3/200x200xpa/photo1.jpg", 0 },
                    { new Guid("b4b9ffd1-046c-4f1e-af60-281a4016f3ea"), 0.0, "<p style=\"text-align: right;\">Игроку предстоит примерить на себя роль рыцаря. Бродить по округам городов и деревень, выполнять различные задания и сразиться с финальным БОССОМ, не позволив ему разрушить Королевство!</p>\r\n<p>&nbsp;</p>\r\n<p>Игра в жанре Print and Play в мире фэнтези.</p>\r\n<p>&nbsp;</p>\r\n<p>Выполняйте задания, сражайтесь с монстрами, покупайте снаряжение и усиливайте своих героев!</p>", 25, 4, 5, 10, 2, "The Mini Quest", "theminiquest", "Мини-квест", "https://s.tesera.ru/images/items/2270473,3/200x200xpa/photo1.jpg", 2011 },
                    { new Guid("c6f1a1dd-13c3-45ae-855f-6329e055e511"), 0.0, null, 40, 7, 8, 20, 3, "7 Wonders Selection 2nd Edition", "7-wonders-selection-2nd-edition", "Подборка «7 чудес», 2-е издание", null, 0 },
                    { new Guid("db0e3adf-dc43-4344-a9cc-5284d16646a3"), 0.0, "<p>Фанатское дополнение \"7 Чудес: More Leaders\" существенно расширяет стандартный набор лидеров из дополнения 7 Wonders: Leaders. Карты выполнены в дизайне 2-го издания игры и добавлят в игру:</p>\r\n<p>-25 лидеров для Базовой Игры.</p>\r\n<p>-26 лидеров для дополнения Армада.</p>\r\n<p>-6 лидеров для дополнения Здания (Edifice)</p>\r\n<p>-3 лидера для дополнения Города.</p>\r\n<p>-5 лидеров для фанатского дополнения Мореплаватели (Sailors).</p>", 60, 7, 8, 40, 3, "7 Wonders More Leaders", "7-wonders-more-leaders", "7 чудес: больше лидеров", "https://s.tesera.ru/images/items/2270175,3/200x200xpa/photo1.jpg", 2022 },
                    { new Guid("dc8bfcf1-2655-4b7e-b07e-778f44fb6c26"), 0.0, null, 0, 6, 18, 60, 1, "Зомбицид. Вторая редакция: Городские легенды. Набор отродий", "zombitsid-vtoraya-redaktsiya-gorodskiye-legendy-nabor-otrody", "Зомбицид. ", "https://s.tesera.ru/images/items/2270432,3/200x200xpa/photo1.jpg", 0 },
                    { new Guid("de419121-a887-4a61-a1c9-7c71537a3325"), 0.0, "<p style=\"text-align: justify;\">Полный опасностей и приключений путь к победе &mdash; это извилистая дорога к Трону Желаний. Как и в первой части, здесь нет правил. Да они и не нужны, ведь всё самое важное содержится в кратком своде законов, а остальное отмечено прямо на игровом поле.</p>\r\n<p style=\"text-align: justify;\">&nbsp;</p>\r\n<p style=\"text-align: justify;\">Перед стартом выберите себе одного из пяти персонажей с уникальными способностями. Премилая котейка крадёт монеты у других игроков, которые на обгоне не могут сопротивляться соблазну погладить её, умудрённая опытом бабуля мчится к финишу с увесистым рюкзаком полезных вещиц наперевес, а угрюмого парня в готическом плаще радуют лишь выпадающие соперникам единицы, ведь тогда он делает шаг вперёд. Бросайте кубики &mdash; мы начинаем!</p>\r\n<p style=\"text-align: justify;\">&nbsp;</p>\r\n<p>Что нового?</p>\r\n<ul>\r\n<li>У каждого персонажа уже есть своя уникальная способность.</li>\r\n<li>Больше взаимодействия между игроками: вы чаще воруете монеты и карты, больше вредите друг другу &mdash; и всё это становится одновременно конфликтнее, но и ещё веселее.</li>\r\n<li>Куча новых карт, красочное поле с особенными клетками и самыми разными препятствиями.</li>\r\n</ul>\r\n<p>Карты из обеих &ldquo;Бродилок&rdquo; можно смешать в одну колоду и играть любыми героями на любом поле. Максимум реиграбельности и приключений!</p>", 0, 5, 8, 30, 2, "Большая Бродилка: На Пути к Трону Желаний", "bolshaya-brodilka-na-puti-k-tronu-zhelany", "Большая Бродилка: На Пути к Трону Желаний", "https://s.tesera.ru/images/items/2270595,3/200x200xpa/photo1.jpg", 2023 },
                    { new Guid("fa9c3f37-ab5e-47e7-a083-7c2675267f59"), 0.0, "<p><span class=\"Y2IQFc\" lang=\"ru\">Это дополнение вносит разнообразие в промежуточный подсчет очков. <br />Когда начинается этап промежуточного подсчета очков, игроки могут не выбрать ни одного,<br />выбрать некоторые или все 3 заранее определенных выложенными тайлами условий начисления ПО при промежуточном подсчете. </span></p>", 240, 4, 14, 120, 2, "The Gallerist: Scoring Expansion", "galleristscoringexpansion", "The Gallerist: Расширение подсчета очков", "https://s.tesera.ru/images/items/2270333,3/200x200xpa/photo1.jpg", 2019 },
                    { new Guid("fe08f2db-052f-45d1-9d99-fb28d967ad15"), 0.0, "<p>Приветствуем вас, студенты! Космический факультет <i>Technical Academy of Creation</i> рад приветствовать вас на <strong><i>Civolution</i></strong>, выпускном экзамене по цивилизованному дизайну!</p>\r\n<p>&nbsp;</p>\r\n<p>По этому случаю мы подготовили для вас гуманоидный сценарий на изолированном континенте. Здесь каждый из вас имеет ранг местного божества, которое тесно связано с его собственной цивилизацией и должно привести ее к успеху над другими цивилизациями.</p>\r\n<p><br />Ваши возможности развития безграничны и простираются от культурного и технического прогресса до эволюционных адаптаций. Например, что бы вы сочли более полезным для ваших племен: изобретение колеса или выращивание крыльев?</p>\r\n<p><br />Продемонстрируйте свою способность управлять консолью цивилизации и покажите нам, насколько хорошо вы можете приспосабливаться к изменчивым условиям окружающей среды и умеренному творческому хаосу.</p>\r\n<p>&nbsp;</p>\r\n<p>Когда экзамен прекратится после четырех эпох, тот, кому удалось набрать наибольшее количество очков успеха, не только сдаст экзамен, но и станет полноправным членом <i>Technical Academy of Creation</i> и получит возможность перейти в следующую инстанцию.</p>\r\n<p>&nbsp;</p>\r\n<p><strong><i>Civolution</i></strong> &mdash; это евроигра (medium heavy to heavy), в которой используется механизм выбора кубиков для запуска действий в древовидной структуре технологий. Когда вы поймете, как лучше всего использовать свои кубики и ввести в игру свои уникальные карты, появится множество стратегий и путей к победе, хотя каждый раз, когда вы играете, вы будете исследовать лишь часть возможностей, которые предоставляет игровая система и множество карт.</p>", 180, 4, 14, 90, 1, "Civolution", "civolution", "Циволюция", "https://s.tesera.ru/images/items/2271266,3/200x200xpa/photo1.jpg", 2024 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("0dcecdaa-9e42-4834-9d19-47c6773dd180"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("107af80a-82b3-47f6-a4e7-1c8276732b86"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("1383f4d3-2c47-4893-a3b9-59bcdc16c045"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("15c5ce86-6f43-4622-abec-7855addc8a89"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("195b794b-208b-4725-b61a-24f256d5d2e6"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("1be92ae4-dd17-4409-bdac-da72554ba2d0"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("1c73c50a-c8ae-4162-b0f4-ced4cc12055b"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("21f13b5c-f3cb-43be-a38d-a17ee5c78e3e"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("32014b04-ed97-4493-bde1-ed00352d9872"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("37ca2c55-e7c6-4b7f-be44-300a046fc0f5"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("3c6acb93-06f7-4eac-9c43-835db1e76130"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("3f48b3f6-2b79-4a65-bd59-0b2f15b530d2"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("4110d242-69c2-45a4-8766-39464a621588"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("46d53c82-16c0-4495-9b8a-81f2d31349fa"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("51bfe9d8-49d4-4fbf-85b0-4ce915a6b877"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("6821b468-c35f-461f-8f19-8509c0c4bfb0"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("6dbd2626-3d47-40a4-b452-a9b726b792be"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("6e005aa8-80a2-4a57-9996-eee379f196b8"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("7833acb5-eb90-4c1f-82ab-9813ca5531ff"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("80a84cb7-8b13-41ea-9f07-d5e5799a1bd8"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("914bed20-6025-4584-b3dc-5b3cd94be1fb"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("924e7d58-679a-44e1-a76b-bff9a5e5a3a4"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("9db08a35-e100-4223-860d-fe4986ddfd3f"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("b4b9ffd1-046c-4f1e-af60-281a4016f3ea"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("c6f1a1dd-13c3-45ae-855f-6329e055e511"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("db0e3adf-dc43-4344-a9cc-5284d16646a3"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("dc8bfcf1-2655-4b7e-b07e-778f44fb6c26"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("de419121-a887-4a61-a1c9-7c71537a3325"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("fa9c3f37-ab5e-47e7-a083-7c2675267f59"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("fe08f2db-052f-45d1-9d99-fb28d967ad15"));
        }
    }
}
