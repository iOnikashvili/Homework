using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Failures
{
    public class Finder
    {
        /// <summary>
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <param name="failureTypes">
        /// 0 for unexpected shutdown, 
        /// 1 for short non-responding, 
        /// 2 for hardware failures, 
        /// 3 for connection problems
        /// </param>
        /// <param name="deviceId"></param>
        /// <param name="times"></param>
        /// <param name="devices"></param>
        /// <returns></returns>
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes,
            int[] deviceId,
            object[][] times,
            List<Dictionary<string, object>> devices)
        {
            
            var myFailureTypes = new List<Failure>(); //Создаём лист Ошибок
            for(var i = 0; i < failureTypes.Length; i++)
                myFailureTypes.Add(new Failure(failureTypes[i], times[i])); //Не смог сделать через Linq тк. одновременно нужно работать сразу с 2-мя коллекциями

            var myDevices = devices.Select(device => new Device((int)device["DeviceId"],
                (string)device["Name"])).ToList(); //Преобразуем Девайсы из листа словарей в лист конкретно Девайсов

            for (var i = 0; i < myDevices.Count; i++) // Ставим в соответствие каждому Девайсу его Ошибку
                myDevices[i].Failure = myFailureTypes[i];
            return FindDevicesFailedBeforeDate(myDevices, new int[] { day, month, year });
        }

        public static List<string> FindDevicesFailedBeforeDate(List<Device> devices, int[] checkDate) //Метод который сравнивает даты, собирает подходящие имена результатов в лист и возвращает результат
        {
            var result = devices.Where(device => device.Failure.FailureType == FailureTypes.IsSerious
            && (device.Failure.Date[0] < checkDate[0]
            || device.Failure.Date[1] < checkDate[1] 
            || device.Failure.Date[1] < checkDate[1])).Select(d => d.Name).ToList();
            return result;
        }
    }

    public class Device //Создаем объект Девайса
    {
        public Device(int id, string name)
        {
            Name = name;
            Id = id;
        }
        public int Id;
        public string Name;
        public Failure Failure; //Хранить тип Ошибки удобнее в самом Девайсе
    }

    public class Failure //Создаём обьект ошибки
    {
        public Failure(int failureType, object[] date)//Конструктор
        {
            FailureType = (FailureTypes)IsSerious(failureType);
            Date = GetDate(date);
        }
        public FailureTypes FailureType;
        public int[] Date;

        public static int IsSerious(int failureTypes) //Метод возвращает тип ошибки в зависимости от её номера
        {
            return failureTypes % 2 == 0 ? 1 : 0;
        }

        public static int[] GetDate(object[] date) //Метод преобразование даты из object[] в int[]
        {
            return date.Select(d => (int)d).ToArray(); 
        }
    }

    public enum FailureTypes //Перечисление
    {
        UnSerious,
        IsSerious
    }
}