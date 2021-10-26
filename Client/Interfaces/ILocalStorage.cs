using System.Threading.Tasks;
using AppUTM.Enum;

namespace AppUTM.Interfaces
{
    public interface ILocalStorage
    {
        /// <summary>
        /// Return a specific value from Enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetValue<T>(ValueKeys key);

        /// <summary>
        /// Save in memory a value from specific kind
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task SetValue<T>(ValueKeys key, T value);

        /// <summary>
        /// Remove item in memory from Enum
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task RemoveItem(ValueKeys key);

        /// <summary>
        /// Clean all in memory
        /// </summary>
        /// <returns></returns>
        Task ClearAll();
    }
}