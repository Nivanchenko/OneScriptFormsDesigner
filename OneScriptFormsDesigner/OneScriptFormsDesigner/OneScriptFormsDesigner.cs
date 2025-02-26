﻿using System;
using System.Collections;
using System.Collections.Generic;
using ScriptEngine.Machine.Contexts;
using ScriptEngine.Machine;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace osfDesigner
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!
    [ContextClass("ДизайнерФормДляОдноСкрипта", "OneScriptFormsDesigner")]
    public class OneScriptFormsDesigner : AutoContext<OneScriptFormsDesigner>
    {
        public static System.Collections.Hashtable hashtable = new Hashtable();// хранит связь исходного объекта с его дублером
        public static System.Collections.Hashtable hashtableDesignerTabName = new Hashtable();// хранит имя вкладок дизайнера
        public static System.Collections.Hashtable hashtableDesignerTabRootComponent = new Hashtable();// хранит связь rootComponent и создаваемой для него вкладки дризайнера
        public static System.Collections.Hashtable hashtableNodeName = new Hashtable();// хранит имя последнего созданного узла дерева для дерева
        public static System.Collections.Hashtable hashtableDataGridTableStyleName = new Hashtable();// хранит имя последнего созданного СтильТаблицыСеткиДанных для СеткиДанных
        public static System.Collections.Hashtable hashtableDataGridColumnStyleName = new Hashtable();// хранит имя последнего созданного СтильКолонкиСеткиДанных для СтильТаблицыСеткиДанных
        public static System.Collections.Hashtable hashtableListViewItemName = new Hashtable();// хранит имя последнего созданного ЭлементСпискаЭлементов для СписокЭлементов
        public static System.Collections.Hashtable hashtableListViewSubItemName = new Hashtable();// хранит имя последнего созданного ПодэлементСпискаЭлементов для ЭлементСпискаЭлементов
        public static System.Collections.Hashtable hashtableToolBarButtonName = new Hashtable();// хранит имя последней созданной КнопкаПанелиИнструментов для ПанельИнструментов
        public static System.Collections.Hashtable hashtableColumnHeaderName = new Hashtable();// хранит имя последней созданной Колонка для СписокЭлементов
        public static System.Collections.Hashtable hashtableStatusBarPanelName = new Hashtable();// хранит имя последней созданной ПанельСтрокиСостояния для СтрокаСостояния
        public static System.Collections.Hashtable hashtableMenuName = new Hashtable();// хранит имя последнего созданного ЭлементМеню для ГлавноеМеню или ЭлементМеню
        public static System.Collections.Hashtable hashtableSeparatorName = new Hashtable();// хранит имя последнего созданного разделителя для меню
        public static string str1 = "";
        public static int tic1 = 0;// счетчик для правильной работы TabControl, пропуск двух шагов по созданию дизайнером двух вкладок по умолчанию
        public static bool block1 = false;// тригер для блокировки выделения объекта на форме


        //!!!!!!!!!!!!!!!!!!!!!!!!!!
        //////public static System.Collections.ArrayList ArrayListComponentsAddingOrder = new System.Collections.ArrayList();// хранит порядок добавления компонентов
        //////public static bool loadForm = false;


        [ScriptConstructor]
        public static IRuntimeContextInstance Constructor()
        {
            return new OneScriptFormsDesigner();
        }

        [ContextMethod("Дизайнер", "Designer")]
        public void Designer(string p1 = null)
        {
            if (p1 == "ВосстановитьКонсоль")
            {
                RestoreConsole();
            }
            else if (p1 == "СкрытьКонсоль")
            {
                HideConsole();
            }
            else
            {
                MinimizedConsole();
            }
            var thread = new Thread(() =>
            {
                osfDesigner.Program Program1 = new Program();
                Program1.Main();
            }
            );
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
	
        public static void PassProperties(dynamic p1, dynamic p2)//p1 - исходный объект (OriginalObj), p2 - дублёр исходного объекта (SimilarObj), для отображения свойств в сетке свойств
        {
            string str1 = "";
            System.Reflection.PropertyInfo[] PropertyInfo = p2.GetType().GetProperties();
            for (int i = 0; i < PropertyInfo.Length; i++)
            {
                try
                {
                    if (p1.GetType().ToString() == "System.Windows.Forms.TabPage")
                    {
                        if (PropertyInfo[i].Name !=  "Parent")
                        {
                            p2.GetType().GetProperty(PropertyInfo[i].Name).SetValue(p2, p1.GetType().GetProperty(PropertyInfo[i].Name).GetValue(p1));
                        }
                    }
                    else
                    {
                        p2.GetType().GetProperty(PropertyInfo[i].Name).SetValue(p2, p1.GetType().GetProperty(PropertyInfo[i].Name).GetValue(p1));
                    }
                }
                catch
                {
                    str1 = str1 + Environment.NewLine + "действие - Не удалось передать свойство " + p1.GetType().ToString() + " - " + PropertyInfo[i].Name;
                }
            }
        }

        public static void ReturnProperties(dynamic p1, dynamic p2)//p1 - исходный объект (OriginalObj), p2 - дублёр исходного объекта (SimilarObj), для отображения свойств в сетке свойств
        {
            string str1 = "";
            System.Reflection.PropertyInfo[] PropertyInfo = p1.GetType().GetProperties();
            for (int i = 0; i < PropertyInfo.Length; i++)
            {
                try
                {
                    p1.GetType().GetProperty(PropertyInfo[i].Name).SetValue(p1, p2.GetType().GetProperty(PropertyInfo[i].Name).GetValue(p2));
                }
                catch
                {
                    str1 = str1 + Environment.NewLine + "действие - Не удалось вернуть свойство " + p2.GetType().ToString() + " - " + PropertyInfo[i].Name;
                }
            }
        }
	
        public static dynamic RevertNodeName(dynamic p1)//p1 - дерево, для каждого дерева своя нумерация
        {
            string p2 = null;
            if (!OneScriptFormsDesigner.hashtableNodeName.ContainsKey(p1))
            {
                p2 = "Узел0";
                OneScriptFormsDesigner.hashtableNodeName.Add(p1, p2);
                return p2;
            }
            else
            {
                foreach (System.Collections.DictionaryEntry de in OneScriptFormsDesigner.hashtableNodeName)
                {
                    if (de.Key.Equals(p1))
                    {
                        p2 = (string)de.Value;
                        int value = Int32.Parse(p2.Substring(4));
                        if (value < Int32.MaxValue)
                        {
                            p2 = "Узел" + (value + 1).ToString();
                        }
                        break;
                    }
                }
                OneScriptFormsDesigner.hashtableNodeName[p1] = p2;
                return p2;
            }
        }

        public static dynamic RevertDesignerTabName(string p1)//p1 - имя открываемой во вкладке формы, p2 - имя для вкладки дизайнера
        {
            string p2 = "Вкладка" + (OneScriptFormsDesigner.hashtableDesignerTabName.Count).ToString() + "(" + p1 + ")";
            OneScriptFormsDesigner.hashtableDesignerTabName.Add(p2, p1);
            return p2;
        }
	
        public static dynamic RevertStatusBarPanelName(dynamic p1, string p2)//p1 - ПанельСтрокиСостояния, p2 - имя для osfDesigner.StatusBarPanel
        {
            if (p2 == "")
            {
                p2 = "ПанельСтрокиСостояния" + (OneScriptFormsDesigner.hashtableStatusBarPanelName.Count).ToString();
                OneScriptFormsDesigner.hashtableStatusBarPanelName.Add(p1, p2);
                return p2;
            }
            return p2;
        }
	
        public static dynamic RevertColumnHeaderName(dynamic p1, string p2)//p1 - Колонка, p2 - имя для osfDesigner.ColumnHeader
        {
            if (p2 == "ColumnHeader")
            {
                p2 = "Колонка" + (OneScriptFormsDesigner.hashtableColumnHeaderName.Count).ToString();
                OneScriptFormsDesigner.hashtableColumnHeaderName.Add(p1, p2);
                return p2;
            }
            return p2;
        }
	
        public static dynamic RevertListViewItemName(dynamic p1, string p2)//p1 - ЭлементСпискаЭлементов, p2 - имя для osfDesigner.ListViewItem
        {
            if (p2.Length == 0)
            {
                p2 = "Элемент" + (OneScriptFormsDesigner.hashtableListViewItemName.Count).ToString();
                OneScriptFormsDesigner.hashtableListViewItemName.Add(p1, p2);
                return p2;
            }
            return p2;
        }

        public static dynamic RevertListViewSubItemName(dynamic p1, string p2)//p1 - ПодэлементСпискаЭлементов, p2 - имя для osfDesigner.ListViewSubItem
        {
            if (p2.Length == 0)
            {
                p2 = "Подэлемент" + (OneScriptFormsDesigner.hashtableListViewSubItemName.Count).ToString();
                OneScriptFormsDesigner.hashtableListViewSubItemName.Add(p1, p2);
                return p2;
            }
            return p2;
        }
	
        public static dynamic RevertMenuName(dynamic p1, string p2)//p1 - Меню, p2 - имя для osfDesigner.MenuItemEntry
        {
            if (p2.Length == 0)
            {
                p2 = "Меню" + (OneScriptFormsDesigner.hashtableMenuName.Count).ToString();
                OneScriptFormsDesigner.hashtableMenuName.Add(p1, p2);
                return p2;
            }
            return p2;
        }
	
        public static dynamic RevertSeparatorName(dynamic p1, string p2)//p1 - Меню, p2 - имя для osfDesigner.MenuItemEntry
        {
            OneScriptFormsDesigner.hashtableSeparatorName.Add(p1, p2);
            string p3 = "Сепаратор" + (OneScriptFormsDesigner.hashtableSeparatorName.Count - 1).ToString();
            return p3;
        }

        public static dynamic RevertToolBarButtonName(dynamic p1, string p2)//p1 - КнопкаПанелиИнструментов, p2 - имя для osfDesigner.ToolBar
        {
            if (p2.Length == 0)
            {
                p2 = "Кн" + (OneScriptFormsDesigner.hashtableToolBarButtonName.Count).ToString();
                OneScriptFormsDesigner.hashtableToolBarButtonName.Add(p1, p2);
                return p2;
            }
            return p2;
        }
	
        public static dynamic RevertDataGridTableStyleName(dynamic p1, string p2)//p1 - СтильТаблицыСеткиДанных, p2 - имя для osfDesigner.DataGridTableStyle
        {
            if (p2 == null)
            {
                p2 = "Стиль" + (OneScriptFormsDesigner.hashtableDataGridTableStyleName.Count).ToString();
                OneScriptFormsDesigner.hashtableDataGridTableStyleName.Add(p1, p2);
                return p2;
            }
            return p2;
        }
	
        public static dynamic RevertDataGridColumnStyleName(dynamic p1, string p2)//p1 - СтильТаблицыСеткиДанных, p2 - имя для osfDesigner.DataGridTableStyle
        {
            if (p2 == null)
            {
                if (p1.GetType() == typeof(osfDesigner.DataGridBoolColumn))
                {
                    p2 = "СтильКолонкиБулево" + (OneScriptFormsDesigner.hashtableDataGridColumnStyleName.Count).ToString();
                    OneScriptFormsDesigner.hashtableDataGridColumnStyleName.Add(p1, p2);
                    return p2;
                }
                else if (p1.GetType() == typeof(osfDesigner.DataGridTextBoxColumn))
                {
                    p2 = "СтильКолонкиПолеВвода" + (OneScriptFormsDesigner.hashtableDataGridColumnStyleName.Count).ToString();
                    OneScriptFormsDesigner.hashtableDataGridColumnStyleName.Add(p1, p2);
                    return p2;
                }
                else if (p1.GetType() == typeof(osfDesigner.DataGridComboBoxColumnStyle))
                {
                    p2 = "СтильКолонкиПолеВыбора" + (OneScriptFormsDesigner.hashtableDataGridColumnStyleName.Count).ToString();
                    OneScriptFormsDesigner.hashtableDataGridColumnStyleName.Add(p1, p2);
                    return p2;
                }
            }
            return p2;
        }

        public static void AddToHashtable(dynamic p1, dynamic p2)//p1 - исходный объект (OriginalObj), p2 - дублёр исходного объекта (SimilarObj), для отображения свойств в сетке свойств
        {
            if (!OneScriptFormsDesigner.hashtable.ContainsKey(p1))
            {
                OneScriptFormsDesigner.hashtable.Add(p1, p2);
            }
        }

        public static void AddToHashtableDesignerTabRootComponent(dynamic p1, dynamic p2)//p1 - RootComponent, форма; p2 - DesignerTab, вкладка дизайнера для этой формы
        {
            if (!OneScriptFormsDesigner.hashtableDesignerTabRootComponent.ContainsKey(p1))
            {
                OneScriptFormsDesigner.hashtableDesignerTabRootComponent.Add(p1, p2);
            }
        }

        public static dynamic RevertDesignerTab(dynamic rootComponent)// возвращает вкладку, на которой находится форма
        {
            foreach (System.Collections.DictionaryEntry de in OneScriptFormsDesigner.hashtableDesignerTabRootComponent)
            {
                if (de.Key.Equals(rootComponent))
                {
                    return de.Value;
                }
            }
            return null;
        }

        public static dynamic RevertSimilarObj(dynamic OriginalObj)
        {
            foreach (System.Collections.DictionaryEntry de in OneScriptFormsDesigner.hashtable)
            {
                if (de.Key.Equals(OriginalObj))
                {
                    return de.Value;
                }
            }
            return null;
        }

        public static dynamic RevertOriginalObj(dynamic SimilarObj)
        {
            foreach (System.Collections.DictionaryEntry de in OneScriptFormsDesigner.hashtable)
            {
                if (de.Value.Equals(SimilarObj))
                {
                    return de.Key;
                }
            }
            return null;
        }
	
        public static void ChangeVisibilityBasedOnMode(ref object[] v, List<string> list)
        {
            List<string> PropertiesToHide = list;
            foreach (var vObject in v)
            {
                System.Reflection.PropertyInfo[] myPropertyInfo;
                myPropertyInfo = vObject.GetType().GetProperties();
                var properties = myPropertyInfo;
                foreach (var p in properties)
                {
                    foreach (string hideProperty in PropertiesToHide)
                    {
                        if (p.Name.ToLower() == hideProperty.ToLower())
                        {
                            setBrowsableProperty(hideProperty, false, vObject);
                        }
                    }
                }
            }
        }

        private static void setBrowsableProperty(string strPropertyName, bool bIsBrowsable, object vObject)
        {
            try
            {
                PropertyDescriptor theDescriptor = System.ComponentModel.TypeDescriptor.GetProperties(vObject.GetType())[strPropertyName];
                BrowsableAttribute theDescriptorBrowsableAttribute = (BrowsableAttribute)theDescriptor.Attributes[typeof(BrowsableAttribute)];
                FieldInfo isBrowsable = theDescriptorBrowsableAttribute.GetType().GetField("Browsable", BindingFlags.IgnoreCase | BindingFlags.NonPublic | BindingFlags.Instance);
                isBrowsable.SetValue(theDescriptorBrowsableAttribute, bIsBrowsable);
            }
            catch { }
        }

        public static string ObjectConvertToString(object p1)
        {
            object obj = p1;
            if (obj != null)
            {
                System.Type objType = p1.GetType();
                TypeConverter Converter1 = System.ComponentModel.TypeDescriptor.GetConverter(objType);
                if (objType == typeof(bool))
                {
                    return MyBooleanConverter.ConvertToString(obj);
                }
                else if (objType == typeof(Size))
                {
                    return MySizeConverter.ConvertToString(obj);
                }
                else if (objType == typeof(Cursor))
                {
                    return MyCursorConverter.ConvertToString(obj);
                }
                else if (objType == typeof(Color))
                {
                    return MyColorConverter.ConvertToString(Converter1.ConvertToString(obj));
                }
                else if (objType == typeof(Point))
                {
                    return MyLocationConverter.ConvertToString(obj);
                }
                else if (objType == typeof(Font))
                {
                    return MyFontConverter.ConvertToString(Converter1.ConvertToString(obj));
                }
                else if (objType == typeof(Bitmap))
                {
                    return Converter1.ConvertToString( (System.Drawing.Bitmap)obj ) + " (" + ( (System.Drawing.Bitmap)obj).Tag + ")";
                }
	
                else if (objType == typeof(osfDesigner.MyIcon))
                {
                    return MyIconConverter.ConvertToString(obj);
                }
	
                else
                {
                    return Converter1.ConvertToString(obj);
                }
            }

            return "";
        }
	
        [DllImport("kernel32.dll")] static extern IntPtr GetConsoleWindow();
        [DllImport("User32")] private static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        public void HideConsole()
        {
            ShowWindow(GetConsoleWindow(), 0);
        }

        public void MinimizedConsole()
        {
            ShowWindow(GetConsoleWindow(), 7);
        }

        public void RestoreConsole()
        {
            ShowWindow(GetConsoleWindow(), 9);
        }
	
        public static string GetDisplayName(object value, string memberName)
        {
            string DisplayName = "";
            try
            {
                System.ComponentModel.PropertyDescriptor PropertyDescriptorCollection1 = System.ComponentModel.TypeDescriptor.GetProperties(value.GetType())[memberName];
                System.ComponentModel.AttributeCollection attributes = System.ComponentModel.TypeDescriptor.GetProperties(value.GetType())[memberName].Attributes;
                System.ComponentModel.DisplayNameAttribute myDisplayNameAttribute = (System.ComponentModel.DisplayNameAttribute)attributes[typeof(System.ComponentModel.DisplayNameAttribute)];
                DisplayName = myDisplayNameAttribute.DisplayName;
            }
            catch { }
            return DisplayName;
        }

        public static string GetPropName(object value, string displayName)// по отображаемому имени свойства возвращает имя  свойства
        {
            PropertyInfo[] properties = value.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    if (attr.GetType() == typeof(System.ComponentModel.DisplayNameAttribute))
                    {
                        DisplayNameAttribute DisplayNameAttribute1 = (DisplayNameAttribute)attr;
                        if (DisplayNameAttribute1 != null)
                        {
                            var attributeName = DisplayNameAttribute1.DisplayName;
                            if (attributeName == displayName)
                            {
                                return prop.Name;
                            }
                        }
                    }
                }
            }
            return null;
        }
	
        public static Dictionary<string, string> colors = new Dictionary<string, string>
            {
                {"Aquamarine", "Аквамариновый"},
                {"Аквамариновый", "Aquamarine"},
                {"AntiqueWhite", "АнтичныйБелый"},
                {"АнтичныйБелый", "AntiqueWhite"},
                {"Beige", "Бежевый"},
                {"Бежевый", "Beige"},
                {"WhiteSmoke", "БелаяДымка"},
                {"БелаяДымка", "WhiteSmoke"},
                {"White", "Белый"},
                {"Белый", "White"},
                {"NavajoWhite", "БелыйНавахо"},
                {"БелыйНавахо", "NavajoWhite"},
                {"Turquoise", "Бирюзовый"},
                {"Бирюзовый", "Turquoise"},
                {"Bisque", "Бисквитный"},
                {"Бисквитный", "Bisque"},
                {"PaleTurquoise", "БледноБирюзовый"},
                {"БледноБирюзовый", "PaleTurquoise"},
                {"Cornsilk", "БледноЖелтый"},
                {"БледноЖелтый", "Cornsilk"},
                {"PaleGreen", "БледноЗеленый"},
                {"БледноЗеленый", "PaleGreen"},
                {"PaleGoldenrod", "БледноЗолотистый"},
                {"БледноЗолотистый", "PaleGoldenrod"},
                {"CornflowerBlue", "Васильковый"},
                {"Васильковый", "CornflowerBlue"},
                {"HotTrack", "Выделенный"},
                {"Выделенный", "HotTrack"},
                {"ControlLightLight", "ВыделеныйЭлемент"},
                {"ВыделеныйЭлемент", "ControlLightLight"},
                {"DeepPink", "ГлубокийРозовый"},
                {"ГлубокийРозовый", "DeepPink"},
                {"DeepSkyBlue", "Голубой"},
                {"Голубой", "DeepSkyBlue"},
                {"ActiveBorder", "ГраницаАктивного"},
                {"ГраницаАктивного", "ActiveBorder"},
                {"InactiveBorder", "ГраницаНеактивного"},
                {"ГраницаНеактивного", "InactiveBorder"},
                {"LightSlateGray", "ГрифельноСерый"},
                {"ГрифельноСерый", "LightSlateGray"},
                {"SlateBlue", "ГрифельноСиний"},
                {"ГрифельноСиний", "SlateBlue"},
                {"YellowGreen", "ЖелтоЗеленый"},
                {"ЖелтоЗеленый", "YellowGreen"},
                {"Yellow", "Желтый"},
                {"Желтый", "Yellow"},
                {"ActiveCaption", "ЗаголовокАктивного"},
                {"ЗаголовокАктивного", "ActiveCaption"},
                {"InactiveCaption", "ЗаголовокНеактивного"},
                {"ЗаголовокНеактивного", "InactiveCaption"},
                {"DodgerBlue", "ЗащитноСиний"},
                {"ЗащитноСиний", "DodgerBlue"},
                {"SpringGreen", "ЗеленаяВесна"},
                {"ЗеленаяВесна", "SpringGreen"},
                {"LawnGreen", "ЗеленаяЛужайка"},
                {"ЗеленаяЛужайка", "LawnGreen"},
                {"SeaGreen", "ЗеленоеМоре"},
                {"ЗеленоеМоре", "SeaGreen"},
                {"GreenYellow", "ЗеленоЖелтый"},
                {"ЗеленоЖелтый", "GreenYellow"},
                {"Green", "Зеленый"},
                {"Зеленый", "Green"},
                {"LimeGreen", "ЗеленыйЛайм"},
                {"ЗеленыйЛайм", "LimeGreen"},
                {"ForestGreen", "ЗеленыйЛесной"},
                {"ЗеленыйЛесной", "ForestGreen"},
                {"Goldenrod", "Золотарник"},
                {"Золотарник", "Goldenrod"},
                {"Gold", "Золотой"},
                {"Золотой", "Gold"},
                {"Indigo", "Индиго"},
                {"Индиго", "Indigo"},
                {"IndianRed", "ИндийскийКрасный"},
                {"ИндийскийКрасный", "IndianRed"},
                {"Firebrick", "Кирпичный"},
                {"Кирпичный", "Firebrick"},
                {"SaddleBrown", "КожаноКоричневый"},
                {"КожаноКоричневый", "SaddleBrown"},
                {"Coral", "Коралловый"},
                {"Коралловый", "Coral"},
                {"Maroon", "КоричневоМалиновый"},
                {"КоричневоМалиновый", "Maroon"},
                {"Brown", "Коричневый"},
                {"Коричневый", "Brown"},
                {"RoyalBlue", "КоролевскийСиний"},
                {"КоролевскийСиний", "RoyalBlue"},
                {"Red", "Красный"},
                {"Красный", "Red"},
                {"Crimson", "Кровавый"},
                {"Кровавый", "Crimson"},
                {"Lavender", "Лаванда"},
                {"Лаванда", "Lavender"},
                {"Azure", "Лазурный"},
                {"Лазурный", "Azure"},
                {"Lime", "Лайм"},
                {"Лайм", "Lime"},
                {"PaleVioletRed", "Лиловый"},
                {"Лиловый", "PaleVioletRed"},
                {"Control", "ЛицеваяЭлемента"},
                {"ЛицеваяЭлемента", "Control"},
                {"Salmon", "Лососевый"},
                {"Лососевый", "Salmon"},
                {"Linen", "Льняной"},
                {"Льняной", "Linen"},
                {"Magenta", "Малиновый"},
                {"Малиновый", "Magenta"},
                {"Honeydew", "Медовый"},
                {"Медовый", "Honeydew"},
                {"Menu", "Меню"},
                {"Меню", "Menu"},
                {"Moccasin", "Мокасиновый"},
                {"Мокасиновый", "Moccasin"},
                {"Aqua", "МорскаяВолна"},
                {"МорскаяВолна", "Aqua"},
                {"SeaShell", "МорскаяРакушка"},
                {"МорскаяРакушка", "SeaShell"},
                {"MintCream", "МятноКремовый"},
                {"МятноКремовый", "MintCream"},
                {"SkyBlue", "НебесноГолубой"},
                {"НебесноГолубой", "SkyBlue"},
                {"LightSkyBlue", "НебесноГолубойСветлый"},
                {"НебесноГолубойСветлый", "LightSkyBlue"},
                {"OliveDrab", "НежноОливковый"},
                {"НежноОливковый", "OliveDrab"},
                {"Window", "Окно"},
                {"Окно", "Window"},
                {"Olive", "Оливковый"},
                {"Оливковый", "Olive"},
                {"OrangeRed", "ОранжевоКрасный"},
                {"ОранжевоКрасный", "OrangeRed"},
                {"Orange", "Оранжевый"},
                {"Оранжевый", "Orange"},
                {"Orchid", "Орхидея"},
                {"Орхидея", "Orchid"},
                {"Sienna", "Охра"},
                {"Охра", "Sienna"},
                {"PeachPuff", "Персиковый"},
                {"Персиковый", "PeachPuff"},
                {"SandyBrown", "Песочный"},
                {"Песочный", "SandyBrown"},
                {"PapayaWhip", "ПобегПапайи"},
                {"ПобегПапайи", "PapayaWhip"},
                {"Info", "Подсказка"},
                {"Подсказка", "Info"},
                {"ScrollBar", "ПолосаПрокрутки"},
                {"ПолосаПрокрутки", "ScrollBar"},
                {"MidnightBlue", "ПолуночноСиний"},
                {"ПолуночноСиний", "MidnightBlue"},
                {"PowderBlue", "ПороховаяСинь"},
                {"ПороховаяСинь", "PowderBlue"},
                {"GhostWhite", "ПризрачноБелый"},
                {"ПризрачноБелый", "GhostWhite"},
                {"Transparent", "Прозрачный"},
                {"Прозрачный", "Transparent"},
                {"Purple", "Пурпурный"},
                {"Пурпурный", "Purple"},
                {"IsEmpty", "Пусто"},
                {"Пусто", "IsEmpty"},
                {"Wheat", "Пшеничный"},
                {"Пшеничный", "Wheat"},
                {"AppWorkspace", "РабочаяОбластьПриложения"},
                {"РабочаяОбластьПриложения", "AppWorkspace"},
                {"Desktop", "РабочийСтол"},
                {"РабочийСтол", "Desktop"},
                {"WindowFrame", "РамкаОкна"},
                {"РамкаОкна", "WindowFrame"},
                {"RosyBrown", "РозовоКоричневый"},
                {"РозовоКоричневый", "RosyBrown"},
                {"LavenderBlush", "РозовоЛавандовый"},
                {"РозовоЛавандовый", "LavenderBlush"},
                {"Pink", "Розовый"},
                {"Розовый", "Pink"},
                {"LightSeaGreen", "СветлаяМорскаяВолна"},
                {"СветлаяМорскаяВолна", "LightSeaGreen"},
                {"LightYellow", "СветлоЖелтый"},
                {"СветлоЖелтый", "LightYellow"},
                {"LightGoldenrodYellow", "СветлоЖелтыйЗолотистый"},
                {"СветлоЖелтыйЗолотистый", "LightGoldenrodYellow"},
                {"LightGreen", "СветлоЗеленый"},
                {"СветлоЗеленый", "LightGreen"},
                {"LightCoral", "СветлоКоралловый"},
                {"СветлоКоралловый", "LightCoral"},
                {"Peru", "СветлоКоричневый"},
                {"СветлоКоричневый", "Peru"},
                {"BlanchedAlmond", "СветлоКремовый"},
                {"СветлоКремовый", "BlanchedAlmond"},
                {"LemonChiffon", "СветлоЛимонный"},
                {"СветлоЛимонный", "LemonChiffon"},
                {"LightPink", "СветлоРозовый"},
                {"СветлоРозовый", "LightPink"},
                {"LightGray", "СветлоСерый"},
                {"СветлоСерый", "LightGray"},
                {"LightBlue", "СветлоСиний"},
                {"СветлоСиний", "LightBlue"},
                {"LightCyan", "СветлыйЗеленоватоГолубой"},
                {"СветлыйЗеленоватоГолубой", "LightCyan"},
                {"LightSalmon", "СветлыйЛососевый"},
                {"СветлыйЛососевый", "LightSalmon"},
                {"ControlLight", "СветлыйЭлемента"},
                {"СветлыйЭлемента", "ControlLight"},
                {"Silver", "Серебристый"},
                {"Серебристый", "Silver"},
                {"CadetBlue", "СероСиний"},
                {"СероСиний", "CadetBlue"},
                {"Gray", "Серый"},
                {"Серый", "Gray"},
                {"GrayText", "СерыйТекст"},
                {"СерыйТекст", "GrayText"},
                {"SlateGray", "СерыйШифер"},
                {"СерыйШифер", "SlateGray"},
                {"LightSteelBlue", "СинеГолубойСоСтальнымОттенком"},
                {"СинеГолубойСоСтальнымОттенком", "LightSteelBlue"},
                {"Teal", "СинеЗеленый"},
                {"СинеЗеленый", "Teal"},
                {"BlueViolet", "СинеФиолетовый"},
                {"СинеФиолетовый", "BlueViolet"},
                {"Blue", "Синий"},
                {"Синий", "Blue"},
                {"AliceBlue", "СинийЭлис"},
                {"СинийЭлис", "AliceBlue"},
                {"SteelBlue", "СиняяСталь"},
                {"СиняяСталь", "SteelBlue"},
                {"Plum", "Сливовый"},
                {"Сливовый", "Plum"},
                {"Ivory", "СлоноваяКость"},
                {"СлоноваяКость", "Ivory"},
                {"OldLace", "СтароеКружево"},
                {"СтароеКружево", "OldLace"},
                {"HighlightText", "ТекстВыбранных"},
                {"ТекстВыбранных", "HighlightText"},
                {"ActiveCaptionText", "ТекстЗаголовкаАктивного"},
                {"ТекстЗаголовкаАктивного", "ActiveCaptionText"},
                {"InactiveCaptionText", "ТекстЗаголовкаНеактивного"},
                {"ТекстЗаголовкаНеактивного", "InactiveCaptionText"},
                {"MenuText", "ТекстМеню"},
                {"ТекстМеню", "MenuText"},
                {"WindowText", "ТекстОкна"},
                {"ТекстОкна", "WindowText"},
                {"InfoText", "ТекстПодсказки"},
                {"ТекстПодсказки", "InfoText"},
                {"ControlText", "ТекстЭлемента"},
                {"ТекстЭлемента", "ControlText"},
                {"DarkSalmon", "ТемнаяЛососина"},
                {"ТемнаяЛососина", "DarkSalmon"},
                {"DarkSeaGreen", "ТемнаяМорскаяВолна"},
                {"ТемнаяМорскаяВолна", "DarkSeaGreen"},
                {"DarkOrchid", "ТемнаяОрхидея"},
                {"ТемнаяОрхидея", "DarkOrchid"},
                {"ControlDarkDark", "ТемнаяТеньЭлемента"},
                {"ТемнаяТеньЭлемента", "ControlDarkDark"},
                {"DarkSlateGray", "ТемноАспидныйСерый"},
                {"ТемноАспидныйСерый", "DarkSlateGray"},
                {"DarkCyan", "ТемноГолубой"},
                {"ТемноГолубой", "DarkCyan"},
                {"DarkGreen", "ТемноЗеленый"},
                {"ТемноЗеленый", "DarkGreen"},
                {"DarkRed", "ТемноКрасный"},
                {"ТемноКрасный", "DarkRed"},
                {"DarkTurquoise", "ТемноМандариновый"},
                {"ТемноМандариновый", "DarkTurquoise"},
                {"DarkMagenta", "ТемноПурпурный"},
                {"ТемноПурпурный", "DarkMagenta"},
                {"DarkGray", "ТемноСерый"},
                {"ТемноСерый", "DarkGray"},
                {"DarkBlue", "ТемноСиний"},
                {"ТемноСиний", "DarkBlue"},
                {"DarkViolet", "ТемноФиолетовый"},
                {"ТемноФиолетовый", "DarkViolet"},
                {"DarkSlateBlue", "ТемныйГрифельноСиний"},
                {"ТемныйГрифельноСиний", "DarkSlateBlue"},
                {"DarkGoldenrod", "ТемныйЗолотой"},
                {"ТемныйЗолотой", "DarkGoldenrod"},
                {"DarkOliveGreen", "ТемныйОливковоЗеленый"},
                {"ТемныйОливковоЗеленый", "DarkOliveGreen"},
                {"DarkOrange", "ТемныйОранжевый"},
                {"ТемныйОранжевый", "DarkOrange"},
                {"DarkKhaki", "ТемныйХаки"},
                {"ТемныйХаки", "DarkKhaki"},
                {"ControlDark", "ТеньЭлемента"},
                {"ТеньЭлемента", "ControlDark"},
                {"Tomato", "Томатный"},
                {"Томатный", "Tomato"},
                {"Gainsboro", "ТуманноБелый"},
                {"ТуманноБелый", "Gainsboro"},
                {"MistyRose", "ТусклоРозовый"},
                {"ТусклоРозовый", "MistyRose"},
                {"DimGray", "ТусклоСерый"},
                {"ТусклоСерый", "DimGray"},
                {"MediumAquamarine", "УмеренныйАквамарин"},
                {"УмеренныйАквамарин", "MediumAquamarine"},
                {"MediumTurquoise", "УмеренныйБирюзовый"},
                {"УмеренныйБирюзовый", "MediumTurquoise"},
                {"MediumSpringGreen", "УмеренныйВесеннеЗеленый"},
                {"УмеренныйВесеннеЗеленый", "MediumSpringGreen"},
                {"MediumSlateBlue", "УмеренныйГрифельноСиний"},
                {"УмеренныйГрифельноСиний", "MediumSlateBlue"},
                {"MediumSeaGreen", "УмеренныйМорскаяВолна"},
                {"УмеренныйМорскаяВолна", "MediumSeaGreen"},
                {"MediumOrchid", "УмеренныйОрхидея"},
                {"УмеренныйОрхидея", "MediumOrchid"},
                {"MediumBlue", "УмеренныйСиний"},
                {"УмеренныйСиний", "MediumBlue"},
                {"MediumVioletRed", "УмеренныйФиолетовоКрасный"},
                {"УмеренныйФиолетовоКрасный", "MediumVioletRed"},
                {"MediumPurple", "УмеренныйФиолетовый"},
                {"УмеренныйФиолетовый", "MediumPurple"},
                {"Violet", "Фиолетовый"},
                {"Фиолетовый", "Violet"},
                {"Highlight", "ФонВыбранных"},
                {"ФонВыбранных", "Highlight"},
                {"Fuchsia", "Фуксия"},
                {"Фуксия", "Fuchsia"},
                {"Khaki", "Хаки"},
                {"Хаки", "Khaki"},
                {"Tan", "ЦветЗагара"},
                {"ЦветЗагара", "Tan"},
                {"FloralWhite", "ЦветочноБелый"},
                {"ЦветочноБелый", "FloralWhite"},
                {"BurlyWood", "ЦветПлотнойДревесины"},
                {"ЦветПлотнойДревесины", "BurlyWood"},
                {"Navy", "ЦветФормыМорскихОфицеров"},
                {"ЦветФормыМорскихОфицеров", "Navy"},
                {"Cyan", "Циан"},
                {"Циан", "Cyan"},
                {"Black", "Черный"},
                {"Черный", "Black"},
                {"Thistle", "Чертополох"},
                {"Чертополох", "Thistle"},
                {"Chartreuse", "Шартрез"},
                {"Шартрез", "Chartreuse"},
                {"Chocolate", "Шоколадный"},
                {"Шоколадный", "Chocolate"},
                {"Snow", "ЯркийБелый"},
                {"ЯркийБелый", "Snow"},
                {"HotPink", "ЯркоРозовый"},
                {"ЯркоРозовый", "HotPink"}
            };
	
        public static Dictionary<string, string> namesEnRu = new Dictionary<string, string>
            {
                {"RadioButton", "Переключатель"},
                {"Button", "Кнопка"},
                {"CheckBox", "Флажок"},
                {"ColorDialog", "ДиалогВыбораЦвета"},
                {"ComboBox", "ПолеВыбора"},
                {"DataGrid", "СеткаДанных"},
                {"DateTimePicker", "ПолеКалендаря"},
                {"FileSystemWatcher", "НаблюдательФайловойСистемы"},
                {"FolderBrowserDialog", "ДиалогВыбораКаталога"},
                {"FontDialog", "ДиалогВыбораШрифта"},
                {"GroupBox", "РамкаГруппы"},
                {"HProgressBar", "ИндикаторГоризонтальный"},
                {"HScrollBar", "ГоризонтальнаяПрокрутка"},
                {"ImageList", "СписокИзображений"},
                {"LinkLabel", "НадписьСсылка"},
                {"Label", "Надпись"},
                {"ListBox", "ПолеСписка"},
                {"ListView", "СписокЭлементов"},
                {"MainMenu", "ГлавноеМеню"},
                {"MonthCalendar", "Календарь"},
                {"NotifyIcon", "ЗначокУведомления"},
                {"NumericUpDown", "РегуляторВверхВниз"},
                {"OpenFileDialog", "ДиалогОткрытияФайла"},
                {"Panel", "Панель"},
                {"PictureBox", "ПолеКартинки"},
                {"VProgressBar", "ИндикаторВертикальный"},
                {"ProgressBar", "Индикатор"},
                {"PropertyGrid", "СеткаСвойств"},
                {"RichTextBox", "ФорматированноеПолеВвода"},
                {"SaveFileDialog", "ДиалогСохраненияФайла"},
                {"Splitter", "Разделитель"},
                {"StatusBar", "СтрокаСостояния"},
                {"TabControl", "ПанельВкладок"},
                {"TabPage", "Вкладка"},
                {"TextBox", "ПолеВвода"},
                {"Timer", "Таймер"},
                {"ToolBar", "ПанельИнструментов"},
                {"ToolTip", "Подсказка"},
                {"TreeView", "Дерево"},
                {"UserControl", "ПользовательскийЭлементУправления"},
                {"VScrollBar", "ВертикальнаяПрокрутка"}
            };

        public static Dictionary<string, string> namesRuEn = new Dictionary<string, string>
            {
                {"Переключатель", "RadioButton"},
                {"Кнопка", "Button"},
                {"Флажок", "CheckBox"},
                {"ДиалогВыбораЦвета", "ColorDialog"},
                {"ПолеВыбора", "ComboBox"},
                {"СеткаДанных", "DataGrid"},
                {"ПолеКалендаря", "DateTimePicker"},
                {"НаблюдательФайловойСистемы", "FileSystemWatcher"},
                {"ДиалогВыбораКаталога", "FolderBrowserDialog"},
                {"ДиалогВыбораШрифта", "FontDialog"},
                {"РамкаГруппы", "GroupBox"},
                {"ИндикаторГоризонтальный", "HProgressBar"},
                {"ГоризонтальнаяПрокрутка", "HScrollBar"},
                {"СписокИзображений", "ImageList"},
                {"НадписьСсылка", "LinkLabel"},
                {"Надпись", "Label"},
                {"ПолеСписка", "ListBox"},
                {"СписокЭлементов", "ListView"},
                {"ГлавноеМеню", "MainMenu"},
                {"Календарь", "MonthCalendar"},
                {"ЗначокУведомления", "NotifyIcon"},
                {"РегуляторВверхВниз", "NumericUpDown"},
                {"ДиалогОткрытияФайла", "OpenFileDialog"},
                {"Панель", "Panel"},
                {"ПолеКартинки", "PictureBox"},
                {"ИндикаторВертикальный", "VProgressBar"},
                {"Индикатор", "ProgressBar"},
                {"СеткаСвойств", "PropertyGrid"},
                {"ФорматированноеПолеВвода", "RichTextBox"},
                {"ДиалогСохраненияФайла", "SaveFileDialog"},
                {"Разделитель", "Splitter"},
                {"СтрокаСостояния", "StatusBar"},
                {"ПанельВкладок", "TabControl"},
                {"Вкладка", "TabPage"},
                {"ПолеВвода", "TextBox"},
                {"Таймер", "Timer"},
                {"ПанельИнструментов", "ToolBar"},
                {"Подсказка", "ToolTip"},
                {"Дерево", "TreeView"},
                {"ПользовательскийЭлементУправления", "UserControl"},
                {"ВертикальнаяПрокрутка", "VScrollBar"}
            };

        public static Dictionary<string, string> namesEnum = new Dictionary<string, string>
            {
                {"АвтоРазмерПанелиСтрокиСостояния", "StatusBarPanelAutoSize"},
                {"АктивацияЭлемента", "ItemActivation"},
                {"ВыравниваниеВкладок", "TabAlignment"},
                {"ВыравниваниеВСпискеЭлементов", "ListViewAlignment"},
                {"ВыравниваниеСодержимого", "ContentAlignment"},
                {"ВыравниваниеТекстаВПанелиИнструментов", "ToolBarTextAlign"},
                {"ГлубинаЦвета", "ColorDepth"},
                {"ГоризонтальноеВыравнивание", "HorizontalAlignment"},
                {"День", "Day"},
                {"ДеревоДействие", "TreeViewAction"},
                {"Звуки", "Sounds"},
                {"ЗначокОкнаСообщений", "MessageBoxIcon"},
                {"Клавиши", "Keys"},
                {"КнопкиМыши", "MouseButtons"},
                {"КнопкиОкнаСообщений", "MessageBoxButtons"},
                {"ЛевоеПравоеВыравнивание", "LeftRightAlignment"},
                {"НаблюдательИзмененияВида", "WatcherChangeTypes"},
                {"НачальноеПоложениеФормы", "FormStartPosition"},
                {"ОриентацияПолосы", "ScrollOrientation"},
                {"ОсобаяПапка", "SpecialFolder"},
                {"Оформление", "Appearance"},
                {"ОформлениеВкладок", "TabAppearance"},
                {"ОформлениеПанелиИнструментов", "ToolBarAppearance"},
                {"ПлоскийСтиль", "FlatStyle"},
                {"ПоведениеСсылки", "LinkLabelLinkBehavior"},
                {"ПозицияПоиска", "SeekOrigin"},
                {"ПолосыПрокрутки", "ScrollBars"},
                {"ПорядокСортировки", "SortOrder"},
                {"ПричинаЗакрытия", "CloseReason"},
                {"РазмещениеИзображения", "ImageLayout"},
                {"РегистрСимволов", "CharacterCasing"},
                {"РежимВыбора", "SelectionMode"},
                {"РежимОтображения", "View"},
                {"РежимРазмераВкладок", "TabSizeMode"},
                {"РежимРазмераПоляКартинки", "PictureBoxSizeMode"},
                {"РежимРисования", "DrawMode"},
                {"РезультатДиалога", "DialogResult"},
                {"СлияниеМеню", "MenuMerge"},
                {"СортировкаСвойств", "PropertySort"},
                {"СостояниеОкнаФормы", "FormWindowState"},
                {"СостояниеСтрокиДанных", "DataRowState"},
                {"СостояниеФлажка", "CheckState"},
                {"СочетаниеКлавиш", "Shortcut"},
                {"СтилиПривязки", "AnchorStyles"},
                {"СтильГраницы", "BorderStyle"},
                {"СтильГраницыПанелиСтрокиСостояния", "StatusBarPanelBorderStyle"},
                {"СтильГраницыФормы", "FormBorderStyle"},
                {"СтильЗаголовкаКолонки", "ColumnHeaderStyle"},
                {"СтильКнопокПанелиИнструментов", "ToolBarButtonStyle"},
                {"СтильОкнаПроцесса", "ProcessWindowStyle"},
                {"СтильПоляВыбора", "ComboBoxStyle"},
                {"СтильСтыковки", "DockStyle"},
                {"СтильШрифта", "FontStyle"},
                {"СтильШтриховки", "HatchStyle"},
                {"СтильЭлементаУправления", "ControlStyles"},
                {"ТипДанных", "DataType"},
                {"ТипСобытияПрокрутки", "ScrollEventType"},
                {"ТипСортировки", "SortType"},
                {"ТипЭлементаСетки", "GridItemType"},
                {"ФильтрыУведомления", "NotifyFilters"},
                {"ФлагиМыши", "MouseFlags"},
                {"ФорматированноеПолеВводаПоиск", "RichTextBoxFinds"},
                {"ФорматированноеПолеВводаТипыПотоков", "RichTextBoxStreamType"},
                {"ФорматПикселей", "PixelFormat"}
            };

        public static Dictionary<string, System.Windows.Forms.Cursor> namesCursorRuEn = new Dictionary<string, System.Windows.Forms.Cursor>
            {
                {"БезДвижения2D", System.Windows.Forms.Cursors.NoMove2D},
                {"БезДвиженияВертикально", System.Windows.Forms.Cursors.NoMoveVert},
                {"БезДвиженияГоризонтально", System.Windows.Forms.Cursors.NoMoveHoriz},
                {"ВРазделитель", System.Windows.Forms.Cursors.VSplit},
                {"ГРазделитель", System.Windows.Forms.Cursors.HSplit},
                {"КурсорВ", System.Windows.Forms.Cursors.PanEast},
                {"КурсорЗ", System.Windows.Forms.Cursors.PanWest},
                {"КурсорОжидания", System.Windows.Forms.Cursors.WaitCursor},
                {"КурсорС", System.Windows.Forms.Cursors.PanNorth},
                {"КурсорСВ", System.Windows.Forms.Cursors.PanNE},
                {"КурсорСЗ", System.Windows.Forms.Cursors.PanNW},
                {"КурсорЮ", System.Windows.Forms.Cursors.PanSouth},
                {"КурсорЮВ", System.Windows.Forms.Cursors.PanSE},
                {"КурсорЮЗ", System.Windows.Forms.Cursors.PanSW},
                {"Ладонь", System.Windows.Forms.Cursors.Hand},
                {"Луч", System.Windows.Forms.Cursors.IBeam},
                {"Нет", System.Windows.Forms.Cursors.No},
                {"Перекрестие", System.Windows.Forms.Cursors.Cross},
                {"ПоУмолчанию", System.Windows.Forms.Cursors.Default},
                {"ПриСтарте", System.Windows.Forms.Cursors.AppStarting},
                {"РазмерЗВ", System.Windows.Forms.Cursors.SizeWE},
                {"РазмерСВЮЗ", System.Windows.Forms.Cursors.SizeNESW},
                {"РазмерСЗЮВ", System.Windows.Forms.Cursors.SizeNWSE},
                {"РазмерСЮ", System.Windows.Forms.Cursors.SizeNS},
                {"РазмерЧетырехконечный", System.Windows.Forms.Cursors.SizeAll},
                {"Справка", System.Windows.Forms.Cursors.Help},
                {"Стрелка", System.Windows.Forms.Cursors.Arrow},
                {"СтрелкаВверх", System.Windows.Forms.Cursors.UpArrow}
            };

        public static Dictionary<string, string> namesCursorEnRu = new Dictionary<string, string>
            {
                {"NoMove2D", "БезДвижения2D"},
                {"NoMoveVert", "БезДвиженияВертикально"},
                {"NoMoveHoriz", "БезДвиженияГоризонтально"},
                {"VSplit", "ВРазделитель"},
                {"HSplit", "ГРазделитель"},
                {"PanEast", "КурсорВ"},
                {"PanWest", "КурсорЗ"},
                {"WaitCursor", "КурсорОжидания"},
                {"PanNorth", "КурсорС"},
                {"PanNE", "КурсорСВ"},
                {"PanNW", "КурсорСЗ"},
                {"PanSouth", "КурсорЮ"},
                {"PanSE", "КурсорЮВ"},
                {"PanSW", "КурсорЮЗ"},
                {"Hand", "Ладонь"},
                {"IBeam", "Луч"},
                {"No", "Нет"},
                {"Cross", "Перекрестие"},
                {"Default", "ПоУмолчанию"},
                {"AppStarting", "ПриСтарте"},
                {"SizeWE", "РазмерЗВ"},
                {"SizeNESW", "РазмерСВЮЗ"},
                {"SizeNWSE", "РазмерСЗЮВ"},
                {"SizeNS", "РазмерСЮ"},
                {"SizeAll", "РазмерЧетырехконечный"},
                {"Help", "Справка"},
                {"Arrow", "Стрелка"},
                {"UpArrow", "СтрелкаВверх"}
            };

        public static ArrayList StrFindBetween(string p1, string p2 = null, string p3 = null, bool p4 = true, bool p5 = true)
        {
            //p1 - исходная строка
            //p2 - подстрока поиска от которой ведем поиск
            //p3 - подстрока поиска до которой ведем поиск
            //p4 - не включать p2 и p3 в результат
            //p5 - в результат не будут включены участки, содержащие другие найденные участки, удовлетворяющие переданным параметрам
            //функция возвращает массив строк
            string str1 = p1;
            int Position1;
            ArrayList ArrayList1 = new ArrayList();
            if (p2 != null && p3 == null)
            {
                Position1 = str1.IndexOf(p2);
                while (Position1 >= 0)
                {
                    ArrayList1.Add("" + ((p4) ? str1.Substring(Position1 + p2.Length) : str1.Substring(Position1)));
                    str1 = str1.Substring(Position1 + 1);
                    Position1 = str1.IndexOf(p2);
                }
            }
            else if (p2 == null && p3 != null)
            {
                Position1 = str1.IndexOf(p3) + 1;
                int SumPosition1 = Position1;
                while (Position1 > 0)
                {
                    ArrayList1.Add("" + ((p4) ? str1.Substring(0, SumPosition1 - 1) : str1.Substring(0, SumPosition1 - 1 + p3.Length)));
                    try
                    {
                        Position1 = str1.Substring(SumPosition1 + 1).IndexOf(p3) + 1;
                        SumPosition1 = SumPosition1 + Position1 + 1;
                    }
                    catch
                    {
                        break;
                    }
                }
            }
            else if (p2 != null && p3 != null)
            {
                Position1 = str1.IndexOf(p2);
                while (Position1 >= 0)
                {
                    string Стр2;
                    Стр2 = (p4) ? str1.Substring(Position1 + p2.Length) : str1.Substring(Position1);
                    int Position2 = Стр2.IndexOf(p3) + 1;
                    int SumPosition2 = Position2;
                    while (Position2 > 0)
                    {
                        if (p5)
                        {
                            if (Стр2.Substring(0, SumPosition2 - 1).IndexOf(p3) <= -1)
                            {
                                ArrayList1.Add("" + ((p4) ? Стр2.Substring(0, SumPosition2 - 1) : Стр2.Substring(0, SumPosition2 - 1 + p3.Length)));
                            }
                        }
                        else
                        {
                            ArrayList1.Add("" + ((p4) ? Стр2.Substring(0, SumPosition2 - 1) : Стр2.Substring(0, SumPosition2 - 1 + p3.Length)));
                        }
                        try
                        {
                            Position2 = Стр2.Substring(SumPosition2 + 1).IndexOf(p3) + 1;
                            SumPosition2 = SumPosition2 + Position2 + 1;
                        }
                        catch
                        {
                            break;

                        }
                    }
                    str1 = str1.Substring(Position1 + 1);
                    Position1 = str1.IndexOf(p2);
                }
            }
            return ArrayList1;
        }
	
        public static System.Drawing.Image Base64ToImage(string base64String)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new System.IO.MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = System.Drawing.Image.FromStream(ms, true);
                return image;
            }
        }
	
        public static string ParseBetween(string p1, string p2 = null, string p3 = null)
        {
            //p1 - исходная строка
            //p2 - подстрока поиска от которой ведем поиск
            //p3 - подстрока поиска до которой ведем поиск
            //возвращает строку, ограниченную p2 и p3
            string str1 = p1;
            int Position1;
            if (p2 != null && p3 == null)
            {
                Position1 = str1.IndexOf(p2);
                if (Position1 >= 0)
                {
                    return str1.Substring(Position1 + p2.Length);
                }
            }
            else if (p2 == null && p3 != null)
            {
                Position1 = str1.IndexOf(p3) + 1;
                if (Position1 > 0)
                {
                    return str1.Substring(0, Position1 - 1);
                }
            }
            else if (p2 != null && p3 != null)
            {
                Position1 = str1.IndexOf(p2);
                while (Position1 >= 0)
                {
                    string Стр2;
                    Стр2 = str1.Substring(Position1 + p2.Length);
                    int Position2 = Стр2.IndexOf(p3) + 1;
                    int SumPosition2 = Position2;
                    while (Position2 > 0)
                    {
                        if (Стр2.Substring(0, SumPosition2 - 1).IndexOf(p3) <= -1)
                        {
                            return Стр2.Substring(0, SumPosition2 - 1);
                        }
                        try
                        {
                            Position2 = Стр2.Substring(SumPosition2 + 1).IndexOf(p3) + 1;
                            SumPosition2 = SumPosition2 + Position2 + 1;
                        }
                        catch
                        {
                            break;
                        }
                    }
                    str1 = str1.Substring(Position1 + 1);
                    Position1 = str1.IndexOf(p2);
                }
            }
            return null;
        }

        public static Component HighlightedComponent()// возвращает выделенный в настоящее время компонент
        {
            IDesignerHost host = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost();
            ISelectionService iSel = host.GetService(typeof(ISelectionService)) as ISelectionService;
            Component comp = null;
            if (iSel != null)
            {
                return (Component)iSel.PrimarySelection;
            }
            return comp;
        }

        public static Component GetComponentByName(string name)// возвращает компонент найденный по имени
        {
            Component comp = null;

            IDesignerHost host = pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost();
            ISelectionService iSel = host.GetService(typeof(ISelectionService)) as ISelectionService;
            if (iSel != null)
            {
                ComponentCollection ctrlsExisting = host.Container.Components;
                for (int i = 0; i < ctrlsExisting.Count; i++)
                {
                    if (ctrlsExisting[i].Site.Name == name)
                    {
                        return (Component)ctrlsExisting[i];
                    }
                }
            }
            return comp;
        }

        public static string GetNameByComponent(Component comp)// возвращает имя для компонента
        {
            Component comp1 = comp;

            if (comp.GetType().ToString() == "System.Windows.Forms.TabPage" || 
                comp.GetType().ToString() == "System.Windows.Forms.ImageList" || 
                comp.GetType().ToString() == "System.Windows.Forms.MainMenu")
            {
                comp1 = OneScriptFormsDesigner.RevertSimilarObj(comp);
            }
            return comp1.Site.Name;
        }

        public Form GetRootComponent()
        {
            return (Form)pDesigner.DSME.ActiveDesignSurface.GetIDesignerHost().Container.Components[0];
        }


    }
}
