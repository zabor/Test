using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyTest
{

    [Serializable]
    struct OneRecord
    {
        string onestring;
        float onedigitf;
        public OneRecord(string onestring, float onedigitf)
        {
            this.onestring = onestring;
            this.onedigitf = onedigitf;
        }

        public string OneString
        {
            get { return onestring;  }
            set { onestring = value; }
        }
        public float OneDigitf
        {
            get { return onedigitf; }
            set { onedigitf = value; }
        }
    }

    [Serializable]
    class SaveInFile
    {
        List<OneRecord> AllListRecord = new List<OneRecord>();

        public void AddList(OneRecord rec)
        {
            AllListRecord.Add(rec);
        }
        public void AddList(string onestring, float onedigit)
        {
            AllListRecord.Add(new OneRecord(onestring,onedigit));
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (OneRecord oneRecord in AllListRecord)
            {
                stringBuilder.Append($"{oneRecord.OneString } {oneRecord.OneDigitf } \n");
            }
            return stringBuilder.ToString();
        }
        public SaveInFile Load(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(path, FileMode.Open))
            {
                return (SaveInFile)formatter.Deserialize(fileStream);
            }

        }
        public void Save(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(fileStream, this);
                fileStream.Close();
            }
         }
    }
    
    
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"g:\test.txt";
            SaveInFile test = new SaveInFile();
            test.AddList("one", 1.1f);
            test.AddList(new OneRecord("two", 2.2f));
            test.AddList("ten", 10.23f);
            test.Save(path);
            Console.WriteLine(test.Load(path).ToString());

            
            
        }
    }
}
