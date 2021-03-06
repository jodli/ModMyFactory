﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace ModMyFactory.Models
{
    /// <summary>
    /// A mod that has been extracted to a directory.
    /// </summary>
    sealed class ExtractedMod : Mod
    {
        /// <summary>
        /// The mods directory.
        /// </summary>
        public DirectoryInfo Directory { get; }

        protected override string FallbackName => Directory.Name.Split('_')[0];

        private FileInfo GetInfoFile(DirectoryInfo directory)
        {
            return directory.EnumerateFiles("info.json").First();
        }

        /// <summary>
        /// Creates a mod.
        /// </summary>
        /// <param name="directory">The mods directory.</param>
        /// <param name="factorioVersion">The version of Factorio this mod is compatible with.</param>
        /// <param name="parentCollection">The collection containing this mod.</param>
        /// <param name="modpackCollection">The collection containing all modpacks.</param>
        /// <param name="messageOwner">The window that ownes the deletion message box.</param>
        public ExtractedMod(DirectoryInfo directory, Version factorioVersion, ICollection<Mod> parentCollection, ICollection<Modpack> modpackCollection, Window messageOwner)
            : base(factorioVersion, parentCollection, modpackCollection, messageOwner)
        {
            Directory = directory;

            FileInfo infoFile = GetInfoFile(Directory);
            using (Stream stream = infoFile.OpenRead())
            {
                base.ReadInfoFile(stream);
            }
        }

        protected override void DeleteFilesystemObjects()
        {
            Directory.Delete(true);
        }
    }
}
