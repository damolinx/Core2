using Core2.Commands;
using Core2.Commands.Menu;
using System;
using System.IO;

namespace Core2.Sample3.Menus
{
    public class DriveMenu : PageMenuCommand
    {
        public DriveMenu(string title, DriveInfo drive)
            : base(title)
        {
            this.Drive = drive ?? throw new ArgumentNullException(nameof(drive));
        }

        public DriveInfo Drive { get; }

        protected override void RenderHeader(CommandContext context)
        {
            base.RenderHeader(context);
            Console.WriteLine("  Type:           {0}", this.Drive.DriveType);
            Console.WriteLine("  Format:         {0}", this.Drive.IsReady ? this.Drive.DriveFormat : string.Empty);
            Console.WriteLine("  User-available: {0} bytes", this.Drive.IsReady ? this.Drive.AvailableFreeSpace.ToString() : "--");
            Console.WriteLine("  Free space:     {0} bytes", this.Drive.IsReady ? this.Drive.TotalFreeSpace.ToString() : "--");
            Console.WriteLine("  Capacity:       {0} bytes", this.Drive.IsReady ? this.Drive.TotalSize.ToString() : "--");
            Console.WriteLine();
        }
    }
}
