﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using NuGet.Services.Work.Jobs.Models;

namespace NuGet.Services.Work
{
    public static class StorageHelpers
    {
        public static readonly string PackageHashMetadataKey = "Hash";

        public static readonly string PackageBackupsDirectory = "packages";

        private static readonly string PackageBlobNameFormat = "{0}.{1}.nupkg";
        private static readonly string PackageBackupBlobNameFormat = PackageBackupsDirectory + "/{0}/{1}/{2}.nupkg";

        public static string GetPackageBlobName(PackageRef package)
        {
            return GetPackageBlobName(package.Id, package.Version);
        }

        public static string GetPackageBlobName(string id, string version)
        {
            return String.Format(
                CultureInfo.InvariantCulture, 
                PackageBlobNameFormat, 
                id, 
                version).ToLowerInvariant();
        }

        public static string GetPackageBackupBlobName(PackageRef package)
        {
            return GetPackageBackupBlobName(package.Id, package.Version, package.Hash);
        }

        public static string GetPackageBackupBlobName(string id, string version, string hash)
        {
            return String.Format(
                CultureInfo.InvariantCulture, 
                PackageBackupBlobNameFormat,
                id.ToLowerInvariant(),
                version.ToLowerInvariant(),
                WebUtility.UrlEncode(hash));
        }

        public static CloudBlobDirectory GetBlobDirectory(CloudStorageAccount account, string path)
        {
            var client = account.CreateCloudBlobClient();
            client.DefaultRequestOptions = new BlobRequestOptions()
            {
                ServerTimeout = TimeSpan.FromMinutes(5)
            };

            string[] segments = path.Split('/');
            string containerName;
            string prefix;

            if (segments.Length < 2)
            {
                // No "/" segments, so the path is a container and the catalog is at the root...
                containerName = path;
                prefix = String.Empty;
            }
            else
            {
                // Found "/" segments, but we need to get the first segment to use as the container...
                containerName = segments[0];
                prefix = String.Join("/", segments.Skip(1)) + "/";
            }

            var container = client.GetContainerReference(containerName);
            var dir = container.GetDirectoryReference(prefix);
            return dir;
        }
    }
}
