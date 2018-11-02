using Core2.Commands;
using Core2.Commands.Menu;
using System;
using System.Collections.Generic;
using System.IO;

namespace Core2.Sample3.Menus
{
    public class DrivesMenu : PageMenuCommand
    {
        public DrivesMenu(string backLabel = "Exit")
            : base("Disk Drives", backLabel)
        {
        }

        protected override IReadOnlyList<MenuEntry> GetMenuEntries(CommandContext context)
        {
            var menuEntries = new List<MenuEntry>(base.GetMenuEntries(context));
            var baseKey = ConsoleKey.D1;

            foreach (var drive in DriveInfo.GetDrives())
            {
                var text = (drive.IsReady && !string.IsNullOrWhiteSpace(drive.VolumeLabel)) ? drive.VolumeLabel : drive.DriveType.ToString();
                var title = $"{text} ({drive.Name})";
                var menuEntry = new MenuEntry
                {
                    CommandFactory = () => new DriveMenu(title, drive),
                    Key = baseKey++,
                    Text = title
                };

                menuEntries.Add(menuEntry);
            }

            return menuEntries;
        }
    }
}
