﻿using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using GitHub.Commands;
using GitHub.Exports;
using GitHub.Services;

namespace GitHub.VisualStudio.Commands
{
    /// <summary>
    /// Opens the currently selected text on GitHub.com or an Enterprise instance.
    /// </summary>
    [Export(typeof(IOpenLinkCommand))]
    public class OpenLinkCommand : LinkCommandBase, IOpenLinkCommand
    {
        [ImportingConstructor]
        protected OpenLinkCommand(IGitHubServiceProvider serviceProvider)
            : base(CommandSet, CommandId, serviceProvider)
        {
        }

        /// <summary>
        /// Gets the GUID of the group the command belongs to.
        /// </summary>
        public static readonly Guid CommandSet = Guids.guidContextMenuSet;

        /// <summary>
        /// Gets the numeric identifier of the command.
        /// </summary>
        public const int CommandId = PkgCmdIDList.openLinkCommand;

        /// <summary>
        /// Opens the link in the browser.
        /// </summary>
        public async override Task Execute()
        {
            var isgithub = await IsGitHubRepo();
            if (!isgithub)
                return;

            var link = await GenerateLink(LinkType.Blob);
            if (link == null)
                return;
            var browser = ServiceProvider.TryGetMEFComponent<IVisualStudioBrowser>();
            browser?.OpenUrl(link.ToUri());

            await UsageTracker.IncrementCounter(x => x.NumberOfOpenInGitHub);
        }
    }
}
