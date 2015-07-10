# Books
Да се направи приложение на ASP.NET MVC, което да предлага следната функционалност:
1)	При зареждането на приложението да се отваря страница за Login, където потребителят да въведе в 2 текстови полета username и password. При невъведен текст или при некоректни данни да се извежда подходящо съобщение. Да има бутон Login, който да пренасочва към основната страница на приложението. Да има възможност за регистриране на нов потребител чрез линк, който води към страница за регистрация (виж т.2).
2)	При зареждането на страницата за регистрация, потребителят да има възможност да въведе следните данни: username (текстово поле), password (текстово поле от тип парола), password confirmation (текстово поле от тип парола), age (текстово поле, което да позволява въвеждането на цели числа от 18 до 99). При некоректни данни да се изведе подходящо съобщение. При коректни данни, потребителят се пренасочва към страницата за Login.
3)	Основната страница трябва да предоставя следната функционалност:
•	При опит за директно отваряне на страницата (чрез URL) от потребител, който не се е log-нал в приложението, потребителят да бъде автоматично препратен към Login страницата.
•	В горния ляв ъгъл да има секция, която да изписва текста „Welcome, [username]”, където username е името на вписания потребител.
•	На страницата да се визуализира таблица, която да има следните колони: обложка на книга (картинка), име на книга, година на издаване, автор. Най-отдолу в самата таблица да има възможност за добавяне на нова книга – за целта трябва да има контрола за добавяне на картинка (позволени разширения са .jpg, .jpeg, .png, .gif), текстово поле за име на книгата, текстово поле за годината на издаване на книгата и падащ списък (drop down list) с имена на автори. При първоначалното отваряне на страницата, тъй като няма да има данни, трябва да има възможност за добавяне на нова книга по същия начин. При некоректни данни да се извежда подходящо съобщение за грешка. Ако при добавяне на нова книга не се избере картинка за обложка,  по подразбиране да се слага предварително избрана картинка.
•	За стилизиране на таблицата да се използва jQuery DataTables плъгин
•	Последната колона от таблицата да показва две подходящи иконки за редакция и изтриване на даден ред. При кликане върху иконката за изтриване, текущият ред да бъде изтрит от таблицата. При кликане върху иконката за редакция да се позволява редактирането на записа, като потребителят бъде пренасочен към страница за редакция на полетата, където да има два бутона – за запис на направените промени и за отказ.
•	Над таблицата да има 2 текстови полета за търсене по име на книга и име на автор. Полетата не са задължителни. До тях да има 2 бутона – „Search” и „Clear”. При натискането на първия бутон в таблицата да се покажат само тези записи, които отговарят на филтъра. При натискането на бутона „Clear“, да се зачистят текстовите полета и в таблицата да се покажат всички налични записи.
4)	Да се използват Dependency injection, Repository pattern и Unit of work pattern
5)	За запазване на информацията за книгите, авторите и потребителите да се ползва SQL Server. Да се използва Entity Framework Power Tools при създаването на необходимите таблици в базата с техните полета (със съответните им типове и ограничения, ключове) и да се направят необходимите връзки между таблиците (ако има). Чрез Post Deployment Script да се добавят произволен брой автори по избор директно в базата от данни. Книгите ще се добавят през приложението и не е необходимо да се добавят директно в базата.
6)	При възникване на неочаквана грешка в приложението, потребителят да бъде пренасочен към специално създадена за целта Error страница, където да се извежда съобщението от възникналата грешка и да има линк към основната страница.
7)	Да се създаде CSS файл, в който да се зададе общ стил на бутоните и текстовите полета в приложението и подходящ фон на страниците от приложението.
8)	Да се използва T4MVC
Общо време за работа: 40 часа (5 дни)


