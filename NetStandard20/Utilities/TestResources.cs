using System;
using System.Collections.Generic;
using System.IO;

#nullable enable

namespace NetStandard20.Utilities
{
    /// <summary>
    /// Creates a folder with the selected name and files for use in tests. <br/>
    /// Use <see cref="Dispose"/> to clear the folder. <br/>
    /// <![CDATA[Version: 1.0.0.0]]> <br/>
    /// <![CDATA[Dependency: ResourcesUtilities.ReadFileAsBytes(string name, Assembly? assembly = null)]]> <br/>
    /// </summary>
    public class TestResources : List<string>, IDisposable
    {
        #region Properties

        /// <summary>
        /// Initial folder name.
        /// </summary>
        public string FolderName { get; }

        /// <summary>
        /// Initial file names.
        /// </summary>
        public IList<string> FileNames { get; }

        /// <summary>
        /// Full path to test folder.
        /// </summary>
        public string Folder { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Base constructor
        /// </summary>
        /// <param name="folderName"></param>
        /// <param name="fileNames"></param>
        public TestResources(string folderName, params string[] fileNames)
        {
            FolderName = folderName ?? throw new ArgumentNullException(nameof(folderName));
            FileNames = fileNames ?? throw new ArgumentNullException(nameof(fileNames));

            Folder = Path.Combine(Path.GetTempPath(), folderName);
            Directory.CreateDirectory(Folder);

            foreach (var name in fileNames)
            {
                var path = Path.Combine(Folder, name);
                var bytes = ResourcesUtilities.ReadFileAsBytes(name);

                File.WriteAllBytes(path, bytes);

                Add(path);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Recursive deletes the folder
        /// </summary>
        public void Dispose()
        {
            try
            {
                Directory.Delete(FolderName, true);
            }
            catch (IOException)
            {
                // ignore
            }
        }

        #endregion
    }
}