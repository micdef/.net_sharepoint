using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Voyage.App
{
    public class G_Console
    {
        #region Constants

        // Application Size
        private const int SCREEN_COL_MIN = 1;
        private const int SCREEN_COL_MAX = 80;
        private const int SCREEN_LINE_MIN = 1;
        private const int SCREEN_LINE_MAX = 30;

        // Application Color [Text]
        private const ConsoleColor TEXT_COLOR = ConsoleColor.Gray;
        private const ConsoleColor ERROR_TEXT_COLOR = ConsoleColor.White;
        private const ConsoleColor SUCCESS_TEXT_COLOR = ConsoleColor.Green;

        // Application Color [Background]
        private const ConsoleColor BACKGROUND_COLOR = ConsoleColor.Black;
        private const ConsoleColor ERROR_BACKGROUND_COLOR = ConsoleColor.Red;

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
        private const int BORDER_TITLE_LINE_TOP = 1;
        private const int BORDER_TITLE_LINE_BOTTOM = 3;
        private const int BORDER_MAIN_BOTTOM = SCREEN_LINE_MAX - 5;
        private const int BORDER_ENCOD_BOTTOM = SCREEN_LINE_MAX - 3;

        // Application Screen Using
        private const int TITLE_LINE = SCREEN_LINE_MIN + 1;
        private const int MAIN_LINE_TOP = SCREEN_LINE_MIN + 3;
        private const int MAIN_LINE_BOTTOM = SCREEN_LINE_MAX - 6;
        private const int ENCOD_LINT = SCREEN_LINE_MAX - 4;
        private const int MESSAGE_LINE_TOP = SCREEN_LINE_MAX - 2;
        private const int MESSAGE_LINE_BOTTOM = SCREEN_LINE_MAX - 1;
        private const int USAGE_COL_MIN = SCREEN_COL_MIN + 2;
        private const int USAGE_COL_MAX = SCREEN_COL_MAX - 2;

        // Application Informations
        private const string TITLE = "Gestion de Voyage";
        private const string VERSION = "1.0.0.0";

        #endregion

        #region Enumerates

        public enum EnumPosition
        {
            CENTER = 0,
            RIGHT = 1,
            LEFT = 2,
        }

        public enum EnumTypeMessage
        {
            MESSAGE = 0,
            ERROR = 1,
            SUCCESS = 2,
        }

        #endregion

        #region Fields

        private static G_Console _instance;

        #endregion

        #region Constructors

        /// <summary>
        /// Prépare la console
        /// </summary>
        private G_Console()
        {
            // Vidage de la console
            Clear();

            // Mise en forme de la console
            Console.SetWindowSize(SCREEN_COL_MAX, SCREEN_LINE_MAX);
            Console.ForegroundColor = TEXT_COLOR;
            Console.BackgroundColor = BACKGROUND_COLOR;
            Console.Title = $"{TITLE} V:{VERSION}";

            // Affichage de base
            AfficheCadre();

            // Affichage des premières informations
            AfficherMessage("Initialisation de la console", EnumTypeMessage.MESSAGE);
            AfficheTitre();
            AfficherMessage("Console Initialisée", EnumTypeMessage.SUCCESS);
            this.ViderZoneMessage();
        }

        /// <summary>
        /// Gère l'instance unique de la console
        /// </summary>
        /// <returns>L'instance unique de la console</returns>
        public static G_Console GetInstance()
        {
            if (_instance == null)
                _instance = new G_Console();
            return _instance;
        }

        #endregion

        #region Console's Behavior

        /// <summary>
        /// Vide la console complètement
        /// </summary>
        public void Clear()
        {
            Console.Clear();
        }

        /// <summary>
        /// Fait attendre le système le temps donné en millisecondes
        /// </summary>
        /// <param name="milliseconds">Temps d'attente en millisecondes</param>
        public void Wait(int milliseconds = 0)
        {
            if (milliseconds < 0)
                throw new Exception("Le temps d'attente ne peut pas être négatif");
            if (milliseconds == 0)
                Console.ReadKey();
            else
                Thread.Sleep(milliseconds);
        }

        /// <summary>
        /// Récupère ce que l'utilisateur à entré sous format de string
        /// </summary>
        /// <returns>Retourne l'entrée utilisateur sous format de string</returns>
        public string GetUserInputString()
        {
            string s = null;
            s = Console.ReadLine();
            return s;
        }

        /// <summary>
        /// Affiche un caractère à la position X et Y de la console
        /// </summary>
        /// <param name="x">Position X (colonne) du curseur</param>
        /// <param name="y">Position Y (colonne) du curseur</param>
        /// <param name="c">Caractère à afficher</param>
        public void ShowCarInPos(int x, int y, char c)
        {
            if (x < SCREEN_COL_MIN || x > SCREEN_COL_MAX)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{SCREEN_COL_MIN} et n°{SCREEN_COL_MAX} incluses");
            if (y < SCREEN_LINE_MIN || y > SCREEN_LINE_MAX)
                throw new IndexOutOfRangeException($"Le curseur ne peut être déplacé qu'entre les colonnes n°{SCREEN_LINE_MIN} et n°{SCREEN_LINE_MAX} incluses");
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        /// <summary>
        /// Vide la ligne d'utilisation
        /// </summary>
        /// <param name="line">Numéro de la ligne entre 1 et la ligne maximum définie</param>
        /// <param name="colMin">Numéro de la colonne de départ</param>
        /// <param name="colMax">Numéro de la colonne de fin</param>
        public void ClearLine(int line, int colMin, int colMax)
        {
            for (int i = colMin; i <= colMax; i++)
                ShowCarInPos(i, line, ' ');
        }

        #endregion

        #region Affichages

        public void AfficheCadre()
        {
            for (int i = SCREEN_LINE_MIN; i <= SCREEN_LINE_MAX; i++)
                switch (i)
                {
                    case BORDER_TITLE_LINE_TOP:
                        for (int j = SCREEN_COL_MIN; j <= SCREEN_COL_MAX; j++)
                            ShowCarInPos(j, i, (j == SCREEN_COL_MIN ? BORDER_TOP_LEFT : (j == SCREEN_COL_MAX ? BORDER_TOP_RIGHT : BORDER_HORIZONTAL))); 
                        break;
                    case BORDER_TITLE_LINE_BOTTOM:
                    case BORDER_MAIN_BOTTOM:
                    case BORDER_ENCOD_BOTTOM:
                        for (int j = SCREEN_COL_MIN; j <= SCREEN_COL_MAX; j++)
                            ShowCarInPos(j, i, (j == SCREEN_COL_MIN ? BORDER_MID_LEFT : (j == SCREEN_COL_MAX ? BORDER_MID_RIGHT : BORDER_HORIZONTAL)));
                        break;
                    case SCREEN_LINE_MAX:
                        for (int j = SCREEN_COL_MIN; j <= SCREEN_COL_MAX; j++)
                            ShowCarInPos(j, i, (j == SCREEN_COL_MIN ? BORDER_BOTTOM_LEFT : (j == SCREEN_COL_MAX ? BORDER_BOTTOM_RIGHT : BORDER_HORIZONTAL)));
                        break;
                    default:
                        ShowCarInPos(SCREEN_COL_MIN, i, BORDER_VERTICAL);
                        ShowCarInPos(SCREEN_COL_MAX, i, BORDER_VERTICAL);
                        break;
                }    
        }

        public void AfficheTitre()
        {
            string s = CentreTexte($"{TITLE} V:{VERSION}");
            for (int i = 0; i < s.Length; i++)
                ShowCarInPos(USAGE_COL_MIN + i, TITLE_LINE, s.Substring(i, 1).ToCharArray()[0]);
        }

        public string CentreTexte(string t)
        {
            string s = null;
            int l = t.Length;
            int pos = (int)((USAGE_COL_MAX - USAGE_COL_MIN - l)/2);
            for (int i = 0; i < pos; i++)
                s += " ";
            s += t;
            return s;
        }

        public void AfficherMessage(string m, EnumTypeMessage typeMessage)
        {
            if (m == null || m.Trim().Length == 0)
                AfficherMessage("Le message d'erreur ne peut pas être vide.", EnumTypeMessage.ERROR);
            if (m.Length > (USAGE_COL_MAX - USAGE_COL_MIN) * 2)
                AfficherMessage($"Le message est trop long, sa longueur maximum est de : {(USAGE_COL_MAX - USAGE_COL_MIN) * 2}", EnumTypeMessage.ERROR);
            switch (typeMessage)
            {
                case EnumTypeMessage.ERROR:
                    Console.ForegroundColor = ERROR_TEXT_COLOR;
                    Console.BackgroundColor = ERROR_BACKGROUND_COLOR;
                    break;
                case EnumTypeMessage.SUCCESS:
                    Console.ForegroundColor = SUCCESS_TEXT_COLOR;
                    Console.BackgroundColor = BACKGROUND_COLOR;
                    break;
                default:
                    Console.ForegroundColor = TEXT_COLOR;
                    Console.BackgroundColor = BACKGROUND_COLOR;
                    break;
            }
            ViderZoneMessage();
            for (int i = 0; i < USAGE_COL_MAX - USAGE_COL_MIN && i < m.Length; i++)
                ShowCarInPos(USAGE_COL_MIN + i, MESSAGE_LINE_TOP, m.Substring(i, 1).ToCharArray()[0]);
            if (m.Length > USAGE_COL_MAX - USAGE_COL_MIN)
                for (int i = USAGE_COL_MAX - USAGE_COL_MIN; i < m.Length; i++)
                    ShowCarInPos(USAGE_COL_MIN + i, MESSAGE_LINE_BOTTOM, m.Substring(i, 1).ToCharArray()[0]);
            Console.ForegroundColor = TEXT_COLOR;
            Console.BackgroundColor = BACKGROUND_COLOR;
            this.Wait(1000);
        }

        public void ViderZoneMessage()
        {
            for (int i = MESSAGE_LINE_TOP; i <= MESSAGE_LINE_BOTTOM; i++)
                ClearLine(i, USAGE_COL_MIN, USAGE_COL_MAX);
        }

        #endregion

    }
}
