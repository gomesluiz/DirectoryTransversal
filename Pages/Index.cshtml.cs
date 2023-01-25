using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DirectoryTransversal.Pages
{
    public class IndexModel : PageModel
    {
        private IHostEnvironment _hostEnvironment;

        [BindProperty]
        public string FileName { get; set; }
        public string FileContent { get; set; }

        public IndexModel(IHostEnvironment hostEnvironment)
        {
            FileName = FileContent = string.Empty;
            _hostEnvironment = hostEnvironment;
        }

        /*
        public void OnPost()
        {
            if (!string.IsNullOrEmpty(FileName))
            {
                var fullFilePath = _hostEnvironment.ContentRootPath + "/wwwroot/relatorios/" + FileName;
                var FileContent = System.IO.File.ReadAllText(fullFilePath);

                ViewData["FileName"] = FileName;
                ViewData["FileContent"] = FileContent;
            }
        }
        */
        public void OnPost()
        {
            
            if (!string.IsNullOrEmpty(FileName))
            {
                ViewData["FileName"] = FileName;
                FileName = FileName.Replace("..", "");
                   
                var fullFilePath = _hostEnvironment.ContentRootPath + "/wwwroot/relatorios/" + FileName;
                if (System.IO.File.Exists(fullFilePath))
                {
                    FileContent = System.IO.File.ReadAllText(fullFilePath);
                    ViewData["FileContent"] = FileContent;

                }
                else
                {
                    ViewData["FileContent"] = "Arquivo não encontrado!";

                }
            }
        }

        private bool ValidateFileName(string fileName)
        {
            fileName = fileName.Replace("..", "");
            
            if (!fileName.StartsWith("Relatorio"))
                return false;

            

            return true;
        }
    }
}