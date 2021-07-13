using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;

namespace DirectoryExtensions
{
    public static class DirectoryExtension
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] uint dwFlags, [Out] StringBuilder lpExeName, [In, Out] ref uint lpdwSize);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool MoveFileEx(string existingFileName, string newFileName, int flags);

        /// <summary>
        /// Forcefully deletes an entire directory, including all subdirectories and files.
        /// </summary>
        /// <param name="directory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        public static void ForceDeleteDirectory(string directory)
        {
            if (string.IsNullOrEmpty(directory)) throw new ArgumentNullException(nameof(directory));
            if (!Directory.Exists(directory)) throw new DirectoryNotFoundException(nameof(directory));

            var files = Directory.GetFiles(directory);
            var folders = Directory.GetDirectories(directory);
            for (int i = 0; i < files.Length; i++) File.Delete(files[i]);
            for (int i = 0; i < folders.Length; i++) ForceDeleteDirectory(folders[i]);
            Directory.Delete(directory, true);
        }

        /// <summary>
        /// Returns entire path to a running process
        /// </summary>
        /// <param name="directory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GetMainModuleFileName(Process process, int buffer = 1024)
        {
            if (process == null) throw new ArgumentNullException("process");
            if (buffer < 1) throw new ArgumentOutOfRangeException("buffer");
            var fileNameBuilder = new StringBuilder(buffer);
            uint bufferLength = (uint)fileNameBuilder.Capacity + 1;
            return QueryFullProcessImageName(process.Handle, 0, fileNameBuilder, ref bufferLength) ? fileNameBuilder.ToString() : string.Empty;
        }

        /// <summary>
        /// Adds specified security rights to folder or file
        /// </summary>
        /// <param name="directory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="SystemException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="PlatformNotSupportedException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        public static void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            if (string.IsNullOrEmpty(FileName)) throw new ArgumentNullException(nameof(FileName));
            if (string.IsNullOrEmpty(Account)) throw new ArgumentNullException(nameof(Account));
            if (!File.Exists(FileName) && !Directory.Exists(FileName))
            {
                if (!string.IsNullOrEmpty(Path.GetExtension(FileName))) throw new FileNotFoundException(FileName);
                else throw new DirectoryNotFoundException(FileName);
            }
            var dirInfo = new DirectoryInfo(FileName);

            var dSecurity = dirInfo.GetAccessControl();

            dSecurity.AddAccessRule(new FileSystemAccessRule(Account,
            Rights,
            ControlType));

            dirInfo.SetAccessControl(dSecurity);

        }

        /// <summary>
        /// Removes specified security rights to folder or file
        /// </summary>
        /// <param name="directory"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="SystemException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="PlatformNotSupportedException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        public static void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            if (string.IsNullOrEmpty(FileName)) throw new ArgumentNullException(nameof(FileName));
            if (string.IsNullOrEmpty(Account)) throw new ArgumentNullException(nameof(Account));
            if (!File.Exists(FileName) && !Directory.Exists(FileName))
            {
                if (!string.IsNullOrEmpty(Path.GetExtension(FileName))) throw new FileNotFoundException(FileName);
                else throw new DirectoryNotFoundException(FileName);
            }
            var dirInfo = new DirectoryInfo(FileName);

            var dSecurity = dirInfo.GetAccessControl();

            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account,
            Rights,
            ControlType));

            dirInfo.SetAccessControl(dSecurity);
        }

        public static long GetDirectorySize(string directory, bool recursive = false)
        {
            if (string.IsNullOrEmpty(directory)) throw new ArgumentNullException(nameof(directory));
            if (!Directory.Exists(directory)) throw new DirectoryNotFoundException(nameof(directory));

            var size = 0L;

            var files = Directory.GetFiles(directory);
            for (int i = 0; i < files.Length; i++) size += new FileInfo(files[i])?.Length ?? 0;

            if (recursive)
            {
                var folders = Directory.GetDirectories(directory);

                for (int i = 0; i < folders.Length; i++) GetDirectorySize(folders[i], true);
            }

            return size;
        }

        //Think this is partially from Microsoft
        public static void CopyDirectory(string directory, string destination, bool recursive = false)
        {
            if (string.IsNullOrEmpty(directory)) throw new ArgumentNullException(nameof(directory));
            if (string.IsNullOrEmpty(destination)) throw new ArgumentNullException(nameof(destination));

            // Get the subdirectories for the specified directory.
            var dir = new DirectoryInfo(directory);

            if (!dir.Exists) throw new DirectoryNotFoundException(nameof(directory));


            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destination))
            {
                new DirectoryInfo(destination).Create();
            }

            // Get the files in the directory and copy them to the new location.
            var files = dir.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                var file = files[i];
                file.CopyTo(Path.Combine(destination, file.Name), false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (recursive)
            {
                var dirs = dir.GetDirectories();
                for (int i = 0; i < dirs.Length; i++)
                {
                    var subdir = dirs[i];
                    string temppath = Path.Combine(destination, subdir.Name);
                    CopyDirectory(subdir.FullName, temppath, true);
                }
            }
        }
    }
}
