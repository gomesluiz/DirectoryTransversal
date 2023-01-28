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
        /// Method vulnerable to Directory Traversal attack.
        /// 
        /// Examples:
        /// 
        /// 1) ..\..\appsettings.json
        /// 2) ..\..\..\..\..\..\..\Windows\win.ini
        /// 
        /// </summary>
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
    }
}