﻿#region License

// Copyright (c) 2011, ClearCanvas Inc.
// All rights reserved.
// http://www.clearcanvas.ca
//
// This software is licensed under the Open Software License v3.0.
// For the complete license, see http://www.clearcanvas.ca/OSLv3.0

#endregion

using System;
using System.IO;

namespace ClearCanvas.Common.Utilities
{
    /// <summary>
    /// Static Class containing utilities for manipulating directories.
    /// </summary>
    public static class DirectoryUtility
    {
        /// <summary>
        /// Callback delegate used by DirectoryUtility.Copy() to report the progress.
        /// </summary>
        /// <param name="path">The file that is being copied</param>
        public delegate void CopyProcessCallback(string path);

        /// <summary>
        /// Returns the number of files in the specified directory that satisfies a given condition
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchParttern"></param>
        /// <param name="recursive"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static long Count(string path, string searchParttern, bool recursive, Predicate<string> condition)
        {
            long counter = 0;
            FileProcessor.Process(path, searchParttern,
                                  delegate(string file) { if (condition == null || condition(file)) counter++; },
                                  recursive);
            return counter;
        }

        /// <summary>
        /// Moves a study from one location to another.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void Move(string source, string destination)
        {
            Copy(source, destination);
            DeleteIfExists(source);
        }

        /// <summary>
        /// Recursively Copy a directory without progress callback.
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="targetDirectory"></param>
        /// <returns></returns>
        public static ulong Copy(string sourceDirectory, string targetDirectory)
        {
            return Copy(sourceDirectory, targetDirectory, null);
        }

        /// <summary>
        /// Recursively copy a directory
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="targetDirectory"></param>
        /// <param name="progressCallback"> </param>
        public static ulong Copy(string sourceDirectory, string targetDirectory, CopyProcessCallback progressCallback)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            return InternalCopy(diSource, diTarget, progressCallback);
        }

        private static ulong InternalCopy(DirectoryInfo source, DirectoryInfo target, CopyProcessCallback progressCallback)
        {
            ulong bytesCopied = 0;
            // Check if the target directory exists, if not, create it.
            if (Directory.Exists(target.FullName) == false)
            {
                Directory.CreateDirectory(target.FullName);
            }

            // Copy each file into it's new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                //Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
                bytesCopied += (ulong)fi.Length;
                fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
                if (progressCallback != null)
                    progressCallback(fi.FullName);

            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                bytesCopied += InternalCopy(diSourceSubDir, nextTargetSubDir, progressCallback);
            }

            return bytesCopied;
        }

        /// <summary>
        /// Delete a directory if it exists.  Do not delete the parent directory if its empty.
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteIfExists(string dir)
        {
            if (Directory.Exists(dir))
                DeleteIfExists(dir, false);
        }

        /// <summary>
        /// Delete a directory if its empty.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool DeleteIfEmpty(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    if (files.Length == 0)
                    {
                        string[] subDirs = Directory.GetDirectories(path);
                        if (subDirs.Length > 0)
                        {
                            foreach (string subDir in subDirs)
                            {
                                if (!DeleteIfEmpty(subDir))
                                {
                                    return false;
                                }
                            }
                            // all sub-directories are empty and deleted
                        }

                        Directory.Delete(path);
                        return true;
                    }
                    // not empty
                    return false;
                }
            }
            catch (Exception e)
            {
                Platform.Log(LogLevel.Error, e, "Unexpected exception when attempting to delete directory: {0}", path);
                return false;
            }

            return true;// not exist = empty 
        }

        /// <summary>
        /// Deletes a folder and its ascendants if they are empty. Stops when reaching the specified parent folder.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="stopAtParentPath"></param>
        /// <returns></returns>
        public static bool DeleteIfEmpty(string path, string stopAtParentPath)
        {
            try
            {
                DirectoryInfo parent = Directory.GetParent(path);
                if (DeleteIfEmpty(path))
                {
                    if (parent != null && !parent.FullName.Equals(stopAtParentPath))
                    {
                        DeleteIfEmpty(parent.FullName);
                    }

                    return true;
                }
            }
            catch (Exception e)
            {
                Platform.Log(LogLevel.Error, e, "Unexpected exception when attempting to delete directory: {0}", path);
                return false;
            }

            return false;
        }

        /// <summary>
        /// Delete any empty subdirectories
        /// </summary>
        /// <param name="path"></param>
        /// <param name="recursive"></param>
        public static void DeleteEmptySubDirectories(string path, bool recursive)
        {
            if (Directory.Exists(path))
            {
                var dirInfo = new DirectoryInfo(path);
                DirectoryInfo[] subDirs = dirInfo.GetDirectories();
                foreach (DirectoryInfo subDir in subDirs)
                {
                    DeleteEmptySubDirectories(subDir.FullName, recursive);
                    DeleteIfEmpty(subDir.FullName);
                }
            }
        }

        /// <summary>
        /// Delete a directory if it exists.
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="deleteParentIfEmpty"></param>
        public static void DeleteIfExists(string dir, bool deleteParentIfEmpty)
        {
            if (Directory.Exists(dir))
            {
                DirectoryInfo parent = Directory.GetParent(dir);
                Directory.Delete(dir, true);

                if (deleteParentIfEmpty && parent != null)
                {
                    // delete the parent too
                    DeleteIfEmpty(parent.FullName);
                }
            }
        }

        /// <summary>
        /// Is Directory empty?
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsEmpty(string path)
        {
            string[] files = Directory.GetFiles(path);
            if (files.Length == 0)
            {
                string[] subDirs = Directory.GetDirectories(path);
                return subDirs.Length == 0;
            }

            return false;
        }
    }
}