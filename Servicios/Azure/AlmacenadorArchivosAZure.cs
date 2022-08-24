﻿using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Cine.Servicios.Azure
{
    public class AlmacenadorArchivosAZure : IAlmacenadorArchivos
    {
        private readonly string connectionString;
        public AlmacenadorArchivosAZure(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("AzureStorage");
        }

        public async Task BorrarArchivo(string ruta, string contenedor)
        {
            if (string.IsNullOrEmpty(ruta))
            {
                return;
            }
            var cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
            var archivo = Path.GetFileName(ruta);
            var blob    = cliente.GetBlobClient(archivo);
            await blob.DeleteIfExistsAsync();

        }

        public async Task<string> EditarArchivo(byte[] contenido, string extension, string contenedor, string ruta, string contentType)
        {
            await BorrarArchivo(ruta, contenedor);
            return await GuardarArchivo(contenido,extension,contenedor,contentType);
        }

        public async Task<string> GuardarArchivo(byte[] contenido, string extension, string contenedor, string contentType)
        {
            var   cliente = new BlobContainerClient(connectionString, contenedor);
            await cliente.CreateIfNotExistsAsync();
                  cliente.SetAccessPolicy(PublicAccessType.Blob);

            var archivoname       = $"{Guid.NewGuid()}{extension}";
            var blob              = cliente.GetBlobClient(archivoname);
            var blobUploadOptions = new BlobUploadOptions();
            var BlobHttpHeader    = new BlobHttpHeaders();
                BlobHttpHeader.ContentType = contentType;
             blobUploadOptions.HttpHeaders = BlobHttpHeader;

            await  blob.UploadAsync(new BinaryData(contenido), blobUploadOptions);
            return blob.Uri.ToString();
        }
    }
}
