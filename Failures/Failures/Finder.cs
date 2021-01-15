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
            
            var myFailureTypes = new List<Failure>();
            for(var i = 0; i < failureTypes.Length; i++)
                myFailureTypes.Add(new Failure(failureTypes[i], times[i]));

            var myDevices = devices.Select(device => new Device((int)device["DeviceId"],
                (string)device["Name"])).ToList();

            for (var i = 0; i < myDevices.Count; i++)
                myDevices[i].Failure = myFailureTypes[i];
            return FindDevicesFailedBeforeDate(myDevices, new int[] { day, month, year });
        }

        public static List<string> FindDevicesFailedBeforeDate(List<Device> devices, int[] checkDate)
        {
            var result = devices.Where(device => device.Failure.FailureType == FailureTypes.IsSerious
            && (device.Failure.Date[0] < checkDate[0]
            || device.Failure.Date[1] < checkDate[1] 
            || device.Failure.Date[1] < checkDate[1])).Select(d => d.Name).ToList();
            return result;
        }
    }

    public class Device
    {
        public Device(int id, string name)
        {
            Name = name;
            Id = id;
        }
        public int Id;
        public string Name;
        public Failure Failure;
    }

    public class Failure
    {
        public Failure(int failureType, object[] date)
        {
            FailureType = (FailureTypes)IsSerious(failureType);
            Date = GetDate(date);
        }
        public FailureTypes FailureType;
        public int[] Date;

        public static int IsSerious(int failureTypes)
        {
            return failureTypes % 2 == 0 ? 1 : 0;
        }

        public static int[] GetDate(object[] date)
        {
            return date.Select(d => (int)d).ToArray(); 
        }
    }

    public enum FailureTypes
    {
        UnSerious,
        IsSerious
    }
}