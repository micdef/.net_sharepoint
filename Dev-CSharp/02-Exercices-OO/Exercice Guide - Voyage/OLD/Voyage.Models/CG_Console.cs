using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Voyage.Models
{
    public static class G_Console // Classe d'affichage Console
    {
        #region Enumerates

        public enum EnumPosition
        {
            CENTER = 0,
            RIGHT = 1,
            LEFT = 2
        }

        #endregion

        #region Constants

        private const int MAX_COLUMNS = 80;
        private const int MAX_LINES = 40;
        private const ConsoleColor DEFAULT_TEXT_COLOR = ConsoleColor.Gray;
        private const ConsoleColor DEFAULT_BACKGROUND_COLOR = ConsoleColor.Black;
        private const char TOP_LEFT = '╔';
        private const char TOP_RIGHT = '╗';
        private const char MID_LEFT = '╠';
        private const char MID_RIGHT = '╣';
        private const char BOTTOM_LEFT = '╚';
        private const char BOTTOM_RIGHT = '╝';
        private const char HORIZONTAL = '═';
        private const char VERTICAL = '║';

        #endregion

        #region Static Methods

        public static void ShowTitle(string t)
        {
            DrawLine(TOP_LEFT, HORIZONTAL, TOP_RIGHT);
            ShowText(VERTICAL, t, VERTICAL, EnumPosition.CENTER);
            DrawLine(MID_LEFT, HORIZONTAL, MID_RIGHT);
        }

        public static void ShowText(char firstChar, string text, char lastChar, EnumPosition pos = EnumPosition.LEFT, ConsoleColor textColor = DEFAULT_TEXT_COLOR, ConsoleColor backgroundColor = DEFAULT_BACKGROUND_COLOR)
        {
            ColorConsole(textColor, backgroundColor);
            string s = firstChar + " ";
            if (text.Length < MAX_COLUMNS - 4)
            {
                switch (pos)
                {
                    case EnumPosition.CENTER:
                        int midText = text.Length / 2;
                        for (int i = midText; i < (MAX_COLUMNS - 2) / 2; i++)
                            s += ' ';
                        s += text;
                        break;
                    case EnumPosition.RIGHT:
                        for (int i = text.Length; i < MAX_COLUMNS - 4; i++)
                            s += ' ';
                        s += text;
                        break;
                    default:
                        s += text;
                        break;
                }
            }
            else if (text.Length == MAX_COLUMNS - 4)
                s += text;
            else
            {

            }
            if (s.Length < MAX_COLUMNS - 1)
                for (int i = s.Length; i < MAX_COLUMNS - 1; i++)
                    s += ' ';
            s += lastChar;
            Console.WriteLine(s);
            ColorConsole(default, default);
        }

        public static void ColorConsole(ConsoleColor foregroundColor = DEFAULT_TEXT_COLOR, ConsoleColor backgroundColor = DEFAULT_BACKGROUND_COLOR)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
        }

        public static void DrawLine(char firstChar, char mainChar, char lastChar, ConsoleColor textColor = DEFAULT_TEXT_COLOR, ConsoleColor backgroundColor = DEFAULT_BACKGROUND_COLOR)
        {
            ColorConsole(textColor, backgroundColor);
            string s = "";
            for (int i = 0; i < MAX_COLUMNS; i++)
            {
                if (i == 0)
                    s += firstChar;
                else if (i == MAX_COLUMNS - 1)
                    s += lastChar;
                else
                    s += mainChar;
            }
            Console.WriteLine(s);
            ColorConsole(default, default);
        }

        /// <summary>
        ///     Affiche un texte
        /// </summary>
        /// <param name="text">Text à afficher</param>
        public static void ShowText(string text)
        {
            Console.WriteLine(text);
        }

        /// <summary>
        ///     Vide le contenu de la console
        /// </summary>
        public static void ClearConsole()
        {
            Console.Clear();
        }

        /// <summary>
        /// Affiche une liste d'élements sous format de pagination.
        /// La liste des éléments commence à 1 et est au maximum de 9
        /// </summary>
        /// <param name="elementsType">Le type d'élément sous format texte</param>
        /// <param name="elements">La liste des éléments</param>
        /// <param name="page">La page courante</param>
        /// <param name="nbElemByPage">Le nombre d'éléments affichés par page. Entre 1 et 9</param>
        /// <param name="exitWithoutChoice">Y-a-t-il un choix pour sortir sans faire de choix?</param>
        /// <returns></returns>
        public static char ShowPaginatedText(string elementsType, List<String> elements, int page, int nbElemByPage, bool exitWithoutChoice = false)
        {
            try
            {
                if (nbElemByPage > 9) throw new Exception("Le nombre d'élement dépasse la limite autorisée (Max : 9).");
                if (nbElemByPage < 1) throw new Exception("Le nombre d'élement ne peut pas être plus petit que 1");
                Console.WriteLine(
                    $"Liste des {elementsType}. Page n°{page}/{(elements.Count % nbElemByPage == 0 ? elements.Count % nbElemByPage : (elements.Count / nbElemByPage) + 1)}");
                DrawLine('-');
                Console.WriteLine();
                for (int i = 1; i <= nbElemByPage && (i * page) < elements.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {elements[(page * i) - 1]}");
                }

                Console.WriteLine();
                DrawLine('=');
                Console.WriteLine();
                if (page > 1) Console.WriteLine("[P] Page précédente");
                if (page < (elements.Count % nbElemByPage == 0
                    ? elements.Count / nbElemByPage
                    : (elements.Count / nbElemByPage) + 1)) Console.WriteLine("[S] Page suivante");
                if (exitWithoutChoice) Console.WriteLine("[Q] Quitter sans choisir");
                Console.WriteLine();
                DrawLine('=');
                Console.WriteLine();
                Console.Write("Votre choix : ");
                return Console.ReadKey().KeyChar;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        /// <summary>
        /// Affiche une ligne de nombre maximum de caractère contenu dans la constante MAX_COLUMNS et qui a
        /// pour caractère, le caractère passé dans les paramètres
        /// </summary>
        /// <param name="underlineChar">Le cacactère à afficher pour la ligne</param>
        public static void DrawLine(char underlineChar)
        {
            for (int i = 0; i < MAX_COLUMNS; i++)
                Console.Write(underlineChar);
            Console.WriteLine();
        }

        /// <summary>
        /// Affiche une erreur de choix.
        /// </summary>
        public static void DrawError(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ReadKey();
            Console.ForegroundColor = default;
            Console.BackgroundColor = default;
        }

        #endregion


    }
}
