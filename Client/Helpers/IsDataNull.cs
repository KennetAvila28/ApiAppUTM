﻿using System.Text.Json;

namespace AppUTM.Helpers
{
    public class IsDataNull
    {
        public static T Check<T>(string data)
        {
            return data == null ? default : JsonSerializer.Deserialize<T>(data); ;
        }

        public static T CreateInstanceIfIsNull<T>(T data) where T : new()
        {
            return data == null ? new T() : data;
        }
    }
}