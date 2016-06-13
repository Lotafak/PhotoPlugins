using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CommandInterface;

namespace PhotoPlugin
{
    class UndoRedoManager
    {
        private Stack<ICommand> _Undocommands = new Stack<ICommand>();
        private Stack<ICommand> _Redocommands = new Stack<ICommand>();

        public event EventHandler EnableDisableUndoRedoFeature;

        private Image _image;

        public Image ImageObject
        {
            get { return _image; }
            set { _image = value; }
        }

        public void Redo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (_Redocommands.Count != 0)
                {
                    ICommand command = _Redocommands.Pop();
                    command.Execute();
                    _Undocommands.Push(command);
                }

            }
            EnableDisableUndoRedoFeature?.Invoke(null, null);
        }

        public void Undo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (_Undocommands.Count != 0)
                {
                    ICommand command = _Undocommands.Pop();
                    command.UnExecute();
                    _Redocommands.Push(command);
                }

            }
            EnableDisableUndoRedoFeature?.Invoke(null, null);
        }

        //public void InsertInUnDoRedoForResize(Image image)
        //{
        //    ICommand cmd = new RotateClockwise(image);
        //    _Undocommands.Push(cmd); _Redocommands.Clear();
        //    EnableDisableUndoRedoFeature?.Invoke(null, null);
        //}

        public bool IsUndoPossible()
        {
            if (_Undocommands.Count != 0)
            {
                return true;
            }
            return false;
        }

        public bool IsRedoPossible()
        {
            if (_Redocommands.Count != 0)
            {
                return true;
            }
            return false;
        }
    }
}
