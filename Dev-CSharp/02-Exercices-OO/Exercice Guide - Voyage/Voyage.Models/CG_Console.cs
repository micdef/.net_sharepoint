using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

        private const int MAX_COLUMNS = 80;

        #region Static Methods

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
