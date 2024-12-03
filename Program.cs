using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string connectionString = "DefaultEndpointsProtocol=https;AccountName=ngdevst;AccountKey=xxxxxxxx;EndpointSuffix=core.windows.net";

var blobServiceClient = new BlobServiceClient(connectionString);

//Create a unique name for the container
string containerName = "quickstartblobs" + Guid.NewGuid().ToString(); // you can use any name, just that no 2 container can have the same name.

// Create the container and return a container client object
// Create a new container with pattern: quickstartblobse38a8873-e95f-4466-b66f-c9f720cca424
BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);

string localPath = "data";
// Create a new folder in the current folder
Directory.CreateDirectory(localPath);

// Define a new file called quickstartde2f29c6-9eb5-45d5-8752-5a7fa01bbd6e
string fileName = "quickstart" + Guid.NewGuid().ToString() + ".txt";
string localFilePath = Path.Combine(localPath, fileName);

// Write Hello World inside the file
await File.WriteAllTextAsync(localFilePath, "Hello, World");

var blobClient = containerClient.GetBlobClient(fileName);

// upload the file to the file as a blob
await blobClient.UploadAsync(localFilePath, true);

Console.WriteLine("Listing blobs...");

// List all blobs in the container
await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
{
    Console.WriteLine("\t" + blobItem.Name);
}
