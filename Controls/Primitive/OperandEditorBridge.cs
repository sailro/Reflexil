using System;

namespace Reflexil.Editors
{
	public class OperandEditorBridge<T> : IDisposable
	{
		private OperandEditor<T> _left;
		private OperandEditor<T> _right;

		private bool _recurseMarker;
		private readonly EventHandler _leftOnSelectedOperandChanged;
		private readonly EventHandler _rightOnSelectedOperandChanged;

		public OperandEditorBridge(OperandEditor<T> left, OperandEditor<T> right)
		{
			_left = left;
			_right = right;

			_leftOnSelectedOperandChanged = (sender, args) => OnSelectedOperandChanged(left, right);
			left.SelectedOperandChanged += _leftOnSelectedOperandChanged;

			_rightOnSelectedOperandChanged = (sender, args) => OnSelectedOperandChanged(right, left);
			right.SelectedOperandChanged += _rightOnSelectedOperandChanged;
		}


		private void OnSelectedOperandChanged(OperandEditor<T> source, OperandEditor<T> destination)
		{
			if (_recurseMarker)
				return;

			_recurseMarker = true;
			destination.SelectedOperand = source.SelectedOperand;
			_recurseMarker = false;
		}

		public void Dispose()
		{
			if (_left != null)
			{
				_left.SelectedOperandChanged -= _leftOnSelectedOperandChanged;
				_left = null;
			}

			if (_right == null)
				return;

			_right.SelectedOperandChanged -= _rightOnSelectedOperandChanged;
			_right = null;
		}
	}
}