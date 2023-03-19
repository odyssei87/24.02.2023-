var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
app.Run(async(context)=> 
{ 
    var response1=context.Response;
    var reqvest1 = context.Request;

    response1.ContentType= "text/html; charset=utf-8";
    if (reqvest1.Path=="/upload" && reqvest1.Method=="POST")
    {
        IFormFileCollection files = reqvest1.Form.Files;
        var uploadRath=$"{Directory.GetCurrentDirectory()}/uploads";
        Directory.CreateDirectory(uploadRath);
        foreach (var file in files)
        {
            string fullPath=$"{uploadRath}/{file.FileName}";
            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }
        await response1.WriteAsync("Файлы успешно загружены");
    }
    else
    {
        await response1.SendFileAsync("html/index.html");
    }
});

app.Run();
