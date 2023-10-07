

using Newtonsoft.Json;

namespace Application.FileStorages
{
    public class FileStorage
    {
        private readonly string _dataDirectory;

        public FileStorage()
        {
            // Verilerin saklanacağı dizini belirtin
            _dataDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppData");
            Directory.CreateDirectory(_dataDirectory); // Dizin yoksa oluştur
        }

        public void SaveData<T>(string fileName, T data)
        {
            string filePath = Path.Combine(_dataDirectory, fileName);

            // Veriyi JSON formatında dosyaya yaz
            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(filePath, jsonData);
        }

        public T LoadData<T>(string fileName)
        {
            string filePath = Path.Combine(_dataDirectory, fileName);

            if (File.Exists(filePath))
            {
                // Dosyadan JSON verisini oku ve nesneye dönüştür
                string jsonData = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<T>(jsonData);
            }

            return default(T); // Dosya yoksa veya veri okunamazsa varsayılan değeri döndür
        }
    }
}
