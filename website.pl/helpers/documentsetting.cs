namespace website.pl.helpers
{
    public static class documentsetting
    {
        public static string uploadpath(IFormFile file, string filname)
        {
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","files", filname);

            var filepath = $"{Guid.NewGuid()}{file.FileName}";

            var filefullpath = Path.Combine(folderpath, filepath);

            var filestream = new FileStream(filefullpath, FileMode.Create);
            file.CopyTo(filestream);
            return filepath;


        }
        public static void delete(string foldername, string filname)
        {
            var folderpath = Path.Combine(Directory.GetCurrentDirectory(), "/wwwroot/files", filname,foldername);
            if (Directory.Exists(folderpath))
            {
                Directory.Delete(folderpath);
            }

        }

    }
}
