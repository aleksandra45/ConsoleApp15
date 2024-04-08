using System;
using System.IO;

namespace Task1;

class Program
{
    public static void Main()
    {
        Console.WriteLine("Укажите путь до каталога:");
        var path = Console.ReadLine();
        GetCatalogs(path!);
    }
    static void GetCatalogs(string path)
    {
        if (Directory.Exists(path))
        {
            try
            {
                var dirTime = Directory.GetLastWriteTime(path);
                Console.WriteLine($"Дата, время создание папки {dirTime}");
                var timeSpan = TimeSpan.FromMinutes(30);
                var dateTime = DateTime.Now - timeSpan;
                if (dateTime <= dirTime) return;
                var dirs = Directory.GetDirectories(path);
                foreach (var dir in dirs)
                {
                    try
                    {
                        Directory.Delete(dir, true);
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine($"Для директории {dir} установлен статус {e.Message}");
                        throw;
                    }
                    finally
                    {
                        Console.WriteLine($"Удаление поддиректорий по пути {path} произведена");
                    }
                }

                var files = Directory.GetFiles(path);
                foreach (var file in files)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Для файла {file} установлен статус {e.Message}");
                        throw;
                    }
                    finally
                    {
                        Console.WriteLine($"Удаление файлов по пути {path} произведена");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        else
        {
            Console.WriteLine("Директория не существует");
        }

        Console.ReadKey();
    }
}