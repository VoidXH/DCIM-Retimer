using System.Globalization;

static void Process(string file) {
    string fileName = Path.GetFileNameWithoutExtension(file);
    if (fileName.Length >= 19) { // At least IMG_YYYYMMDD_HHMMSS long
        try {
            string source = fileName.Substring(4, 15);
            DateTime parsed = DateTime.ParseExact(source, "yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
            File.SetCreationTime(file, parsed);
            File.SetLastWriteTime(file, parsed);
            File.SetLastAccessTime(file, parsed);
            Console.WriteLine($"Successfully updated timestamps for: {fileName}");
        } catch (Exception e) {
            Console.WriteLine($"Failed to parse date/time for: {fileName} ({e.Message})");
        }
    } else {
        Console.WriteLine($"File name does not match the expected format: {fileName}");
    }
}

if (args.Length == 0) {
    Console.WriteLine("Please provide a file or folder path as an argument or drag and drop.");
    return;
}

if (File.Exists(args[0])) {
    Process(args[0]);
} else if (Directory.Exists(args[0])) {
    string[] files = Directory.GetFiles(args[0]);
    for (int i = 0; i < files.Length; i++) {
        Process(files[i]);
    }
} else {
    Console.WriteLine("The file or directory doesn't exist.");
}