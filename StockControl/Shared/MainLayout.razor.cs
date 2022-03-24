using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace StockControl.Shared
{
    public partial class MainLayout
    {
        [Inject] public ILocalStorageService _localStorageService { get; set; }

        bool _drawerOpen = true;
        public bool AlarmOn { get; set; }
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected override async Task OnInitializedAsync()
        {
            if (await _localStorageService.ContainKeyAsync("theme"))
            {
                _themeName = await _localStorageService.GetItemAsStringAsync("theme");
            }
            else _themeName = "light";

            _currentTheme = _themeName == "light" ? _lightTheme : _darkTheme;
            AlarmOn = _themeName != "light";

        }



        private string _themeName = "light";

        private MudTheme? _currentTheme = null;

        private MudTheme _darkTheme = new MudTheme()
        {
            Palette = new Palette()
            {
                AppbarBackground = "#0097FF",
                AppbarText = "#FFFFFF",
                Primary = "#007CD1",
                TextPrimary = "#FFFFFF",
                Background = "#001524",
                TextSecondary = "#E2EEF6",
                DrawerBackground = "#093958",
                DrawerText = "#FFFFFF",
                Surface = "#093958",
                ActionDefault = "#0C1217",
                ActionDisabled = "#2F678C",
                TextDisabled = "#B0B0B0",
                Tertiary = "#265336"
            }
        };

        private MudTheme _lightTheme = new MudTheme()
        {
            Palette = new Palette()
            {
                AppbarBackground = "#0097FF",
                AppbarText = "#FFFFFF",
                Primary = "#007CD1",
                TextPrimary = "#080808",
                Background = "#F4FDFF",
                TextSecondary = "#0C1217",
                DrawerBackground = "#C5E5FF",
                DrawerText = "#0C1217",
                Surface = "#E4FAFF",
                ActionDefault = "#0C1217",
                ActionDisabled = "#2F678C",
                TextDisabled = "#686767",
            }
        };

        //private async Task ChangeThemeAsync()
        //{

        //    if (_themeName == "light")
        //    {
        //        _currentTheme = _darkTheme;
        //        _themeName = "dark";
        //        AlarmOn = false;
        //    }
        //    else
        //    {
        //        _currentTheme = _lightTheme;
        //        _themeName = "light";
        //        AlarmOn = true;
        //    }

        //    await _localStorageService.SetItemAsStringAsync("theme", _themeName);
        //}

        public async Task OnToggledChanged(bool toggled)
        {
            // Because variable is not two-way bound, we need to update it ourself
            AlarmOn = toggled;

            if (AlarmOn)
            {
                _currentTheme = _darkTheme;
                _themeName = "dark";

            }
            else
            {
                _currentTheme = _lightTheme;
                _themeName = "light";
            }

            await _localStorageService.SetItemAsStringAsync("theme", _themeName);
        }

    }
}