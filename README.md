# OneScriptFormsDesigner
Дизайнер форм для сценарного языка OneScript.

В дополнение к графическому интерфейсу для сценарного языка OneScript написан представленный здесь дизайнер форм.

Он позволяет создавать формы, размещать на форме элементы управления, устанавливать их свойства, обработчики событий в виде не заполненных кодом процедур. Спроектированную форму можно сразу запустить на исполнение и увидеть результаты, или сохранить форму в скрипт с синтакисом OneScript, или сгенерировать и просмотреть код непосредственно в дизайнере.

Как основная ставилась цель полной русификации всех свойств компонентов и она достигнута. Исходными данными послужила справка из моего репозитория для OneScriptForms. Всё на русском и при запуске в среде не русифицированной Windows (только русская раскладка консоли естественно должна быть). Редакторы коллекций дизайнера не поддающиеся модификации были написаны с нуля.

![Дизайнер](https://github.com/ahyahy/OneScriptFormsDesigner/blob/main/docs/OneScriptFormsDesigner.png)

Огромную помощь в работе оказала разработка примера работы с дизайнером **picoFormDesigner** (coded by Paolo Foti <https://www.codeproject.com/Articles/60175/The-DesignSurface-Extended-Class-is-Back-Together?fid=1561636&df=90&mpp=25&sort=Position&spc=Relaxed&prof=True&view=Normal&fr=26#xx0xx> под лицензией (CPOL) <https://www.codeproject.com/info/cpol10.aspx>).

Предполагается, что OneScript на компьютере уже установлен. Дизайнер реализован в виде библиотеки и подключается к OneScript как внешняя компонента. Её найдете в каталоге /docs . Запустить можно так:
```bsl
     ПодключитьВнешнююКомпоненту("ВашКаталогНаДиске\OneScriptFormsDesigner.dll");
     ДФ = Новый ДизайнерФормДляОдноСкрипта();
     ДФ.Дизайнер();
```

**ПРОЕКТ НАХОДИТСЯ В СТАДИИ ДОРАБОТКИ.** Не полнофункционально только сохранение формы в файл **osd** и её последующее восстановление для возобновления работы. Пока корректно восстановить можно форму наполенную простыми компонентами (кнопками, панелями, не визуальными компонентами). Решение мной найдено, нужно ещё немного времени.

Формирование сценария в файл **os** выполняется корректно.

Так же ещё не написана справка для дизайнера. Тоже дело времени.

После решения этих моментов будет выпущен релиз. А пока жду замечаний по поводу работы и внешнего вида дизайнера.
