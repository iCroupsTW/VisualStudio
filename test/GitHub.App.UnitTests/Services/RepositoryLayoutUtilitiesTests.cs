﻿using System;
using GitHub.Services;
using NUnit.Framework;

public static class RepositoryLayoutUtilitiesTests
{
    public class TheGetDefaultPathAndLayoutMethod
    {
        [TestCase(@"c:\source\owner\repositoryName", "https://github.com/owner/repositoryName", @"c:\source", RepositoryLayout.OwnerName)]
        [TestCase(@"c:\source\owner\differentName", "https://github.com/owner/repositoryName", @"c:\source", RepositoryLayout.OwnerName)]
        [TestCase(@"c:\source\repositoryName", "https://github.com/owner/repositoryName", @"c:\source", RepositoryLayout.Name)]
        [TestCase(@"c:\source\differentName", "https://github.com/owner/repositoryName", @"c:\source", RepositoryLayout.Name)]
        public void GetDefaultPathAndLayout(string repositoryPath, string cloneUrl, string expectPath, RepositoryLayout expectLayout)
        {
            var (path, layout) = RepositoryLayoutUtilities.GetDefaultPathAndLayout(repositoryPath, cloneUrl);

            Assert.That((path, layout), Is.EqualTo((expectPath, expectLayout)));
        }
    }

    public class TheGetRepositoryLayoutMethod
    {
        [TestCase("Name", RepositoryLayout.Name)]
        [TestCase("OwnerName", RepositoryLayout.OwnerName)]
        [TestCase("Default", RepositoryLayout.Default)]
        [TestCase("__UNKNOWN__", RepositoryLayout.Default)]
        public void GetDefaultPathAndLayout(string setting, RepositoryLayout expectedLayout)
        {
            var layout = RepositoryLayoutUtilities.GetRepositoryLayout(setting);

            Assert.That(layout, Is.EqualTo(expectedLayout));
        }
    }
}