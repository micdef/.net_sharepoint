using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Voyage.Models;

namespace Voyage.App
{
    class ConsoleApplication : A_Console
    {

        #region Constants

        // Application Size
        private const int SCREEN_XSTART = 1;
        private const int SCREEN_XSTOP = 80;
        private const int SCREEN_YSTART = 1;
        private const int SCREEN_YSTOP = 30;

        // Application Border Characters
        private const char BORDER_TOP_LEFT = '╔';
        private const char BORDER_TOP_RIGHT = '╗';
        private const char BORDER_MID_LEFT = '╠';
        private const char BORDER_MID_RIGHT = '╣';
        private const char BORDER_BOTTOM_LEFT = '╚';
        private const char BORDER_BOTTOM_RIGHT = '╝';
        private const char BORDER_HORIZONTAL = '═';
        private const char BORDER_VERTICAL = '║';

        // Application Borders Position
        private const int BORDER_TITLE_Y = 3;
        private const int BORDER_MAIN_Y = SCREEN_YSTOP - 5;
        private const int BORDER_ENCOD_Y = SCREEN_YSTOP - 3;

        // Application Screen Using
        private const int TITLE_Y = SCREEN_YSTART + 1;
        private const int MAIN_YSTART = SCREEN_YSTART + 3;
        private const int MAIN_YSTOP = SCREEN_YSTOP - 6;
        private const int ENCOD_Y = SCREEN_YSTOP - 4;
        private const int MESSAGE_YSTART = SCREEN_YSTOP - 2;
        private const int MESSAGE_YSTOP = SCREEN_YSTOP - 1;
        private const int USAGE_XSTART = SCREEN_XSTART + 2;
        private const int USAGE_XSTOP = SCREEN_XSTOP - 2;

        #endregion

        #region Enumerates

        public enum EnumPositions
        {
            CENTER = 0,
            RIGHT = 1,
            LEFT = 2
        }

        public enum EnumMessageTypes
        {
            ERROR = 0,
            SUCCESS = 1,
            DEFAULT = 3
        }

        #endregion

        #region Fields

        private readonly Dictionary<string, Dictionary<string, ConsoleColor>> _messageTypeConfig;
        private readonly List<string> _messageType;
        private readonly int _midXScreen;
        private static ConsoleApplication _instance;

        #endregion

        #region Constructors

        private ConsoleApplication() : base (SCREEN_XSTOP, SCREEN_YSTOP)
        {
            _messageTypeConfig = new Dictionary<string, Dictionary<string, ConsoleColor>>();
            _messageType = new List<string>();
            _midXScreen = (SCREEN_XSTOP - SCREEN_XSTART) / 2;
            RemplirListeTypeMessage();
            RemplirListeConfigurationTypeMessage();
        }

        public static ConsoleApplication GetInstance()
        {
            if (_instance == null)
                _instance = new ConsoleApplication();
            return _instance;
        }

        #endregion

        #region Getters-Setter



        #endregion

        #region Private Methods

        private void RemplirListeTypeMessage()
        {
            _messageType.Add("default");
            _messageType.Add("error");
            _messageType.Add("success");
        }

        private void RemplirListeConfigurationTypeMessage()
        {
            foreach(string tm in _messageType)
            {
                Dictionary<string, ConsoleColor> d = new Dictionary<string, ConsoleColor>();
                switch (tm)
                {
                    case "error":
                        d.Add("text", ConsoleColor.Red);
                        d.Add("background", ConsoleColor.Black);
                        _messageTypeConfig.Add("error", d);
                        break;
                    case "success":
                        d.Add("text", ConsoleColor.Green);
                        d.Add("background", ConsoleColor.Black);
                        _messageTypeConfig.Add("success", d);
                        break;
                    default:
                        d.Add("text", ConsoleColor.Gray);
                        d.Add("background", ConsoleColor.Black);
                        _messageTypeConfig.Add("default", d);
                        break;
                }
            }
        }

        private string CentrerTexte(string texte)
        {
            string s = null;
            int pos = _midXScreen - (int)(texte.Length / 2);
            for (int i = 0; i < pos; i++)
                s += " ";
            s += texte;
            return s;
        }

        #endregion

        #region Public Methods

        public void AfficherCadre()
        {
            for (int y = SCREEN_YSTART; y <= SCREEN_YSTOP; y++)
                switch (y)
                {
                    case SCREEN_YSTART:
                        for (int x = SCREEN_XSTART; x <= SCREEN_XSTOP; x++)
                            base.ShowCarInPos(x, y, (x == SCREEN_XSTART ? BORDER_TOP_LEFT : (x == SCREEN_XSTOP ? BORDER_TOP_RIGHT : BORDER_HORIZONTAL)));
                        break;
                    case BORDER_TITLE_Y:
                    case BORDER_MAIN_Y:
                    case BORDER_ENCOD_Y:
                        for (int x = SCREEN_XSTART; x <= SCREEN_XSTOP; x++)
                            base.ShowCarInPos(x, y, (x == SCREEN_XSTART ? BORDER_MID_LEFT : (x == SCREEN_XSTOP ? BORDER_MID_RIGHT : BORDER_HORIZONTAL)));
                        break;
                    case SCREEN_YSTOP:
                        for (int x = SCREEN_XSTART; x <= SCREEN_XSTOP; x++)
                            base.ShowCarInPos(x, y, (x == SCREEN_XSTART ? BORDER_BOTTOM_LEFT : (x == SCREEN_XSTOP ? BORDER_BOTTOM_RIGHT : BORDER_HORIZONTAL)));
                        break;
                    default:
                        base.ShowCarInPos(SCREEN_XSTART, y, BORDER_VERTICAL);
                        base.ShowCarInPos(SCREEN_XSTOP, y, BORDER_VERTICAL);
                        break;
                }
        }

        public void AfficherTitre(string titre, string version)
        {
            if (titre == null || version == null || titre.Length + version.Length == 0)
                AfficherMessage("Les objets passés ne peuvent pas être null.", EnumMessageTypes.ERROR);
            if (titre.Length + version.Length + " V:".Length > USAGE_XSTOP - USAGE_XSTART + 1)
                AfficherMessage($"Le longueur du titre et de la version réunis ne peuvent pas dépasser {USAGE_XSTOP - USAGE_XSTART + 1 - " V:".Length} caractères", EnumMessageTypes.ERROR);
            string s = CentrerTexte($"{titre} V:{version}");
            base.WriteLine(USAGE_XSTART, USAGE_XSTOP, TITLE_Y, TITLE_Y, s);
            base.Title = $"{titre} V:{version}";
        }

        public void AfficherMessage(string msg, EnumMessageTypes errorType)
        {
            if (msg == null || msg.Trim().Length == 0)
                AfficherMessage("Le message ne peut pas être vide", EnumMessageTypes.ERROR);
            if (msg.Length > (USAGE_XSTOP - USAGE_XSTART + 1) * (MESSAGE_YSTOP - MESSAGE_YSTART + 1))
                AfficherMessage($"Le message est trop long, la longueur maximum autorisée est de : {(USAGE_XSTOP - USAGE_XSTART + 1) * (MESSAGE_YSTOP - MESSAGE_YSTART + 1)}", EnumMessageTypes.ERROR);
            Dictionary<string, ConsoleColor> consoleDef = _messageTypeConfig[errorType.ToString().ToLower()];
            base.TextColor = consoleDef["text"];
            base.BackgroundColor = consoleDef["background"];
            for (int y = MESSAGE_YSTART; y <= MESSAGE_YSTOP; y++)
                base.ClearLine(y, USAGE_XSTART, USAGE_XSTOP);
            base.WriteLine(USAGE_XSTART, USAGE_XSTOP, MESSAGE_YSTART, MESSAGE_YSTOP, msg);
            base.ResetDefaultColors();
            base.Wait(2500);
        }

        public string RecupereChaineCaract(string msg)
        {
            base.ClearLine(ENCOD_Y, USAGE_XSTART, USAGE_XSTOP);
            int posX = USAGE_XSTART + msg.Length;
            base.WriteLine(USAGE_XSTART, USAGE_XSTOP, ENCOD_Y, ENCOD_Y, msg);
            SetCursorXY(posX, ENCOD_Y);
            return base.GetUserInputString();
        }

        public char RecupererCaract(string msg)
        {
            base.ClearLine(ENCOD_Y, USAGE_XSTART, USAGE_XSTOP);
            int posX = USAGE_XSTART + msg.Length;
            base.WriteLine(USAGE_XSTART, USAGE_XSTOP, ENCOD_Y, ENCOD_Y, msg);
            base.SetCursorXY(posX, ENCOD_Y);
            return base.GetUserInputChar();
        }

        public void ViderZonePrincipale()
        {
            for (int y = MAIN_YSTART; y <= MAIN_YSTOP; y++)
                base.ClearLine(y, USAGE_XSTART, USAGE_XSTOP);
        }

        public void VidezZoneMessage()
        {
            for (int y = MESSAGE_YSTART; y <= MESSAGE_YSTOP; y++)
                base.ClearLine(y, USAGE_XSTART, USAGE_XSTOP);
        }

        public void VidezZoneInput()
        {
            base.ClearLine(ENCOD_Y, USAGE_XSTART, USAGE_XSTOP);
        }

        public void AfficherMenu(string title, List<String> elements, int page = 1, int nbElemPage = 10, bool optionQuitter = false)
        {
            // Vide la zone principale
            ViderZonePrincipale();

            // Affiche le titre et le souligne
            string underline = null;
            for (int i = 0; i < title.Length; i++)
                underline += "-";
            title = CentrerTexte(title);
            base.WriteLine(USAGE_XSTART, USAGE_XSTOP, MAIN_YSTART, MAIN_YSTOP, title);
            underline = CentrerTexte(underline);
            base.WriteLine(USAGE_XSTART, USAGE_XSTOP, MAIN_YSTART + 1, MAIN_YSTART + 1, underline);

            // Afficher les élements
            int y = (MAIN_YSTART + 3);
            for (int i = 0; i < nbElemPage && i < elements.Count; i++, y++)
            {
                string l = $"[{i}] {elements[((page - 1) * nbElemPage) + i]}";
                base.WriteLine(USAGE_XSTART, USAGE_XSTOP, y, y, l);
            }

            // Afficher les options supplementaires
            y += 2;
            int nbPagesMax = (elements.Count / nbElemPage) + (elements.Count % nbElemPage == 0 ? 0 : 1);
            if (page > 1) base.WriteLine(USAGE_XSTART, USAGE_XSTOP, y++, y++, "[P] Page Précédente");
            if (page < nbPagesMax) base.WriteLine(USAGE_XSTART, USAGE_XSTOP, y++, y++, "[S] Page Suivante");
            if (optionQuitter) base.WriteLine(USAGE_XSTART, USAGE_XSTOP, y++, y++, "[Q] Quitter sans faire de sélection");
        }

        public void AfficherMessageZP(string message)
        {
            ViderZonePrincipale();
            base.WriteLine(USAGE_XSTART, USAGE_XSTOP, MAIN_YSTART, MAIN_YSTOP, message);
        }

        #endregion

    }
}
