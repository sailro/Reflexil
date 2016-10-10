// Martin Lottering, Lukasz Swiatkowski.
// From CodeProject.com "Simple pop-up control" "http://www.codeproject.com/cs/miscctrl/simplepopup.asp".
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Reflexil.Editors
{
	public partial class CheckBoxComboBox : PopupComboBox
	{
		internal CheckBoxComboBoxListControl CheckBoxComboBoxListControl;
		private string _displayMemberSingleItem;
		internal bool MustAddHiddenItem;
		private CheckBoxProperties _checkBoxProperties;

		internal string GetCSVText(bool skipFirstItem)
		{
			var listText = string.Empty;
			var startIndex =
				DropDownStyle == ComboBoxStyle.DropDownList
				&& DataSource == null
				&& skipFirstItem
					? 1
					: 0;
			for (var index = startIndex; index <= CheckBoxComboBoxListControl.Items.Count - 1; index++)
			{
				var item = CheckBoxComboBoxListControl.Items[index];
				if (item.Checked)
					listText += string.IsNullOrEmpty(listText) ? item.Text : string.Format(", {0}", item.Text);
			}
			return listText;
		}

		[Browsable(false)]
		public CheckBoxComboBoxItemList CheckBoxItems
		{
			get
			{
				// Added to ensure the CheckBoxItems are ALWAYS
				// available for modification via code.
				if (CheckBoxComboBoxListControl.Items.Count != Items.Count)
					CheckBoxComboBoxListControl.SynchroniseControlsWithComboBoxItems();
				return CheckBoxComboBoxListControl.Items;
			}
		}

		public new object DataSource
		{
			get { return base.DataSource; }
			set
			{
				base.DataSource = value;
				if (!string.IsNullOrEmpty(ValueMember))
					// This ensures that at least the checkboxitems are available to be initialised.
					CheckBoxComboBoxListControl.SynchroniseControlsWithComboBoxItems();
			}
		}

		public new string ValueMember
		{
			get { return base.ValueMember; }
			set
			{
				base.ValueMember = value;
				if (!string.IsNullOrEmpty(ValueMember))
					// This ensures that at least the checkboxitems are available to be initialised.
					CheckBoxComboBoxListControl.SynchroniseControlsWithComboBoxItems();
			}
		}

		public string DisplayMemberSingleItem
		{
			get { return string.IsNullOrEmpty(_displayMemberSingleItem) ? DisplayMember : _displayMemberSingleItem; }
			set { _displayMemberSingleItem = value; }
		}

		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new ObjectCollection Items
		{
			get { return base.Items; }
		}

		[Description("The properties that will be assigned to the checkboxes as default values.")]
		[Browsable(true)]
		public CheckBoxProperties CheckBoxProperties
		{
			get { return _checkBoxProperties; }
			set
			{
				_checkBoxProperties = value;
				_CheckBoxProperties_PropertyChanged(this, EventArgs.Empty);
			}
		}

		private void _CheckBoxProperties_PropertyChanged(object sender, EventArgs e)
		{
			foreach (var item in CheckBoxItems)
				item.ApplyProperties(CheckBoxProperties);
		}

		public event EventHandler CheckBoxCheckedChanged;

		private void Items_CheckBoxCheckedChanged(object sender, EventArgs e)
		{
			OnCheckBoxCheckedChanged(sender, e);
		}

		protected void OnCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			var listText = GetCSVText(true);
			// The DropDownList style seems to require that the text
			// part of the "textbox" should match a single item.
			if (DropDownStyle != ComboBoxStyle.DropDownList)
				Text = listText;
			// This refreshes the Text of the first item (which is not visible)
			else if (DataSource == null)
			{
				Items[0] = listText;
				// Keep the hidden item and first checkbox item in 
				// sync in order to ensure the Synchronise process
				// can match the items.
				CheckBoxItems[0].ComboBoxItem = listText;
			}

			var handler = CheckBoxCheckedChanged;
			if (handler != null)
				handler(sender, e);
		}

		protected override void OnDropDownStyleChanged(EventArgs e)
		{
			base.OnDropDownStyleChanged(e);

			if (DropDownStyle == ComboBoxStyle.DropDownList
			    && DataSource == null
			    && !DesignMode)
				MustAddHiddenItem = true;
		}

		protected override void OnResize(EventArgs e)
		{
			// When the ComboBox is resized, the width of the dropdown 
			// is also resized to match the width of the ComboBox. I think it looks better.
			var size = new Size(Width, DropDownControl.Height);
			PopupDropDown.Size = size;
			base.OnResize(e);
		}

		public CheckBoxComboBox()
		{
			InitializeComponent();
			_checkBoxProperties = new CheckBoxProperties();
			_checkBoxProperties.PropertyChanged += _CheckBoxProperties_PropertyChanged;
			// Dumps the ListControl in a(nother) Container to ensure the ScrollBar on the ListControl does not
			// Paint over the Size grip. Setting the Padding or Margin on the Popup or host control does
			// not work as I expected. I don't think it can work that way.
			var containerControl = new CheckBoxComboBoxListControlContainer();
			CheckBoxComboBoxListControl = new CheckBoxComboBoxListControl(this);
			CheckBoxComboBoxListControl.Items.CheckBoxCheckedChanged += Items_CheckBoxCheckedChanged;
			containerControl.Controls.Add(CheckBoxComboBoxListControl);
			// This padding spaces neatly on the left-hand side and allows space for the size grip at the bottom.
			containerControl.Padding = new Padding(4, 0, 0, 14);
			// The ListControl FILLS the ListContainer.
			CheckBoxComboBoxListControl.Dock = DockStyle.Fill;
			// The DropDownControl used by the base class. Will be wrapped in a popup by the base class.
			DropDownControl = containerControl;
			// Must be set after the DropDownControl is set, since the popup is recreated.
			// NOTE: I made the PopupDropDown protected so that it can be accessible here. It was private.
			PopupDropDown.Resizable = true;
		}

		public void Clear()
		{
			Items.Clear();
			if (DropDownStyle == ComboBoxStyle.DropDownList && DataSource == null)
				MustAddHiddenItem = true;
		}

		public void ClearSelection()
		{
			foreach (var item in CheckBoxItems.Where(item => item.Checked))
				item.Checked = false;
		}

		protected override void WndProc(ref Message m)
		{
			// 323 : Item Added
			// 331 : Clearing
			if (m.Msg == 331
			    && DropDownStyle == ComboBoxStyle.DropDownList
			    && DataSource == null)
			{
				MustAddHiddenItem = true;
			}

			base.WndProc(ref m);
		}
	}

	[ToolboxItem(false)]
	public sealed class CheckBoxComboBoxListControlContainer : UserControl
	{
		public CheckBoxComboBoxListControlContainer()
		{
			BackColor = SystemColors.Window;
			BorderStyle = BorderStyle.FixedSingle;
			AutoScaleMode = AutoScaleMode.Inherit;
			ResizeRedraw = true;
			// If you don't set this, then resize operations cause an error in the base class.
			MinimumSize = new Size(1, 1);
			MaximumSize = new Size(800, 600);
		}

		protected override void WndProc(ref Message m)
		{
			var popup = Parent as Popup;
			if (popup != null && popup.ProcessResizing(ref m))
			{
				return;
			}
			base.WndProc(ref m);
		}
	}

	[ToolboxItem(false)]
	public sealed class CheckBoxComboBoxListControl : ScrollableControl
	{
		private readonly CheckBoxComboBox _checkBoxComboBox;
		private readonly CheckBoxComboBoxItemList _items;

		public CheckBoxComboBoxItemList Items
		{
			get { return _items; }
		}

		public CheckBoxComboBoxListControl(CheckBoxComboBox owner)
		{
			DoubleBuffered = true;
			_checkBoxComboBox = owner;
			_items = new CheckBoxComboBoxItemList(_checkBoxComboBox);
			BackColor = SystemColors.Window;
			// AutoScaleMode = AutoScaleMode.Inherit;
			AutoScroll = true;
			ResizeRedraw = true;
			// if you don't set this, a Resize operation causes an error in the base class.
			MinimumSize = new Size(1, 1);
			MaximumSize = new Size(800, 640);
		}

		protected override void WndProc(ref Message m)
		{
			var popup = Parent.Parent as Popup;
			if (popup != null && popup.ProcessResizing(ref m))
			{
				return;
			}
			base.WndProc(ref m);
		}

		protected override void OnVisibleChanged(EventArgs e)
		{
			// Synchronises the CheckBox list with the items in the ComboBox.
			SynchroniseControlsWithComboBoxItems();
			base.OnVisibleChanged(e);
		}

		public void SynchroniseControlsWithComboBoxItems()
		{
			SuspendLayout();
			if (_checkBoxComboBox.MustAddHiddenItem)
			{
				_checkBoxComboBox.Items.Insert(
					0, _checkBoxComboBox.GetCSVText(false)); // INVISIBLE ITEM
				_checkBoxComboBox.SelectedIndex = 0;
				_checkBoxComboBox.MustAddHiddenItem = false;
			}
			Controls.Clear();

			for (var index = _items.Count - 1; index >= 0; index--)
			{
				var item = _items[index];
				if (!_checkBoxComboBox.Items.Contains(item.ComboBoxItem))
				{
					_items.Remove(item);
					item.Dispose();
				}
			}

			var hasHiddenItem =
				_checkBoxComboBox.DropDownStyle == ComboBoxStyle.DropDownList
				&& _checkBoxComboBox.DataSource == null
				&& !DesignMode;

			var newList = new CheckBoxComboBoxItemList(_checkBoxComboBox);
			for (var index0 = 0; index0 <= _checkBoxComboBox.Items.Count - 1; index0++)
			{
				var Object = _checkBoxComboBox.Items[index0];
				CheckBoxComboBoxItem item = null;
				// The hidden item could match any other item when only
				// one other item was selected.
				if (index0 == 0 && hasHiddenItem && _items.Count > 0)
					item = _items[0];
				else
				{
					var startIndex = hasHiddenItem
						? 1 // Skip the hidden item, it could match 
						: 0;
					for (var index1 = startIndex; index1 <= _items.Count - 1; index1++)
					{
						if (_items[index1].ComboBoxItem != Object)
							continue;

						item = _items[index1];
						break;
					}
				}
				if (item == null)
				{
					item = new CheckBoxComboBoxItem(_checkBoxComboBox, Object);
					item.ApplyProperties(_checkBoxComboBox.CheckBoxProperties);
				}
				newList.Add(item);
				item.Dock = DockStyle.Top;
			}
			_items.Clear();
			_items.AddRange(newList);

			if (newList.Count > 0)
			{
				// This reverse helps to maintain correct docking order.
				newList.Reverse();
				// If you get an error here that "Cannot convert to the desired 
				// type, it probably means the controls are not binding correctly.
				// The Checked property is binded to the ValueMember property. 
				// It must be a bool for example.
				// ReSharper disable once CoVariantArrayConversion
				Controls.AddRange(newList.ToArray());
			}

			// Keep the first item invisible
			if (_checkBoxComboBox.DropDownStyle == ComboBoxStyle.DropDownList
			    && _checkBoxComboBox.DataSource == null
			    && !DesignMode)
				_checkBoxComboBox.CheckBoxItems[0].Visible = false;

			ResumeLayout();
		}
	}

	[ToolboxItem(false)]
	public sealed class CheckBoxComboBoxItem : CheckBox
	{
		private readonly CheckBoxComboBox _checkBoxComboBox;

		public object ComboBoxItem { get; internal set; }

		public CheckBoxComboBoxItem(CheckBoxComboBox owner, object comboBoxItem)
		{
			DoubleBuffered = true;
			_checkBoxComboBox = owner;
			ComboBoxItem = comboBoxItem;
			if (_checkBoxComboBox.DataSource != null)
				AddBindings();
			else
				Text = comboBoxItem.ToString();
		}

		public void AddBindings()
		{
			// Note, the text uses "DisplayMemberSingleItem", not "DisplayMember" (unless its not assigned)
			DataBindings.Add(
				"Text",
				ComboBoxItem,
				_checkBoxComboBox.DisplayMemberSingleItem
			);
			// The ValueMember must be a bool type property usable by the CheckBox.Checked.
			DataBindings.Add(
				"Checked",
				ComboBoxItem,
				_checkBoxComboBox.ValueMember,
				false,
				// This helps to maintain proper selection state in the Binded object,
				// even when the controls are added and removed.
				DataSourceUpdateMode.OnPropertyChanged,
				false, null, null);
			// Helps to maintain the Checked status of this
			// checkbox before the control is visible
			var item = ComboBoxItem as INotifyPropertyChanged;
			if (item != null)
				item.PropertyChanged +=
					CheckBoxComboBoxItem_PropertyChanged;
		}

		protected override void OnCheckedChanged(EventArgs e)
		{
			// Found that when this event is raised, the bool value of the binded item is not yet updated.
			if (_checkBoxComboBox.DataSource != null)
			{
				var pi = ComboBoxItem.GetType().GetProperty(_checkBoxComboBox.ValueMember);
				pi.SetValue(ComboBoxItem, Checked, null);
			}
			base.OnCheckedChanged(e);
			// Forces a refresh of the Text displayed in the main TextBox of the ComboBox,
			// since that Text will most probably represent a concatenation of selected values.
			// Also see DisplayMemberSingleItem on the CheckBoxComboBox for more information.
			if (_checkBoxComboBox.DataSource != null)
			{
				var oldDisplayMember = _checkBoxComboBox.DisplayMember;
				_checkBoxComboBox.DisplayMember = null;
				_checkBoxComboBox.DisplayMember = oldDisplayMember;
			}
		}

		internal void ApplyProperties(CheckBoxProperties properties)
		{
			Appearance = properties.Appearance;
			AutoCheck = properties.AutoCheck;
			AutoEllipsis = properties.AutoEllipsis;
			AutoSize = properties.AutoSize;
			CheckAlign = properties.CheckAlign;
			FlatAppearance.BorderColor = properties.FlatAppearanceBorderColor;
			FlatAppearance.BorderSize = properties.FlatAppearanceBorderSize;
			FlatAppearance.CheckedBackColor = properties.FlatAppearanceCheckedBackColor;
			FlatAppearance.MouseDownBackColor = properties.FlatAppearanceMouseDownBackColor;
			FlatAppearance.MouseOverBackColor = properties.FlatAppearanceMouseOverBackColor;
			FlatStyle = properties.FlatStyle;
			ForeColor = properties.ForeColor;
			RightToLeft = properties.RightToLeft;
			TextAlign = properties.TextAlign;
			ThreeState = properties.ThreeState;
		}

		private void CheckBoxComboBoxItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == _checkBoxComboBox.ValueMember)
				Checked =
					(bool) ComboBoxItem
						.GetType()
						.GetProperty(_checkBoxComboBox.ValueMember)
						.GetValue(ComboBoxItem, null);
		}
	}

	[ToolboxItem(false)]
	public class CheckBoxComboBoxItemList : List<CheckBoxComboBoxItem>
	{
		public CheckBoxComboBoxItemList(CheckBoxComboBox checkBoxComboBox)
		{
			_checkBoxComboBox = checkBoxComboBox;
		}

		private readonly CheckBoxComboBox _checkBoxComboBox;

		public CheckBoxComboBoxItem this[string displayName]
		{
			get
			{
				var startIndex =
					// An invisible item exists in this scenario to help 
					// with the Text displayed in the TextBox of the Combo
					_checkBoxComboBox.DropDownStyle == ComboBoxStyle.DropDownList
					&& _checkBoxComboBox.DataSource == null
						? 1
						// Ubiklou : 2008-04-28 : Ignore first item. (http://www.codeproject.com/KB/combobox/extending_combobox.aspx?fid=476622&df=90&mpp=25&noise=3&sort=Position&view=Quick&select=2526813&fr=1#xx2526813xx)
						: 0;
				for (var index = startIndex; index <= Count - 1; index++)
				{
					var item = this[index];

					string text;
					// The binding might not be active yet
					if (string.IsNullOrEmpty(item.Text)
					    && item.DataBindings["Text"] != null
					)
					{
						var propertyInfo
							= item.ComboBoxItem.GetType().GetProperty(
								item.DataBindings["Text"].BindingMemberInfo.BindingMember);
						text = (string) propertyInfo.GetValue(item.ComboBoxItem, null);
					}
					else
						text = item.Text;
					if (String.Compare(text, displayName, StringComparison.Ordinal) == 0)
						return item;
				}
				throw new ArgumentOutOfRangeException(String.Format("\"{0}\" does not exist in this combo box.", displayName));
			}
		}

		public event EventHandler CheckBoxCheckedChanged;

		protected void OnCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			var handler = CheckBoxCheckedChanged;
			if (handler != null)
				handler(sender, e);
		}

		private void item_CheckedChanged(object sender, EventArgs e)
		{
			OnCheckBoxCheckedChanged(sender, e);
		}

		//[Obsolete("Do not add items to this list directly. Use the ComboBox items instead.", false)]
		public new void Add(CheckBoxComboBoxItem item)
		{
			item.CheckedChanged += item_CheckedChanged;
			base.Add(item);
		}

		public new void AddRange(IEnumerable<CheckBoxComboBoxItem> enumerable)
		{
			var collection = enumerable.ToList();
			foreach (var item in collection)
				item.CheckedChanged += item_CheckedChanged;
			base.AddRange(collection);
		}

		public new void Clear()
		{
			foreach (var item in this)
				item.CheckedChanged -= item_CheckedChanged;
			base.Clear();
		}

		//[Obsolete("Do not remove items from this list directly. Use the ComboBox items instead.", false)]
		public new bool Remove(CheckBoxComboBoxItem item)
		{
			item.CheckedChanged -= item_CheckedChanged;
			return base.Remove(item);
		}
	}

	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class CheckBoxProperties
	{
		private Appearance _appearance = Appearance.Normal;
		private bool _autoSize;
		private bool _autoCheck = true;
		private bool _autoEllipsis;
		private ContentAlignment _checkAlign = ContentAlignment.MiddleLeft;
		private Color _flatAppearanceBorderColor = Color.Empty;
		private int _flatAppearanceBorderSize = 1;
		private Color _flatAppearanceCheckedBackColor = Color.Empty;
		private Color _flatAppearanceMouseDownBackColor = Color.Empty;
		private Color _flatAppearanceMouseOverBackColor = Color.Empty;
		private FlatStyle _flatStyle = FlatStyle.Standard;
		private Color _foreColor = SystemColors.ControlText;
		private RightToLeft _rightToLeft = RightToLeft.No;
		private ContentAlignment _textAlign = ContentAlignment.MiddleLeft;
		private bool _threeState;

		[DefaultValue(Appearance.Normal)]
		public Appearance Appearance
		{
			get { return _appearance; }
			set
			{
				_appearance = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(true)]
		public bool AutoCheck
		{
			get { return _autoCheck; }
			set
			{
				_autoCheck = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(false)]
		public bool AutoEllipsis
		{
			get { return _autoEllipsis; }
			set
			{
				_autoEllipsis = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(false)]
		public bool AutoSize
		{
			get { return _autoSize; }
			set
			{
				_autoSize = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(ContentAlignment.MiddleLeft)]
		public ContentAlignment CheckAlign
		{
			get { return _checkAlign; }
			set
			{
				_checkAlign = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(typeof(Color), "")]
		public Color FlatAppearanceBorderColor
		{
			get { return _flatAppearanceBorderColor; }
			set
			{
				_flatAppearanceBorderColor = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(1)]
		public int FlatAppearanceBorderSize
		{
			get { return _flatAppearanceBorderSize; }
			set
			{
				_flatAppearanceBorderSize = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(typeof(Color), "")]
		public Color FlatAppearanceCheckedBackColor
		{
			get { return _flatAppearanceCheckedBackColor; }
			set
			{
				_flatAppearanceCheckedBackColor = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(typeof(Color), "")]
		public Color FlatAppearanceMouseDownBackColor
		{
			get { return _flatAppearanceMouseDownBackColor; }
			set
			{
				_flatAppearanceMouseDownBackColor = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(typeof(Color), "")]
		public Color FlatAppearanceMouseOverBackColor
		{
			get { return _flatAppearanceMouseOverBackColor; }
			set
			{
				_flatAppearanceMouseOverBackColor = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get { return _flatStyle; }
			set
			{
				_flatStyle = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(typeof(SystemColors), "ControlText")]
		public Color ForeColor
		{
			get { return _foreColor; }
			set
			{
				_foreColor = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(RightToLeft.No)]
		public RightToLeft RightToLeft
		{
			get { return _rightToLeft; }
			set
			{
				_rightToLeft = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(ContentAlignment.MiddleLeft)]
		public ContentAlignment TextAlign
		{
			get { return _textAlign; }
			set
			{
				_textAlign = value;
				OnPropertyChanged();
			}
		}

		[DefaultValue(false)]
		public bool ThreeState
		{
			get { return _threeState; }
			set
			{
				_threeState = value;
				OnPropertyChanged();
			}
		}

		public event EventHandler PropertyChanged;

		protected void OnPropertyChanged()
		{
			EventHandler handler = PropertyChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}
	}
}