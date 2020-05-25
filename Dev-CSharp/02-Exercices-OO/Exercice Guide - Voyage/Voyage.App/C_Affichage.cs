using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Voyage.App
{
    public class Affichage : A_Console
    {
        #region Constants

        // Application Size
        private const int SCREEN_XSTART = 1;
        private const int SCREEN_XSTOP = 100;
        private const int SCREEN_YSTART = 1;
        private const int SCREEN_YSTOP = 40;

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
        private const int BORDER_TITLE_Y = SCREEN_YSTART + 2;
        private const int BORDER_MAIN_Y = SCREEN_YSTOP - 5;
        private const int BORDER_ENCOD_Y = SCREEN_YSTOP - 3;

        // Application Screen Using
        private const int TITLE_YSTART = SCREEN_YSTART + 1;
        private const int TITLE_YSTOP = SCREEN_YSTART + 1;
        private const int MAIN_YSTART = SCREEN_YSTART + 3;
        private const int MAIN_YSTOP = SCREEN_YSTOP - 6;
        private const int INPUT_YSTART = SCREEN_YSTOP - 4;
        private const int INPUT_YSTOP = SCREEN_YSTOP - 4;
        private const int MESSAGE_YSTART = SCREEN_YSTOP - 2;
        private const int MESSAGE_YSTOP = SCREEN_YSTOP - 1;
        private const int USAGE_XSTART = SCREEN_XSTART + 2;
        private const int USAGE_XSTOP = SCREEN_XSTOP - 2;

        #endregion

        #region Fields

        private readonly Dictionary<string, Dictionary<string, ConsoleColor>> _messageTypeConfig;
        private readonly List<string> _messageType;
        private readonly int _midXScreen;
        private static Affichage _instance;

        #endregion

        #region Enumerators

        public enum EnumDateOrder
        {
            JOUR_MOIS_ANNEE = 1,
            MOIS_JOUR_ANNEE = 2,
            ANNEE_MOIS_JOUR = 3,
            ANNEE_JOUR_MOIS = 4
        }

        public enum EnumZoneAffichage
        {
            TITLE = 1,
            MAIN = 2,
            INPUT = 3,
            MESSAGE = 4,
        }

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

        public enum EnumReturnType
        {
            BOOL = 0,
            BYTE = 1,
            CHAR = 2,
            DATETIME = 3,
            DECIMAL = 4,
            DOUBLE = 5,
            FLOAT = 6,
            INT = 7,
            LONG = 8,
            SBYTE = 9,
            SHORT = 10,
            STRING = 11,
            UINT = 12,
            ULONG = 13,
            USHORT = 14
        }

        #endregion

        #region Constructors

        private Affichage() : base(SCREEN_XSTOP, SCREEN_YSTOP)
        {
            _messageTypeConfig = new Dictionary<string, Dictionary<string, ConsoleColor>>();
            _messageType = new List<string>();
            _midXScreen = (SCREEN_XSTOP - SCREEN_XSTART) / 2;
            RemplirListeTypeMessage();
            RemplirListeConfigurationTypeMessage();
        }

        public static Affichage GetInstance()
        {
            if (_instance == null)
                _instance = new Affichage();
            return _instance;
        }

        #endregion

        #region Private Methods

        #region Messages Types

        private void RemplirListeTypeMessage()
        {
            _messageType.Add("default");
            _messageType.Add("error");
            _messageType.Add("success");
        }

        private void RemplirListeConfigurationTypeMessage()
        {
            foreach (string tm in _messageType)
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

        #endregion

        #region Text

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

        #region Clear Zone

        private void ViderZone(EnumZoneAffichage zone)
        {
            switch (zone)
            {
                case EnumZoneAffichage.TITLE:
                    for (int y = TITLE_YSTART; y <= TITLE_YSTOP; y++)
                        base.ClearLine(y, USAGE_XSTART, USAGE_XSTOP);
                    break;
                case EnumZoneAffichage.MAIN:
                    for (int y = MAIN_YSTART; y <= MAIN_YSTOP; y++)
                        base.ClearLine(y, USAGE_XSTART, USAGE_XSTOP);
                    break;
                case EnumZoneAffichage.INPUT:
                    for (int y = INPUT_YSTART; y <= INPUT_YSTOP; y++)
                        base.ClearLine(y, USAGE_XSTART, USAGE_XSTOP);
                    break;
                case EnumZoneAffichage.MESSAGE:
                    for (int y = MESSAGE_YSTART; y <= MESSAGE_YSTOP; y++)
                        base.ClearLine(y, USAGE_XSTART, USAGE_XSTOP);
                    break;
                default:
                    AfficherZoneMessage("La zone choisie n'existe pas.", -1, EnumMessageTypes.ERROR);
                    break;
            }
        }

        #endregion

        #region Write In Zone

        private void AfficherZonePrincipale(string texte, int ligne = -1, int colonneStart = -1)
        {
            if (texte == null || texte.Trim().Length == 0)
                throw new ArgumentNullException("L'objet passé ne peut pas être vide.");
            if ((ligne < 0 && ligne != -1) || ligne + MAIN_YSTART > MAIN_YSTOP)
                throw new IndexOutOfRangeException($"L'index doit être entr 0 et {MAIN_YSTOP - MAIN_YSTART + 1}.");
            base.WriteLine((colonneStart == -1 ? USAGE_XSTART : USAGE_XSTART + colonneStart), USAGE_XSTOP, MAIN_YSTART + (ligne == -1 ? 0 : ligne), (ligne == -1 ? MAIN_YSTOP : MAIN_YSTART + ligne), texte);
        }

        private void AfficherZoneTitre(string texte, int ligne = -1)
        {
            if (texte == null || texte.Trim().Length == 0)
                throw new ArgumentNullException("L'objet passé ne peut pas être vide.");
            if ((ligne < 0 && ligne != -1) || ligne + TITLE_YSTART > TITLE_YSTOP)
                throw new IndexOutOfRangeException($"L'index doit être entr 0 et {TITLE_YSTOP - TITLE_YSTART + 1}.");
            base.WriteLine(USAGE_XSTART, USAGE_XSTOP, TITLE_YSTART + (ligne == -1 ? 0 : ligne), (ligne == -1 ? TITLE_YSTOP : TITLE_YSTART + ligne), texte);
        }

        private void AfficherZoneEncodage(string texte, int ligne = -1)
        {
            if (texte == null || texte.Trim().Length == 0)
                throw new ArgumentNullException("L'objet passé ne peut pas être vide.");
            if ((ligne < 0 && ligne != -1) || ligne + INPUT_YSTART > INPUT_YSTOP)
                throw new IndexOutOfRangeException($"L'index doit être entr 0 et {INPUT_YSTOP - INPUT_YSTART + 1}.");
            base.WriteLine(USAGE_XSTART, USAGE_XSTOP, INPUT_YSTART + (ligne == -1 ? 0 : ligne), (ligne == -1 ? INPUT_YSTOP : INPUT_YSTART + ligne), texte);
        }

        private void AfficherZoneMessage(string texte, int ligne = -1, EnumMessageTypes messageType = EnumMessageTypes.DEFAULT)
        {
            if (texte == null || texte.Trim().Length == 0)
                throw new ArgumentNullException("L'objet passé ne peut pas être vide.");
            if ((ligne < 0 && ligne != -1) || ligne + MESSAGE_YSTART > MESSAGE_YSTOP)
                throw new IndexOutOfRangeException($"L'index doit être entr 0 et {MESSAGE_YSTOP - MESSAGE_YSTART + 1}.");
            ViderZone(EnumZoneAffichage.MESSAGE);
            Dictionary<string, ConsoleColor> consoleDef = _messageTypeConfig[messageType.ToString().ToLower()];
            base.TextColor = consoleDef["text"];
            base.BackgroundColor = consoleDef["background"];
            base.WriteLine(USAGE_XSTART, USAGE_XSTOP, MESSAGE_YSTART + (ligne == -1 ? 0 : ligne), (ligne == -1 ? MESSAGE_YSTOP : MESSAGE_YSTART + ligne), texte);
            base.ResetDefaultColors();
            base.Wait(2000);
        }

        #endregion

        #region Asking User

        private void DemandeUtilisateur(EnumReturnType returnType, string messageEncodage, out object varOut, bool viderZoneMessageUtilisateur = false, string messageUtilisateur = null,
            int messageUtilisateurY = -1, int messageEncodageY = -1, char separateurDate = '/', EnumDateOrder ordreDate = EnumDateOrder.JOUR_MOIS_ANNEE)
        {
            bool ok;
            char c;
            int posX;
            try
            {
                varOut = null;
                if (viderZoneMessageUtilisateur) ViderZone(EnumZoneAffichage.MAIN);
                if (messageUtilisateur != null) AfficherZoneMessage(messageUtilisateur, messageUtilisateurY);
                switch (returnType)
                {
                    case EnumReturnType.BOOL:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            messageEncodage = "[O]ui / [N]on -->";
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            c = base.GetUserInputChar();
                            ok = (c == 'o' || c == 'O' || c == 'n' || c == 'N' ? true : false);
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                            else
                                varOut = (c == 'o' || c == 'O');
                        } while (!ok);
                        break;
                    case EnumReturnType.BYTE:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            byte val;
                            ok = byte.TryParse(base.GetUserInputString(), out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    case EnumReturnType.CHAR:
                        ViderZone(EnumZoneAffichage.INPUT);
                        posX = USAGE_XSTART + messageEncodage.Length;
                        AfficherZoneEncodage(messageEncodage, messageEncodageY);
                        base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                        c = base.GetUserInputChar();
                        varOut = c;
                        break;
                    case EnumReturnType.DATETIME:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            DateTime val;
                            ok = VerificationDate(base.GetUserInputString(), ordreDate, separateurDate, out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    case EnumReturnType.DECIMAL:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            decimal val;
                            ok = decimal.TryParse(base.GetUserInputString(), out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    case EnumReturnType.DOUBLE:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            double val;
                            ok = double.TryParse(base.GetUserInputString(), out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    case EnumReturnType.FLOAT:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            float val;
                            ok = float.TryParse(base.GetUserInputString(), out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    case EnumReturnType.INT:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            int val;
                            ok = int.TryParse(base.GetUserInputString(), out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    case EnumReturnType.LONG:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            long val;
                            ok = long.TryParse(base.GetUserInputString(), out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    case EnumReturnType.SBYTE:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            sbyte val;
                            ok = sbyte.TryParse(base.GetUserInputString(), out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    case EnumReturnType.SHORT:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            short val;
                            ok = short.TryParse(base.GetUserInputString(), out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    case EnumReturnType.STRING:
                        ViderZone(EnumZoneAffichage.INPUT);
                        posX = USAGE_XSTART + messageEncodage.Length;
                        AfficherZoneEncodage(messageEncodage, messageEncodageY);
                        base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                        string s = base.GetUserInputString();
                        varOut = s;
                        break;
                    case EnumReturnType.UINT:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            uint val;
                            ok = uint.TryParse(base.GetUserInputString(), out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    case EnumReturnType.ULONG:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            ulong val;
                            ok = ulong.TryParse(base.GetUserInputString(), out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    case EnumReturnType.USHORT:
                        do
                        {
                            ViderZone(EnumZoneAffichage.INPUT);
                            posX = USAGE_XSTART + messageEncodage.Length;
                            AfficherZoneEncodage(messageEncodage, messageEncodageY);
                            base.SetCursorXY(posX, (messageEncodageY == -1 ? INPUT_YSTART : INPUT_YSTART + messageEncodageY));
                            ushort val;
                            ok = ushort.TryParse(base.GetUserInputString(), out val);
                            varOut = val;
                            if (!ok)
                                AfficherZoneMessage("Ce que vous avez entré n'est pas valide. Veuillez recommencer svp.", default, EnumMessageTypes.ERROR);
                        } while (!ok);
                        break;
                    default:
                        throw new MissingMemberException("La type choisi n'existe pas dans les types définis.");
                }
            }
            catch (Exception e)
            {
                varOut = null;
                throw e;
            }
        }

        #endregion

        #region Entry Verifications
        private static bool VerificationDate(string sDate, EnumDateOrder ordre, char separateur, out DateTime date)
        {
            try
            {
                string[] s = sDate.Split(separateur);
                if (s.Length != 3)
                    throw new Exception();
                switch (ordre)
                {
                    case EnumDateOrder.ANNEE_JOUR_MOIS:
                        if (s[0].Length != 4 || s[1].Length != 2 || s[2].Length != 2)
                            throw new Exception();
                        date = new DateTime(int.Parse(s[0]), int.Parse(s[2]), int.Parse(s[1]));
                        return true;
                    case EnumDateOrder.ANNEE_MOIS_JOUR:
                        if (s[0].Length != 4 || s[1].Length != 2 || s[2].Length != 2)
                            throw new Exception();
                        date = new DateTime(int.Parse(s[0]), int.Parse(s[1]), int.Parse(s[2]));
                        return true;
                    case EnumDateOrder.JOUR_MOIS_ANNEE:
                        if (s[0].Length != 2 || s[1].Length != 2 || s[2].Length != 4)
                            throw new Exception();
                        date = new DateTime(int.Parse(s[2]), int.Parse(s[1]), int.Parse(s[0]));
                        return true;
                    case EnumDateOrder.MOIS_JOUR_ANNEE:
                        if (s[0].Length != 2 || s[1].Length != 2 || s[2].Length != 4)
                            throw new Exception();
                        date = new DateTime(int.Parse(s[2]), int.Parse(s[0]), int.Parse(s[1]));
                        return true;
                    default:
                        throw new Exception();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #endregion

        #region Public Methods

        #region Asking User

        public bool RecupBool(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.BOOL, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            bool val = (bool)o;
            return val;
        }

        public byte RecupByte(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.BYTE, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            byte val = (byte)o;
            return val;
        }

        public char RecupChar(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.CHAR, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            char val = (char)o;
            return val;
        }

        public DateTime RecupDateTime(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1, char separateurDate = '/', EnumDateOrder ordreDate = EnumDateOrder.JOUR_MOIS_ANNEE)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.DATETIME, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur, default, separateurDate, ordreDate);
            DateTime val = (DateTime)o;
            return val;
        }

        public decimal RecupDecimal(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.DECIMAL, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            decimal val = (decimal)o;
            return val;
        }

        public double RecupDouble(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.DOUBLE, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            double val = (double)o;
            return val;
        }

        public float RecupFloat(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.FLOAT, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            float val = (float)o;
            return val;
        }

        public int RecupInt(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.INT, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            int val = (int)o;
            return val;
        }

        public long RecupLong(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.LONG, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            long val = (long)o;
            return val;
        }

        public sbyte RecupSByte(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.SBYTE, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            sbyte val = (sbyte)o;
            return val;
        }

        public short RecupShort(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.SHORT, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            short val = (short)o;
            return val;
        }

        public string RecupString(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.STRING, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            string val = (string)o;
            return val;
        }

        public uint RecupUInt(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.UINT, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            uint val = (uint)o;
            return val;
        }

        public ulong RecupULong(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.ULONG, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            ulong val = (ulong)o;
            return val;
        }

        public ushort RecupUShort(string messageEncodage = null, bool viderZonePrincipale = false, string messageUtilisateur = null, int posYMessageUtilisateur = -1)
        {
            object o;
            DemandeUtilisateur(EnumReturnType.USHORT, (messageEncodage == null ? "--> " : messageEncodage), out o, viderZonePrincipale, messageUtilisateur, posYMessageUtilisateur);
            ushort val = (ushort)o;
            return val;
        }

        #endregion

        #region Clear Zone
        
        public void ViderZoneTitre()
        {
            ViderZone(EnumZoneAffichage.TITLE);
        }

        public void ViderZonePrincipale()
        {
            ViderZone(EnumZoneAffichage.MAIN);
        }

        public void ViderZoneEncodage()
        {
            ViderZone(EnumZoneAffichage.INPUT);
        }

        public void ViderZoneMessage()
        {
            ViderZone(EnumZoneAffichage.MESSAGE);
        }

        #endregion

        #region Menu

        public void AffichMenuPrincipal(int page = 1)
        {
            // Vider les zones d'affichage
            ViderZone(EnumZoneAffichage.MAIN);
            ViderZone(EnumZoneAffichage.INPUT);
            ViderZone(EnumZoneAffichage.MESSAGE);

            // Mise en place des éléments du menu
            string titre = "Menu Principal";
            List<string> menu = new List<string>();
            menu.Add("Voyage");
            menu.Add("Participant");

            // Affichage du menu
            AfficherChoixPagine(titre, menu, 10, page, true);
        }

        public void AffichMenuVoyage(int page = 1)
        {
            // Vider les zones d'affichage
            ViderZone(EnumZoneAffichage.MAIN);
            ViderZone(EnumZoneAffichage.INPUT);
            ViderZone(EnumZoneAffichage.MESSAGE);

            // Mise en place des éléments du menu
            string titre = "Menu Voyage";
            List<string> menu = new List<string>();
            menu.Add("Créer un voyage");
            menu.Add("Modifier les information du voyage");
            menu.Add("Consulter les information du voyage");

            // Affichage du menu
            AfficherChoixPagine(titre, menu, 10, page, true);
        }

        public void AffichMenuVoyageConsultation(int page = 1)
        {
            // Vider les zones d'affichage
            ViderZone(EnumZoneAffichage.MAIN);
            ViderZone(EnumZoneAffichage.INPUT);
            ViderZone(EnumZoneAffichage.MESSAGE);

            // Mise en place des éléments du menu
            string titre = "Menu Voyage - Consultation";
            List<string> menu = new List<string>();
            menu.Add("Voir les informations du voyage");
            menu.Add("Voir/Calculer la réduction organisateur");
            menu.Add("Voir les carnets choisis");
            menu.Add("Voir les films choisis");

            // Affichage du menu
            AfficherChoixPagine(titre, menu, 10, page, true);
        }

        #endregion

        #region Show Other In GUI

        public void AfficherChoixPagine(string titre, List<string> elements, int nbElemParPage = 10, int page = 1, bool optionQuitter = false)
        {
            // Vider les zones d'affichage
            ViderZone(EnumZoneAffichage.MAIN);

            // Affiche le titre et le souligne
            string underline = null;
            foreach (char c in titre)
                underline += "-";
            base.TextColor = ConsoleColor.DarkYellow;
            AfficherZonePrincipale(CentrerTexte(titre), 0);
            AfficherZonePrincipale(CentrerTexte(underline), 1);
            base.ResetDefaultColors();

            // Afficher les élements
            int y = 3;
            for (int i = 0; i < nbElemParPage && i < elements.Count; i++, y++)
                AfficherZonePrincipale($"[{i}] {elements[((page - 1) * nbElemParPage) + i]}", y);

            // Afficher les options supplémentaires
            y += 2;
            int nbPagesMax = (elements.Count / nbElemParPage) + (elements.Count % nbElemParPage == 0 ? 0 : 1);
            if (page > 1) AfficherZonePrincipale("[P] Page précédente", y++);
            if (page < nbPagesMax) AfficherZonePrincipale("[S] Page suivante", y++);
            if (optionQuitter)
            {
                base.TextColor = ConsoleColor.Red;
                AfficherZonePrincipale("[Q] Quitter sans faire de sélection", y++);
                base.ResetDefaultColors();
            }
        }

        public void AfficherErreur(string message, int ligne = -1)
        {
            AfficherZoneMessage(message, ligne, EnumMessageTypes.ERROR);
        }

        public void AfficherMessage(string message, int ligne = -1)
        {
            AfficherZoneMessage(message, ligne, EnumMessageTypes.DEFAULT);
        }

        public void AfficherReussite(string message, int ligne = -1)
        {
            AfficherZoneMessage(message, ligne, EnumMessageTypes.SUCCESS);
        }

        public void AfficherInformations(string message, int ligne = -1, int colonneStart = -1)
        {
            AfficherZonePrincipale(message, ligne, colonneStart);
        }

        public void AfficherReponseCouleur(string s, int ligne, int colonneStart, ConsoleColor color)
        {
            base.TextColor = color;
            AfficherInformations(s, ligne, colonneStart);
            base.ResetDefaultColors();
        }

        public void AfficherEcranFin()
        {
            ViderZonePrincipale();
            ViderZoneEncodage();
            ViderZoneMessage();
            AfficherZonePrincipale("Merci d'avoir utilisé notre application.", 0);
            AfficherZonePrincipale("Au plaisir de vous revoir.", 2);
            AfficherZonePrincipale("Appuyez sur une touche pour continuer...", 4);
            base.Wait(0);
        }

        public void AfficherEcranSplash(string titre, string version)
        {
            int midY = (MAIN_YSTOP - MAIN_YSTART + 1) / 2;
            base.TextColor = ConsoleColor.Green;
            AfficherZonePrincipale(CentrerTexte($"{titre}".ToUpper()), midY - 1);
            AfficherZonePrincipale(CentrerTexte($"Version : {version}".ToUpper()), midY + 1);
            base.ResetDefaultColors();
        }

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

        public void PreparerEcran(string titre, string version)
        {
            base.Clear();
            AfficherCadre();
            AfficherEcranSplash(titre, version);
            base.Wait(5000);
            ViderZonePrincipale();
            ViderZoneEncodage();
            ViderZoneMessage();
            base.Title = $"{titre} V:{version}";
            base.TextColor = ConsoleColor.Green;
            AfficherZoneTitre(CentrerTexte($"{titre} V:{version}"));
            base.ResetDefaultColors();
        }

        #endregion

        #endregion

    }
}
