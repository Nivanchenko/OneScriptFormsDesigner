﻿using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Windows.Forms;

namespace osfDesigner
{
    public class StatusBar : System.Windows.Forms.StatusBar
    {

        private string _DoubleClick_osf;
        private string _KeyUp_osf;
        private string _KeyDown_osf;
        private string _KeyPress_osf;
        private string _MouseEnter_osf;
        private string _MouseLeave_osf;
        private string _Click_osf;
        private string _LocationChanged_osf;
        private string _Enter_osf;
        private string _MouseHover_osf;
        private string _MouseDown_osf;
        private string _MouseUp_osf;
        private string _Move_osf;
        private string _MouseMove_osf;
        private string _Paint_osf;
        private string _LostFocus_osf;
        private string _Leave_osf;
        private string _SizeChanged_osf;
        private string _TextChanged_osf;
        private string _ControlAdded_osf;
        private string _ControlRemoved_osf;

        public StatusBar()
        {
            Enabled_osf = base.Enabled;
            Visible_osf = base.Visible;
        }

        [DisplayName("ДвойноеНажатие")]
        [Description("Возвращает или задает код для выполнения, когда элемент управления дважды щелкнут.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string DoubleClick_osf
        {
            get { return _DoubleClick_osf; }
            set { _DoubleClick_osf = value; }
        }

        [DisplayName("Доступность")]
        [Description("Возвращает или задает значение, указывающее, может ли элемент управления реагировать на действия пользователя.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public bool Enabled_osf { get; set; }
				
        //скроем унаследованное свойство, для того чтобы оно не мешало нашему замещающему свойству использовать свой эдитор и конвертер.
        [Browsable(false)]
        public new bool Enabled { get; set; }

        [DisplayName("ЗахватРазмера")]
        [Description("Возвращает или задает значение, указывающее, отображается ли в правом нижнем углу элемента управления захват для изменения размера.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool SizingGrip
        {
            get { return base.SizingGrip; }
            set { base.SizingGrip = value; }
        }

        [DisplayName("ИспользоватьКурсорОжидания")]
        [Description("Возвращает или задает значение, указывающее, следует ли использовать курсор ожидания для текущего элемента управления и всех дочерних элементов управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool UseWaitCursor
        {
            get { return base.UseWaitCursor; }
            set { base.UseWaitCursor = value; }
        }

        [DisplayName("КлавишаВверх")]
        [Description("Возвращает или задает код для события, которое происходит, когда отпускается клавиша, если элемент управления имеет фокус.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string KeyUp_osf
        {
            get { return _KeyUp_osf; }
            set { _KeyUp_osf = value; }
        }

        [DisplayName("КлавишаВниз")]
        [Description("Возвращает или задает код, выполняемый при нажатии клавиши в то время как элемент управления имеет фокус.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string KeyDown_osf
        {
            get { return _KeyDown_osf; }
            set { _KeyDown_osf = value; }
        }

        [DisplayName("КлавишаНажата")]
        [Description("Возвращает или задает код, выполняемый при нажатии клавиши в то время как элемент управления имеет фокус.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string KeyPress_osf
        {
            get { return _KeyPress_osf; }
            set { _KeyPress_osf = value; }
        }

        [DisplayName("Курсор")]
        [Description("Возвращает или задает курсор, который отображается, когда указатель мыши находится над элементом управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyCursorConverter))]
        [Editor(typeof(MyCursorEditor), typeof(UITypeEditor))]
        public new Cursor Cursor
        {
            get { return base.Cursor; }
            set { base.Cursor = value; }
        }

        [DisplayName("МышьНадЭлементом")]
        [Description("Возвращает или задает код для выполнения, когда указатель мыши находится над элементом управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseEnter_osf
        {
            get { return _MouseEnter_osf; }
            set { _MouseEnter_osf = value; }
        }

        [DisplayName("МышьПокинулаЭлемент")]
        [Description("Возвращает или задает код для выполнения, когда указатель мыши покидает пределы элемента управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseLeave_osf
        {
            get { return _MouseLeave_osf; }
            set { _MouseLeave_osf = value; }
        }

        [DisplayName("Нажатие")]
        [Description("Возвращает или задает код для выполнения при нажатии элемента управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Click_osf
        {
            get { return _Click_osf; }
            set { _Click_osf = value; }
        }

        [DisplayName("Отображать")]
        [Description("Возвращает или задает значение, указывающее, отображаются ли элемент управления и все его дочерние элементы управления. Истина, если элемент управления отображается; в противном случае - Ложь.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public bool Visible_osf { get; set; }
				
        //скроем унаследованное свойство, для того чтобы оно не мешало нашему замещающему свойству использовать свой эдитор и конвертер.
        [Browsable(false)]
        public new bool Visible { get; set; }

        [DisplayName("Панели")]
        [Description("Возвращает коллекцию панелей объекта СтрокаСостояния (StatusBar).")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyCollectionConverter))]
        [Editor(typeof(MyStatusBarPanelCollectionEditor), typeof(UITypeEditor))]
        public new StatusBarPanelCollection Panels
        {
            get { return base.Panels; }
        }

        [DisplayName("ПоказатьПанели")]
        [Description("Возвращает или задает значение, указывающее, отображаются ли какие-либо из панелей.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool ShowPanels
        {
            get { return base.ShowPanels; }
            set { base.ShowPanels = value; }
        }

        [DisplayName("Положение")]
        [Description("Возвращает или задает координаты верхнего левого угла элемента управления относительно верхнего левого угла его контейнера.")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MyLocationConverter))]
        [Editor(typeof(MyLocationEditor), typeof(UITypeEditor))]
        public Point Location_osf
        {
            get { return base.Location; }
            set { base.Location = value; }
        }
				
        //скроем унаследованное свойство, для того чтобы оно не мешало нашему замещающему свойству использовать свой эдитор и конвертер.
        [Browsable(false)]
        public new Point Location { get; set; }

        [DisplayName("ПоложениеИзменено")]
        [Description("Возвращает или задает код для выполнения при изменении свойства Положение (Location).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string LocationChanged_osf
        {
            get { return _LocationChanged_osf; }
            set { _LocationChanged_osf = value; }
        }

        [DisplayName("ПорядокОбхода")]
        [Description("Возвращает или задает последовательность перехода по клавише TAB между элементами управления внутри контейнера.")]
        [Category("Поведение")]
        [Browsable(true)]
        public new int TabIndex
        {
            get { return base.TabIndex; }
            set { base.TabIndex = value; }
        }

        [DisplayName("ПриВходе")]
        [Description("Возвращает или задает код для выполнения при входе в элемент управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Enter_osf
        {
            get { return _Enter_osf; }
            set { _Enter_osf = value; }
        }

        [DisplayName("ПриЗадержкеМыши")]
        [Description("Возвращает или задает код для выполнения когда указатель мыши задерживается на элементе управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseHover_osf
        {
            get { return _MouseHover_osf; }
            set { _MouseHover_osf = value; }
        }

        [DisplayName("ПриНажатииКнопкиМыши")]
        [Description("Возвращает или задает код для выполнения при нажатии кнопки мыши, если указатель мыши находится на элементе управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseDown_osf
        {
            get { return _MouseDown_osf; }
            set { _MouseDown_osf = value; }
        }

        [DisplayName("ПриОтпусканииМыши")]
        [Description("Возвращает или задает код для выполнения при отпускании кнопки мыши, когда указатель мыши находится на элементе управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseUp_osf
        {
            get { return _MouseUp_osf; }
            set { _MouseUp_osf = value; }
        }

        [DisplayName("ПриПеремещении")]
        [Description("Возвращает или задает код для выполнения при перемещении элемента управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Move_osf
        {
            get { return _Move_osf; }
            set { _Move_osf = value; }
        }

        [DisplayName("ПриПеремещенииМыши")]
        [Description("Возвращает или задает код для выполнения, когда указатель мыши переместился над элементом управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string MouseMove_osf
        {
            get { return _MouseMove_osf; }
            set { _MouseMove_osf = value; }
        }

        [DisplayName("ПриПерерисовке")]
        [Description("Возвращает или задает код для выполнения, когда элемент управления перерисован.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Paint_osf
        {
            get { return _Paint_osf; }
            set { _Paint_osf = value; }
        }

        [DisplayName("ПриПотереФокуса")]
        [Description("Возвращает или задает код для выполнения, когда элемент управления теряет фокус.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string LostFocus_osf
        {
            get { return _LostFocus_osf; }
            set { _LostFocus_osf = value; }
        }

        [DisplayName("ПриУходе")]
        [Description("Возвращает или задает код для выполнения, когда фокус ввода покидает элемент управления.")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string Leave_osf
        {
            get { return _Leave_osf; }
            set { _Leave_osf = value; }
        }

        [DisplayName("Размер")]
        [Description("Возвращает или задает высоту и ширину элемента управления.")]
        [Category("Макет")]
        [Browsable(true)]
        [TypeConverter(typeof(MySizeConverter))]
        [Editor(typeof(MySizeEditor), typeof(UITypeEditor))]
        public new Size Size
        {
            get { return base.Size; }
            set { base.Size = value; }
        }

        [DisplayName("РазмерИзменен")]
        [Description("Возвращает или задает код для выполнения при изменении значения свойства Размер (Size).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string SizeChanged_osf
        {
            get { return _SizeChanged_osf; }
            set { _SizeChanged_osf = value; }
        }

        [DisplayName("Стыковка")]
        [Description("Возвращает или задает, к какому краю родительского контейнера прикреплен элемент управления.")]
        [Category("Макет")]
        [Browsable(true)]
        [Editor(typeof(MyDockEditor), typeof(UITypeEditor))]
        public new DockStyle Dock
        {
            get { return (DockStyle)base.Dock; }
            set { base.Dock = (System.Windows.Forms.DockStyle)value; }
        }

        [DisplayName("ТабФокус")]
        [Description("Возвращает или задает значение, указывающее, может ли пользователь перевести фокус на данный элемент управления при помощи клавиши TAB.")]
        [Category("Поведение")]
        [Browsable(true)]
        [TypeConverter(typeof(MyBooleanConverter))]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = value; }
        }

        [DisplayName("Текст")]
        [Description("Возвращает или задает текст, связанный с этим элементом управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        public new string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        [DisplayName("ТекстИзменен")]
        [Description("Возвращает или задает код для выполнения, при изменении свойства Текст (Text).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string TextChanged_osf
        {
            get { return _TextChanged_osf; }
            set { _TextChanged_osf = value; }
        }

        [DisplayName("Шрифт")]
        [Description("Возвращает или задает шрифт текста, отображаемый элементом управления.")]
        [Category("Внешний вид")]
        [Browsable(true)]
        [TypeConverter(typeof(MyFontConverter))]
        public new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        [DisplayName("ЭлементДобавлен")]
        [Description("Возвращает или задает код для выполнения при добавлении нового элемента управления в ЭлементыУправления (ControlCollection).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string ControlAdded_osf
        {
            get { return _ControlAdded_osf; }
            set { _ControlAdded_osf = value; }
        }

        [DisplayName("ЭлементУдален")]
        [Description("Возвращает или задает код для выполнения при удалении элемента управления из ЭлементыУправления (ControlCollection).")]
        [Category("Прочее")]
        [Browsable(true)]
        public  string ControlRemoved_osf
        {
            get { return _ControlRemoved_osf; }
            set { _ControlRemoved_osf = value; }
        }

        [DisplayName("Якорь")]
        [Description("Возвращает или задает границы контейнера, с которым связан элемент управления, и определяет способ изменения размеров элемента управления при изменении размеров его родительского элемента.")]
        [Category("Макет")]
        [Browsable(true)]
        [Editor(typeof(MyAnchorEditor), typeof(UITypeEditor))]
        public new AnchorStyles Anchor
        {
            get { return (AnchorStyles)base.Anchor; }
            set { base.Anchor = (System.Windows.Forms.AnchorStyles)value; }
        }

        [Browsable(false)]
        public new dynamic AccessibilityObject { get; set; }
        
        [Browsable(false)]
        public new dynamic AccessibleDefaultActionDescription { get; set; }
        
        [Browsable(false)]
        public new dynamic AccessibleDescription { get; set; }
        
        [Browsable(false)]
        public new dynamic AccessibleName { get; set; }
        
        [Browsable(false)]
        public new dynamic AccessibleRole { get; set; }
        
        [Browsable(false)]
        public new dynamic AllowDrop { get; set; }
        
        [Browsable(false)]
        public new dynamic AutoScrollOffset { get; set; }
        
        [Browsable(false)]
        public new dynamic AutoSize { get; set; }
        
        [Browsable(false)]
        public new dynamic BackgroundImageLayout { get; set; }
        
        [Browsable(false)]
        public new dynamic BindingContext { get; set; }
        
        [Browsable(false)]
        public new dynamic CanSelect { get; set; }
        
        [Browsable(false)]
        public new dynamic CausesValidation { get; set; }
        
        [Browsable(false)]
        public new dynamic CompanyName { get; set; }
        
        [Browsable(false)]
        public new dynamic Container { get; set; }
        
        [Browsable(false)]
        public new dynamic ContainsFocus { get; set; }
        
        [Browsable(false)]
        public new dynamic ContextMenuStrip { get; set; }
        
        [Browsable(false)]
        public new dynamic Created { get; set; }
        
        [Browsable(false)]
        public new dynamic DataBindings { get; set; }
        
        [Browsable(false)]
        public dynamic DeviceDpi { get; set; }
        
        [Browsable(false)]
        public new dynamic DisplayRectangle { get; set; }
        
        [Browsable(false)]
        public new dynamic Disposing { get; set; }
        
        [Browsable(false)]
        public new dynamic Handle { get; set; }
        
        [Browsable(false)]
        public new dynamic HasChildren { get; set; }
        
        [Browsable(false)]
        public new dynamic ImeMode { get; set; }
        
        [Browsable(false)]
        public new dynamic InvokeRequired { get; set; }
        
        [Browsable(false)]
        public new dynamic IsAccessible { get; set; }
        
        [Browsable(false)]
        public new dynamic IsDisposed { get; set; }
        
        [Browsable(false)]
        public new dynamic IsHandleCreated { get; set; }
        
        [Browsable(false)]
        public new dynamic IsMirrored { get; set; }
        
        [Browsable(false)]
        public new dynamic LayoutEngine { get; set; }
        
        [Browsable(false)]
        public new dynamic Margin { get; set; }
        
        [Browsable(false)]
        public new dynamic MaximumSize { get; set; }
        
        [Browsable(false)]
        public new dynamic MinimumSize { get; set; }
        
        [Browsable(false)]
        public new dynamic Padding { get; set; }
        
        [Browsable(false)]
        public new dynamic PreferredSize { get; set; }
        
        [Browsable(false)]
        public new dynamic RecreatingHandle { get; set; }
        
        [Browsable(false)]
        public new dynamic Region { get; set; }
        
        [Browsable(false)]
        public new dynamic RightToLeft { get; set; }
        
        [Browsable(false)]
        public new dynamic Site { get; set; }
        
        [Browsable(false)]
        public new dynamic Tag { get; set; }
        
        [Browsable(false)]
        public new dynamic WindowTarget { get; set; }

        private System.Collections.Hashtable toolTip = new System.Collections.Hashtable();
        [Browsable(false)]
        public System.Collections.Hashtable ToolTip
        {
            get { return toolTip; }
            set { toolTip = value; }
        }

        [Browsable(false)]
        public string DefaultValues { get; set; }
			
        [Browsable(false)]
        public string RequiredValues
        {
            get
            {
                return @"
Панели ==
";
            }
        }
    }
}
