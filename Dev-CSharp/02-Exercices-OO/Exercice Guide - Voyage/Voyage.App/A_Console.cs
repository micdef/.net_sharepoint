using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voyage.App
{
    public abstract class A_Console
    {
        #region Constants

        private const int MIN_COLUMNS = 10;
        private const int MAX_COLUMNS = 100;
        private const int MIN_LINES = 10;
        private const int MAX_LINES = 40;
        
        #endregion

        #region Fields

        private int _columnStart;
        private int _columnEnd;
        private int _lineStart;
        private int _lineEnd;
        private readonly ConsoleColor _defaultTextColor;
        private readonly ConsoleColor _defaultBackgroundColor;

        #endregion

        #region MyRegion

        public A_Console()
        {
            this._columnStart = 1;
            this._columnEnd = 80;
            this._lineStart = 1;
            this._lineEnd = 24;
            this._defaultTextColor = ConsoleColor.Gray;
            this._defaultBackgroundColor = ConsoleColor.Black;
            Console.SetWindowSize(this._columnEnd + 1, this._lineEnd);
            this.BackgroundColor = this._defaultBackgroundColor;
            this.TextColor = this._defaultTextColor;
        }

        public A_Console(int columnEnd, int lineEnd)
        {
            this._columnStart = 1;
            this.ColumnEnd = columnEnd;
            this._lineStart = 1;
            this.LineEnd = lineEnd;
            this._defaultTextColor = ConsoleColor.Gray;
            this._defaultBackgroundColor = ConsoleColor.Black;
            Console.SetWindowSize(this.ColumnEnd + 1, this.LineEnd);
            this.BackgroundColor = this._defaultBackgroundColor;
            this.TextColor = this._defaultTextColor;
        }

        #endregion

        #region Getters-Setters

        public int ColumnStart
        {
            get { return _columnStart; }
        }

        public int ColumnEnd
        {
            get { return _columnEnd; }
            private set 
            {
                if (value <= _columnStart || value > MAX_COLUMNS)
                    throw new IndexOutOfRangeException($"La valeur sélectionnée ne se trouve pas dans le range possible du nombre de colonnes (Minimum {_columnStart + 1}, Maximum {MAX_COLUMNS})");
                if (value < MIN_COLUMNS)
                    throw new Exception($"La valeur de largeur de la console doit être au minimum de {MIN_COLUMNS} colonnes");
                _columnEnd = value;
            }
        }

        public int LineStart
        {
            get { return _lineStart; }
        }

        public int LineEnd
        {
            get { return _lineEnd; }
            private set 
            {
                if (value <= _lineStart || value > MAX_LINES)
                    throw new IndexOutOfRangeException($"La valeur sélectionnée ne se trouve pas dans le range possible du nombre de colonnes (Minimum {_lineStart + 1}, Maximum {MAX_LINES})");
                if (value < MIN_COLUMNS)
                    throw new Exception($"La valeur de largeur de la console doit être au minimum de {MIN_LINES} colonnes");
                _lineEnd = value;
            }
        }

        public string Title
        {
            get { return Console.Title; }
            set 
            {
                if (value == null || value.Trim().Length == 0)
                    throw new ArgumentNullException("L'argument passé ne peut pas être vide");
                Console.Title = value;
            }
        }

        public ConsoleColor TextColor
        {
            get { return Console.ForegroundColor; }
            set { Console.ForegroundColor = value; }
        }

        public ConsoleColor BackgroundColor
        {
            get { return Console.BackgroundColor; }
            set { Console.BackgroundColor = value; }
        }

        #endregion

        #region Methods

        public void Clear()
        {
            Console.Clear();
        }

        public void ResetDefaultColors()
        {
            this.TextColor = _defaultTextColor;
            this.BackgroundColor = _defaultBackgroundColor;
        }

        public void Wait(int milliseconds = 0)
        {
            if (milliseconds < 0)
                throw new Exception("Le temps d'attente ne peut pas être négatif");
            if (milliseconds == 0)
                Console.ReadKey();
            else
                System.Threading.Thread.Sleep(milliseconds);
        }

        public string GetUserInputString()
        {
            string s = null;
            s = Console.ReadLine();
            return s;
        }

        public char GetUserInputChar()
        {
            char c = ' ';
            c = Console.ReadKey().KeyChar;
            return c;
        }

        public void ShowCarInPos(int x, int y, char c)
        {
            if (x < _columnStart || x > _columnEnd)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{_columnStart} et n°{_columnEnd} incluses");
            if (y < _lineStart || y > _lineEnd)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{_lineStart} et n°{_lineEnd} incluses");
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }
        
        public void SetCursorXY(int x, int y)
        {
            if (x < _columnStart || x > _columnEnd)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{_columnStart} et n°{_columnEnd} incluses");
            if (y < _lineStart || y > _lineEnd)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{_lineStart} et n°{_lineEnd} incluses");
            Console.SetCursorPosition(x, y);
        }

        public void ClearLine(int y, int xStart, int xStop)
        {
            if (xStart < _columnStart || xStart > _columnEnd)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{_columnStart} et n°{_columnEnd} incluses");
            if (xStop < _columnStart || xStop > _columnEnd)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{_columnStart} et n°{_columnEnd} incluses");
            if (y < _lineStart || y > _lineEnd)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{_lineStart} et n°{_lineEnd} incluses");
            for (int x = xStart; x <= xStop; x++)
                ShowCarInPos(x, y, ' ');
        }

        public void WriteLine(int xStart, int xStop, int yStart, int yStop, string m)
        {
            if (xStart < _columnStart || xStart > _columnEnd)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{_columnStart} et n°{_columnEnd} incluses");
            if (xStop < _columnStart || xStop > _columnEnd)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{_columnStart} et n°{_columnEnd} incluses");
            if (yStart < _lineStart || yStart > _lineEnd)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{_lineStart} et n°{_lineEnd} incluses");
            if (yStop < _lineStart || yStop > _lineEnd)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{_lineStart} et n°{_lineEnd} incluses");
            if (m.Length > (xStop - xStart + 1) * (yStop - yStart + 1))
                throw new Exception($"Message trop long pour la zonne que vous avez sélectionné. Le message fait {m.Length} caractères et la zone fait {(xStop - xStart + 1) * (yStop - yStart + 1)} caractère");
            for (int y = yStart; y <= yStop; y++)
                ClearLine(y, xStart, xStop);
            for (int y = yStart, i = 0; y <= yStop && i < m.Length; y++)
                for (int x = xStart; x <= xStop && i < m.Length; x++)
                    ShowCarInPos(x, y, m.Substring(i++, 1).ToCharArray()[0]);
        }

        #endregion
    }
}
