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

        /// <summary>
        /// Method protected from Directory Traversal attack.
        /// 
        /// Test examples:
        /// 
        /// 1) ..\..\appsettings.json
        /// 2) ..\..\..\..\..\..\..\Windows\win.ini
        /// 
        /// </summary>
        public void OnPost()
        {

            if (!string.IsNullOrEmpty(FileName))
            {
                // Defines BASE_DIRECTORY and canonicalize the path.
                var BaseDirectory = Path.Join(_hostEnvironment.ContentRootPath, "wwwroot", "relatorios");

                // Converts to canonicalized path and combine with base directory.
                var CanonicalizedPath = FileName.Replace("..", "");
                var FullFilePath  = Path.Join($"{BaseDirectory}/{CanonicalizedPath}");
                
                // Sets default values.
                ViewData["FileContent"] = "Arquivo não encontrado!";
                ViewData["FileName"] = FileName;

                // Checks if the file exists.
                if (System.IO.File.Exists(FullFilePath))
                {
                    FileContent = System.IO.File.ReadAllText(FullFilePath);
                    ViewData["FileContent"] = FileContent;

                }
            }
        }
    }
}