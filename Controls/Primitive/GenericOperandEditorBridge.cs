using System;
using Reflexil.Editors;

namespace Controls.Primitive
{
	public class GenericOperandEditorBridge<T> : IDisposable
	{
		private GenericOperandEditor<T> _left;
		private GenericOperandEditor<T> _right;

		private bool _recurseMarker;
		private readonly EventHandler _leftOnSelectedOperandChanged;
		private readonly EventHandler _rightOnSelectedOperandChanged;

		public GenericOperandEditorBridge(GenericOperandEditor<T> left, GenericOperandEditor<T> right)
		{
			_left = left;
			_right = right;

			_leftOnSelectedOperandChanged = (sender, args) => OnSelectedOperandChanged(left, right);
			left.SelectedOperandChanged += _leftOnSelectedOperandChanged;
	
			_rightOnSelectedOperandChanged = (sender, args) => OnSelectedOperandChanged(right, left);
			right.SelectedOperandChanged += _rightOnSelectedOperandChanged;
		}


		private void OnSelectedOperandChanged(GenericOperandEditor<T> source, GenericOperandEditor<T> destination)
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
