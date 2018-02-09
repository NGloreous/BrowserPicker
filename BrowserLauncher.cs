namespace BrowserPicker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;
    using BrowserPicker.Settings;

    public class BrowserLauncher
    {
        public BrowserLauncher(IEnumerable<string> args)
        {
            this.Arguments = BrowserPickerArguments.Parse(args);
        }

        private BrowserPickerArguments Arguments { get; }

        public void Execute()
        {
            switch (this.Arguments.Mode)
            {
                case Mode.OpenUri:
                    {
                        var settings = BrowserPickerSettings.Load();
                        BrowserInfo browser = settings.GetBrowser(this.Arguments.Uri);
                        Launch(browser.Path, $"{browser.Arguments} {this.Arguments.Uri.AbsoluteUri}");
                        break;
                    }
                case Mode.OpenSettings:
                    {
                        Launch(null, BrowserPickerSettings.DefaultSettingFileName);
                        break;
                    }
            }
        }

        private static void Launch(string program, string arguments)
        {
            using (string.IsNullOrEmpty(program) ? Process.Start(arguments) : Process.Start(program, arguments))
            {
            }
        }

        private enum Mode
        {
            None = 0,
            OpenUri,
            OpenSettings,
        }

        private class BrowserPickerArguments
        {
            private BrowserPickerArguments()
            {
                this.Mode = Mode.OpenUri;
            }

            public Mode Mode { get; private set; }

            public Uri Uri { get; private set; }

            public static BrowserPickerArguments Parse(IEnumerable<string> args)
            {
                var arguments = new BrowserPickerArguments();
                foreach (var arg in args.Skip(1))
                {
                    switch (arg.ToLowerInvariant())
                    {
                        case "/config":
                        case "/c":
                            arguments.Mode = Mode.OpenSettings;
                            break;
                        default:
                            arguments.Uri = new Uri(arg);
                            break;
                    }
                }

                return arguments;
            }
        }
    }
}
