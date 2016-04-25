using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using MahApps.Metro;
using ourseWorkDB_DudasVI.MVVM.ViewModels;

namespace CourseWorkDB_DudasVI.MVVM.ViewModels
{
    public class SettingsViewModel : ViewModelBaseInside
    {
        #region Constructor

        public SettingsViewModel()
        {
            AccentColorlist = AppearanceManager.GetAccentNames();
            ThemeColorlist = AppearanceManager.GetThemeNames();
            SelectedAccent = AppearanceManager.GetApplicationAccent();
            SelectedTheme = AppearanceManager.GetApplicationTheme();
        }

        #endregion

        #region Commands

        public ICommand ChangePasswordCommand { get; private set; }

        #endregion

        #region Public Methods

        public void SetDefaultSettings(string themeName, string accentName)
        {
            SelectedTheme = themeName;
            SelectedAccent = accentName;
        }

        #endregion

        public class AppearanceManager
        {
            private static readonly string LightThemeText;
            private static readonly string DarkThemeText;

            #region Constructors

            static AppearanceManager()
            {
                LightThemeText = "BaseLight";
                DarkThemeText = "BaseDark";
            }

            #endregion

            #region Public Static Methods

            public static List<string> GetAccentNames()
            {
                var list = ThemeManager.Accents.ToList().Select(a => a.Name).ToList();
                return list;
            }

            public static List<string> GetThemeNames()
            {
                var res = new List<string> {LightThemeText, DarkThemeText};
                return res;
            }

            public static string GetApplicationAccent()
            {
                var theme = ThemeManager.DetectAppStyle(Application.Current);
                if (theme != null)
                    return theme.Item2.Name;
                return "Blue";
            }

            public static string GetApplicationTheme()
            {
                var theme = ThemeManager.DetectAppStyle(Application.Current);

                if (theme != null)
                    return theme.Item1.Name;
                return "BaseLight";
            }

            public void ChangeAccent(string SelectedTheme, string accentName)
            {
                var accent = ThemeManager.Accents.First(x => x.Name == accentName);
                ThemeManager.ChangeAppStyle(Application.Current, accent,
                    ThemeManager.GetAppTheme(SelectedTheme ?? GetApplicationTheme()));
            }

            public void ChangeTheme(string SelectedTheme, string SelectedAccent)
            {
                if (string.CompareOrdinal(LightThemeText, SelectedTheme) == 0)
                {
                    ThemeManager.ChangeAppStyle(Application.Current,
                        ThemeManager.GetAccent(SelectedAccent ?? GetApplicationAccent()),
                        ThemeManager.GetAppTheme(SelectedTheme));
                }
                else if (string.CompareOrdinal(DarkThemeText, SelectedTheme) == 0)
                {
                    ThemeManager.ChangeAppStyle(Application.Current,
                        ThemeManager.GetAccent(SelectedAccent ?? GetApplicationAccent()),
                        ThemeManager.GetAppTheme(SelectedTheme ?? GetApplicationTheme()));
                }
            }

            #endregion
        }

        #region Public properties

        public IList<string> AccentColorlist { get; private set; }
        public IList<string> ThemeColorlist { get; private set; }
        private readonly AppearanceManager _appearanceManager = new AppearanceManager();

        public string SelectedAccent
        {
            get { return _selectedAccent; }
            set
            {
                if (value == _selectedAccent)
                    return;
                _selectedAccent = value;
                OnPropertyChanged("SelectedAccent");
                AccentChangeRequested();
            }
        }

        public string SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                if (value == _selectedTheme)
                    return;
                _selectedTheme = value;
                OnPropertyChanged("SelectedTheme");
                ThemeChangeRequested();
            }
        }

        public string OldPassword
        {
            get { return _oldPassword; }
            set
            {
                if (value == _oldPassword)
                    return;
                _oldPassword = value;
                OnPropertyChanged("OldPassword");
            }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                if (value == _newPassword)
                    return;
                _newPassword = value;
                OnPropertyChanged("NewPassword");
                OnPropertyChanged("ReenterNewPassword");
            }
        }

        public string ReenterNewPassword
        {
            get { return _reenterNewPassword; }
            set
            {
                if (value == _reenterNewPassword)
                    return;
                _reenterNewPassword = value;
                OnPropertyChanged("ReenterNewPassword");
            }
        }

        #endregion

        #region Private Helpers

        private void ThemeChangeRequested()
        {
            _appearanceManager.ChangeTheme(SelectedTheme, SelectedAccent);
            //PromptUserToSaveAppearance();
        }

        private void AccentChangeRequested()
        {
            _appearanceManager.ChangeAccent(SelectedTheme, SelectedAccent);
            // PromptUserToSaveAppearance();
        }

        private void PromptUserToSaveAppearance()
        {
        }

        private void ChangePassword()
        {
        }

        #endregion

        #region Member Variables

        private string _selectedAccent;
        private string _selectedTheme;
        private string _oldPassword;
        private string _newPassword;
        private string _reenterNewPassword;

        #endregion
    }
}