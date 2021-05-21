using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace bkp_sinacom
{
    class Gdrive
    {
        public UserCredential Autenticar()
        {
            UserCredential credenciais;
            string token = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\client_id.json";

            using (var stream = new FileStream(token, FileMode.Open, FileAccess.Read))
            {
                var diretorioAtual = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var diretorioCredenciais = Path.Combine(diretorioAtual, "credential");

                credenciais = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { DriveService.Scope.DriveFile },
                    "user",
                    System.Threading.CancellationToken.None,
                    new Google.Apis.Util.Store.FileDataStore(diretorioCredenciais, true)).Result;
            }

            return credenciais;
        }

        public void ListarArquivos(DriveService servico, string file = null)
        {
            var request = servico.Files.List();
            if (file != null)
            {
                request.Q += string.Format("name = '{0}'", file);
            }
            request.Fields = "files(id, name, modifiedTime)";
            var resultado = request.Execute();
            var arquivos = resultado.Files;
            if (arquivos != null && arquivos.Any())
            {
                foreach (var arquivo in arquivos)
                {
                    Console.WriteLine(":: Arquivo: "+arquivo.Name);
                    Console.WriteLine(":::: ID do Arquivo: " + arquivo.Id);
                    Console.WriteLine(":::: Data de Modificação: " + arquivo.ModifiedTime);
                }
            }
            else
            {
                Console.WriteLine("Arquivo "+ file + " não encontrado ");
            }
        }

        public void Upload(DriveService servico, string caminhoArquivo, string folder = null)
        {
            var arquivo = new Google.Apis.Drive.v3.Data.File();
            arquivo.Name = Path.GetFileName(caminhoArquivo);
            arquivo.MimeType = MimeTypes.MimeTypeMap.GetMimeType(Path.GetExtension(caminhoArquivo));

            using (var stream = new FileStream(caminhoArquivo, FileMode.Open, FileAccess.Read))
            {
                Google.Apis.Upload.ResumableUpload<Google.Apis.Drive.v3.Data.File, Google.Apis.Drive.v3.Data.File> request;
                var ids = ProcurarArquivoId(servico, arquivo.Name);
                if (ids == null || !ids.Any())
                {
                    if (folder != null)
                    {
                        var found = ProcurarArquivoId(servico, folder);
                        if (found != null && found.Any())
                        {
                            arquivo.Parents = found;
                        }
                        else
                        {
                            arquivo.Parents = CriarDiretorio(servico, folder);
                        }

                    }
                    request = servico.Files.Create(arquivo, stream, arquivo.MimeType);                    
                }
                else
                {
                    request = servico.Files.Update(arquivo, ids.First(), stream, arquivo.MimeType);
                }
                request.Upload();
                var ret = request.ResponseBody;
                Console.WriteLine("Arquivo enviado. ID: " + ret.Id);
            }
        }

        public string[] CriarDiretorio(DriveService servico, string nome)
        {
            var arquivo = new Google.Apis.Drive.v3.Data.File();
            arquivo.Name = Path.GetFileName(nome);
            arquivo.MimeType = "application/vnd.google-apps.folder";
            var request = servico.Files.Create(arquivo);
            request.Fields = "id";
            var file = request.Execute();
            return ProcurarArquivoId(servico, nome);
        }

        public string[] ProcurarArquivoId(DriveService servico, string nome, bool procurarNaLixeira = false)
        {
            var retorno = new List<string>();

            var request = servico.Files.List();
            request.Q = string.Format("name = '{0}'", nome);
            if (!procurarNaLixeira)
            {
                request.Q += " and trashed = false";
            }
            request.Fields = "files(id)";
            var resultado = request.Execute();
            var arquivos = resultado.Files;

            if (arquivos != null && arquivos.Any())
            {
                foreach (var arquivo in arquivos)
                {
                    retorno.Add(arquivo.Id);
                }
            }

            return retorno.ToArray();
        }

        public void Download(DriveService servico, string nome, string destino)
        {
            var ids = ProcurarArquivoId(servico, nome);
            if (ids != null && ids.Any())
            {
                var request = servico.Files.Get(ids.First());
                using (var stream = new FileStream(destino, FileMode.Create, FileAccess.Write))
                {
                    request.Download(stream);
                }
            }
        }

        public DriveService AbrirServico(UserCredential credenciais)
        {
            return new DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credenciais
            });
        }
    }
}
