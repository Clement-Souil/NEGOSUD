using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Input;
using NEGOSUDClient.Services;

namespace NEGOSUDClient;

public partial class App : Application
{
    private List<Key> _currentKeys = new List<Key>();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Lancer un écouteur global
        EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyDownEvent, new KeyEventHandler(OnKeyDown));
        EventManager.RegisterClassHandler(typeof(Window), Keyboard.KeyUpEvent, new KeyEventHandler(OnKeyUp));
    }


    private void OnKeyDown(object sender, KeyEventArgs e)
    {
        if (!_currentKeys.Contains(e.Key))
        {
            _currentKeys.Add(e.Key);
        }

        CheckAdminShortcut();
    }

    private void OnKeyUp(object sender, KeyEventArgs e)
    {
        _currentKeys.Remove(e.Key);
    }

    private void CheckAdminShortcut()
    {
        // Exemple : Ctrl + Shift + A pour passer en mode admin
        if (_currentKeys.Contains(Key.LeftCtrl) &&
            _currentKeys.Contains(Key.LeftShift) &&
            _currentKeys.Contains(Key.A))
        {
            AuthService.SwitchToAdmin();
            _currentKeys.Clear();
        }

        // Exemple : Ctrl + Shift + E pour revenir en mode employé
        if (_currentKeys.Contains(Key.LeftCtrl) &&
            _currentKeys.Contains(Key.LeftShift) &&
            _currentKeys.Contains(Key.E))
        {
            AuthService.SwitchToEmploye();
            _currentKeys.Clear();
        }
    }
}
